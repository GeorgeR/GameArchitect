using System;
using System.Text;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendInclude(this StringBuilder sb, string path)
        {
            return sb.AppendLine($"#include \"{path}\"");
        }

        public static StringBuilder AppendUsing(this StringBuilder sb, string @namespace)
        {
            return sb.AppendLine($"using {@namespace};");
        }
    }
}