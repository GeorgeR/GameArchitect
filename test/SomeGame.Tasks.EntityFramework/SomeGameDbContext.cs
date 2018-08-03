using GameArchitect.Support.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SomeGame.Design.Data;

namespace SomeGame.Tasks.EntityFramework
{
    public class SomeGameDbContext : GameArchitectDbContextBase
    {
        private string ConnectionString { get; }

        public DbSet<ReferencedItem> ReferencedItems { get; set; }
        public DbSet<Player> Players { get; set; }

        public SomeGameDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDb");
        }
    }
}