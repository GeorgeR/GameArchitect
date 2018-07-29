using System;

namespace GameArchitect.Design.Attributes
{
    /* Marks an object as exportable for tasks and codegeneration. All your design entities should include this. */
    [AttributeUsage(AttributeTargets.All)]
    public class ExportAttribute : Attribute
    {
    }
}