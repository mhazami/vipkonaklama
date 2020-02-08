using System;
using Radyn.Common.DataStructure;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class Hotel : DataStructureBase<Hotel>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _name;
        [DbType("nvarchar(150)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        private Int32 _countryId;
        [DbType("int")]
        public Int32 CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                base.SetPropertyValue("CountryId", value);
                this.Country = new Country { Id = value };
            }
        }
        [Assosiation(PropName = "CountryId")]
        public Country Country { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private Guid? _picId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? PicId
        {
            get { return _picId; }
            set { base.SetPropertyValue("PicId", value); }
        }
        [Assosiation]
        public File Picture { get; set; }


        private Guid? _galleryId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? GalleryId
        {
            get { return _galleryId; }
            set { base.SetPropertyValue("GalleryId", value); }
        }
        [Assosiation(PropName = "GalleryId")]
        public Gallery.DataStructure.Gallery Gallery { get; set; }

        private string _address;
        [IsNullable]
        [DbType("nvarchar(300)")]
        public string Address
        {
            get { return _address; }
            set { base.SetPropertyValue("Address", value); }
        }

        private string _location;
        [IsNullable]
        [DbType("nvarchar(2000)")]
        public string Location
        {
            get { return _location; }
            set { base.SetPropertyValue("Location", value); }
        }

        private Int32? _provinceId;
        [IsNullable]
        [DbType("int")]
        public Int32? ProvinceId
        {
            get
            {
                return this._provinceId;
            }
            set
            {
                base.SetPropertyValue("ProvinceId", value);
                if (value.HasValue)
                    this.Province = new Province { Id = value.Value };
                else
                    this.Province = null;
            }
        }
        [Assosiation(PropName = "ProvinceId")]
        public Province Province { get; set; }

        private Int32? _cityId;
        [IsNullable]
        [DbType("int")]
        public Int32? CityId
        {
            get
            {
                return this._cityId;
            }
            set
            {
                base.SetPropertyValue("CityId", value);
                if (value.HasValue)
                    this.City = new City { Id = value.Value };
                else
                    this.City = null;
            }
        }
        [Assosiation(PropName = "CityId")]
        public City City { get; set; }

        private Guid? _parishId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParishId
        {
            get
            {
                return this._parishId;
            }
            set
            {
                base.SetPropertyValue("ParishId", value);
                if (value.HasValue)
                    this.Parish = new Parish { Id = value.Value };
                else
                    this.Parish = null;
            }
        }
        [Assosiation(PropName = "ParishId")]
        public Parish Parish { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }

}