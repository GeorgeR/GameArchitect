using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using GameArchitect.Tasks;

namespace SomeGame.Tasks.Empty
{
    [Export(typeof(ITask))]
    public class InvalidTask : ITask
    {
        public string Name { get; set; } = "Invalid";
        public Type ParameterType { get; }
        public Type OptionsType { get; } = typeof(InvalidOptions);

        public async Task PreTask(ITaskParameters parameters) { }

        public async Task<bool> Run(ITaskParameters parameters)
        {
            return true;
        }

        public async Task PostTask(ITaskParameters parameters) { }
    }
}