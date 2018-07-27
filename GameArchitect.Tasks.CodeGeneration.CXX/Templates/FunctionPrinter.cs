using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class FunctionPrinter : IPrinter<FunctionInfo>
    {
        protected virtual INameTransformer NameTransformer { get; } = new CXXNameTransformer();
        protected virtual ITypeTransformer TypeTransformer { get; } = new CXXTypeTransformer();

        protected virtual IPrinter<IMemberInfo> ParameterPrinter { get; } = new ParameterPrinter();

        protected virtual string PrintParameters(FunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            info.GetParameters().ForEach(p =>
            {
                var deconstructed = new List<IMemberInfo>();
                if (DeconstructAttribute.TryDeconstruct(p, ref deconstructed))
                    deconstructed.ForEach(o => { sb.Append(ParameterPrinter.Print(o, fileType) + ", "); });
                else
                    sb.Append(ParameterPrinter.Print(p, fileType) + ", ");
            });

            sb.RemoveLastComma();

            return sb.ToString();
        }

        public virtual string Print(FunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType);

            info.ForAttribute<AsyncAttribute>(attr =>
            {
                parameterStr += $", std::function<void({(info.Type != typeof(void) ? returnTypeStr : string.Empty)})> Callback";
                returnTypeStr = "void";
            });

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }

        public string Print(FunctionInfo info)
        {
            throw new NotImplementedException($"Using the CXXFileType overload.");
        }
    }
}