using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public interface ICXXTemplate : CodeGeneration.ITemplate
    {
        string Print(CXXFileType fileType);
    }

    public interface ICXXPrinter<in TMeta> : CodeGeneration.IPrinter<TMeta> where TMeta : IMetaInfo
    {
        string Print(TMeta info, CXXFileType fileType);
    }
}