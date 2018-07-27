using System;
using System.Collections.Generic;
using System.Text;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    internal class EventPrinter : CXX.Templates.EventPrinter
    {
        protected override INameTransformer NameTransformer { get; } = new UnrealNameTransformer();
        protected override ITypeTransformer TypeTransformer { get; } = new UnrealTypeTransformer();

        protected override CXX.Templates.IPrinter<IMemberInfo> ParameterPrinter { get; } = new ParameterPrinter();

        protected virtual string PrintParameters(EventInfo info, CXXFileType fileType, out int parameterCount)
        {
            var sb = new StringBuilder();

            var pCount = 0;
            info.GetParameters().ForEach(p =>
            {
                var deconstructed = new List<IMemberInfo>();
                if (DeconstructAttribute.TryDeconstruct(p, ref deconstructed))
                    deconstructed.ForEach(o =>
                    {
                        sb.Append(ParameterPrinter.Print(o, fileType) + ", ");
                        pCount++;
                    });
                else
                {
                    sb.Append(ParameterPrinter.Print(p, fileType) + ", ");
                    pCount++;
                }
            });

            sb.RemoveLastComma();

            parameterCount = pCount;

            return sb.ToString();
        }

        public override string Print(EventInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            int parameterCount = 0;

            var eventDeclarationName = info.HasAttribute<UnrealEventAttribute>() 
                ? "DECLARE_DYNAMIC_EVENT_" 
                : "DECLARE_EVENT_";

            //var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType, out parameterCount);
            
            info.GetParameters().ForEach(p =>
            {

            });

            return sb.ToString();
        }
    }
}