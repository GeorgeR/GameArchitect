using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers
{
    public class UnrealPropertyPrinter : CXXPropertyPrinter
    {
        public UnrealPropertyPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer) 
            : base(log, nameTransformer, typeTransformer) { }

        public override string Print(IPropertyInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            info.ForAttribute<UnrealPropertyAttribute>(attr =>
            {
                var bpAccess = info.Permission.HasFlag(Permission.Write)
                    ? UnrealPropertyFlags.BlueprintReadWrite
                    : UnrealPropertyFlags.BlueprintReadOnly;

                var editorAccess = UnrealPropertyFlags.EditAnywhere;

                sb.AppendLine($"UPROPERTY({bpAccess},{editorAccess})");
            });

            sb.AppendLine(base.Print(info, fileType));
            sb.RemoveLastLine();

            return sb.ToString();
        }
    }
}