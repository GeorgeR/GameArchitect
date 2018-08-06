using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealStructTemplate : UnrealTypeTemplate
    {
        public UnrealStructTemplate(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer, 
            ICXXPrinter<IPropertyInfo> propertyPrinter, 
            ICXXPrinter<IEventInfo> eventPrinter,
            ICXXPrinter<IFunctionInfo> functionPrinter,
            ITypeInfo info) 
            : base(log, nameTransformer, typeTransformer, propertyPrinter, eventPrinter, functionPrinter, info) { }

        public override string Print(CXXFileType fileType)
        {
            var sb = new StringBuilder();


            return sb.ToString();
        }
    }
}