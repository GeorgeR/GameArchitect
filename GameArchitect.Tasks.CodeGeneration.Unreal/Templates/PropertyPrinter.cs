using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    internal class PropertyPrinter : CXX.Templates.PropertyPrinter
    {
        protected override INameTransformer NameTransformer { get; } = new UnrealNameTransformer();
        protected override ITypeTransformer TypeTransformer { get; } = new UnrealTypeTransformer();

        public override string Print(PropertyInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            info.ForAttribute<UnrealPropertyAttribute>(attr =>
            {
                var bpAccess = info.Permission.HasFlag(Permission.Write) ? "BlueprintReadWrite" : "BlueprintReadOnly";
                var editorAccess = "EditAnywhere";

                sb.AppendLine($"UPROPERTY({bpAccess},{editorAccess})");
            });

            sb.AppendLine(base.Print(info, fileType));

            return sb.ToString();
        }
    }
}