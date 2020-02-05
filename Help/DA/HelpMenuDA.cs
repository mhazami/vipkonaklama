using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;

namespace Radyn.Help.DA
{
public sealed class HelpMenuDA : DALBase<HelpMenu>
{
	public HelpMenuDA(IConnectionHandler connectionHandler): base(connectionHandler){}
	internal class HelpMenuCommandBuilder{}
}
}
