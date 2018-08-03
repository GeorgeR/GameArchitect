using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXPropertyPrinter : PrinterBase, ICXXPrinter<PropertyInfo>
    {
        public CXXPropertyPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer)
            : base(log, nameTransformer, typeTransformer) { }

        public virtual string Print(PropertyInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{TypeTransformer.TransformType(info)} {NameTransformer.TransformName(info, info.Name, NameContext.Property)};", 1);

            return sb.ToString();
        }

        public string Print(PropertyInfo info) { throw new NotImplementedException($"Using the CXXFileType overload."); }
    }
}