using System;
using Radyn.Framework;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed class WebDesignForms : DataStructureBase<WebDesignForms>
    {
        private Guid _webId;
        [Key(false)]
        [DbType("uniqueidentifier")]
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
                this.WebSiteForm = new Radyn.FormGenerator.DataStructure.FormStructure { Id = value };
            }
        }

        [Assosiation(PropName = "FormId")]
        public Radyn.FormGenerator.DataStructure.FormStructure WebSiteForm { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteForm.Name; }
        }
    }
}
