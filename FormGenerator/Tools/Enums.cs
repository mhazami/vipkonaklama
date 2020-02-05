using Radyn.Framework;

namespace Radyn.FormGenerator.Tools
{
    public enum TextMode : byte
    {
        [Description("SingleLine", Type = typeof(Resources.FormGenerator))]
        SingleLine = 1,
        [Description("Password", Type = typeof(Resources.FormGenerator))]
        Password = 2,
        [Description("MultiLine", Type = typeof(Resources.FormGenerator))]

        MultiLine = 3
    }
    public enum Align : byte
    {
        [Description("Right", Type = typeof(Resources.FormGenerator))]
        Right,
        [Description("Left", Type = typeof(Resources.FormGenerator))]
        Left
    }
    public enum FormState : byte
    {
        DesgineMode,
        DataEntryMode,
        DetailsMode,
        FirstLoadMode,
        ReportMode,
        ShowInGrid
    }
    public enum RepeatDirection : byte
    {
        [Description("Horizontal", Type = typeof(Resources.FormGenerator))]
        Horizontal,
        [Description("Vertical", Type = typeof(Resources.FormGenerator))]
        Vertical
    }
    public enum ControlType : byte
    {
        [Description("TextBox", Type = typeof(Resources.FormGenerator))]
        TextBox,
        [Description("DropDownList", Type = typeof(Resources.FormGenerator))]
        DropDownList,
        [Description("Label", Type = typeof(Resources.FormGenerator))]
        Label,
        [Description("CheckBox", Type = typeof(Resources.FormGenerator))]
        CheckBox,
        [Description("RadioButton", Type = typeof(Resources.FormGenerator))]
        RadioButton,
        [Description("FileUploader", Type = typeof(Resources.FormGenerator))]
        FileUploader,
        [Description("CheckBoxList", Type = typeof(Resources.FormGenerator))]
        CheckBoxList,
        [Description("RadioButtonList", Type = typeof(Resources.FormGenerator))]
        RadioButtonList,
        [Description("DateTimePicker", Type = typeof(Resources.FormGenerator))]
        DateTimePicker,
       
      
    }
    public enum ValidateType : byte
    {
        [Description("RequiredFieldValidator", Type = typeof(Resources.FormGenerator))]
        RequiredFieldValidator,
        [Description("EmailValidator", Type = typeof(Resources.FormGenerator))]
        EmailValidator,
        [Description("CompareValidator", Type = typeof(Resources.FormGenerator))]
        CompareValidator,

    }
    public enum ValidationDataType : byte
    {
        [Description("String", Type = typeof(Resources.FormGenerator))]
        String,
        [Description("Integer", Type = typeof(Resources.FormGenerator))]
        Integer,
        [Description("Double", Type = typeof(Resources.FormGenerator))]
        Double,
        [Description("Decimal", Type = typeof(Resources.FormGenerator))]
        Decimal,
        [Description("Date", Type = typeof(Resources.FormGenerator))]
        Date
    }
    public enum ValidationOperator : byte
    {
        [Description("Equal", Type = typeof(Resources.FormGenerator))]
        Equal,
        [Description("NotEqual", Type = typeof(Resources.FormGenerator))]
        NotEqual,
        [Description("GreaterThan", Type = typeof(Resources.FormGenerator))]
        GreaterThan,
        [Description("GreaterThanEqual", Type = typeof(Resources.FormGenerator))]
        GreaterThanEqual,
        [Description("LessThan", Type = typeof(Resources.FormGenerator))]
        LessThan,
        [Description("LessThanEqual", Type = typeof(Resources.FormGenerator))]
        LessThanEqual
    }
    public enum DatetypeEnum
    {
        [Description("Persian", Type = typeof(Resources.FormGenerator))]
        Persian,
        [Description("Gregorian", Type = typeof(Resources.FormGenerator))]
        Gregorian
    }



}
