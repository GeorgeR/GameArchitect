using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SomeGame.Design.Data;

namespace SomeGame.Tasks.EntityFramework
{
    [Export(typeof(ITaskParameters))]
    public class DbParameters : TaskParameters
    {
        public DbParameters() { }

        public DbParameters(
            IServiceProvider serviceProvider,
            ILogger<ITaskParameters> logger, 
            ExportCatalog exports, 
            ITaskOptions options) 
            : base(serviceProvider, logger, exports, options) { }

        public override void Setup(IServiceCollection services)
        {
            base.Setup(services);

            services.AddEntityFrameworkInMemoryDatabase();
        }
    }

    [Export(typeof(ITask))]
    public class DbTask : ITask
    {
        public string Name { get; set; }
        public Type ParameterType { get; } = typeof(DbParameters);
        public Type OptionsType { get; }

        public async Task PreTask(ITaskParameters parameters) { }

        public async Task<bool> Run(ITaskParameters parameters)
        {
            if(parameters == null)
                throw new NullReferenceException();

            using (var db = new SomeGameDbContext(parameters.GetService<IMetadataProvider>(), string.Empty))
            {
                var created = db.Database.EnsureCreated();

                var player = new Player()
                {
                    ReferencedItem = new ReferencedItem()
                };

                var playerSet = db.Set<Player>();

                var entry = playerSet.Add(player);
                db.SaveChanges();

                player = playerSet.FirstOrDefault();
                var id = player.Id;
                var o = id;
            }

            return true;
        }

        public async Task PostTask(ITaskParameters parameters) { }
    }
}