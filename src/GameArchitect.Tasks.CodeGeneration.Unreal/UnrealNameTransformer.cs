using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealNameTransformer : CXXNameTransformer
    {
        public override string TransformName(IMetaInfo info, string name, NameContext context)
        {
            var prefix = string.Empty;
            var suffix = string.Empty;
            var forBlueprint = true;

            if (info is TypeInfo typeInfo)
            {
                var asBaseClass = false;

                typeInfo.ForAttribute<UnrealTypeAttributeBase>(attr =>
                {
                    forBlueprint = attr.ForBlueprint;

                    prefix = forBlueprint ? "U" : "F";
                    asBaseClass = attr.AsBaseClass;
                });

                typeInfo.ForAttribute<UnrealActorAttribute>(attr =>
                {
                    prefix = "A";
                    suffix = "Actor";
                });

                typeInfo.ForAttribute<UnrealComponentAttribute>(attr =>
                {
                    suffix = "Component";
                });

                typeInfo.ForAttribute<UnrealStructAttribute>(attr =>
                {
                    prefix = "F";
                });

                if (asBaseClass)
                    suffix += "Base";
            }

            return $"{prefix}{name.ToPascalCase()}{suffix}";
        }
    }
}