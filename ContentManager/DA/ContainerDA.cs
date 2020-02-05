using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.DA
{
public sealed class ContainerDA : DALBase<Container>
{
public ContainerDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class ContainerCommandBuilder
{
}
}
