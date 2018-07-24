using System;

namespace GameArchitect.Design.Support.Attributes.Db
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DbKeyAttribute : Attribute
    {
        public int Part { get; } = 0;

        public DbKeyAttribute(int? part)
        {
            if (part.HasValue)
                Part = part.Value;
        }
    }
}