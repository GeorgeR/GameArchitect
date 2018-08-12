using System.IO;
using System.Linq;
using System.Text;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealHeaderTemplate : CXXHeaderFileTemplate
    {
        private string GeneratedHeaderName { get; set; }

        public UnrealHeaderTemplate(params UnrealTypeTemplate[] typeTemplates) : base(typeTemplates.Cast<CXXTypeTemplate>().ToArray()) { }

        public void AddGeneratedHeaderInclude(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            GeneratedHeaderName = $"{fileName}.Generated.h";
        }

        public override string Print()
        {
            var sb = new StringBuilder();

            sb.AppendLine("#pragma once");
            sb.AppendEmptyLine();
            sb.Append(PrintIncludes(GeneratedHeaderName));
            sb.AppendEmptyLine();

            sb.Append(PrintBody(CXXFileType.Declaration));

            return sb.ToString();
        }
    }
}