using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class Country : DataStructureBase<Country>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _name;
        [DbType("nvarchar(200)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        private string _languageId;
        [DbType("char(5)")]
        public string LanguageId
        {
            get { return _languageId; }
            set
            {
                base.SetPropertyValue("LanguageId", value);
                if (Language == null)
                    this.Language = new Language { Id = value };
            }
        }

        [Assosiation(PropName = "LanguageId")]
        //[Assosiation]
        public Language Language { get; set; }

        private Guid? _image;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? Image
        {
            get { return _image; }
            set { base.SetPropertyValue("Image", value); }
        }


        private string _phoneCode;
        [DbType("char(5)")]
        [IsNullable]
        public string PhoneCode
        {
            get { return _phoneCode; }
            set { base.SetPropertyValue("PhoneCode", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
