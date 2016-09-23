using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace types.Models
{
    public class TypesRepository : ITypesRepository
    {
        private List<Typex> allTypes;
        private XDocument typesData;
        //constructor
        public TypesRepository()
        {
            allTypes = new List<Typex>();
            typesData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Types.xml"));


            var types =  from type in typesData.Descendants("type")
                         select  new Typex((int)type.Element("id"), type.Element("name").Value,
                         (uint)type.Element("callcode"));
            allTypes.AddRange(types.ToList<Typex>());



            //var countries = from country in typesData.Descendants("country")
            //               select new Type((int)country.Element("id"), country.Element("name").Value,
            //               country.Element("continent").Value, country.Element("language").Value,
            //               country.Element("capital").Value, (ulong)country.Element("area"),(ulong)country.Element("population"),
            //               (uint)country.Element("callcode"));
            //allTypes.AddRange(countries.ToList<Type>());
        }
        // return a list of all countries
        public IEnumerable<Typex> GetTypes()
        {
            return allTypes;
        }
        public Typex GetTypeByID(int id)
        {
            return allTypes.Find(type => type.id == id);
        }
        //insert record
        public void InsertType(Typex type)
        {
            type.id = (int)(from b in typesData.Descendants("type") orderby (int)b.Element("id") descending select (int)b.Element("id")).FirstOrDefault() + 1;

            typesData.Root.Add(new XElement("type", new XElement("id", type.id), new XElement("name", type.name), 
                               new XElement("callcode", type.callcode.ToString()   )  ) 
                               );

            typesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Types.xml"));
        }
        public void DeleteType(int id)
        {
            typesData.Root.Elements("country").Where(i => (int)i.Element("id") == id).Remove();
            typesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Types.xml"));
        }
        public void EditType(Typex type)
        {
            XElement node = typesData.Root.Elements("type").Where(i => (int)i.Element("id") == type.id).FirstOrDefault();

            node.SetElementValue("continent", type.id);
            node.SetElementValue("name", type.name);
            node.SetElementValue("callcode", type.callcode.ToString());
            typesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Types.xml"));
        }

    }
}