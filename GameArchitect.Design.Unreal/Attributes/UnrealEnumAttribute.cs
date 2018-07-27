using System;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class UnrealEnumAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            ForMeta<TypeInfo>(info, e =>
            {
                if(!e.Inherits<byte>())
                    throw new Exception($"An enum marked as UnrealEnum ({info.GetPath()}) must inherit from byte (uint8)");
            });

            return true;
        }
    }
}