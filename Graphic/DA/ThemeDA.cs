using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Graphic.DataStructure;

namespace Radyn.Graphic.DA
{
    public sealed class ThemeDA : DALBase<Theme>
    {
        public ThemeDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class ThemeCommandBuilder
    {
    }
}
