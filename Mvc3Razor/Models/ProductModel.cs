using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc3Razor.Models
{
    public class ProductModel
    {

        [Required(ErrorMessage = "Country is required")]
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name {get; set;}
        [Required(ErrorMessage = "Continent is required")]
        public string continent { get; set; }
        [Required(ErrorMessage = "Language is required")]
        public string language {get; set;}
        [Required(ErrorMessage = "Description is required")]
        public string description {get; set;}
        [Required(ErrorMessage = "Area is required")]
        public ulong area {get; set;}
        [Required(ErrorMessage = "Population is required")]
        public ulong population {get; set;}
        [Required(ErrorMessage = "Calling code is required")]
        public uint callcode {get; set;}
        public ProductModel(int id, string name, string continent, string language, string description, ulong area, ulong population, uint callcode)
        {
            this.id = id;
            this.name = name;
            this.continent = continent;
            this.language = language;
            this.description = description;
            this.area = area;
            this.population = population;
            this.callcode = callcode;
        }
        public ProductModel()
        {
            this.id = 0;
            this.name = null;
            this.continent = null;
            this.language = null;
            this.description = null;
            this.area = 0;
            this.population = 0;
            this.callcode = 0;
        }


    }
}