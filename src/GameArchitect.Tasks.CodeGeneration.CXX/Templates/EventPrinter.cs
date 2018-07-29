using System;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class EventPrinter : IPrinter<EventInfo>
    {
        protected virtual INameTransformer NameTransformer { get; } = new CXXNameTransformer();
        protected virtual ITypeTransformer TypeTransformer { get; } = new CXXTypeTransformer();

        protected virtual IPrinter<IMemberInfo> ParameterPrinter { get; } = new ParameterPrinter();

        public virtual string Print(EventInfo info, CXXFileType fileType)
        {
            throw new NotImplementedException();
        }

        public string Print(EventInfo info)
        {
            throw new NotImplementedException($"Using the CXXFileType overload.");
        }
    }
}