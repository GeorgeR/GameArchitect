using System;

namespace GameArchitect.Design.Attributes.Db
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DbKeyAttribute : DbPropertyAttribute
    {
        public int? Part { get; }

        public DbKeyAttribute() { }

        public DbKeyAttribute(int part)
        {
            Part = part;
        }
    }
}