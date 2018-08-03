using System;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public class CXXTypeInfo : TypeInfo
    {
        public bool AsBaseClass { get; protected set; }

        public CXXTypeInfo(Type native) : base(native) { }
    }
}