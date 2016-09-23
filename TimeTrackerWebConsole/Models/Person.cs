using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace persons.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Person is required")]
        public int id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string firstname {get; set;}
        [Required(ErrorMessage = "LastName is required")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Telephone is required")]
        public string telephone { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public uint age { get; set; }
      
        
        public Person(int id, string firstname,string  lastname, string address,  string telephone,  uint age)
        {
            this.id = id;
            this.firstname =  firstname;
            this.lastname = lastname;
            this.address = address;
            this.telephone = telephone;
            this.age = age;
        }
        public Person()
        {
            this.id = 0;
            this.firstname = null;
            this.lastname = null;
            this.address = null;
            this.telephone = null;
            this.age = 0;
        }
    }
}