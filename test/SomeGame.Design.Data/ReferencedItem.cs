using System.Collections.Generic;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Attributes.Db;
using GameArchitect.Design.Attributes.Net;

namespace SomeGame.Design.Data
{
    [Export]
    public class ReferencedItem : IDeconstructible
    {
        [DbKey]
        [Required]
        [NetProperty(1)]
        public int Id { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "Id" };
        }
    }
}