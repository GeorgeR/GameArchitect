using System.Collections.Generic;

namespace GameArchitect.Design.Primitives
{
    public class Vector4<T> : IDeconstructible
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }
        public T W { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "X", "Y", "Z", "W" };
        }
    }
}