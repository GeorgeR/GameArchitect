using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using GameArchitect.DependencyInjection;
using GameArchitect.Design;
using GameArchitect.Tasks;
using GameArchitect.Tasks.CodeGeneration.Unreal;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SomeGame.Design.Data;

namespace SomeGame.Tasks.CodeGeneration.Unreal
{
    [Export(typeof(ITaskParameters))]
    public class UnrealParameters : TaskParameters
    {
        public UnrealParameters() { }

        public UnrealParameters(
            IServiceProvider serviceProvider,
            ILogger<ITaskParameters> logger, 
            ExportCatalog exports, 
            ITaskOptions options) 
            : base(serviceProvider, logger, exports, options) { }

        public override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            
            services.AddConfigurations(typeof(UnrealConfiguration).Assembly);
        }
    }

    [Export(typeof(ITask))]
    public class CreateUnrealTypesTask : ITask
    {
        public string Name { get; set; }
        public Type ParameterType { get; } = typeof(UnrealParameters);
        public Type OptionsType { get; }

        public async Task PreTask(ITaskParameters parameters) { }

        public async Task<bool> Run(ITaskParameters parameters)
        {
            var playerType = parameters.Exports.Get<Player>();

            var templateFactory = parameters.GetService<UnrealTemplateFactory>();
            var template = templateFactory.Create(playerType) as UnrealTypeTemplate;
            template.ModuleName = "SomeGame";

            const string filePath = @"C:\Temp\Out.txt";
            var fileTemplate = new UnrealHeaderTemplate(template);
            fileTemplate.AddGeneratedHeaderInclude(filePath);

            File.WriteAllText(filePath, fileTemplate.Print());

            return true;
        }

        public async Task PostTask(ITaskParameters parameters) { }
    }
}