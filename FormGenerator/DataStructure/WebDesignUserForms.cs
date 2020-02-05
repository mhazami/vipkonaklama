using System;
using Radyn.Framework;


namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed class WebDesignUserForms : DataStructureBase<WebDesignUserForms>
    {

        private Guid _webId;
        [DbType("uniqueidentifier")]
        [Key(false)]
        public Guid WebId
        {
            get
            {
                return this._webId;
            }
            set
            {
                base.SetPropertyValue("WebId", value);
                
            }
        }

    
      

        private Guid _formId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FormId
        {
            get
            {
                return this._formId;
            }
            set
            {
                base.SetPropertyValue("FormId", value);
                if (FormStructure == null)
                    this.FormStructure = new FormStructure { Id = value };
            }
        }

        [Assosiation(PropName = "FormId")]
        public FormStructure FormStructure { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.FormStructure.Name; }
        }
    }
}
