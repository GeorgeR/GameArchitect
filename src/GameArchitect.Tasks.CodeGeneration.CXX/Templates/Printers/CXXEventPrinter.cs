using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public class CXXEventPrinter : PrinterBase, ICXXPrinter<IEventInfo>
    {
        protected virtual ICXXPrinter<IMemberInfo> ParameterPrinter { get; }

        public CXXEventPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer,
            ICXXPrinter<IMemberInfo> parameterPrinter)
            : base(log, nameTransformer, typeTransformer)
        {
            ParameterPrinter = parameterPrinter;
        }

        public virtual string Print(IEventInfo info, CXXFileType fileType)
        {
            throw new NotImplementedException();
        }

        public string Print(IEventInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}