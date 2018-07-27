using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration
{
    public interface IPrinter
    {
        string Print();
    }

    public interface IPrinter<in TMeta> where TMeta : IMetaInfo
    {
        string Print(TMeta info);
    }
}