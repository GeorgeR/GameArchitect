using System;

namespace GameArchitect.Design.Attributes.Db
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DbPropertyAttribute : Attribute
    {
        public int? Index { get; }
        public string Name { get; }

        public DbPropertyAttribute()
        {
            
        }
    }
}