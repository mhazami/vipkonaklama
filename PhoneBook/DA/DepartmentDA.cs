using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
public sealed class DepartmentDA : DALBase<Department>
{
	public DepartmentDA(IConnectionHandler connectionHandler): base(connectionHandler){}
	internal class DepartmentCommandBuilder{}
}
}
