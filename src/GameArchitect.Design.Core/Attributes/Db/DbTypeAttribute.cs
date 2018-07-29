using System;

namespace GameArchitect.Design.Attributes.Db
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DbTypeAttribute : Attribute
    {
        public string TypeName { get; }

        public DbTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}