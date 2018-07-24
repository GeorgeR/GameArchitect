namespace GameArchitect.Design.Support.Metadata
{
    public sealed class EventInfo : MemberInfoBase<System.Reflection.EventInfo>
    {
        public override string TypeName => "Event";

        public EventInfo(TypeInfo declaringType, System.Reflection.EventInfo native) 
            : base(declaringType, native)
        {
            Name = Native.Name;
            Type = ResolveType(Native.EventHandlerType, Native);
        }

        public override string GetPath()
        {
            return $"{DeclaringType.GetPath()}.{Name} : {GetTypeString()}";
        }
    }
}