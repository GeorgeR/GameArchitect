using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public sealed class EventInfo : MemberInfoBase<System.Reflection.EventInfo>
    {
        public override string TypeName { get; } = "Event";

        public EventInfo(TypeInfo declaringType, System.Reflection.EventInfo native)
            : base(declaringType, native)
        {
            Name = Native.Name;
            Type = ResolveType(Native.EventHandlerType, Native);
        }

        private IQueryable<ParameterInfo> _parameters;
        public IQueryable<ParameterInfo> GetParameters()
        {
            if (_parameters != null)
                return _parameters;

            //_parameters = Native
            //    .GetParameters()
            //    .Select(o => new ParameterInfo(this, DeclaringType, o))
            //    .AsQueryable();

            return GetParameters();
        }

        public override string GetPath()
        {
            return $"{DeclaringType.GetPath()}.{Name} : {GetTypeString()}";
        }

        public override bool IsValid(ILogger<IValidatable> logger)
        {
            var result = base.IsValid(logger);

            return result && GetAttributes().Where(o => o.GetType().ImplementsInterface<IDelegatedValidatable>())
                       .Cast<IDelegatedValidatable>()
                       .All(a => a.IsValid(logger, this));
        }
    }
}