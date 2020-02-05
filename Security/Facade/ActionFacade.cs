using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.Facade.Interface;
using Action = Radyn.Security.DataStructure.Action;

namespace Radyn.Security.Facade
{
    internal sealed class ActionFacade : SecurityBaseFacade<Action>, IActionFacade
    {
        internal ActionFacade()
        {
        }

        internal ActionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      

        public IEnumerable<Action> GetNotaddedInUser(Guid userId)
        {
            try
            {
                return new ActionBO().GetNotaddedInUser(this.ConnectionHandler, userId);
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

        public IEnumerable<Action> GetNotaddedInRole(Guid roleId)
        {
            try
            {
                return new ActionBO().GetNotaddedInRole(this.ConnectionHandler, roleId);
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
    }
}
