using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealNameTransformer : CXXNameTransformer
    {
        public override string TransformName(IMetaInfo info, string name, NameContext context)
        {
            return name.ToPascalCase();
        }
    }
}