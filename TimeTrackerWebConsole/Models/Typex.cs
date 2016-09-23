using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace types.Models
{
    public class Typex
    {
        [Required(ErrorMessage = "Type is required")]
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name {get; set;}
        //[Required(ErrorMessage = "Continent is required")]
        //public string continent { get; set; }
        //[Required(ErrorMessage = "Language is required")]
        //public string language {get; set;}
        //[Required(ErrorMessage = "Capital is required")]
        //public string capital {get; set;}
        //[Required(ErrorMessage = "Area is required")]
        //public ulong area {get; set;}
        //[Required(ErrorMessage = "Population is required")]
        //public ulong population {get; set;}
        [Required(ErrorMessage = "Calling code is required")]
        public uint callcode {get; set;}
        //public Type(int id, string name, string continent, string language, string capital, ulong area, ulong population, uint callcode)
        public Typex(int id, string name,  uint callcode)
        {
            this.id = id;
            this.name = name;
            this.callcode = callcode;
        }
        public Typex()
        {
            this.id = 0;
            this.name = null;
            this.callcode = 0;
        }
    }
}