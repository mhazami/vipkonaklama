namespace Radyn.Statistics.Tools
{
   public class ModelView
    {
       public class StatisticsModel
        {
           public int TodayViewCount { get; set; }
           public int TodaySesstionCount { get; set; }

           public int YesterDayViewCount { get; set; }
           public int YesterDaySesstionViewCount { get; set; }

           public int LastWeekViewCount { get; set; }
           public int LastWeekSesstionViewCount { get; set; }

            public int Last7DaysSesstionViewCount { get; set; }

            public int LastMonthViewCount { get; set; }
           public int LastMonthSesstionViewCount { get; set; }

            public int CurrentMonthSessionViewCount { get; set; }

            public int TotalSesstionCount { get; set; }
        }
   }
}
