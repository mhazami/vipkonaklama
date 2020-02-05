using System;
using System.Data;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager.DAL
{
    public sealed class FileDA : DALBase<File>
    {
        public FileDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
        public override int Insert(File obj)
        {
            var commandBiulder = new FileCommandBuilder();
            var command = commandBiulder.Insert(obj);
            return DBManager.ExecuteNonQuery(base.ConnectionHandler, command);
        }

        public override int Update(File obj)
        {
            var commandBiulder = new FileCommandBuilder();
            var command = commandBiulder.Update(obj);
            return DBManager.ExecuteNonQuery(base.ConnectionHandler, command);
        }

        
    }
    internal class FileCommandBuilder
    {
        public IDbCommand Insert(File obj)
        {
            using (var command = new System.Data.SqlClient.SqlCommand())
            {
                var query = "INSERT INTO [FileManager].[File](";
                var values = string.Empty;
                query += "Id, ";
                values += "@Id, ";
                query += "FileName, ";
                values += "@FileName, ";
                query += "ContentType, ";
                values += "@ContentType, ";
                query += "Extension, ";
                values += "@Extension, ";
                query += "Content, ";
                values += "@Content, ";
                query += "FolderId, ";
                values += "@FolderId, ";
                query += "LanguageId, ";
                values += "@LanguageId, ";
                query += "SaveDate, ";
                values += "@SaveDate, ";
                query += "CreatorId, ";
                values += "@CreatorId, ";
                query += "Tags, ";
                values += "@Tags, ";
                query = query.Substring(0, query.Length - 2);
                values = values.Substring(0, values.Length - 2);
                query = string.Format("{0}) VALUES ({1})", query, values);

                command.CommandText = query;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", obj.Id));
                command.Parameters.Add(string.IsNullOrEmpty(obj.FileName)
                        ? new System.Data.SqlClient.SqlParameter("@FileName", DBNull.Value)
                        : new System.Data.SqlClient.SqlParameter("@FileName", obj.FileName));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContentType", obj.ContentType));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Extension", obj.Extension));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", obj.Content));
                command.Parameters.Add(obj.FolderId == null
                    ? new System.Data.SqlClient.SqlParameter("@FolderId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@FolderId", obj.FolderId));
                command.Parameters.Add(string.IsNullOrEmpty(obj.LanguageId)
                    ? new System.Data.SqlClient.SqlParameter("@LanguageId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@LanguageId", obj.LanguageId));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SaveDate", obj.SaveDate));
                command.Parameters.Add(obj.CreatorId == null
                    ? new System.Data.SqlClient.SqlParameter("@CreatorId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@CreatorId", obj.CreatorId));
                command.Parameters.Add(string.IsNullOrEmpty(obj.Tags)
                    ? new System.Data.SqlClient.SqlParameter("@Tags", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@Tags", obj.Tags));
                return command;
            }
        }

        public IDbCommand Update(File obj)
        {
            using (var command = new System.Data.SqlClient.SqlCommand())
            {
                var query = "UPDATE [FileManager].[File] SET ";
                query += "FileName = @FileName, ";
                query += "ContentType = @ContentType, ";
                query += "Extension =@Extension, ";
                query += "Content = @Content, ";
                query += "FolderId = @FolderId, ";
                query += "LanguageId = @LanguageId, ";
                query += "SaveDate = @SaveDate, ";
                query += "CreatorId = @CreatorId, ";
                query += "Tags = @Tags  ";
                query += " WHERE Id = @Id";

                command.CommandText = query;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", obj.Id));
                command.Parameters.Add(string.IsNullOrEmpty(obj.FileName)
                        ? new System.Data.SqlClient.SqlParameter("@FileName", DBNull.Value)
                        : new System.Data.SqlClient.SqlParameter("@FileName", obj.FileName));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContentType", obj.ContentType));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Extension", obj.Extension));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", obj.Content));
                command.Parameters.Add(obj.FolderId == null
                    ? new System.Data.SqlClient.SqlParameter("@FolderId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@FolderId", obj.FolderId));
                command.Parameters.Add(string.IsNullOrEmpty(obj.LanguageId)
                    ? new System.Data.SqlClient.SqlParameter("@LanguageId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@LanguageId", obj.LanguageId));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SaveDate", obj.SaveDate));
                command.Parameters.Add(obj.CreatorId == null
                    ? new System.Data.SqlClient.SqlParameter("@CreatorId", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@CreatorId", obj.CreatorId));
                command.Parameters.Add(string.IsNullOrEmpty(obj.Tags)
                    ? new System.Data.SqlClient.SqlParameter("@Tags", DBNull.Value)
                    : new System.Data.SqlClient.SqlParameter("@Tags", obj.Tags));
                return command;
            }
        }

        
    }
}
