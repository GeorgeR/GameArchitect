using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers
{
    public class UnrealPropertyPrinter : ICXXPrinter<UnrealPropertyInfo>
    {
        public string Print(UnrealPropertyInfo info, CXXFileType fileType)
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

            //sb.AppendLine(base.Print(info, fileType));
            sb.RemoveLastLine();

            return sb.ToString();
        }

        public string Print(UnrealPropertyInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}