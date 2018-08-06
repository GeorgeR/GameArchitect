namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public interface ICXXTemplate : CodeGeneration.ITemplate
    {
        string Print(CXXFileType fileType);
    }
}