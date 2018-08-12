using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public sealed class CXXFunctionPrinter : ICXXPrinter<CXXFunctionInfo>
    {
        private CXXNameTransformer NameTransformer { get; }
        private CXXTypeTransformer TypeTransformer { get; }

        private CXXParameterPrinter ParameterPrinter { get; }

        public CXXFunctionPrinter(CXXNameTransformer nameTransformer, CXXTypeTransformer typeTransformer, CXXParameterPrinter parameterPrinter)
        {
            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
            ParameterPrinter = parameterPrinter;
        }

        private string PrintParameters(CXXFunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            info.Parameters.ForEach(p =>
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

        public string Print(CXXFunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType);

            info.ForAttribute<AsyncAttribute>(attr =>
            {
                parameterStr += $", std::function<void({(info.Type.Native != typeof(void) ? returnTypeStr : string.Empty)})> Callback";
                returnTypeStr = "void";
            });

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }

        public string Print(CXXFunctionInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}