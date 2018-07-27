using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Indicates that the property or parameter is optional.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class OptionalAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            if (info.HasAttribute<RequiredAttribute>())
                throw new Exception($"Property {info.GetPath()} was marked as Optional but also has an Required attribute.");

            return true;
        }
    }
}