using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameArchitect.Extensions
{
    public static class StringExtensions
    {
        public static string PrependIfNot(this string source, string str)
        {
            if (source.StartsWith(str))
                return source;

            return str + source;
        }

        public static string Indent(this string str, int tabCount)
        {
            var tabs = Enumerable.Repeat('\t', tabCount);
            var split = str.Split(Environment.NewLine);
            var result = string.Empty;
            foreach (var segment in split)
                result += $"{tabs}{segment}{Environment.NewLine}";

            return result;
        }

        public static string ToPascalCase(this string str)
        {
            if (str == null) return str;
            if (str.Length < 2) return str.ToUpper();

            // Split the string into words.
            string[] words = str.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            var result = "";
            foreach (var word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }

        public static string ToCamelCase(this string str)
        {
            if (str == null || str.Length < 2)
                return str;

            var words = str.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            var result = words[0].ToLower();
            for (var i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var startUnderscores = Regex.Match(str, @"^_+");
            return startUnderscores + Regex.Replace(str, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}