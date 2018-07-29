using Microsoft.Extensions.Logging;

namespace GameArchitect
{
    public interface IValidatable
    {
        bool IsValid(ILogger<IValidatable> logger);
    }
}