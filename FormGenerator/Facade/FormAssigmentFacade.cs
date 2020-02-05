using System;
using System.Collections.Generic;
using System.Data;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class FormAssigmentFacade : FormGeneratorBaseFacade<FormAssigment>, IFormAssigmentFacade
    {
        internal FormAssigmentFacade()
        {
        }

        internal FormAssigmentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

    

        public bool Insert(Guid formId, List<FormAssigment> list)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var formAssigmentBo = new FormAssigmentBO();
                foreach (var dataModel in list)
                {
                    var formAssigment = formAssigmentBo.Get(this.ConnectionHandler, formId, dataModel.Url.ToLower());
                    if (formAssigment == null)
                    {
                        dataModel.FormStructureId = formId;
                        var byFilter = formAssigmentBo.Any(this.ConnectionHandler,
                            x => x.Url == dataModel.Url.ToLower());
                        if (!byFilter)
                        {
                            if (!formAssigmentBo.Insert(this.ConnectionHandler, dataModel))
                                throw new Exception("خطایی در ذخیره فرم وجود دارد");
                        }
                    }
                    else
                    {
                        if (!formAssigmentBo.Update(this.ConnectionHandler, dataModel))
                            throw new Exception("خطایی در ذخیره فرم وجود دارد");
                    }

                }
                this.ConnectionHandler.CommitTransaction();
                return true;

            }

            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }



        public FormAssigment GetByUrl(string url)
        {
            try
            {
                return new FormAssigmentBO().GetByUrl(this.ConnectionHandler, url);
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


    }
}
