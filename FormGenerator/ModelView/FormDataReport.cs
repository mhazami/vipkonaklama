using System;

namespace Radyn.FormGenerator.ModelView
{
    [Serializable]
    public class FormDataReport
    {
        public string Caption { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }

        public byte[] FileData { get; set; }
    }
}
