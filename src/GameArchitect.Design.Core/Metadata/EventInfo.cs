using System.Collections.Generic;
using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IEventInfo :  IMemberInfo<System.Reflection.EventInfo>
    {
        IList<IParameterInfo> Parameters { get; }
    }

    public class EventInfo : MemberInfoBase<System.Reflection.EventInfo>, IEventInfo
    {
        public override string TypeName { get; } = "Event";

        public EventInfo(ITypeInfo declaringType, System.Reflection.EventInfo native)
            : base(declaringType, native)
        {
            Name = Native.Name;
            Type = ResolveType(Native.EventHandlerType, Native);
        }

        private IList<IParameterInfo> _parameters;
        public IList<IParameterInfo> Parameters
        {
            get
            {
                if (_parameters != null)
                    return _parameters;

                _parameters = new List<IParameterInfo>();
                //_parameters.AddRange(
                //    Native
                //        .GetParameters()
                //        .Select(o => new ParameterInfo(this, DeclaringType, o)));

                return _parameters;
            }
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