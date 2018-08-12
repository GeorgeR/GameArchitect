using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public sealed class CXXPropertyPrinter : ICXXPrinter<CXXPropertyInfo>
    {
        private CXXNameTransformer NameTransformer { get; }
        private CXXTypeTransformer TypeTransformer { get; }

        public CXXPropertyPrinter(CXXNameTransformer nameTransformer, CXXTypeTransformer typeTransformer)
        {
            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
        }

        public string Print(CXXPropertyInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{TypeTransformer.TransformType(info)} {NameTransformer.TransformName(info, info.Name, NameContext.Property)};", 1);

            return sb.ToString();
        }

        public string Print(CXXPropertyInfo info) { throw new NotImplementedException($"Using the CXXFileType overload."); }
    }
}