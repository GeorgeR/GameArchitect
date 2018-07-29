using System.Collections.Generic;

namespace GameArchitect.Design.Primitives
{
    public class Box<T> : IDeconstructible
    {
        public Vector3<T> Min { get; set; }
        public Vector3<T> Max { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "Min.X", "Min.Y", "Min.Z", "Max.X", "Max.Y", "Max.Z" };
        }
    }
}