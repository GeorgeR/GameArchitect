using GameArchitect.Design;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    internal class UnrealParameterPrinter : CXXParameterPrinter
    {
        public UnrealParameterPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer) 
            : base(log, nameTransformer, typeTransformer) { }
    }
}