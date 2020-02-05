using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("TabularControl"), DataContract(Name = "TabularControl")]
    public class TabularControl : Control
    {
        public TabularControl()
        {
            Rows = new List<GridRow>();
        }
        public List<GridRow> Rows { get; set; }




    }
}
