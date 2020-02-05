using System;
using System.Collections.Generic;
using Radyn.Common;
using Radyn.Common.Component;
using Radyn.ContentManager;
using Radyn.FileManager;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;

namespace Radyn.FormGenerator.BO
{
    public class FormStructureBO : BusinessBase<FormStructure>
    {

        public override bool Insert(IConnectionHandler connectionHandler, FormStructure structure)
        {

            structure.CreateDate = DateTime.Now;
            var id = structure.Id;
            BOUtility.GetGuidForId(ref id);
            structure.Id = id;
            return base.Insert(connectionHandler, structure);
        }
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler contentmanagerconnection, FormStructure structure)
        {

            if (!this.Insert(connectionHandler, structure))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            if (!SetMenu(connectionHandler, contentmanagerconnection, ref structure))
                throw new Exception("خطایی در ویرایش منوی فرم وجود دارد");
            return true;
        }

        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler contentmanagerconnection, FormStructure structure)
        {
            if (!this.Update(connectionHandler, structure))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            if (!SetMenu(connectionHandler, contentmanagerconnection, ref structure))
                throw new Exception("خطایی در ویرایش منوی فرم وجود دارد");
            return true;

        }



        private bool SetMenu(IConnectionHandler connectionHandler, IConnectionHandler contentmanagerconnection, ref FormStructure structure)
        {
            structure.Link = "/FormGenerator/UI/View/" + structure.Id;
            if (structure.MenuId != null)
            {
                var menu = ContentManagerComponent.Instance.MenuTransactionalFacade(contentmanagerconnection).Get(structure.MenuId);
                menu.Link = structure.Link;
                if (!ContentManagerComponent.Instance.MenuTransactionalFacade(contentmanagerconnection).Update(menu))
                    throw new Exception("خطایی در ویرایش منوی فرم وجود دارد");
            }
            if (!this.Update(connectionHandler, structure))
                throw new Exception("خطایی در ویرایش  فرم وجود دارد");
            return true;

        }

        public override FormStructure Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            var structure = base.Get(connectionHandler, keys);
            if (structure == null) return null;
            if (!string.IsNullOrEmpty(structure.StructureFileId))
            {
                var file = FileManagerComponent.Instance.FileFacade.Get(structure.StructureFileId.ToGuid());
                if (file != null)
                    structure.Structure = StringUtils.Unzip(file.Content);

            }
            DeSerializeForm(ref structure);
            return structure;
        }

        public void DeSerializeForm(ref FormStructure structure)
        {
            structure.GetFormControl = new Dictionary<string, object>();
            structure.Controls = Extentions.DeSerialize(structure.Structure);
        }

        public FormStructure GetByCulture(IConnectionHandler connectionHandler, Guid id, string culture)
        {
            var structure = base.GetLanuageContent(connectionHandler, culture, id);
            if (structure == null) return null;
            structure.Controls = new Controls();
            if (string.IsNullOrEmpty(structure.StructureFileId))

                structure.Controls = Extentions.DeSerialize(structure.Structure);
            else
            {
                var file = FileManagerComponent.Instance.FileFacade.Get(structure.StructureFileId);
                if (file != null)
                    structure.Controls = Extentions.DeSerialize(StringUtils.Unzip(file.Content));
            }

            return structure;
        }

        public bool Delete(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, params object[] keys)
        {

            var obj = this.Get(connectionHandler, keys);
            var objectkeyskey = obj.GetCultureKeys();
            var formDataBo = new FormDataBO();

            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnectionHandler);
            var byFilter = formDataBo.Select(connectionHandler, x => x.Id, x => x.StructureId == obj.Id);
            foreach (var formData in byFilter)
            {
                if (!formDataBo.Delete(connectionHandler, formData))
                    throw new Exception("خطایی در حذف فرم وجود دارد");
            }
            var formAssigmentBo = new FormAssigmentBO();
            var formAssigments = formAssigmentBo.Select(connectionHandler, x => x.Url, x => x.FormStructureId == obj.Id);
            foreach (var formAssigment in formAssigments)
            {
                if (!formAssigmentBo.Delete(connectionHandler, obj.Id, formAssigment))
                    throw new Exception("خطایی در حذف فرم وجود دارد");
            }
            var languageContentFacade = CommonComponent.Instance.LanguageContentFacade;
            var lan = CommonComponent.Instance.LanguageFacade.GetValidList();
            foreach (var language in lan)
            {
                var content = languageContentFacade.Get(objectkeyskey["StructureFileId"], language);
                if (content == null || string.IsNullOrWhiteSpace(content.Value) == true) continue;
                if (!fileTransactionalFacade.Delete(content.Value.ToGuid()))
                    throw new Exception("خطایی در حذف فرم وجود دارد");
            }
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف فرم وجود دارد");

            return true;
        }


        public Dictionary<string, double> GetFormControlsWeightInform(IConnectionHandler connectionHandler, FormStructure formStructure)
        {
            var dictionary = new Dictionary<string, double>();
            foreach (var control in formStructure.Controls)
            {
                var weightInForm = ((Control)control).WeightInForm;
                if (weightInForm == null) dictionary.Add(((Control)control).Id, 0);
                else dictionary.Add(((Control)control).Id, (double)weightInForm);
            }
            return dictionary;
        }
        public Dictionary<string, double> GetFormControlsWeight(IConnectionHandler connectionHandler, FormStructure formStructure)
        {
            var dictionary = new Dictionary<string, double>();
            foreach (var control in formStructure.Controls)
            {
                var id = ((Control)control).Id;
                var formEvaluation = new FormEvaluationBO().Get(connectionHandler, id);
                if (formEvaluation == null) dictionary.Add(((Control)control).Id, 0);
                else dictionary.Add(((Control)control).Id, (formEvaluation.Weight ?? 0));
            }
            return dictionary;
        }
    }
}
