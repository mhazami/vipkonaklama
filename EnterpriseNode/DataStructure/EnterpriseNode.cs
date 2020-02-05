using System;
using System.Collections.Generic;
using Radyn.Common.DataStructure;
using Radyn.EnterpriseNode.Tools;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.DataStructure
{
    [Serializable]
    [Schema("EnterpriseNode")]
    public sealed class EnterpriseNode : DataStructureBase<EnterpriseNode>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Enums.EnterpriseNodeType _enterpriseNodeTypeId;
        [DbType("int")]
        public Enums.EnterpriseNodeType EnterpriseNodeTypeId
        {
            get
            {
                return this._enterpriseNodeTypeId;
            }
            set
            {
                base.SetPropertyValue("EnterpriseNodeTypeId", value);
                
            }
        }

       

        private string _email;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string Email
        {
            get { return _email; }
            set { base.SetPropertyValue("Email", value); }
        }



        private string _tel;
        [IsNullable]
        [DbType("varchar(50)")]
        public string Tel
        {
            get { return _tel; }
            set { base.SetPropertyValue("Tel", value); }
        }

        private string _cellphone;
        [IsNullable]
        [DbType("varchar(50)")]
        public string Cellphone
        {
            get { return _cellphone; }
            set { base.SetPropertyValue("Cellphone", value); }
        }

        private string _address;
        [IsNullable]
        [MultiLanguage]
        [DbType("nvarchar(500)")]
        public string Address
        {
            get { return _address; }
            set { base.SetPropertyValue("Address", value); }
        }

        private string _website;
        [Layout(Caption = "وب سایت")]
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string Website
        {
            get { return _website; }
            set { base.SetPropertyValue("Website", value); }
        }

        private Guid? _enterpriseNodeParentId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? EnterpriseNodeParentId
        {
            get
            {
                return this._enterpriseNodeParentId;
            }
            set
            {
                base.SetPropertyValue("EnterpriseNodeParentId", value);
                if (ParentEnterpriseNode == null)
                    this.ParentEnterpriseNode = value.HasValue ? new EnterpriseNode { Id = value.Value } : null;
            }
        }

        [Assosiation]
        public EnterpriseNode ParentEnterpriseNode { get; set; }


        private int? _cityId;

        [IsNullable]
        [DbType("int")]
        [Layout(Caption = "شهر")]
        public int? CityId
        {
            get { return _cityId; }
            set
            {
                this._cityId = value;
                if (this.City == null)
                    this.City = value.HasValue ? new City { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "CityId")]
        public City City { get; set; }

        private string _fax;
        [Layout(Caption = "فکس")]
        [IsNullable]
        [DbType("varchar(50)")]
        public string Fax
        {
            get { return _fax; }
            set { base.SetPropertyValue("Fax", value); }
        }


        private int? _provinceId;

        [IsNullable]
        [DbType("int")]
        [Layout(Caption = "استان")]
        public int? ProvinceId
        {
            get { return _provinceId; }
            set
            {
                this._provinceId = value;
                if (Province == null)
                    this.Province = value.HasValue ? new Province() { Id = value.Value } : null;
            }
        }
        [Assosiation(PropName = "ProvinceId")]
        public Province Province { get; set; }

        private byte? _prefixTitleId;
        [IsNullable]
        [DbType("tinyint")]
        public byte? PrefixTitleId
        {
            get { return _prefixTitleId; }
            set
            {
                base.SetPropertyValue("PrefixTitleId", value);
                if (PrefixTitle == null)
                    this.PrefixTitle = value.HasValue ? new PrefixTitle { Id = value.Value } : null;
            }
        }
        [Assosiation]
        public PrefixTitle PrefixTitle { get; set; }

        private Guid? _pictureId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? PictureId
        {
            get
            {
                return this._pictureId;
            }
            set
            {
                base.SetPropertyValue("PictureId", value);
                if (Picture == null)
                    this.Picture = value.HasValue ? new File { Id = value.Value } : null;
            }
        }

        [Assosiation]
        public File Picture { get; set; }


        private LegalEnterpriseNode _legalEnterpriseNode;


        [Assosiation(PropName = "Id")]

        public LegalEnterpriseNode LegalEnterpriseNode
        {
            get { return _legalEnterpriseNode; }
            set
            {

                _legalEnterpriseNode = value;
            }
        }

        private RealEnterpriseNode _realEnterpriseNode;

        [Assosiation(PropName = "Id")]
        public RealEnterpriseNode RealEnterpriseNode
        {
            get { return _realEnterpriseNode; }
            set
            {

                _realEnterpriseNode = value;
            }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public IEnumerable<EnterpriseNode> ChildEnterpriseNode { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get
            {
                var result = string.Empty;
                if (this.LegalEnterpriseNode != null)
                {
                    result = this.LegalEnterpriseNode.Title;
                }
                if (this.RealEnterpriseNode != null)
                {
                    result = string.Format("{0} {1}", this.RealEnterpriseNode.FirstName, this.RealEnterpriseNode.LastName);
                }
                return result;
            }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string DescriptionFieldWithGender
        {
            get
            {
                var result = string.Empty;
                if (this.PrefixTitleId.HasValue)
                    result += PrefixTitle.Title + " ";
                if (this.LegalEnterpriseNode != null)
                    result += this.LegalEnterpriseNode.Title;
                if (this.RealEnterpriseNode != null)
                    result += this.RealEnterpriseNode.FirstName + " " + this.RealEnterpriseNode.LastName;
                return result;
            }
        }
    }
}
