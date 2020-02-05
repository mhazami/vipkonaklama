using System.Collections.Generic;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager.DAL
{
    public sealed class FolderDA : DALBase<Folder>
    {
        public FolderDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }
        public IEnumerable<Folder> GetAll(bool isexternal)
        {

            var commandBiulder = new FolderCommandBuilder();
            var command = commandBiulder.GetAll(isexternal);
            return DBManager.GetCollection<Folder>(base.ConnectionHandler, command);
        }
        public IEnumerable<Folder> GetParents(bool isexternal)
        {

            var commandBiulder = new FolderCommandBuilder();
            var command = commandBiulder.GetParents(isexternal);
            return DBManager.GetCollection<Folder>(base.ConnectionHandler, command);
        }
        public Folder GetFirstParent(bool isexternal)
        {

            var commandBiulder = new FolderCommandBuilder();
            var command = commandBiulder.GetFirstParent(isexternal);
            return DBManager.GetObject<Folder>(base.ConnectionHandler, command);
        }


    }
    internal class FolderCommandBuilder
    {
        public string GetAll(bool isexternal)
        {

            return string.Format("SELECT * FROM [FileManager].[Folder] WHERE IsExternal={0} ORDER BY [Title]", isexternal ? "1" : "0");
        }
        public string GetParents(bool isexternal)
        {

            return string.Format("SELECT * FROM [FileManager].[Folder] WHERE [ParentFolderId] is null and IsExternal={0} ORDER BY [Title]", isexternal ? "1" : "0");
        }
        public string GetFirstParent(bool isexternal)
        {

            return string.Format("SELECT Top(1)* FROM [FileManager].[Folder] WHERE [ParentFolderId] is null and IsExternal={0} ORDER BY [Title]", isexternal ? "1" : "0");
        }


    }
}
