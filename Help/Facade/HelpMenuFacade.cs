using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.BO;
using Radyn.Help.DataStructure;
using Radyn.Help.Facade.Interface;
using Radyn.Security.DataStructure;

namespace Radyn.Help.Facade
{
    internal sealed class HelpMenuFacade : HelpFacade<HelpMenu>, IHelpMenuFacade
    {
        internal HelpMenuFacade() { }

        internal HelpMenuFacade(IConnectionHandler connectionHandler) 
            : base(connectionHandler){}

        public IEnumerable<Menu> SearchAddedInHelp(Guid helpId, string value)
        {
            try
            {
                var list = new HelpMenuBO().SearchAddedInHelp(this.ConnectionHandler, helpId, value);
                return list;
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

