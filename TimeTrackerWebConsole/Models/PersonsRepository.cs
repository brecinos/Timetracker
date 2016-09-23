using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace persons.Models
{
    public class  PersonsRepository : IPersonsRepository   
    {
        private List<Person> allPersons;
        private XDocument personsData;
        //constructor
        public PersonsRepository()
        {
            allPersons = new List<Person>();
            personsData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));


            var persons = from person in personsData.Descendants("person")
                        select new Person((int)person.Element("id"), person.Element("firstname").Value, person.Element("lastname").Value,
                            person.Element("address").Value, person.Element("telephone").Value, 
                         (uint)person.Element("age"));
            allPersons.AddRange(persons.ToList<Person>());
            
        }
        // return a list of all countries
        public IEnumerable<Person> GetPersons()
        {
            return allPersons;
        }
        public Person GetPersonByID(int id)
        {
            return allPersons.Find(person => person.id == id);
        }
        //insert record
        public void InsertPerson(Person person)
        {
            person.id = (int)(from b in personsData.Descendants("person") orderby (int)b.Element("id") descending select (int)b.Element("id")).FirstOrDefault() + 1;

            personsData.Root.Add(new XElement("person", new XElement("id", person.id), new XElement("firstname", person.firstname),
                                 new XElement("lastname", person.lastname), new XElement("address", person.address), new XElement("telephone", person.telephone), 
                               new XElement("age", person.age.ToString()   )  ) 
                               );

            personsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }
        public void DeletePerson(int id)
        {
            personsData.Root.Elements("person").Where(i => (int)i.Element("id") == id).Remove();
            personsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }
        public void EditPerson(Person person)
        {
            XElement node = personsData.Root.Elements("person").Where(i => (int)i.Element("id") == person.id).FirstOrDefault();

            node.SetElementValue("person", person.id);
            node.SetElementValue("firstname", person.firstname);
            node.SetElementValue("lastname", person.lastname);
            node.SetElementValue("address", person.address);
            node.SetElementValue("telephone", person.telephone);
            node.SetElementValue("age", person.age.ToString());
            personsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }

    }
}