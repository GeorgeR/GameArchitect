using System;
using System.Linq;
using System.Reflection;

namespace GameArchitect.Design.Support.Metadata
{
    public sealed class TypeInfo : MetaInfoBase, IMetaInfo<Type>
    {
        public System.Type Native { get; }
        protected override ICustomAttributeProvider AttributeProvider => Native;

        public override string Name { get; protected set; }
        public override string TypeName => "Type";

        public TypeType TypeType { get; set; }

        public Lazy<IQueryable<PropertyInfo>> Properties { get; }
        public Lazy<IQueryable<EventInfo>> Events { get; }
        public Lazy<IQueryable<FunctionInfo>> Functions { get; }

        public TypeInfo(Type native)
        {
            Native = native;
            Name = native.Name;

            TypeType = Native.IsValueType ? TypeType.Value : TypeType.Reference;

            Properties = new Lazy<IQueryable<PropertyInfo>>(() =>
            {
                var result = Native
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Select(o => new PropertyInfo(this, o));

                // Fields not supported for now
                //result = result.Join(
                //    Native
                //        .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                //        .Select(o => new PropertyInfo(this, o)));

                return result.AsQueryable();
            });

            Events = new Lazy<IQueryable<EventInfo>>(() =>
            {
                return Native
                    .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Select(o => new EventInfo(this, o))
                    .AsQueryable();
            });

            Functions = new Lazy<IQueryable<FunctionInfo>>(() =>
            {
                var baseFunctions = new[] { "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone" };

                return Native
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Where(o => !o.IsSpecialName) // Excludes property accessors
                    .Where(o => !baseFunctions.Contains(o.Name)) // Exclude base methods
                    .Select(o => new FunctionInfo(this, o))
                    .AsQueryable();
            });
        }

        public bool ImplementsInterface<TInterface>()
        {
            return typeof(TInterface).IsAssignableFrom(Native);
        }

        public bool Inherits<T>()
        {
            return typeof(T).IsAssignableFrom(Native);
        }

        public override string GetPath()
        {
            return Name;
        }

        public T Create<T>()
        {
            return (T)Activator.CreateInstance(Native);
        }
    }
}