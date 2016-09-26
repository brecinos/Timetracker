using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace products.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product is required")]
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name {get; set;}
        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }
        
        public Product(int id, string name,string  description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        public Product()
        {
            this.id = 0;
            this.name = null;
            this.description = null;
        }
    }
}