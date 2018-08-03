using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealFunctionPrinter : CXXFunctionPrinter
    {
        public UnrealFunctionPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer, 
            ICXXPrinter<IMemberInfo> parameterPrinter) 
            : base(log, nameTransformer, typeTransformer, parameterPrinter) { }

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
                sb.AppendLine($"UFUNCTION(BlueprintCallable, BlueprintType, Blueprintable)");
            });
            
            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }
    }
}