using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public interface IPrinter<in TMeta> : CodeGeneration.IPrinter<TMeta> where TMeta : IMetaInfo
    {
        string Print(TMeta info, CXXFileType fileType);
    }
}