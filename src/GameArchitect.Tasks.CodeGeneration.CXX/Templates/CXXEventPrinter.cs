using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXEventPrinter : PrinterBase, ICXXPrinter<EventInfo>
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

        public virtual string Print(EventInfo info, CXXFileType fileType)
        {
            throw new NotImplementedException();
        }

        public string Print(EventInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}