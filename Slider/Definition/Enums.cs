using System.ComponentModel;

namespace Radyn.Slider.Definition
{
    public class Enums
    {
        public enum SliderCycleFxType
        {
            [Description("blindX")]
            blindX,
            [Description("blindY")]
            blindY,
            [Description("blindZ")]
            blindZ,
            [Description("cover")]
            cover,
            [Description("curtainX")]
            curtainX,
            [Description("curtainY")]
            curtainY,
            [Description("fade")]
            fade,
            [Description("fadeZoom")]
            fadeZoom,
            [Description("growX")]
            growX,
            [Description("growY")]
            growY,
            [Description("scrollUp")]
            scrollUp,
            [Description("scrollDown")]
            scrollDown,
            [Description("scrollLeft")]
            scrollLeft,
            [Description("scrollRight")]
            scrollRight,
            [Description("scrollHorz")]
            scrollHorz,
            [Description("scrollVert")]
            scrollVert,
            [Description("shuffle")]
            shuffle,
            [Description("slideX")]
            slideX,
            [Description("slideY")]
            slideY,
            [Description("toss")]
            toss,
            [Description("turnUp")]
            turnUp,
            [Description("turnDown")]
            turnDown,
            [Description("turnLeft")]
            turnLeft,
            [Description("turnRight")]
            turnRight,
            [Description("uncover")]
            uncover,
            [Description("wipe")]
            wipe,

        }
    }
}
