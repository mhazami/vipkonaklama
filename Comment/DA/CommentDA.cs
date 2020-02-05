using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Comments.DA
{
public sealed class CommentDA : DALBase<Comment>
{
	public CommentDA(IConnectionHandler connectionHandler): base(connectionHandler){}
	internal class CommentCommandBuilder{}
}
}
