using System.Collections.Generic;
using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IEventInfo : IMemberInfo<System.Reflection.EventInfo>
    {
        IList<IParameterInfo> Parameters { get; }
    }

    public interface IEventInfo<TParameterInfo> : IEventInfo where TParameterInfo : class, IParameterInfo
    {
        new IList<TParameterInfo> Parameters { get; }
    }

    public abstract class EventInfoBase<TTypeInfo, TParameterInfo>
        : MemberInfoBase<TTypeInfo, System.Reflection.EventInfo>, 
        IEventInfo<TParameterInfo>
        where TTypeInfo : class, ITypeInfo
        where TParameterInfo : class, IParameterInfo
    {
        protected EventInfoBase(IMetadataProvider metadataProvider, TTypeInfo declaringType, System.Reflection.EventInfo native)
            : base(metadataProvider, declaringType, native)
        {
            Name = native.Name;
            Type = ResolveType(native.EventHandlerType, native);
        }

        private IList<IParameterInfo> _parameters;
        public IList<IParameterInfo> Parameters
        {
            get
            {
                if (_parameters != null)
                    return _parameters;

                // TODO
                _parameters = new List<IParameterInfo>();
                //_parameters.AddRange(
                //    Native
                //        .GetParameters()
                //        .Select(o => new ParameterInfo(this, DeclaringType, o)));

                return _parameters;
            }
        }
        IList<TParameterInfo> IEventInfo<TParameterInfo>.Parameters => Parameters.Cast<TParameterInfo>().ToList();

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

    public sealed class EventInfo : EventInfoBase<TypeInfo, ParameterInfo>
    {
        public EventInfo(IMetadataProvider metadataProvider, TypeInfo declaringType, System.Reflection.EventInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}