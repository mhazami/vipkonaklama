using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.BO;
using Radyn.Statistics.Facade.Interface;
using Radyn.Statistics.Tools;

namespace Radyn.Statistics.Facade
{
    internal sealed class LogFacade : StatisticsBaseFacade<DataStructure.Log>, ILogFacade
    {
        internal LogFacade()
        {
        }

        internal LogFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }


        public ModelView.StatisticsModel GetStatisticsModel(string url)
        {
            try
            {
                var model = new ModelView.StatisticsModel();
                if (string.IsNullOrEmpty(url)) return model;
                var logBo = new LogBO();
                return logBo.GetStatisticsModel(ConnectionHandler, url);
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




        public bool InserNewLog(DataStructure.Log log)
        {
            try
            {
                return new LogBO().Insert(this.ConnectionHandler, log);
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
