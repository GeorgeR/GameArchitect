using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameArchitect.Design.Support.Metadata
{
    public interface IMetaInfo
    {
        // Name without guff (related to generics, nullables)
        string Name { get; }
        string TypeName { get; }

        bool IsStatic { get; }
        Permission Access { get; }

        IEnumerable<Attribute> GetAttributes();
        void ForAttribute<TAttribute>(Action<TAttribute> func) where TAttribute : Attribute;
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
        public abstract string TypeName { get; }

        public virtual bool IsStatic { get; set; } = false;
        public virtual Permission Access { get; set; } = Permission.ReadWrite;

        public IEnumerable<Attribute> GetAttributes()
        {
            return AttributeProvider.GetCustomAttributes(true).Cast<Attribute>();
        }

        public void ForAttribute<TAttribute>(Action<TAttribute> func) where TAttribute : Attribute
        {
            if (HasAttribute<TAttribute>())
            {
                var attribute = (TAttribute)AttributeProvider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
                func(attribute);
            }
        }

        public bool HasAttribute<TAttribute>() where TAttribute : Attribute
        {
            return AttributeProvider.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
        }

        public abstract string GetPath();
    }
}