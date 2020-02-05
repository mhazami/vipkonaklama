using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.Tools;

namespace Radyn.FormGenerator.ControlFactory.Validator
{
    [Serializable, XmlRoot("Compare"), DataContract(Name = "Compare")]
    public class CompareValidator:ValidatorControl
    {
        public CompareValidator()
        {
            this.Type = this.GetType().Name;
        }

        [XmlElement("DataType"), DataMember(Name = "DataType")]
        public byte DataType { get; set; }


        [XmlElement("Comparevalue"), DataMember(Name = "Comparevalue")]
        public string Comparevalue { get; set; }

        [XmlElement("Operator"), DataMember(Name = "Operator")]
        public byte Operator { get; set; }

        public override void Generate()
        {
            base.Generate();
            this.SetValidatorBaseAttributeValues();
            this.Writer.AddAttribute("DataType", this.DataType.ToString());
            this.Writer.AddAttribute("Operator", this.Operator.ToString());
            this.Writer.AddAttribute("Comparevalue", this.Comparevalue);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());
            this.Writer.RenderEndTag();


        }

        public override KeyValuePair<bool, string> ValidateControl(Dictionary<string, object> keyValuePair)
        {
            try
            {
                if (!keyValuePair.ContainsKey(this.ControlToValidate) || keyValuePair[this.ControlToValidate] == null ||
                Equals(keyValuePair[this.ControlToValidate], string.Empty))
                    return new KeyValuePair<bool, string>(true, string.Empty);
                var value = keyValuePair[this.ControlToValidate];
                string propValue1 = (string) value;
                string propValue2 = Comparevalue;
                bool outresult = false;
                switch ((ValidationDataType)DataType)
                {
                    case ValidationDataType.Integer:

                        int ival1 = int.Parse(propValue1);
                        int ival2 = int.Parse(propValue2);

                        switch ((ValidationOperator)Operator)
                        {
                            case ValidationOperator.Equal:outresult = ival1 == ival2;break;
                            case ValidationOperator.NotEqual: outresult = ival1 != ival2; break;
                            case ValidationOperator.GreaterThan: outresult = ival1 > ival2; break;
                            case ValidationOperator.GreaterThanEqual: outresult = ival1 >= ival2; break;
                            case ValidationOperator.LessThan: outresult = ival1 < ival2; break;
                            case ValidationOperator.LessThanEqual: outresult = ival1 <= ival2; break;
                        }
                        break;

                    case ValidationDataType.Double:

                        double dval1 = double.Parse(propValue1);
                        double dval2 = double.Parse(propValue2);

                        switch ((ValidationOperator)Operator)
                        {
                            case ValidationOperator.Equal: outresult = dval1 == dval2; break;
                            case ValidationOperator.NotEqual: outresult = dval1 != dval2; break;
                            case ValidationOperator.GreaterThan: outresult = dval1 > dval2; break;
                            case ValidationOperator.GreaterThanEqual: outresult = dval1 >= dval2; break;
                            case ValidationOperator.LessThan: outresult = dval1 < dval2; break;
                            case ValidationOperator.LessThanEqual: outresult = dval1 <= dval2; break;
                        }
                        break;

                    case ValidationDataType.Decimal:

                        decimal cval1 = decimal.Parse(propValue1);
                        decimal cval2 = decimal.Parse(propValue2);

                        switch ((ValidationOperator)Operator)
                        {
                            case ValidationOperator.Equal: outresult = cval1 == cval2; break;
                            case ValidationOperator.NotEqual: outresult = cval1 != cval2; break;
                            case ValidationOperator.GreaterThan: outresult = cval1 > cval2; break;
                            case ValidationOperator.GreaterThanEqual: outresult = cval1 >= cval2; break;
                            case ValidationOperator.LessThan: outresult = cval1 < cval2; break;
                            case ValidationOperator.LessThanEqual: outresult = cval1 <= cval2; break;
                        }
                        break;

                    case ValidationDataType.Date:

                        DateTime tval1 = DateTime.Parse(propValue1);
                        DateTime tval2 = DateTime.Parse(propValue2);

                        switch ((ValidationOperator)Operator)
                        {
                            case ValidationOperator.Equal: outresult = tval1 == tval2; break;
                            case ValidationOperator.NotEqual: outresult = tval1 != tval2; break;
                            case ValidationOperator.GreaterThan: outresult = tval1 > tval2; break;
                            case ValidationOperator.GreaterThanEqual: outresult = tval1 >= tval2; break;
                            case ValidationOperator.LessThan: outresult = tval1 < tval2; break;
                            case ValidationOperator.LessThanEqual: outresult = tval1 <= tval2; break;
                        }
                        break;

                    case ValidationDataType.String:

                        int result = string.Compare(propValue1, propValue2, StringComparison.CurrentCulture);

                        switch ((ValidationOperator)Operator)
                        {
                            case ValidationOperator.Equal: outresult = result == 0; break;
                            case ValidationOperator.NotEqual: outresult = result != 0; break;
                            case ValidationOperator.GreaterThan: outresult = result > 0; break;
                            case ValidationOperator.GreaterThanEqual: outresult = result >= 0; break;
                            case ValidationOperator.LessThan: outresult = result < 0; break;
                            case ValidationOperator.LessThanEqual: outresult = result <= 0; break;
                        }
                        break;

                }
                return new KeyValuePair<bool, string>(outresult, outresult ? string.Empty : this.ErrorMessage); 
            }
            catch { return new KeyValuePair<bool, string>() ; }
           
        }
    }
}
