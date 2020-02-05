using Radyn.Framework;

namespace Radyn.Graphic.Definition
{
    public class Enums
    {
        public enum ResourceType
        {
            [Description("JSFile", Type = typeof(Resources.Graphic))]
            JSFile,
            [Description("CssFile", Type = typeof(Resources.Graphic))]
            CssFile,
            [Description("JSFunction", Type = typeof(Resources.Graphic))]
            JSFunction,
            [Description("Style", Type = typeof(Resources.Graphic))]
            Style,
        }
    }
}
