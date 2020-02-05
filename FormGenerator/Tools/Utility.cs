using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Radyn.FormGenerator.ControlFactory.Controls;

namespace Radyn.FormGenerator.Tools
{
   
    public static  class Utility
    {
       
        public static List<Type> GetControlTypes()
        {
            try
            {
                return  Assembly.GetExecutingAssembly().GetTypes()
                        .Where(x => x.FullName.ToLower().StartsWith("radyn.contracts.datastructure.formgenerator.controlfactory.controls")).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public static List<Type> GetAllFormGeneratorTypes()
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetTypes()
                        .Where(x => x.FullName.ToLower().StartsWith("radyn.contracts.datastructure.formgenerator.controlfactory")).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public static List<Type> GetValidatorTypes()
        {
            try
            {
                return
                     Assembly.GetExecutingAssembly().GetTypes()
                         .Where(x => x.FullName.ToLower().Contains("radyn.contracts.datastructure.formgenerator.controlfactory.validator")).ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static Type[] KnownTypes
        {
            get { return  new[]{typeof(TextBox),typeof(DropDownList),typeof(Label),typeof(Grid),typeof(CheckBoxList),typeof(RadioButtonList),typeof(DateTimePicker),typeof(FileUploader),typeof(RadioButton),typeof(CheckBox)};}
        }

        
      
    }
}
