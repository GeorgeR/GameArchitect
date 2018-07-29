using System.Collections.Generic;

namespace GameArchitect.Design.Primitives
{
    public class Transform : IDeconstructible
    {
        public Vector3<float> Position { get; set; }
        public Rotation<float> Rotation { get; set; }
        public Vector3<float> Scale { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "Position.X", "Position.Y", "Position.Z", "Rotation.Pitch", "Rotation.Yaw", "Rotation.Roll", "Scale.X", "Scale.Y", "Scale.Z" };
        }
    }
}