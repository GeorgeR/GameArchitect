using System.Text;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXHeaderFileTemplate : CXXFileTemplate
    {
        public CXXHeaderFileTemplate(params CXXTypeTemplate[] typeTemplates) : base(typeTemplates) { }

        public override string Print()
        {
            var sb = new StringBuilder();

            sb.AppendLine("#pragma once");
            sb.AppendEmptyLine();
            sb.Append(PrintIncludes());
            sb.AppendEmptyLine();

            sb.Append(PrintBody(CXXFileType.Declaration));

            return sb.ToString();
        }
    }
}