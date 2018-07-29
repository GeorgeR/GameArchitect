using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Specify a range for a numeric property or bind to a minimum and maximum value
    /// to another property (by name).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class RangeAttribute : Attribute
    {
        public object Minimum { get; } = null;
        public object Maximum { get; } = null;

        public string MinimumProperty { get; } = null;
        public string MaximumProperty { get; } = null;

        public RangeAttribute(object minimum = null, object maximum = null)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public RangeAttribute(string minimumProperty = null, string maximumProperty = null)
        {
            MinimumProperty = minimumProperty;
            MaximumProperty = maximumProperty;
        }

        public bool HasMinimum()
        {
            return Minimum != null && !string.IsNullOrEmpty(MinimumProperty);
        }

        public bool HasMaximum()
        {
            return Maximum != null && !string.IsNullOrEmpty(MaximumProperty);
        }

        private void ResolveProperties()
        {
            // TODD: Resolve property bindings where present and write to Minimum/Maximum
        }

        public T GetMinimum<T>()
        {
            if(!HasMinimum())
                throw new Exception("Attempted to get a minimum value where none is specified.");

            ResolveProperties();

            return (T) Minimum;
        }

        public T GetMaximum<T>()
        {
            if (!HasMaximum())
                throw new Exception("Attempted to get a maximum value where none is specified.");

            ResolveProperties();

            return (T) Maximum;
        }
    }
}