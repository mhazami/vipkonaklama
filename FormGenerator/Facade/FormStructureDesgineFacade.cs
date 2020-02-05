using System;
using System.Collections.Generic;
using System.Data;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.FormGenerator.ModelView;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class FormStructureDesgineFacade
        : FormGeneratorBaseFacade<FormStructure>, IFormStructureDesgineFacade
    {

        internal FormStructureDesgineFacade()
        {
        }

        internal FormStructureDesgineFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      

        public bool UpdateControl(Guid formId,string culture, string Id, object obj,  string validation, string items)
        {
            try
            {
              
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().UpdateControl(this.ConnectionHandler, this.FileManagerConnection, formId, culture, Id, obj, validation, items))
                    return false;
               
                this.FileManagerConnection.CommitTransaction();
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
               
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
               
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            
           

        }
        public bool GenrateNewControl(Guid formId, string culture, object obj,  int? order)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().GenrateNewControl(this.ConnectionHandler, this.FileManagerConnection, formId, culture, obj, order))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
           
        }
        public bool SwapControl(Guid formId, string culture, string Id, string type)
        {

            try
            {
                this.CommonConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().SwapControl(this.CommonConnection,this.FileManagerConnection, formId, culture, Id, type))
                    return false;
                this.CommonConnection.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
           
          
        }
        public bool CustomeSwap(Guid formId, string culture, string Id,  int getorder)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().CustomeSwap(this.ConnectionHandler, this.FileManagerConnection, formId, culture, Id, getorder))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
           
            
        }
        public bool DeleteControl(Guid formId, string culture, string Id)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().DeleteControl(this.ConnectionHandler, this.FileManagerConnection, formId, culture, Id))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public  void SetValidatorIdAndName(Controls list, object obj, Guid formId)
        {
            try
            {

                new FormStructureDesgineBO().SetValidatorIdAndName(list, obj, formId);

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
        public void SetValidator(string value, Control control, Controls controls, Guid formId)
        {
            try
            {

                new FormStructureDesgineBO().SetValidator(value,control,controls,formId);

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
        public void FillListItems(string value, object obj)
        {
            try
            {
                new FormStructureDesgineBO().FillListItems(value, obj);

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

        public Dictionary<Object, string> GenerateFormControlWithHtml(Guid formId, string culture)
        {
            try
            {

                return new FormStructureDesgineBO().GenerateFormControlWithHtml(this.ConnectionHandler, formId, culture);

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

        public bool ModifyStructure(FormStructure obj, string culture)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureDesgineBO().ModifyStructure(this.ConnectionHandler, this.FileManagerConnection, obj, culture))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        
    }
}
