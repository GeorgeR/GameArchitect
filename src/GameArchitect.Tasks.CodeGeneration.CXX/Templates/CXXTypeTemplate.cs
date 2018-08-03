using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXTypeTemplate : PrinterBase, ICXXTemplate
    {
        protected ICXXPrinter<PropertyInfo> PropertyPrinter { get; }
        protected ICXXPrinter<EventInfo> EventPrinter { get; }
        protected ICXXPrinter<FunctionInfo> FunctionPrinter { get; }

        public Dictionary<CXXFileType, HashSet<string>> Includes { get; } =
            new Dictionary<CXXFileType, HashSet<string>>()
            {
                {CXXFileType.Declaration, new HashSet<string>()},
                {CXXFileType.Definition, new HashSet<string>()}
            };

        protected TypeInfo Info { get; }

        public CXXTypeTemplate(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer,
            ICXXPrinter<PropertyInfo> propertyPrinter,
            ICXXPrinter<EventInfo> eventPrinter,
            ICXXPrinter<FunctionInfo> functionPrinter,
            TypeInfo info)
            : base(log, nameTransformer, typeTransformer)
        {
            PropertyPrinter = propertyPrinter;
            EventPrinter = eventPrinter;
            FunctionPrinter = functionPrinter;

            Info = info;
        }

        protected string PrintIncludes(CXXFileType fileType)
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