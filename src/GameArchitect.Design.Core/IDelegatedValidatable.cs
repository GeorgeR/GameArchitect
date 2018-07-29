using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design
{
    public interface IDelegatedValidatable
    {
        bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info) where TMeta : IMetaInfo;
    }
}