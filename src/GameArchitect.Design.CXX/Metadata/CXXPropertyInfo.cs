using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public class CXXPropertyInfo : PropertyInfo
    {
        public CXXPropertyInfo(ITypeInfo declaringType, System.Reflection.PropertyInfo native) 
            : base(declaringType, native) { }
    }
}