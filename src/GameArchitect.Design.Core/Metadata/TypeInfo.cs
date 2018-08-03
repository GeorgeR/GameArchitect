using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameArchitect.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface ITypeInfo : IMetaInfo<Type>
    {
        TypeType TypeType { get; }

        IList<ITypeInfo> BaseTypes { get; }
        IList<IPropertyInfo> Properties { get; }
        IList<IEventInfo> Events { get; }
        IList<IFunctionInfo> Functions { get; }

        bool ImplementsInterface<TInterface>();
        bool Inherits<T>();

        T Create<T>();
    }

    public class TypeInfo : MetaInfoBase, ITypeInfo
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

        private IList<ITypeInfo> _baseTypes;
        public IList<ITypeInfo> BaseTypes
        {
            get
            {
                if (_baseTypes != null)
                    return _baseTypes;

                _baseTypes = new List<ITypeInfo>();
                if (Native.BaseType != null)
                    _baseTypes.Add(new TypeInfo(Native.BaseType));

                var interfaces = Native.GetInterfaces();
                if (interfaces.Length > 0)
                {
                    foreach (var iface in interfaces)
                        _baseTypes.Add(new TypeInfo(iface));
                }

                return _baseTypes;
            }
        }
        
        private IList<IPropertyInfo> _properties;
        public IList<IPropertyInfo> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                
                _properties = new List<IPropertyInfo>();
                _properties.AddRange(
                    Native
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                        .Select(o => new PropertyInfo(this, o)));

                // Fields not supported for now
                //result = result.Join(
                //    Native
                //        .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                //        .Select(o => new PropertyInfo(this, o)));

                return _properties;
            }
        }

        private IList<IEventInfo> _events;
        public IList<IEventInfo> Events
        {
            get
            {
                if (_events != null)
                    return _events;

                _events = new List<IEventInfo>();
                _events.AddRange(
                    Native
                        .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                        .Select(o => new EventInfo(this, o)));

                return _events;
            }
        }

        private IList<IFunctionInfo> _functions;
        public IList<IFunctionInfo> Functions
        {
            get
            {
                if (_functions != null)
                    return _functions;

                var baseFunctions = new[] { "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone" };

                _functions = new List<IFunctionInfo>();
                _functions.AddRange(
                    Native
                        .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                        .Where(o => !o.IsSpecialName) // Excludes property accessors
                        .Where(o => !baseFunctions.Contains(o.Name)) // Exclude base methods
                        .Select(o => new FunctionInfo(this, o)));

                return _functions;
            }
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
                   && BaseTypes.All(t => t.IsValid(logger))
                   && Properties.All(p => p.IsValid(logger))
                   && Events.All(e => e.IsValid(logger))
                   && Functions.All(f => f.IsValid(logger));
        }
    }
}