using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("ListControl"), DataContract(Name = "ListControl")]
    public  class ListControl : Control
    {
        public ListControl()
        {
            Items=new List<ListItem>();
          
        }
        [XmlElement("Items"), DataMember(Name = "Items")]
        public List<ListItem> Items { get; set; }
        [XmlElement("DataSource"), DataMember(Name = "DataSource")]
        public IEnumerable<object> DataSource { get; set; }
        [XmlElement("TextField"), DataMember(Name = "TextField")]
        public string TextField { get; set; }
        [XmlElement("ValueField"), DataMember(Name = "ValueField")]
        public string ValueField { get; set; }
        [XmlElement("IsMultiSelect"), DataMember(Name = "IsMultiSelect")]
        public bool IsMultiSelect { get; set; }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }
        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }


        public List<ListItem> EvaluationItems
        {
            get
            {
                foreach (var listItem in Items)
                    listItem.GetEvaluationItemId = this.Id + "-" + listItem.Value;
                return Items;
            }
        }
        public virtual void DataBind()
        {
        }

        public override void Generate()
        {
            this.DisplayValueType = typeof(string);
            base.Generate();
            
        }
    }
}
