using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    public class UnrealTypeTemplate : ICXXTemplate
    {
        public string ModuleName { get; set; }

        protected UnrealPropertyPrinter PropertyPrinter { get; }
        protected UnrealEventPrinter EventPrinter { get; }
        protected UnrealFunctionPrinter FunctionPrinter { get; }

        public Dictionary<CXXFileType, HashSet<string>> Includes { get; } =
            new Dictionary<CXXFileType, HashSet<string>>
            {
                {CXXFileType.Declaration, new HashSet<string>()},
                {CXXFileType.Definition, new HashSet<string>()}
            };

        protected UnrealTypeInfo Info { get; }

        public UnrealTypeTemplate(
            UnrealPropertyPrinter propertyPrinter,
            UnrealEventPrinter eventPrinter,
            UnrealFunctionPrinter functionPrinter, 
            UnrealTypeInfo info)
        {
            PropertyPrinter = propertyPrinter;
            EventPrinter = eventPrinter;
            FunctionPrinter = functionPrinter;
            Info = info;
        }

        protected string PrintAPI()
        {
            return $"{ModuleName.ToUpper()}_API";
        }

        protected string PrintIncludes(CXXFileType fileType)
        {
            var sb = new StringBuilder();

            Includes.ForEach(i => sb.AppendLine($"#incldue \"{i}\""));
            sb.AppendEmptyLine();

            return sb.ToString();
        }

        public virtual string Print(CXXFileType fileType)
        {
            if (string.IsNullOrEmpty(ModuleName))
                throw new Exception($"For an unreal generated file, the module name must be specified");

            return string.Empty;
        }

        public string Print() { throw new NotImplementedException(); }
    }
}