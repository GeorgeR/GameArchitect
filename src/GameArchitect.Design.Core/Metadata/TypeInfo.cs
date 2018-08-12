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

    public interface ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo> : ITypeInfo
        where TTypeInfo : class, ITypeInfo
        where TPropertyInfo : class, IPropertyInfo
        where TEventInfo : class, IEventInfo
        where TFunctionInfo : class, IFunctionInfo
    {
        new IList<TTypeInfo> BaseTypes { get; }
        new IList<TPropertyInfo> Properties { get; }
        new IList<TEventInfo> Events { get; }
        new IList<TFunctionInfo> Functions { get; }
    }

    public abstract class TypeInfoBase<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>
        : MetaInfoBase,
        ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>
        where TTypeInfo : class, ITypeInfo
        where TPropertyInfo : class, IPropertyInfo
        where TEventInfo : class, IEventInfo
        where TFunctionInfo : class, IFunctionInfo
    {
        protected IMetadataProvider MetadataProvider { get; }

        protected override ICustomAttributeProvider AttributeProvider => Native;
        public System.Type Native { get; }
        
        public override string Name { get; protected set; }

        public TypeType TypeType { get; set; }

        protected TypeInfoBase(IMetadataProvider metadataProvider, Type native)
        {
            MetadataProvider = metadataProvider;

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
                    _baseTypes.Add(MetadataProvider.Create(Native.BaseType));

                var interfaces = Native.GetInterfaces();
                if (interfaces.Length > 0)
                {
                    foreach (var iface in interfaces)
                        _baseTypes.Add(MetadataProvider.Create(iface));
                }

                return _baseTypes;
            }
        }
        IList<TTypeInfo> ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>.BaseTypes => BaseTypes.Cast<TTypeInfo>().ToList();

        private IList<IPropertyInfo> _properties;
        public IList<IPropertyInfo> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;

                _properties = new List<IPropertyInfo>();
                Native.GetProperties(BindingFlags.Public
                                     | BindingFlags.NonPublic
                                     | BindingFlags.Instance
                                     | BindingFlags.Static)
                    .Select(o => MetadataProvider.Create(this, o))
                    .ForEach(o => _properties.Add(o));
                
                // Fields not supported for now
                //result = result.Join(
                //    Native
                //        .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                //        .Select(o => new PropertyInfo(this, o)));

                return _properties;
            }
        }
        IList<TPropertyInfo> ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>.Properties => Properties.Cast<TPropertyInfo>().ToList();

        private IList<IEventInfo> _events;
        public IList<IEventInfo> Events
        {
            get
            {
                if (_events != null)
                    return _events;

                _events = new List<IEventInfo>();
                Native
                    .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Select(o => MetadataProvider.Create(this, o))
                    .ForEach(o => _events.Add(o));
                
                return _events;
            }
        }
        IList<TEventInfo> ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>.Events => Events.Cast<TEventInfo>().ToList();

        private IList<IFunctionInfo> _functions;
        public IList<IFunctionInfo> Functions
        {
            get
            {
                if (_functions != null)
                    return _functions;

                var baseFunctions = new[] { "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone" };

                _functions = new List<IFunctionInfo>();
                Native
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Where(o => !o.IsSpecialName) // Excludes property accessors
                    .Where(o => !baseFunctions.Contains(o.Name)) // Exclude base methods
                    .Select(o => MetadataProvider.Create(this, o))
                    .ForEach(o => _functions.Add(o));
                
                return _functions;
            }
        }
        IList<TFunctionInfo> ITypeInfo<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo>.Functions => Functions.Cast<TFunctionInfo>().ToList();

        public bool ImplementsInterface<TInterface>()
        {
            return typeof(TInterface).IsAssignableFrom(Native);
        }

        public bool Inherits<T>()
        {
            return typeof(T).IsAssignableFrom(Native);
        }

        //public static implicit operator Type(TypeInfo source)
        //{
        //    return source.Native;
        //}

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

    public class TypeInfo 
        : TypeInfoBase<TypeInfo, PropertyInfo, EventInfo, FunctionInfo>
    {
        public TypeInfo(IMetadataProvider metadataProvider, Type native) 
            : base(metadataProvider, native) { }
    }
}