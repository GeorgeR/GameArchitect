﻿using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public class CXXFunctionPrinter : PrinterBase, ICXXPrinter<FunctionInfo>
    {
        protected virtual ICXXPrinter<IMemberInfo> ParameterPrinter { get; }

        public CXXFunctionPrinter(
            ILogger<ITemplate> log, 
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer,
            ICXXPrinter<IMemberInfo> parameterPrinter)
            : base(log, nameTransformer, typeTransformer)
        {
            ParameterPrinter = parameterPrinter;
        }

        protected virtual string PrintParameters(FunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            info.GetParameters().ForEach(p =>
            {
                var deconstructed = new List<IMemberInfo>();
                if (DeconstructAttribute.TryDeconstruct(p, ref deconstructed))
                    deconstructed.ForEach(o => { sb.Append(ParameterPrinter.Print(o, fileType) + ", "); });
                else
                    sb.Append(ParameterPrinter.Print(p, fileType) + ", ");
            });

            sb.RemoveLastComma();

            return sb.ToString();
        }

        public virtual string Print(FunctionInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType);

            info.ForAttribute<AsyncAttribute>(attr =>
            {
                parameterStr += $", std::function<void({(info.Type != typeof(void) ? returnTypeStr : string.Empty)})> Callback";
                returnTypeStr = "void";
            });

            sb.AppendLine($"{(info.IsStatic ? "static " : string.Empty)}{returnTypeStr} {NameTransformer.TransformName(info, info.Name, NameContext.Method)}({parameterStr})", 1);

            return sb.ToString();
        }

        public string Print(FunctionInfo info) { throw new NotImplementedException($"Use the CXXFileType overload."); }
    }
}