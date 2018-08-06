using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXTypeTemplate : PrinterBase, ICXXTemplate
    {
        protected ICXXPrinter<IPropertyInfo> PropertyPrinter { get; }
        protected ICXXPrinter<IEventInfo> EventPrinter { get; }
        protected ICXXPrinter<IFunctionInfo> FunctionPrinter { get; }

        public Dictionary<CXXFileType, HashSet<string>> Includes { get; } =
            new Dictionary<CXXFileType, HashSet<string>>()
            {
                {CXXFileType.Declaration, new HashSet<string>()},
                {CXXFileType.Definition, new HashSet<string>()}
            };

        protected ITypeInfo Info { get; }

        public CXXTypeTemplate(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer,
            ICXXPrinter<IPropertyInfo> propertyPrinter,
            ICXXPrinter<IEventInfo> eventPrinter,
            ICXXPrinter<IFunctionInfo> functionPrinter,
            ITypeInfo info)
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