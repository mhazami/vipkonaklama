using System;
using Radyn.Framework;

namespace Radyn.Graphic.DataStructure
{
    [Serializable]
    [Schema("Graphic")]
    public sealed class Theme : DataStructureBase<Theme>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }
        private string _title;
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }



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
            get { return this.Title; }
        }
    }
}
