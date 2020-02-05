using System;
using System.Runtime.Serialization;
using Radyn.Framework;

namespace Radyn.Security.Tools
{
    [Schema("dbo")]
    public class Access
    {

        public Guid? Id { get; set; }
        public string Title { get; set; }
    }
    [DataContract(Name = "ContactUSModel")]
    public class ContactUSModel
    {



        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Title { get; set; }


        [DataMember]
        public string Tel { get; set; }

        [DataMember]
        public string TelInternal { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal? Price { get; set; }






    }
}
