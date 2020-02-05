using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
public sealed class OfficeDA : DALBase<Office>
{
	public OfficeDA(IConnectionHandler connectionHandler): base(connectionHandler){}
	internal class OfficeCommandBuilder{}
}
}
