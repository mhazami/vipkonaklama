using System;


namespace Radyn.WebApp
{
    [Flags]
    public enum PageMode : byte
    {
        None,
        Details,
        Delete,
        Edit,
        Create,
        BeforeStart,
        Flow,
        AfterStart,
    }


    [Flags]
    public enum ModifyBehavior : byte
    {
        SelfModify,
        DependedModify,

    }
   
}