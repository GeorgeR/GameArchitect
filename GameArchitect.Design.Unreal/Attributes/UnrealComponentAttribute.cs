using System;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UnrealComponentAttribute : UnrealTypeAttributeBase
    {
        public static string ActorComponentName = "ActorComponent";
        public static string SceneComponentName = "SceneComponent";

        public override string BaseClassName { get; } = ActorComponentName;
    }
}