using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class SectionDA : DALBase<Section>
    {
        public SectionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class SectionCommandBuilder
    {
    }
}
