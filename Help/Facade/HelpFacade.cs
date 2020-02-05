using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.BO;
using Radyn.Help.DataStructure;
using Radyn.Help.Facade.Interface;
using Radyn.Utility;

namespace Radyn.Help.Facade
{
    internal sealed class HelpFacade : HelpFacade<DataStructure.Help>, IHelpFacade
    {
        internal HelpFacade()
        {
        }

        internal HelpFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public bool AddHelpRelation(Guid sourceId, List<Guid> destinationIdList)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                foreach (var id in destinationIdList)
                {
                    var helpReationCollection = new HelpReationCollection() { Id = id };
                    if (!new HelpReationCollectionBO().Insert(this.ConnectionHandler, helpReationCollection))
                        throw new Exception("خطایی در ذخیره ارتباط بین راهنما وجود دارد");
                    var helpRelation = new HelpRelation { HelpId = sourceId, HelpRelationCollectionId = id };
                    if (!new HelpRelationBO().Insert(this.ConnectionHandler, helpRelation))
                        throw new Exception("خطایی در ذخیره ارتباط  بین راهنما وجود دارد");
                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public bool AddMenu(Guid helpId, List<Guid> menuList)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var helpMenuBo = new HelpMenuBO();
                foreach (var guid in menuList)
                {
                    var groupRole = helpMenuBo.Get(this.ConnectionHandler, helpId, guid);
                    if (groupRole == null)
                    {
                        var operationMenu = new HelpMenu { HelpId = helpId, MenuId = guid };
                        if (!helpMenuBo.Insert(this.ConnectionHandler, operationMenu))
                            throw new Exception("خطایی در ذخیره منوی راهنما  وجود دارد");
                    }
                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public IEnumerable<DataStructure.Help> GetHelpRelation(Guid sourceId)
        {
            try
            {
                var helplist = new List<Help.DataStructure.Help>();
                var list = new HelpRelationBO().Select(ConnectionHandler,x=>x.HelpRelationCollectionId, relation => relation.HelpId == sourceId);
                if (list.Any())
                    return new HelpBO().Where(ConnectionHandler, x => x.Id.In(list));
                return helplist;
               
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool DeleteHelpRealtion(Guid sourceId, Guid relationid)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var byFilter = new HelpRelationBO().Get(this.ConnectionHandler, sourceId, relationid);
                if (byFilter != null)
                    if (!new HelpRelationBO().Delete(this.ConnectionHandler, sourceId, relationid))
                        throw new Exception("خطایی در حذف ارتباط وجود دارد");
                if (!new HelpReationCollectionBO().Delete(this.ConnectionHandler, relationid))
                    throw new Exception("خطایی در حذف ارتباط وجود دارد");

                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<DataStructure.Help> Search(string txt)
        {
            try
            {
                return new HelpBO().Search(this.ConnectionHandler, txt);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


        public bool Insert(DataStructure.Help obj, HelpContent content)
        {


            //try
            //{
            //base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            return base.Insert(obj);
            //content.HelpId = obj.Id;
            //if (!new HelpContentBO().Insert(ConnectionHandler, content))
            //    throw new Exception("Error In Add Help Content");
            //base.ConnectionHandler.CommitTransaction();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    ConnectionHandler.Transaction.Rollback();
            //    throw new Exception(ex.Message);
            //}




        }


    }
}
