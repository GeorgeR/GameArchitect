namespace GameArchitect.Design.Support.Metadata
{
    public sealed class PropertyInfo : MemberInfoBase<System.Reflection.PropertyInfo>
    {
        public override string TypeName => "Property";

        public PropertyInfo(TypeInfo declaringType, System.Reflection.PropertyInfo native)
            : base(declaringType, native)
        {
            Name = Native.Name;
            Type = ResolveType(Native.PropertyType, Native);

            if (native.CanWrite)
            {
                Mutability = Mutability.Mutable;
                Access |= Permission.Write;
            }

            if (native.CanRead)
                Access |= Permission.Read;
        }

        public override string GetPath()
        {
            return $"{DeclaringType.GetPath()}.{Name} : {GetTypeString()}";
        }
    }
}