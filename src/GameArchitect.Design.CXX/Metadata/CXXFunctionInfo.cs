using System.Reflection;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXFunctionInfo : FunctionInfoBase<CXXTypeInfo, CXXParameterInfo>
    {
        public CXXFunctionInfo(CXXMetadataProvider metadataProvider, CXXTypeInfo declaringType, MethodInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}