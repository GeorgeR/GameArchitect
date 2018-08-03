using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using GameArchitect.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SomeGame.Design.Data;

namespace SomeGame.Tasks.EntityFramework
{
    [Export(typeof(ITask))]
    public class DbTask : ITask
    {
        public string Name { get; set; }
        public Type ParameterType { get; }
        public Type OptionsType { get; }

        public async Task<bool> Run(ITaskParameters parameters)
        {
            var services = parameters.Services;
            services.AddEntityFrameworkInMemoryDatabase();

            using (var db = new SomeGameDbContext(string.Empty))
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
    }
}