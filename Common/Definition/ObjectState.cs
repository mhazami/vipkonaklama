using System;

namespace Radyn.Common.Definition
{
    [Flags]
    public enum ObjectState
    {
        Create,
        Edit,
        Details,
        Delete,
        List
    }
}
