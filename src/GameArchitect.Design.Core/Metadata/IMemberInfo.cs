using System;
using System.Collections;
using System.Reflection;
using System.Text;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Support;
using GameArchitect.Extensions.Reflection;

namespace GameArchitect.Design.Metadata
{
    public interface IMemberInfo : IMetaInfo
    {
        Mutability Mutability { get; }
        bool IsOptional { get; }
        CollectionType CollectionType { get; }

        ITypeInfo Type { get; } // If this is null, the type is void
        ITypeInfo DeclaringType { get; }
    }

    public interface IMemberInfo<out TNative> : IMemberInfo, IMetaInfo<TNative>
    {

    }

    public abstract class MemberInfoBase<TNative> : MetaInfoBase, IMemberInfo<TNative> where TNative : ICustomAttributeProvider
    {
        public virtual TNative Native { get; }
        protected override ICustomAttributeProvider AttributeProvider => Native;

        public override string Name { get; protected set; }
        public abstract override string TypeName { get; }

        public override Permission Permission { get; set; }
        public Mutability Mutability { get; protected set; } = Mutability.Immutable;
        public bool IsOptional { get; protected set; }
        public CollectionType CollectionType { get; protected set; }

        public ITypeInfo Type { get; protected set; }
        public ITypeInfo DeclaringType { get; }

        protected MemberInfoBase(ITypeInfo declaringType, TNative native)
        {
            Native = native;
            DeclaringType = declaringType;
        }

        /* Note Collections of optionals are not supported. */
        protected ITypeInfo ResolveType(Type native, TNative member)
        {
            var isOptional = native.Name.ToLower().Contains("nullable")
                             || native.Name.Contains("?")
                             || native.HasAttribute<OptionalAttribute>()
                             || member.HasAttribute<OptionalAttribute>();

            if (isOptional)
                native = native.GenericTypeArguments.Length > 0 ? native.GenericTypeArguments[0] : native;

            IsOptional = isOptional;

            if (native != typeof(string))
            {
                if (typeof(Stack).IsAssignableFrom(native))
                    CollectionType = CollectionType.Stack;
                else if (typeof(Queue).IsAssignableFrom(native))
                    CollectionType = CollectionType.Queue;
                else if (typeof(IDictionary).IsAssignableFrom(native))
                    CollectionType = CollectionType.Dictionary;
                else if (typeof(IEnumerable).IsAssignableFrom(native))
                    CollectionType = CollectionType.Array;

                if (CollectionType != CollectionType.None)
                    native = native.GenericTypeArguments[0];
            }

            return new TypeInfo(native);
        }

        public abstract override string GetPath();

        protected string GetTypeString()
        {
            var sb = new StringBuilder();

            if (CollectionType != CollectionType.None)
                sb.Append($"{CollectionType.ToString()}<");

            sb.Append(Name);

            if (CollectionType != CollectionType.None)
                sb.Append(">");

            if (IsOptional)
                sb.Append("?");

            return sb.ToString();
        }
    }
}