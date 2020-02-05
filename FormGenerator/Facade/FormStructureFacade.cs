using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class FormStructureFacade : FormGeneratorBaseFacade<FormStructure>, IFormStructureFacade
    {

        internal FormStructureFacade()
        {
        }

        internal FormStructureFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public IEnumerable<Type> GetTypes()
        {
            try
            {
                var controlTypeModels = new List<Type>();
                var enumerable =
                    System.Reflection.Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(x => x.FullName.ToLower().Contains("radyn.formgenerator.controlfactory.controls"));
                foreach (var type in enumerable)
                {

                    controlTypeModels.Add(type);
                }
                return controlTypeModels;

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

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureBO().Delete(this.ConnectionHandler, this.FileManagerConnection, keys))
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

        public override bool Insert(FormStructure obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.ContentManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureBO().Insert(this.ConnectionHandler, this.ContentManagerConnection, obj))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

      

        public override bool Update(FormStructure obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.ContentManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new FormStructureBO().Update(this.ConnectionHandler, this.ContentManagerConnection, obj))
                    return false;
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

       

        public FormStructure GetByCulture(Guid id, string culture)
        {
            try
            {
                var formStructure = new FormStructureBO().GetByCulture(this.ConnectionHandler, id, culture);
             
                return formStructure;
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

       

        public IEnumerable<Type> GetValidatorTypes()
        {
            try
            {
                var controlTypeModels = new List<Type>();
                var enumerable =
                    System.Reflection.Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(x => x.FullName.ToLower().Contains("radyn.formgenerator.controlfactory.validator"));
                foreach (var type in enumerable)
                {

                    controlTypeModels.Add(type);
                }
                return controlTypeModels;
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
        public Dictionary<string, double> GetFormControlsWeightInform(FormStructure formStructure)
        {
            try
            {

                return new FormStructureBO().GetFormControlsWeightInform(ConnectionHandler, formStructure);
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
        public Dictionary<string, double> GetFormControlsWeight(FormStructure formStructure)
        {
            try
            {
               
                return new FormStructureBO().GetFormControlsWeight(ConnectionHandler,formStructure);
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
