using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public abstract class CXXTemplate : IPrinter
    {
        private ILoggerFactory LoggerFactory { get; }

        public Dictionary<CXXFileType, HashSet<string>> Includes { get; } = new Dictionary<CXXFileType, HashSet<string>>();
        public List<string> NamespacePath { get; } = new List<string>();

        internal PropertyPrinter PropertyPrinter { get; } = new PropertyPrinter();
        internal EventPrinter EventPrinter { get; } = new EventPrinter();
        internal FunctionPrinter FunctionPrinter { get; }

        protected CXXTemplate(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;

            FunctionPrinter = new FunctionPrinter(loggerFactory);
        }

        protected string PrintIncludes(CXXFileType fileType)
        {
            var sb = new StringBuilder();

            Includes.ForEach(i => sb.AppendLine($"#incldue \"{i}\""));
            sb.AppendEmptyLine();

            return sb.ToString();
        }
        
        public abstract string Print(CXXFileType fileType);

        // Use Print(fileType)
        public string Print() { throw new NotImplementedException(); }
    }
}