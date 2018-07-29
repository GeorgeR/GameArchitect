using CommandLine;

namespace GameArchitect.Tasks.Runner
{
    public class Options
    {
        [Option('g', "task", Required = false, HelpText = "Task to run")]
        public string Task { get; set; }

        [Option('p', "path", Required = true, HelpText = "Path where task assemblies exist (comma seperated if multiple)")]
        public string AssemblyPaths { get; set; }

        [Option('o', "options", Required = false, HelpText = "Task options")]
        public string TaskOptions { get; set; }
    }
}