namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public interface ICXXTemplate : ITemplate
    {
        string Print(CXXFileType fileType);
    }
}