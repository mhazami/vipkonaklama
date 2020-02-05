using System;
using System.Globalization;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.DA;
using Radyn.Statistics.Tools;

namespace Radyn.Statistics.BO
{
    internal class LogBO : BusinessBase<DataStructure.Log>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.Log obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref  id);
            obj.Id = id;
            obj.Date = DateTime.Now;
            return base.Insert(connectionHandler, obj);
        }

        public int GetDateSesstionCount(IConnectionHandler connectionHandler, string url, string date)
        {
            var da = new LogDA(connectionHandler);
            return da.GetDateSesstionCount(url, date);
        }

        
        public int GetSesstionCountBetweenDate(IConnectionHandler connectionHandler, string url, string minDate, string maxDate)
        {
            var da = new LogDA(connectionHandler);
            return da.GetSesstionCountBetweenDate(url, minDate, maxDate);
        }

        public int GetTotalSesstionCount(IConnectionHandler connectionHandler, string url)
        {
            var da = new LogDA(connectionHandler);
            return da.GetTotalSesstionCount(url);
        }
        public ModelView.StatisticsModel GetStatisticsModel(IConnectionHandler connectionHandler, string url)
        {
            try
            {
                var model = new ModelView.StatisticsModel();
                if (string.IsNullOrEmpty(url)) return model;
                var logBo = new LogBO();

                if (url.ToUpper().StartsWith("WWW"))
                {
                    var lenght = url.Split('.')[0].Length + 1;
                    url = url.Substring(lenght, url.Length - lenght);
                }

                if (url.ToUpper().StartsWith("HTTP"))
                {
                    var lenght = url.Split('/')[0].Length + 2;
                    url = url.Substring(lenght, url.Length - lenght);
                    if (url.ToUpper().StartsWith("WWW"))
                    {
                        var lenght1 = url.Split('.')[0].Length + 1;
                        url = url.Substring(lenght1, url.Length - lenght1);
                    }
                }

                model.TodaySesstionCount = logBo.GetDateSesstionCount(connectionHandler, url, FixData(DateTime.Now));
                model.YesterDaySesstionViewCount = logBo.GetDateSesstionCount(connectionHandler, url, FixData(DateTime.Now.AddDays(-1)));
                var last7Day = DateTime.Now.AddDays(-7);
                var now = DateTime.Now;
                model.Last7DaysSesstionViewCount = logBo.GetSesstionCountBetweenDate(connectionHandler, url, FixData(last7Day), FixData(now));
                var firstMonth = getShamsiStartMonthDate(DateTime.Now);
                var lastMonth = getShamsiEndMonthDate(DateTime.Now);
                model.CurrentMonthSessionViewCount = logBo.GetSesstionCountBetweenDate(connectionHandler, url, FixData(firstMonth), FixData(lastMonth));
                model.TotalSesstionCount = logBo.GetTotalSesstionCount(connectionHandler, url);
                return model;
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
        private DateTime getShamsiStartMonthDate(DateTime d)
        {
            var pc = new PersianCalendar();
            int shamsiYear = pc.GetYear(d);
            int shamsiMonth = pc.GetMonth(d);
            int shamsiDay = 1;

            return pc.ToDateTime(shamsiYear, shamsiMonth, shamsiDay, 0, 0, 0, 0);
        }

        private static string FixData(DateTime last7Day)
        {
            return last7Day.Year + "-" + last7Day.Month + "-" + last7Day.Day;
        }
        private DateTime getShamsiEndMonthDate(DateTime d)
        {
            var pc = new PersianCalendar();
            int shamsiYear = pc.GetYear(d);
            int shamsiMonth = pc.GetMonth(d);
            int shamsiDay = 0;

            if (shamsiMonth >= 1 && shamsiMonth <= 6)
            {
                shamsiDay = 31;
            }
            else if (shamsiMonth >= 7 && shamsiMonth <= 11)
            {
                shamsiDay = 30;
            }
            else
            {
                if (pc.IsLeapYear(shamsiYear))
                {
                    shamsiDay = 30;
                }
                else
                {
                    shamsiDay = 29;
                }
            }

            return pc.ToDateTime(shamsiYear, shamsiMonth, shamsiDay, 0, 0, 0, 0);
        }
    }
}
