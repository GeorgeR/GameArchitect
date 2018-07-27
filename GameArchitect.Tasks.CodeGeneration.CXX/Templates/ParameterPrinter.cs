using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Support;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class ParameterPrinter : IPrinter<IMemberInfo>
    {
        protected virtual INameTransformer NameTransformer { get; } = new CXXNameTransformer();
        protected virtual ITypeTransformer TypeTransformer { get; } = new CXXTypeTransformer();

        public virtual string Print(IMemberInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();
            
            sb.Append($"{(info.Mutability == Mutability.Immutable ? "const " : string.Empty)}{TypeTransformer.TransformType(info.Type)}{(info.Type.TypeType == TypeType.Reference ? "*" : "&")} {NameTransformer.TransformName(info, info.Name, NameContext.Parameter)}");

            return sb.ToString();
        }

        public string Print(IMemberInfo info)
        {
            throw new NotImplementedException($"Using the CXXFileType overload.");
        }
    }
}