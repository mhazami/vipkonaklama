using System.Collections.Generic;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    public class GridRow
    {
        public GridRow()
        {
            Cells = new List<Control>();
        }
        public List<Control> Cells { get; set; }
        public string RowKey { get; set; }
        public int RowIndex { get; set; }

    }
}
