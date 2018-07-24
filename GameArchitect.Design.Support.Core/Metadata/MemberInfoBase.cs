using System;
using System.Collections;
using System.Reflection;
using System.Text;
using GameArchitect.Design.Support.Attributes;

namespace GameArchitect.Design.Support.Metadata
{
    public abstract class MemberInfoBase<TNative> : MetaInfoBase, IMemberInfo<TNative>
        where TNative : ICustomAttributeProvider
    {
        public virtual TNative Native { get; }
        protected override ICustomAttributeProvider AttributeProvider => Native;

        public override string Name { get; protected set; }
        public abstract override string TypeName { get; }

        public override Permission Access { get; set; }
        public Mutability Mutability { get; protected set; } = Mutability.Immutable;
        public bool IsOptional { get; protected set; }
        public CollectionType CollectionType { get; protected set; }

        public TypeInfo Type { get; protected set; }
        public TypeInfo DeclaringType { get; }

        protected MemberInfoBase(TypeInfo declaringType, TNative native)
        {
            Native = native;
            DeclaringType = declaringType;
        }

        /* Note Collections of optionals are not supported. */
        protected TypeInfo ResolveType(Type native, TNative member)
        {
            var isOptional = native.Name.Contains("Nullable", StringComparison.CurrentCultureIgnoreCase)
                             || native.Name.Contains("?")
                             || native.GetCustomAttribute<OptionalAttribute>() != null
                             || member.GetCustomAttributes(typeof(OptionalAttribute), true).Length > 0;

            if (isOptional)
                native = native.GetGenericArguments()[0];

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