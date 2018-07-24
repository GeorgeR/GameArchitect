using System;
using System.Collections.Generic;

namespace GameArchitect.Design.Support.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class DeconstructAttribute : Attribute
    {
        public DeconstructCondition Condition { get; } = DeconstructCondition.Always;
        public List<string> Properties { get; }

        public DeconstructAttribute() { }

        public DeconstructAttribute(List<string> properties)
        {
            Properties = properties;
        }
    }
}