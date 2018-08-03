using System.Collections.Generic;
using System.Text;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CSharp.Templates
{
    public abstract class CSharpTemplate : ITemplate
    {
        public HashSet<string> Usings { get; } = new HashSet<string>();
        public string Namespace { get; private set; }

        internal PropertyPrinter PropertyPrinter { get; } = new PropertyPrinter();
        internal EventPrinter EventPrinter { get; } = new EventPrinter();
        internal FunctionPrinter FunctionPrinter { get; } = new FunctionPrinter();

        protected string PrintUsings()
        {
            var sb = new StringBuilder();

            Usings.ForEach(u => sb.AppendLine($"using {u};"));
            sb.AppendEmptyLine();

            return sb.ToString();
        }

        protected string PrintInsideNamespace(string content)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"namespace {Namespace}");
            sb.OpenBracket();

            sb.Append(content);

            sb.CloseBracket();

            return sb.ToString();
        }

        public abstract string Print();
    }
}