using System;
using System.Collections.Generic;
using System.Linq;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Support;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IPropertyInfo : IMemberInfo<System.Reflection.PropertyInfo>
    {
        IEnumerable<IMemberInfo> Deconstruct();
    }
    
    public abstract class PropertyInfoBase<TTypeInfo> 
        : MemberInfoBase<TTypeInfo, System.Reflection.PropertyInfo>, IPropertyInfo
        where TTypeInfo : class, ITypeInfo
    {
        protected PropertyInfoBase(IMetadataProvider metadataProvider, TTypeInfo declaringType, System.Reflection.PropertyInfo native)
            : base(metadataProvider, declaringType, native)
        {
            Name = native.Name;
            Type = ResolveType(native.PropertyType, native);

            if (native.CanWrite)
            {
                Mutability = Mutability.Mutable;
                Permission |= Permission.Write;
            }

            if (native.CanRead)
                Permission |= Permission.Read;
        }

        public IEnumerable<IMemberInfo> Deconstruct()
        {
            if (!HasAttribute<DeconstructAttribute>() && !Type.ImplementsInterface<IDeconstructible>())
            {
                Console.WriteLine("WHAT");
                return null;
            }
            
            var result = new List<IMemberInfo>();
            return DeconstructAttribute.TryDeconstruct(this, ref result) ? result : null;
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

    public sealed class PropertyInfo : PropertyInfoBase<TypeInfo>
    {
        public PropertyInfo(IMetadataProvider metadataProvider, TypeInfo declaringType, System.Reflection.PropertyInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}