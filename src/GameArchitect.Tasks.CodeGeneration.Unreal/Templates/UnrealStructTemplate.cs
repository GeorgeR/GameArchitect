using System.Text;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealStructTemplate : UnrealTypeTemplate
    {
        public UnrealStructTemplate(
            UnrealPropertyPrinter propertyPrinter, 
            UnrealEventPrinter eventPrinter, 
            UnrealFunctionPrinter functionPrinter, 
            UnrealTypeInfo info) 
            : base(propertyPrinter, eventPrinter, functionPrinter, info) { }

        public override string Print(CXXFileType fileType)
        {
            var sb = new StringBuilder();

            return sb.ToString();
        }
    }
}