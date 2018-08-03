using System;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Support;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXParameterPrinter : PrinterBase, ICXXPrinter<IMemberInfo>
    {
        public CXXParameterPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer)
            : base(log, nameTransformer, typeTransformer) { }

        public virtual string Print(IMemberInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();
            
            sb.Append($"{(info.Mutability == Mutability.Immutable ? "const " : string.Empty)}{TypeTransformer.TransformType(info.Type)}{(info.Type.TypeType == TypeType.Reference ? "*" : "&")} {NameTransformer.TransformName(info, info.Name, NameContext.Parameter)}");

            return sb.ToString();
        }

        public string Print(IMemberInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}