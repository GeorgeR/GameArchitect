using System.Collections.Generic;
using System.Text;
using GameArchitect.Extensions;
using GameArchitect.Tasks.CodeGeneration.CXX.Extensions;
using GameArchitect.Tasks.CodeGeneration.Extensions;

namespace GameArchitect.Tasks.CodeGeneration.CXX.Templates
{
    public abstract class CXXFileTemplate : ITemplate
    {
        protected IList<ICXXTypeTemplate> TypeTemplates { get; }

        protected CXXFileTemplate(params ICXXTypeTemplate[] typeTemplates)
        {
            TypeTemplates = typeTemplates;
        }

        protected HashSet<string> GetIncludes(CXXFileType fileType)
        {
            var includes = new HashSet<string>();
            foreach (var o in TypeTemplates)
                includes.UnionWith(o.Includes[fileType]);

            return includes;
        }

        protected string PrintIncludes(params string[] additionalIncludes)
        {
            var sb = new StringBuilder();

            var includes = GetIncludes(CXXFileType.Declaration);
            includes.ForEach(o => sb.AppendInclude(o));
            
            if (additionalIncludes.Length > 0)
            {
                sb.AppendEmptyLine();
                additionalIncludes.ForEach(o => sb.AppendInclude(o));
            }

            return sb.ToString();
        }

        protected string PrintBody(CXXFileType fileType)
        {
            var sb = new StringBuilder();

            TypeTemplates.ForEach(o =>
            {
                sb.Append(o.Print(fileType));
                sb.AppendEmptyLine();
            });

            sb.RemoveLastLine();
            sb.RemoveLastLine();

            return sb.ToString();
        }

        public abstract string Print();
    }
}