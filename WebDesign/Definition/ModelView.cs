using System;
using System.Collections.Generic;
using Radyn.Framework;

namespace Radyn.WebDesign.Definition
{
    public class ModelView
    {

        [Serializable]
        public class ModifyResult<T>
        {
            public ModifyResult()
            {
                InformList = new InFormEntitiyList<T>();
            }

            public bool Result { get; set; }
            public bool SendInform { get; set; }
            public InFormEntitiyList<T> InformList { get; set; }
            public T RefObject { get; set; }
            public Guid TransactionId { get; set; }

            public void AddInform(T obj, string emilnody, string smsbody)
            {
                if (obj == null) return;
                InformList.Add(obj, emilnody, smsbody);


            }
        }
        [Serializable]
        public class InFormEntitiyList<T> : List<InFormObject<T>>
        {

            public void Add(T obj, string emilnody, string smsbody)
            {
                if (obj == null) return;
                var inFormBody = new InFormObject<T> { obj = obj, SmsBody = smsbody, EmailBody = emilnody };
                this.Add(inFormBody);

            }
        }
        [Serializable]
        public class InFormObject<T>
        {
            public string EmailBody { get; set; }
            public string SmsBody { get; set; }
            public T obj { get; set; }
        }

        [Schema("ReportChartModel")]
        public class ReportChartModel
        {


            public string Value { get; set; }
            public long Count { get; set; }
            public string StringFormat { get; set; }
        }
    }
}
