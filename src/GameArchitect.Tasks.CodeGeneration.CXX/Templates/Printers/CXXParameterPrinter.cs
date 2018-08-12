using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Support;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers
{
    public sealed class CXXParameterPrinter : ICXXPrinter<IMemberInfo>
    {
        private CXXNameTransformer NameTransformer { get; }
        private CXXTypeTransformer TypeTransformer { get; }

        public CXXParameterPrinter(CXXNameTransformer nameTransformer, CXXTypeTransformer typeTransformer)
        {
            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
        }

        public string Print(IMemberInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();
            
            sb.Append($"{(info.Mutability == Mutability.Immutable ? "const " : string.Empty)}{TypeTransformer.TransformType(info.Type)}{(info.Type.TypeType == TypeType.Reference ? "*" : "&")} {NameTransformer.TransformName(info, info.Name, NameContext.Parameter)}");

            return sb.ToString();
        }

        public string Print(IMemberInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}