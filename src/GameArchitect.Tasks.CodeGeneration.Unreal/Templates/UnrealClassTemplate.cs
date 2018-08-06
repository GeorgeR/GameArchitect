using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealClassTemplate : UnrealTypeTemplate
    {
        public UnrealClassTemplate(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer, 
            ICXXPrinter<IPropertyInfo> propertyPrinter, 
            ICXXPrinter<IEventInfo> eventPrinter, 
            ICXXPrinter<IFunctionInfo> functionPrinter,
            ITypeInfo info) 
            : base(log, nameTransformer, typeTransformer, propertyPrinter, eventPrinter, functionPrinter, info) { }

        public override string Print(CXXFileType fileType)
        {
            base.Print(fileType);

            var sb = new StringBuilder();

            if (fileType == CXXFileType.Declaration)
            {
                var isForBlueprint = true;

                if(isForBlueprint) sb.AppendLine($"UCLASS(BlueprintType)");
                sb.AppendLine($"class {PrintAPI()} {NameTransformer.TransformName(Info, Info.Name, NameContext.Type)}");

                var baseClassName = "UObject";
                sb.AppendLine($": public {baseClassName}", 1);
                sb.OpenBracket();

                if (isForBlueprint)
                {
                    sb.AppendLine($"GENERATED_BODY()", 1);
                    sb.AppendEmptyLine();
                }

                sb.AppendLine($"public:");

                Info.Properties.ForEach(o =>
                {
                    sb.Append(PropertyPrinter.Print(o, CXXFileType.Declaration));
                    sb.AppendEmptyLine();
                });
                sb.RemoveLastLine();
                
                sb.CloseBracketWithSemicolon();
            }
            
            return sb.ToString();
        }
    }
}