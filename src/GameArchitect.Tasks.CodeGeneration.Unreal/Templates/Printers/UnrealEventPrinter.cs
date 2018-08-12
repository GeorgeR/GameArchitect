using System.Collections.Generic;
using System.Text;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates.Printers;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates.Printers
{
    public class UnrealEventPrinter : ICXXPrinter<UnrealEventInfo>
    {
        protected UnrealParameterPrinter ParameterPrinter { get; }

        public UnrealEventPrinter(UnrealParameterPrinter parameterPrinter)
        {
            ParameterPrinter = parameterPrinter;
        }

        protected virtual string PrintParameters(UnrealEventInfo info, CXXFileType fileType, out int parameterCount)
        {
            var sb = new StringBuilder();

            var pCount = 0;
            info.Parameters.ForEach(p =>
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

        public string Print(UnrealEventInfo info, CXXFileType fileType)
        {
            var sb = new StringBuilder();

            int parameterCount = 0;

            var eventDeclarationName = info.HasAttribute<UnrealEventAttribute>() 
                ? "DECLARE_DYNAMIC_EVENT_" 
                : "DECLARE_EVENT_";

            //var returnTypeStr = TypeTransformer.TransformType(info.Type);
            var parameterStr = PrintParameters(info, fileType, out parameterCount);
            
            info.Parameters.ForEach(p =>
            {

            });

            return sb.ToString();
        }

        public string Print(UnrealEventInfo info) { throw new System.NotImplementedException(); }
    }
}