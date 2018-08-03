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

    public class PropertyInfo : MemberInfoBase<System.Reflection.PropertyInfo>, IPropertyInfo
    {
        public override string TypeName => "Property";

        public PropertyInfo(ITypeInfo declaringType, System.Reflection.PropertyInfo native)
            : base(declaringType, native)
        {
            Name = Native.Name;
            Type = ResolveType(Native.PropertyType, Native);

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
}