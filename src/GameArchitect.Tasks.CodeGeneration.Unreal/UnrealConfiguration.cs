using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealConfiguration : CXXConfiguration, ICodeGenerationConfiguration
    {
        public override void Setup(IServiceCollection services)
        {
            base.Setup(services);

            services.AddSingleton(this);

            services.AddSingleton<UnrealMetadataProvider>();
            services.AddSingleton<UnrealTemplateFactory>();
            services.AddSingleton<UnrealNameTransformer>();
            services.AddSingleton<UnrealTypeTransformer>();
            services.AddSingleton<UnrealPropertyPrinter>();
            services.AddSingleton<UnrealEventPrinter>();
            services.AddSingleton<UnrealFunctionPrinter>();
            services.AddSingleton<UnrealParameterPrinter>();
        }
    }
}