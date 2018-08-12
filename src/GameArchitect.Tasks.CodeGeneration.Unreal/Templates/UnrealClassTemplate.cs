using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public sealed class UnrealClassTemplate : UnrealTypeTemplate
    {
        private UnrealNameTransformer NameTransformer { get; }
        private UnrealTypeTransformer TypeTransformer { get; }

        public UnrealClassTemplate(
            UnrealPropertyPrinter propertyPrinter, 
            UnrealEventPrinter eventPrinter, 
            UnrealFunctionPrinter functionPrinter, 
            UnrealTypeInfo info, 
            UnrealNameTransformer nameTransformer, 
            UnrealTypeTransformer typeTransformer) 
            : base(propertyPrinter, eventPrinter, functionPrinter, info)
        {
            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
        }

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
                    sb.Append(PropertyPrinter.Print((UnrealPropertyInfo) o, CXXFileType.Declaration));
                    sb.AppendEmptyLine();
                });
                sb.RemoveLastLine();
                
                sb.CloseBracketWithSemicolon();
            }
            
            return sb.ToString();
        }
    }
}