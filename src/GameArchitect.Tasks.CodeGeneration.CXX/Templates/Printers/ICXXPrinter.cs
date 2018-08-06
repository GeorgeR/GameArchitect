using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public interface ICXXPrinter<in TMeta> : IPrinter<TMeta> where TMeta : IMetaInfo
    {
        string Print(TMeta info, CXXFileType fileType);
    }
}