using Radyn.FormGenerator.Facade;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator
{
    public class FormGeneratorComponent
    {
        internal FormGeneratorComponent()
        {

        }

        private static FormGeneratorComponent _instance;
        public static FormGeneratorComponent Instance
        {
            get { return _instance ?? (_instance = new FormGeneratorComponent()); }
        }
        
        private IFormDataFacade _formDataFacade;
        public IFormDataFacade FormDataFacade
        {
            get
            {
                return this._formDataFacade ?? (this._formDataFacade = new FormDataFacade());
            }
        }
        
        private IFormStructureFacade _formStructureFacade;
        public IFormStructureFacade FormStructureFacade
        {
            get
            {
                return this._formStructureFacade ?? (this._formStructureFacade = new FormStructureFacade());
            }
        }

        private IFormEvaluationFacade _formEvaluationFacade;
        public IFormEvaluationFacade FormEvaluationFacade
        {
            get
            {
                return this._formEvaluationFacade ?? (this._formEvaluationFacade = new FormEvaluationFacade());
            }
        }

        private IFormStructureDesgineFacade _formStructureDesgineFacade;
        public IFormStructureDesgineFacade FormStructureDesgineFacade
        {
            get
            {
                return this._formStructureDesgineFacade ?? (this._formStructureDesgineFacade = new FormStructureDesgineFacade());
            }
        }

        private IFormAssigmentFacade _formAssigmentFacade;
        public IFormAssigmentFacade FormAssigmentFacade
        {
            get
            {
                return this._formAssigmentFacade ?? (this._formAssigmentFacade = new FormAssigmentFacade());
            }
        }
        private IGeneratorFacade _generatorFacade;
        public IGeneratorFacade GeneratorFacade
        {
            get
            {
                return this._generatorFacade ?? (this._generatorFacade = new GeneratorFacade());
            }
        }
        public IFormStructureFacade FormStructureTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FormStructureFacade(connectionHandler);
        }
        public IFormDataFacade FormDataTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FormDataFacade(connectionHandler);
        }

        public IFormAssigmentFacade FormAssigmentTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FormAssigmentFacade(connectionHandler);
        }
        public IFormEvaluationFacade FormEvaluationTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FormEvaluationFacade(connectionHandler);
        }
        public IWebDesignFormsFacade WebDesignFormsFacade
        {
            get { return new WebDesignFormsFacade(); }
        }





        public IWebDesignUserFormsFacade WebDesignUserFormsFacade
        {
            get { return new WebDesignUserFormsFacade(); }
        }

    }
}
