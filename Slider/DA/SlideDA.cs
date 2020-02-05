using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Slider.DataStructure;

namespace Radyn.Slider.DA
{
    public sealed class SlideDA : DALBase<Slide>
    {
        public SlideDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class SlideCommandBuilder
    {
    }
}
