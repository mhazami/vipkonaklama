using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.ModuleGeneretor
{

    [Serializable, XmlRoot("module",Namespace = "Radyn.ModuleGeneretor"), DataContract(Name = "module")]
    public class ModuleXml
    {

        public ModuleXml()
        {

            DllModel = new DllsXml();
            DBModel = new DBsXml();
            ResourcesModel = new ResourcesXml();
            xmlpath = new PathXml();
          
        }



        [XmlElement(ElementName = "dlls"), DataMember(Name = "dlls")]
        public DllsXml DllModel { get; set; }

        [XmlElement(ElementName = "dbs"), DataMember(Name = "dbs")]
        public DBsXml DBModel { get; set; }
        

        [XmlElement(ElementName = "resources"), DataMember(Name = "resources")]
        public ResourcesXml ResourcesModel { get; set; }


        [XmlElement(ElementName = "xmlpath"), DataMember(Name = "xmlpath")]
        public PathXml xmlpath { get; set; }


    }

    [Serializable]
    public class DllsXml
    {
        public DllsXml()
        {
            if (Dlls == null)
                Dlls = new List<PathXml>();
        }
        [XmlElement(ElementName = "dll"), DataMember(Name = "dll")]
        public List<PathXml> Dlls { get; set; }


    }
    [Serializable]
    public class DBsXml
    {
        public DBsXml()
        {
            if (DBs == null)
                DBs = new List<PathXml>();
        }
        [XmlElement(ElementName = "script"), DataMember(Name = "script")]
        public List<PathXml> DBs { get; set; }


    }


    [Serializable]
    public class ResourcesXml
    {
        public ResourcesXml()
        {
            if (Resources == null)
                Resources = new List<PathXml>();
        }
        [XmlElement(ElementName = "resource"), DataMember(Name = "resource")]
        public List<PathXml> Resources { get; set; }


    }

    [Serializable]
    public class PathXml
    {
       

        [XmlAttribute(AttributeName = "path"), DataMember(Name = "path")]
        public string Path { get; set; }

       


    }

   
}
