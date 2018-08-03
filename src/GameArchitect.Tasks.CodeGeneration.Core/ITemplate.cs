using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration
{
    public interface ITemplate
    {
        string Print();
    }

    public interface IPrinter<in TMeta> where TMeta : IMetaInfo
    {
        string Print(TMeta info);
    }

    public abstract class PrinterBase
    {
        protected ILogger<ITemplate> Log { get; }

        protected INameTransformer NameTransformer { get; }
        protected ITypeTransformer TypeTransformer { get; }

        protected PrinterBase(ILogger<ITemplate> log, INameTransformer nameTransformer, ITypeTransformer typeTransformer)
        {
            Log = log;

            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
        }
    }
}