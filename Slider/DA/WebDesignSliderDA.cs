using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Slider.DA
{
    public sealed class WebDesignSliderDA : DALBase<DataStructure.WebDesignSlider>
    {
        public WebDesignSliderDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class SliderCommandBuilder
    {
    }
}
