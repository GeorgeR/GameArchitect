using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Indicates the property is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RequiredAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            if(info.HasAttribute<OptionalAttribute>())
                throw new Exception($"Property {info.GetPath()} was marked as Required but also has an Optional attribute.");

            return true;
        }
    }
}