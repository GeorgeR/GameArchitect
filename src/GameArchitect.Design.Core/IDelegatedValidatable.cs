using GameArchitect.Design.Metadata;

namespace GameArchitect.Design
{
    public interface IDelegatedValidatable
    {
        bool IsValid<TMeta>(TMeta info) where TMeta : IMetaInfo;
    }
}