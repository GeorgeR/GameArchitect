using System.Linq;
using GameArchitect.Design.Support;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public sealed class PropertyInfo : MemberInfoBase<System.Reflection.PropertyInfo>
    {
        public override string TypeName => "Property";

        public PropertyInfo(TypeInfo declaringType, System.Reflection.PropertyInfo native)
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