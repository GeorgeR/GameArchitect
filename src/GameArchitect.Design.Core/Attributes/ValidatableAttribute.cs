using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions.Reflection;

namespace GameArchitect.Design.Attributes
{
    public abstract class ValidatableAttribute : Attribute, IDelegatedValidatable
    {
        public virtual bool IsValid<TMeta>(TMeta info) where TMeta : IMetaInfo
        {
            var usage = GetType().GetAttribute<AttributeUsageAttribute>();
            var validOn = usage.ValidOn;

            var message = $"This attribute is not valid on type {info.TypeName} called {info.GetPath()}.";

            if (!(validOn.HasFlag(AttributeTargets.Class) || validOn.HasFlag(AttributeTargets.Struct) || validOn.HasFlag(AttributeTargets.Enum)) && info is TypeInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Property) && info is PropertyInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Method) && info is FunctionInfo)
                throw new Exception(message);

            if (!validOn.HasFlag(AttributeTargets.Delegate) && info is EventInfo)
                throw new Exception(message);

            if(!validOn.HasFlag(AttributeTargets.Parameter) && info is ParameterInfo)
                throw new Exception(message);

            TypeInfo outer = null;
            if (info is TypeInfo)
                outer = (info as TypeInfo);
            else if (info is IMemberInfo)
                outer = ((IMemberInfo) info).DeclaringType;

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
