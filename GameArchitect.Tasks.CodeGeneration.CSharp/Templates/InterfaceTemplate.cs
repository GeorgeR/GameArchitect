using System.Linq;
using System.Text;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CSharp.Templates
{
    public class InterfaceTemplate : CSharpTemplate
    {
        private Access Access { get; }
        private TypeInfo Type { get; }
        private IQueryable<PropertyInfo> Properties { get; }
        private IQueryable<EventInfo> Events { get; }
        private IQueryable<FunctionInfo> Functions { get; }

        public InterfaceTemplate(Access access, TypeInfo type, IQueryable<PropertyInfo> properties, IQueryable<EventInfo> events, IQueryable<FunctionInfo> functions)
        {
            Access = access;
            Type = type;
            Properties = properties;
            Events = events;
            Functions = functions;
        }

        public override string Print()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{Access.ToString()} interface {Type.Name.PrependIfNot("I")}");
            sb.OpenBracket();

            Properties.ForEach(o =>
            {

            });


            sb.CloseBracket();

            return sb.ToString();
        }
    }
}