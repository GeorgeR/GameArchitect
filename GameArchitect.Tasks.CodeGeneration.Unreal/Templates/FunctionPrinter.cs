using System.Text;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    internal class FunctionPrinter : CXX.Templates.FunctionPrinter
    {
        protected override INameTransformer NameTransformer { get; } = new UnrealNameTransformer();
        protected override ITypeTransformer TypeTransformer { get; } = new UnrealTypeTransformer();

        protected override CXX.Templates.IPrinter<IMemberInfo> ParameterPrinter { get; } = new ParameterPrinter();

        public override string Print(FunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType);

            info.ForAttribute<AsyncAttribute>(attr =>
            {
                parameterStr += $", TFunction<void({(info.Type != typeof(void) ? returnTypeStr : string.Empty)})> Callback";
                returnTypeStr = "void";
            });
            
            info.ForAttribute<UnrealFunctionAttribute>(attr =>
            {
                sb.AppendLine($"UFUNCTION(BlueprintCallable)");
            });
            
            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }
    }
}