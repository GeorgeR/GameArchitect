using System;
using GameArchitect.Design.CXX.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public class CXXEventPrinter : ICXXPrinter<CXXEventInfo>
    {
        protected CXXParameterPrinter ParameterPrinter { get; }

        public CXXEventPrinter(CXXParameterPrinter parameterPrinter)
        {
            ParameterPrinter = parameterPrinter;
        }

        public virtual string Print(CXXEventInfo info, CXXFileType fileType)
        {
            throw new NotImplementedException();
        }

        public string Print(CXXEventInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}