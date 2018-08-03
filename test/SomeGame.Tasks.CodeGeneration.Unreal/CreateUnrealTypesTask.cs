using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks;
using GameArchitect.Tasks.CodeGeneration.Unreal;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates;
using Microsoft.Extensions.DependencyInjection;
using SomeGame.Design.Data;

namespace SomeGame.Tasks.CodeGeneration.Unreal
{
    [Export(typeof(ITask))]
    public class CreateUnrealTypesTask : ITask
    {
        public string Name { get; set; }
        public Type ParameterType { get; }
        public Type OptionsType { get; }

        public async Task<bool> Run(ITaskParameters parameters)
        {
            var services = parameters.Services;
            services.AddSingleton<UnrealTemplateFactory>();
            services.AddSingleton<UnrealMetadataProvider>();

            var serviceProvider = services.BuildServiceProvider();
            var attachedMetadataProvider = serviceProvider.GetService<UnrealMetadataProvider>();

            var playerType = parameters.Exports.Get<Player>();
            
            var templateFactory = serviceProvider.GetService<UnrealTemplateFactory>();
            var template = templateFactory.Create(playerType) as UnrealTypeTemplate;
            template.ModuleName = "SomeGame";

            const string filePath = @"C:\Temp\Out.txt";
            var fileTemplate = new UnrealHeaderTemplate(template);
            fileTemplate.AddGeneratedHeaderInclude(filePath);

            File.WriteAllText(filePath, fileTemplate.Print());

            return true;
        }
    }
}