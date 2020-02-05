using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.FormGenerator.ModelView;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.Facade
{
    public sealed class FormEvaluationFacade : FormGeneratorBaseFacade<FormEvaluation>, IFormEvaluationFacade
    {

        internal FormEvaluationFacade()
        {
        }

        internal FormEvaluationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }
        public FormEvaluation GenerateEvaluation(FormEvaluation formEvaluation, bool withoutahp = false, bool newgenerate = false)
        {
            try
            {

                return new FormEvaluationBO().GeneratEvaluationHtml(ConnectionHandler, formEvaluation, withoutahp, newgenerate);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public FormEvaluation GetByCulture(string id, string culture)
        {
            try
            {

                return new FormEvaluationBO().GetByCulture(ConnectionHandler, id, culture);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

     

        public double GetControlWeight(object control, object value)
        {
            try
            {

                return new FormEvaluationBO().GetControlWeight(ConnectionHandler, control,value);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public double GetWeight(Dictionary<string, float> dictionary)
        {
            try
            {
                float val = dictionary.Aggregate<KeyValuePair<string, float>, float>(1, (current, f) => current * f.Value);
                float valpow = 1 / (float)dictionary.Count;
                float ebbvalue = (float)Math.Pow(val, valpow);
                return Math.Round(ebbvalue, 6);
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;

            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex.InnerException);
            }
        }

        public bool ModifyEvaluation(FormEvaluation formEvaluation, string culture)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                new FormEvaluationBO().ModifyEvaluation(ConnectionHandler, this.FileManagerConnection, formEvaluation, culture);
                ConnectionHandler.CommitTransaction();
                FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool ModifyEvaluation(List<FormEvaluation> formEvaluation,string culture)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                foreach (FormEvaluation formEvaluation1 in formEvaluation)
                {
                    new FormEvaluationBO().ModifyEvaluation(ConnectionHandler, this.FileManagerConnection,formEvaluation1, culture);

                }
                ConnectionHandler.CommitTransaction();
                FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public void SetFormControlsWeight(Controls controls)
        {
            try
            {

                new FormEvaluationBO().SetFormControlsWeight(this.ConnectionHandler, controls);


            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
