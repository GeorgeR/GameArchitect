using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    public abstract class ValidatableAttribute : Attribute, IDelegatedValidatable
    {
        public virtual bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info) where TMeta : IMetaInfo
        {
            var usage = GetType().GetAttribute<AttributeUsageAttribute>();
            var validOn = usage.ValidOn;

            var message = $"This attribute is not valid on type {info.GetType().Name} called {info.GetPath()}.";

            if (!(validOn.HasFlag(AttributeTargets.Class) || validOn.HasFlag(AttributeTargets.Struct) || validOn.HasFlag(AttributeTargets.Enum)) && info is ITypeInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Property) && info is IPropertyInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Method) && info is IFunctionInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Delegate) && info is IEventInfo)
                throw new Exception(message);

            if(!validOn.HasFlag(AttributeTargets.Parameter) && info is IParameterInfo)
                throw new Exception(message);

            ITypeInfo outer = null;
            if (info is ITypeInfo typeInfo)
                outer = typeInfo;
            else if (info is IMemberInfo memberInfo)
                outer = memberInfo.DeclaringType;

            if(outer != null)
                if (!outer.HasAttribute<ExportAttribute>())
                    throw new Exception($"Type {outer.GetPath()} should have the [Export] attribute.");
            
            return true;
        }

        protected void ForMeta<TMeta>(IMetaInfo info, Action<TMeta> func) where TMeta : class, IMetaInfo
        {
            if (info is TMeta meta)
                func(meta);
        }
    }
}
