using System;
using System.Text;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class PropertyPrinter : IPrinter<PropertyInfo>
    {
        protected virtual INameTransformer NameTransformer { get; } = new CXXNameTransformer();
        protected virtual ITypeTransformer TypeTransformer { get; } = new CXXTypeTransformer();

        public virtual string Print(PropertyInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{TypeTransformer.TransformType(info)} {NameTransformer.TransformName(info, info.Name, NameContext.Property)};", 1);

            return sb.ToString();
        }

        public string Print(PropertyInfo info)
        {
            throw new NotImplementedException($"Using the CXXFileType overload.");
        }
    }
}