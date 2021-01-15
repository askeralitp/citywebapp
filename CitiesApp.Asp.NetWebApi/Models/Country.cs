using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitiesApp.Asp.NetWebApi.Models
{
    public class Country
    {

        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string Code { get; set; }
    }
}