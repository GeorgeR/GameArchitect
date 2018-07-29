using System.Collections.Generic;

namespace GameArchitect.Design
{
    public interface IDeconstructible
    {
        /* Return list of property names */
        IEnumerable<string> Deconstruct();
    }
}