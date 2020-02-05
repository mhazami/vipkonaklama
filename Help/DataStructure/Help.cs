using System;
using Radyn.Framework;

namespace Radyn.Help.DataStructure
{
    [Serializable]
    [Schema("Help")]
    public sealed class Help : DataStructureBase<Help>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        //private string _defaultTitle;
        //[DbType("nvarchar(500)")]
        public string DefaultTitle { get; set;
            //get { return _defaultTitle; }
            //set { base.SetPropertyValue("DefaultTitle", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        //private string _defaultConent;
        //[DbType("ntext")]
        public string DefaultConent
        {
            //get { return _defaultConent; }
            //set { base.SetPropertyValue("DefaultConent", value); }
            get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.DefaultTitle; }
        }
    }
}
