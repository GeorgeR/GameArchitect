using System.Collections.Generic;

namespace GameArchitect.Design.Primitives
{
    public class Rotation<T> : IDeconstructible
    {
        public T Pitch { get; set; }
        public T Yaw { get; set; }
        public T Roll { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "Pitch", "Yaw", "Roll" };
        }
    }
}