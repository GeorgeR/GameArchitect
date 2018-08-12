using System;
using System.Linq;
using System.Reflection;
using GameArchitect.Tasks;
using GameArchitect.Tasks.Runner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SomeGame.Design.Data;
using SomeGame.Tasks.CodeGeneration.Unreal;
using SomeGame.Tasks.Empty;
using SomeGame.Tasks.EntityFramework;

namespace GameArchitect.Tests
{
    [TestClass]
    public class Tests
    {
        [TestInitialize]
        public void Setup()
        {
            var loader = new AssemblyLoader();
        }

        [TestMethod]        
        public void Can_Scan()
        {
            var taskAssembly = typeof(DbTask).Assembly;
            var types = taskAssembly.GetExportedTypes()
                .Where(o => typeof(ITask).IsAssignableFrom(o))
                .Select(o => (ITask) Activator.CreateInstance(o));
            var task = types.FirstOrDefault();
            Assert.IsNotNull(task);
            
            var l = taskAssembly.Location;
            taskAssembly = Assembly.LoadFile(l);
            types = taskAssembly.GetExportedTypes()
                .Where(o => typeof(ITask).IsAssignableFrom(o))
                .Select(o => (ITask) Activator.CreateInstance(o));
            task = types.FirstOrDefault();
            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void Can_Run_Validation()
        {
            var entityAssembly = typeof(Player).Assembly.Location;

            var options = new Options()
            {
                EntityPaths = entityAssembly
            };

            var application = new Application(options);
        }

        [TestMethod]
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

        [TestMethod]
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
            
            Assert.ThrowsException<Exception>(() => { var application = new Application(options); });
        }

        [TestMethod]
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

        [TestMethod]
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