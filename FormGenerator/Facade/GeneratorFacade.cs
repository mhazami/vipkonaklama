using System;
using System.Collections.Generic;
using System.Web;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;


namespace Radyn.FormGenerator.Facade
{
    internal sealed class GeneratorFacade : IGeneratorFacade
    {

        public FormStructure GenerateForm(FormStructure formStructure)
        {
            try
            {
                return new GeneratorBO().GenerateForm(formStructure);
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

        public FormStructure GenerateForm(Guid formId, FormState formState, string culture)
        {
            try
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                form.FormState = formState;
                return new GeneratorBO().GenerateForm(form, culture);

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

        public FormStructure GenerateForm(string url, FormState formState, string culture)
        {
            try
            {
                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (byUrl == null) return null;
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(byUrl.FormStructureId,
                    culture);
                form.FormState = formState;
                return new GeneratorBO().GenerateForm(form, culture);

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

        public FormStructure GenerateForm(Guid formId, string objactname, string refId, FormState formState,
            string culture, Dictionary<string, Object> dictionary)
        {
            try
            {
                var byCulture = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (byCulture == null) return null;
                byCulture.ObjectName = objactname;
                byCulture.GetFormControl = dictionary;
                byCulture.FormState = formState;
                byCulture.RefId = refId;
                return new GeneratorBO().GenerateForm(byCulture, culture);
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


        public FormStructure GenerateForm(string url, string objactname, string refId, FormState formState,
            string culture, Dictionary<string, Object> dictionary)
        {

            try
            {
                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (byUrl == null) return null;
                var byCulture = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(byUrl.FormStructureId,
                    culture);
                if (byCulture == null) return null;
                byCulture.RefId = refId;
                byCulture.ObjectName = objactname;
                byCulture.GetFormControl = dictionary;
                byCulture.FormState = formState;
                return new GeneratorBO().GenerateForm(byCulture, culture);
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

        public FormStructure ViewFormData(string refId, string objactname, string culture)
        {
            try
            {
                var formData = FormGeneratorComponent.Instance.FormDataFacade.GetFormData(refId, objactname, culture);
                if (formData == null) return null;
                var byCulture = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formData.StructureId,
                    culture);
                if (byCulture == null) return null;
                byCulture.RefId = refId;
                byCulture.ObjectName = string.IsNullOrEmpty(objactname)
                    ? new FormStructure().GetType().Name
                    : objactname;
                byCulture.FormState = FormState.DetailsMode;
                return new GeneratorBO().GenerateForm(byCulture, culture);
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

        public FormStructure GetUserFormReport(FormStructure form,string culture)
        {
            return new GeneratorBO().GenerateForm(form, culture);
        }

      
    }
}
