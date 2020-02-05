using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class SectionPosition : DataStructureBase<SectionPosition>
    {
        private Int32 _id;
        [Key(true)]
        [Unique]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _keyWord;
        [DbType("varchar(50)")]
        public string KeyWord
        {
            get { return _keyWord; }
            set { base.SetPropertyValue("KeyWord", value); }
        }

        private Int32 _sectionId;
        [DbType("int")]
        public Int32 SectionId
        {
            get
            {
                return this._sectionId;
            }
            set
            {
                base.SetPropertyValue("SectionId", value);
                if (AdvertisementSection == null)
                    this.AdvertisementSection = new Section { Id = value };
            }
        }

        [Assosiation(PropName = "SectionId")]
        public Section AdvertisementSection { get; set; }

        private Int32 _adsShowCount;
        [DbType("int")]
        public Int32 AdsShowCount
        {
            get { return _adsShowCount; }
            set { base.SetPropertyValue("AdsShowCount", value); }
        }

        private bool _orientation;
        [DbType("bit")]
        public bool Orientation
        {
            get { return _orientation; }
            set { base.SetPropertyValue("Orientation", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
