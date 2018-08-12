using System;
using GameArchitect.DependencyInjection;
using GameArchitect.Design;

namespace GameArchitect.Tasks.CodeGeneration
{
    public interface ICodeGenerationConfiguration : IServiceConfiguration
    {
        //void SetMetadataProvider(IServiceProvider serviceProvider, ExportCatalog catalog);
    }
}