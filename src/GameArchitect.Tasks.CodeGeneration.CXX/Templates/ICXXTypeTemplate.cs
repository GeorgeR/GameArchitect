using System.Collections.Generic;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public interface ICXXTypeTemplate : ICXXTemplate
    {
        IDictionary<CXXFileType, HashSet<string>> Includes { get; }
    }
}