using System;

namespace GameArchitect.Design.Support
{
    [Flags]
    public enum Permission
    {
        Read = 1 << 0,
        Write = 1 << 1,
        ReadWrite = Read | Write
    }
}