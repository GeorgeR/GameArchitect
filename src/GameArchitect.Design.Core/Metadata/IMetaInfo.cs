using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IMetaInfo : IValidatable
    {
        // Name without guff (related to generics, nullables)
        string Name { get; }
        //string TypeName { get; }

        bool IsStatic { get; }
        Permission Permission { get; }
        
        IEnumerable<Attribute> GetAttributes();
        void ForAttribute<TAttribute>(Action<TAttribute> func) where TAttribute : Attribute;
        TValue ForAttribute<TAttribute, TValue>(Func<TAttribute, TValue> callback, TValue defaultValue) where TAttribute : Attribute;
        TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute;
        bool HasAttribute<TAttribute>() where TAttribute : Attribute;

        // Relative to the root assembly
        string GetPath();
    }

    public interface IMetaInfo<out TNative> : IMetaInfo
    {
        TNative Native { get; }
    }

    public abstract class MetaInfoBase : IMetaInfo
    {
        protected abstract ICustomAttributeProvider AttributeProvider { get; }

        public abstract string Name { get; protected set; }

        public virtual bool IsStatic { get; set; } = false;
        public virtual Permission Permission { get; set; } = Permission.ReadWrite;

        public IEnumerable<Attribute> GetAttributes()
        {
            return AttributeProvider.GetCustomAttributes(true).Cast<Attribute>();
        }

        public void ForAttribute<TAttribute>(Action<TAttribute> func) where TAttribute : Attribute
        {
            if (!HasAttribute<TAttribute>())
                return;

            func(AttributeProvider.GetAttribute<TAttribute>());
        }

        public TValue ForAttribute<TAttribute, TValue>(Func<TAttribute, TValue> callback, TValue defaultValue)
            where TAttribute : Attribute
        {
            if (!HasAttribute<TAttribute>())
                return defaultValue;

            return callback(AttributeProvider.GetAttribute<TAttribute>());
        }

        public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
        {
            return AttributeProvider.GetAttribute<TAttribute>();
        }

        public bool HasAttribute<TAttribute>() where TAttribute : Attribute
        {
            return AttributeProvider.HasAttribute<TAttribute>();
        }

        public abstract string GetPath();

        public virtual bool IsValid(ILogger<IValidatable> logger)
        {
            if(AttributeProvider == null)
                throw new Exception($"AttributeProvider is null for {GetPath()}.");

            return GetAttributes()
                .Where(o => o.GetType().ImplementsInterface<IDelegatedValidatable>())
                .Cast<IDelegatedValidatable>()
                .All(o => o.IsValid(logger, this));
        }
    }
}