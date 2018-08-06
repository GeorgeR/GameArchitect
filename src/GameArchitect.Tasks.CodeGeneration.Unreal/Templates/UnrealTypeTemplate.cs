using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealTypeTemplate : CXXTypeTemplate
    {
        public string ModuleName { get; set; }

        public UnrealTypeTemplate(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer, 
            ICXXPrinter<IPropertyInfo> propertyPrinter, 
            ICXXPrinter<IEventInfo> eventPrinter, 
            ICXXPrinter<IFunctionInfo> functionPrinter,
            ITypeInfo info) 
            : base(log, nameTransformer, typeTransformer, propertyPrinter, eventPrinter, functionPrinter, info) { }

        protected string PrintAPI()
        {
            return $"{ModuleName.ToUpper()}_API";
        }

        public override string Print(CXXFileType fileType)
        {
            if (string.IsNullOrEmpty(ModuleName))
                throw new Exception($"For an unreal generated file, the module name must be specified");

            return string.Empty;
        }
    }
}