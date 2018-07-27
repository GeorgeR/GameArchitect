using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace GameArchitect
{
    /* NOTE: This assumes the dlls are loaded and validated. */
    public class DllComparer 
        : IEqualityComparer<Assembly>, IComparer<Assembly>, IEqualityComparer<string>
    {
        public int Compare(Assembly left, Assembly right)
        {
            var leftFile = new FileInfo(left.Location);
            var rightFile = new FileInfo(right.Location);

            return leftFile.LastWriteTimeUtc.CompareTo(rightFile.LastWriteTimeUtc);
        }

        public bool Equals(Assembly left, Assembly right)
        {
            return Compare(left, right) == 0;
        }

        public int GetHashCode(Assembly obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(string left, string right)
        {
            return AssemblyLoadContext.GetAssemblyName(left).Name == AssemblyLoadContext.GetAssemblyName(right).Name;
        }

        public int GetHashCode(string obj)
        {
            return AssemblyLoadContext.GetAssemblyName(obj).Name.GetHashCode();
        }
    }
}