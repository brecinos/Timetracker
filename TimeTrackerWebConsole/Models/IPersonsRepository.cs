using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace persons.Models
{
    public interface IPersonsRepository 
    {
        IEnumerable<Person> GetPersons();
        Person GetPersonByID(int id);
        void InsertPerson(Person person);
        void DeletePerson(int id);
        void EditPerson(Person person);
    }
}