using CommandLine;

namespace GameArchitect.Tasks.Runner
{
    public class Options
    {
        [Option('e', "entities", Required = false, HelpText = "Path to assemblies containing entities (comma seperated if multiple)")]
        public string EntityPaths { get; set; }

        [Option('t', "task", Required = false, HelpText = "Task to run, defaults to the first task found in the specified assembly.")]
        public string Task { get; set; }

        [Option('p', "path", Required = false, HelpText = "Path where task assemblies exist (comma separated if multiple)")]
        public string TaskPaths { get; set; }

        [Option('o', "options", Required = false, HelpText = "Task options")]
        public string TaskOptions { get; set; }
    }
}