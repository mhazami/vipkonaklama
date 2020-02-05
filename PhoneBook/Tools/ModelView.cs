using System;
using System.Collections.Generic;

namespace Radyn.PhoneBook.Tools
{
    public class ModelView
    {
        public class PersonSearch
        
        {
            public PersonSearch()
            {
                PersonPhoneSearches=new List<string>();
            }
            public string Name { get; set; }    
            public string OfficeName { get; set; }    
            public string DepartmentName { get; set; }    
            public string PersoneliCode { get; set; }    
            public string JopTitle { get; set; }    
            public string Tel { get; set; }    
           public bool Enabled { get; set; }    
            public Guid Id{ get; set; }    
            public Guid? PictureId { get; set; }    
            public List<string> PersonPhoneSearches{ get; set; }    
        }

      
    }
}
