using System.Collections.Generic;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers
{
    public sealed class UnrealFunctionPrinter : ICXXPrinter<UnrealFunctionInfo>
    {
        private UnrealNameTransformer NameTransformer { get; }
        private UnrealTypeTransformer TypeTransformer { get; }

        private UnrealParameterPrinter ParameterPrinter { get; }

        public UnrealFunctionPrinter(UnrealNameTransformer nameTransformer, UnrealTypeTransformer typeTransformer, UnrealParameterPrinter parameterPrinter)
        {
            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
            ParameterPrinter = parameterPrinter;
        }

        private string PrintParameters(UnrealFunctionInfo info, CXXFileType fileType)
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

        public string Print(UnrealFunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType);

            info.ForAttribute<AsyncAttribute>(attr =>
            {
                parameterStr += $", TFunction<void({(info.Type.Native != typeof(void) ? returnTypeStr : string.Empty)})> Callback";
                returnTypeStr = "void";
            });
            
            info.ForAttribute<UnrealFunctionAttribute>(attr =>
            {
                sb.AppendLine($"UFUNCTION(BlueprintCallable, BlueprintType, Blueprintable)");
            });
            
            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }

        public string Print(UnrealFunctionInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}