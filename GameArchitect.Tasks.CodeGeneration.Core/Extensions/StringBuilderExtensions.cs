using System;
using System.Text;

namespace GameArchitect.Tasks.CodeGeneration.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendEmptyLine(this StringBuilder sb)
        {
            return sb.AppendLine(string.Empty);
        }

        public static StringBuilder OpenBracket(this StringBuilder sb, int tabs = 0)
        {
            sb = sb.AppendTabs(tabs);
            return sb.AppendLine("{");
        }

        public static StringBuilder CloseBracket(this StringBuilder sb, int tabs = 0)
        {
            sb = sb.AppendTabs(tabs);
            return sb.AppendLine("}");
        }

        public static StringBuilder CloseBracketWithSemicolon(this StringBuilder sb)
        {
            return sb.AppendLine("};");
        }

        public static StringBuilder RemoveLastLine(this StringBuilder sb)
        {
            var position = sb.ToString().LastIndexOf(Environment.NewLine);
            if (position < 0)
                return sb;

            sb = sb.Remove(position, Environment.NewLine.Length);

            return sb;
        }

        public static StringBuilder RemoveLastComma(this StringBuilder sb)
        {
            return sb = new StringBuilder(sb.ToString().TrimEnd(','));
        }

        public static StringBuilder AppendLine(this StringBuilder sb, string str, int tabs = 0)
        {
            sb = sb.AppendTabs(tabs);
            return sb.AppendLine(str);
        }

        private static StringBuilder AppendTabs(this StringBuilder sb, int tabs)
        {
            if (tabs <= 0)
                return sb;

            return sb.Append('\t', tabs);
        }
    }
}