using System;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.BO
{
    internal class WebDesignFormsBO : BusinessBase<WebDesignForms>
    {
        public bool AssgineForm(IConnectionHandler connectionHandler, IConnectionHandler ContentManagerConnection, Guid WebId, FormStructure formStructure, string url, bool forUser)
        {
           
            var userFormsTransactionalFacade = new WebDesignUserFormsBO();
            var formAssigmentBo = new FormAssigmentBO();
            var formAssigments = formAssigmentBo.Where(connectionHandler,x => x.FormStructureId == formStructure.Id);
            var userform = userFormsTransactionalFacade.Get(connectionHandler, WebId, formStructure.Id);
            if (forUser)
            {
                if (userform == null)
                {
                    var userformdata = new WebDesignUserForms() { WebId = WebId, FormId = formStructure.Id };
                    if (!userFormsTransactionalFacade.Insert(connectionHandler, userformdata))
                        throw new Exception("خطایی در ویرایش فرم وجود دارد");
                }

            }
            else
            {
                if (userform != null)
                {
                    if (!userFormsTransactionalFacade.Delete(connectionHandler, userform))
                        throw new Exception("خطایی در ویرایش فرم وجود دارد");
                }
            }

            foreach (var formAssigment in formAssigments)
            {
                if (!string.IsNullOrEmpty(url) && url.Equals(formAssigment.Url.ToLower())) continue;
                if (!formAssigmentBo.Delete(connectionHandler,formAssigment.FormStructureId, formAssigment.Url))
                    throw new Exception("خطایی در ویرایش فرم وجود دارد");
            }
            if (!string.IsNullOrEmpty(url))
            {
                var assigments = formAssigmentBo.GetByUrl(connectionHandler,url);
                if (assigments == null)
                {
                    assigments = new FormAssigment { FormStructureId = formStructure.Id, Url = url };
                    if (!formAssigmentBo.Insert(connectionHandler,assigments))
                        throw new Exception("خطایی در ویرایش فرم وجود دارد");
                }
                else
                {
                    if (assigments.FormStructureId != formStructure.Id)
                    {
                        if (!formAssigmentBo.Delete(connectionHandler,assigments.FormStructureId, assigments.Url))
                            throw new Exception("خطایی در ویرایش فرم وجود دارد");
                        assigments = new FormAssigment { FormStructureId = formStructure.Id, Url = url };
                        if (!formAssigmentBo.Insert(connectionHandler,assigments))
                            throw new Exception("خطایی در ویرایش فرم وجود دارد");
                    }

                }
            }


            if (!new FormStructureBO().Update(connectionHandler, ContentManagerConnection,formStructure))
                throw new Exception("خطایی در ویرایش فرم وجود دارد");
            return true;
        }
    }
}
