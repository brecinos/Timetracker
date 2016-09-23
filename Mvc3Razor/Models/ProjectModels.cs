using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Mvc3Razor.Models
{
    public class ProjectModels
    {
        public int Id  {   get;  set;   }

        [Required]
        public string ProjectName    {  get; set;  }

        [Required]
        public string Location    {   get;  set;  }
        public bool IsEdit        {  get; set;  }  




    }
}