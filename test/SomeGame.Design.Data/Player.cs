using System.Collections.Generic;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Attributes.Db;
using GameArchitect.Design.Attributes.Net;
using GameArchitect.Design.Primitives;

namespace SomeGame.Design.Data
{
    [Export]
    [NetType(100)]
    public class Player : IDeconstructible
    {
        [DbKey]
        [Required]
        [NetProperty(1)]
        public int Id { get; set; }

        [Required]
        [DbProperty]
        [NetProperty(2)]
        public Vector3<double> Position { get; set; }

        [DbReference]
        public ReferencedItem ReferencedItem { get; set; }

        public delegate void SomethingHappenedDelegate([Deconstruct] Player player, int value);

        [NetEvent]
        public event SomethingHappenedDelegate OnSomethingHappened;

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "Id" };
        }
    }
}