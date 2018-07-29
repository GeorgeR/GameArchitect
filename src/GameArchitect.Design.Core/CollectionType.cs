namespace GameArchitect.Design
{
    /// <summary>
    /// Collection type, if any. Dictionary currently unsupported.
    /// Not all of these are supported by emitted languages.
    /// </summary>
    public enum CollectionType
    {
        None,
        Array,
        List,
        Dictionary,
        Stack,
        Queue
    }
}