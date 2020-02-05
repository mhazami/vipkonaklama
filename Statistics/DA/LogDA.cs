using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Statistics.DA
{
    public sealed class LogDA : DALBase<DataStructure.Log>
    {
        public LogDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public int GetDateSesstionCount(string url, string date)
        {
            var commandBuilder = new LogCommandBuilder();
            var query = commandBuilder.GetDateSesstionCount(url, date);
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }
        
        public int GetSesstionCountBetweenDate(string url, string minDate, string maxDate)
        {
            var commandBuilder = new LogCommandBuilder();
            var query = commandBuilder.GetSesstionCountBetweenDate(url, minDate,maxDate);
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }

     
        public int GetTotalSesstionCount(string url)
        {
            var commandBuilder = new LogCommandBuilder();
            var query = commandBuilder.GetTotalSesstionCount(url);
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }
       
       
    }
    internal class LogCommandBuilder
    {
        //بازدید کننده
        public string GetDateSesstionCount(string url, string date)
        {
            
            return
                string.Format("select count(distinct [SesstionId])  from [Statistics].[Log] where lower([Url])='{0}' and  CAST([Date] AS DATE)='{1}'", url.ToLower(), date);
        }
       
        //بازدید کننده
        public string GetSesstionCountBetweenDate(string url, string mindate, string maxdate)
        {
            return
                string.Format("select count(distinct [SesstionId])  from [Statistics].[Log] where lower([Url])='{0}' and CAST([Date] AS DATE) <='{1}' and CAST([Date] AS DATE) >='{2}'  ", url.ToLower(), maxdate, mindate);
        }
       
       
        //تعدا کل بازدید کننده
        public string GetTotalSesstionCount(string url)
        {
            return
              string.Format("select count(distinct [SesstionId])  from [Statistics].[Log] where lower([Url])='{0}' ", url.ToLower());
        }
    }
}
