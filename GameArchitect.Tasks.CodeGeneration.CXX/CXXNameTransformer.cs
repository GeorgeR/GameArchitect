using System;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX
{
    public class CXXNameTransformer : INameTransformer
    {
        public virtual string TransformName(IMetaInfo info, string name, NameContext context)
        {
            return name;
        }
    }
}