namespace Radyn.ContentManager.Definition
{
    public class Enums
    {
        public enum UiPartialEnums : byte
        {

            [System.ComponentModel.Description("پایین")]
            Down = 0,
            [System.ComponentModel.Description("بالا")]
            Top = 1,
        }
      
        public enum PartialTypes : byte
        {
            [System.ComponentModel.Description("کنترل محتوا")]
            ContentManager = 1,
            [System.ComponentModel.Description("ماژول")]
            Modual = 2,
        }
    }
}
