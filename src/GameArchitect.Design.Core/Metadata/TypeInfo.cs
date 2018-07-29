using System;
using System.Linq;
using System.Reflection;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public sealed class TypeInfo : MetaInfoBase, IMetaInfo<Type>
    {
        public System.Type Native { get; }
        protected override ICustomAttributeProvider AttributeProvider => Native;

        public override string Name { get; protected set; }
        public override string TypeName => "Type";

        public TypeType TypeType { get; set; }

        public TypeInfo(Type native)
        {
            Native = native;
            Name = native.Name;

            TypeType = Native.IsValueType ? TypeType.Value : TypeType.Reference;
        }

        private IQueryable<PropertyInfo> _properties;
        public IQueryable<PropertyInfo> GetProperties()
        {
            if (_properties != null)
                return _properties;

            var result = Native
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(o => new PropertyInfo(this, o));

            // Fields not supported for now
            //result = result.Join(
            //    Native
            //        .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            //        .Select(o => new PropertyInfo(this, o)));

            _properties = result.AsQueryable();

            return GetProperties();
        }

        private IQueryable<EventInfo> _events;
        public IQueryable<EventInfo> GetEvents()
        {
            if (_events != null)
                return _events;

            _events = Native
                .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(o => new EventInfo(this, o))
                .AsQueryable();

            return GetEvents();
        }

        private IQueryable<FunctionInfo> _functions;
        public IQueryable<FunctionInfo> GetFunctions()
        {
            if (_functions != null)
                return _functions;

            var baseFunctions = new[] { "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone" };

            _functions = Native
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(o => !o.IsSpecialName) // Excludes property accessors
                .Where(o => !baseFunctions.Contains(o.Name)) // Exclude base methods
                .Select(o => new FunctionInfo(this, o))
                .AsQueryable();

            return GetFunctions();
        }
        
        public bool ImplementsInterface<TInterface>()
        {
            return typeof(TInterface).IsAssignableFrom(Native);
        }

        public bool Inherits<T>()
        {
            return typeof(T).IsAssignableFrom(Native);
        }

        public static implicit operator Type(TypeInfo source)
        {
            return source.Native;
        }

        public override string GetPath()
        {
            return Name;
        }

        public T Create<T>()
        {
            return (T)Activator.CreateInstance(Native);
        }

        public override bool IsValid(ILogger<IValidatable> logger)
        {
            var result = base.IsValid(logger);

            return result
                   && GetProperties().All(p => p.IsValid(logger))
                   && GetEvents().All(e => e.IsValid(logger))
                   && GetFunctions().All(f => f.IsValid(logger));
        }
    }
}