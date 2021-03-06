﻿using System.Collections.Generic;

namespace GameArchitect.Design.Primitives
{
    public class Vector2<T> : IDeconstructible
    {
        public T X { get; set; }
        public T Y { get; set; }

        /* Return list of property names */
        public IEnumerable<string> Deconstruct()
        {
            return new[] { "X", "Y" };
        }
    }
}