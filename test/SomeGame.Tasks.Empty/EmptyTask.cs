using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using GameArchitect.Tasks;

namespace SomeGame.Tasks.Empty
{
    [Export(typeof(ITask))]
    public class EmptyTask : ITask
    {
        public string Name { get; set; } = "Ham";
        public Type ParameterType { get; }
        public Type OptionsType { get; } = typeof(EmptyOptions);

        public async Task<bool> Run(ITaskParameters parameters)
        {
            return true;
        }
    }
}