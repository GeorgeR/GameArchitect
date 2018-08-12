using System;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXTypeInfo : TypeInfoBase<CXXTypeInfo, CXXPropertyInfo, CXXEventInfo, CXXFunctionInfo>
    {
        public CXXTypeInfo(CXXMetadataProvider metadataProvider, Type native) 
            : base(metadataProvider, native) { }
    }
}