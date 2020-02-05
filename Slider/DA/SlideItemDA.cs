using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Slider.DataStructure;

namespace Radyn.Slider.DA
{
    public sealed class SlideItemDA : DALBase<SlideItem>
    {
        public SlideItemDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class SlideItemCommandBuilder
    {
    }
}
