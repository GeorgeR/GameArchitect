using System;
using GameArchitect.Tasks.Runner;
using NUnit.Framework;
using SomeGame.Design.Data;
using SomeGame.Tasks.CodeGeneration.Unreal;
using SomeGame.Tasks.Empty;
using SomeGame.Tasks.EntityFramework;

namespace GameArchitect.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Can_Run_Validation()
        {
            var entityAssembly = typeof(Player).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly
            };

            var application = new Application(options);
        }

        [Test]
        public void Can_Run_Empty_Task()
        {
            var entityAssembly = typeof(Player).Assembly.Location;
            var taskAssembly = typeof(EmptyTask).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly,
                TaskPaths = taskAssembly,
                Task = "Ham"
            };

            var application = new Application(options);
        }

        [Test]
        public void Cant_Run_Task_With_Invalid_Options()
        {
            var entityAssembly = typeof(Player).Assembly.Location;
            var taskAssembly = typeof(EmptyTask).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly,
                TaskPaths = taskAssembly,
                Task = "invalid"
            };
            
            Assert.Throws<Exception>(() => { var application = new Application(options); });
        }

        [Test]
        public void Can_Run_Db_Task()
        {
            var entityAssembly = typeof(Player).Assembly.Location;
            var taskAssembly = typeof(DbTask).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly,
                TaskPaths = taskAssembly,
                Task = "db"
            };

            var application = new Application(options);
        }

        [Test]
        public void Can_Run_Unreal_Task()
        {
            var entityAssembly = typeof(Player).Assembly.Location;
            var taskAssembly = typeof(CreateUnrealTypesTask).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly,
                TaskPaths = taskAssembly,
                Task = "CreateUnrealTypes"
            };

            var application = new Application(options);
        }
    }
}