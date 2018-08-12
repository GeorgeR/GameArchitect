using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXTypeTemplate : ICXXTemplate
    {
        private CXXPropertyPrinter PropertyPrinter { get; }
        private CXXEventPrinter EventPrinter { get; }
        private CXXFunctionPrinter FunctionPrinter { get; }

        public Dictionary<CXXFileType, HashSet<string>> Includes { get; } =
            new Dictionary<CXXFileType, HashSet<string>>
            {
                {CXXFileType.Declaration, new HashSet<string>()},
                {CXXFileType.Definition, new HashSet<string>()}
            };

        protected CXXTypeInfo Info { get; }

        public CXXTypeTemplate(
            CXXPropertyPrinter propertyPrinter, 
            CXXEventPrinter eventPrinter, 
            CXXFunctionPrinter functionPrinter, 
            CXXTypeInfo info)
        {
            PropertyPrinter = propertyPrinter;
            EventPrinter = eventPrinter;
            FunctionPrinter = functionPrinter;

            Info = info;
        }

        private string PrintIncludes(CXXFileType fileType)
        {
            var sb = new StringBuilder();

            Includes.ForEach(i => sb.AppendLine($"#incldue \"{i}\""));
            sb.AppendEmptyLine();

            return sb.ToString();
        }

        public virtual string Print(CXXFileType fileType)
        {
            throw new NotImplementedException();
        }

        public string Print() { throw new NotImplementedException(); }
    }
}