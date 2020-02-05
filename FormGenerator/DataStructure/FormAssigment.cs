using System;
using Radyn.Framework;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed class FormAssigment : DataStructureBase<FormAssigment>
    {
        private Guid _formStructureId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FormStructureId
        {
            get
            {
                return this._formStructureId;
            }
            set
            {
                base.SetPropertyValue("FormStructureId", value);
                if (FormStructure == null)
                    this.FormStructure = new FormStructure { Id = value };
            }
        }

        [Assosiation(PropName = "FormStructureId")]
        //[Assosiation]
        public FormStructure FormStructure { get; set; }

        private string _url;
        [Key(false)]
        [DbType("nvarchar(250)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }
        private int _order;
        [DbType("int")]
        public int Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.FormStructure.Name; }
        }
    }
}
