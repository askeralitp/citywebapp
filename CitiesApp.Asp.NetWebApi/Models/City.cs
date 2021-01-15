using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitiesApp.Asp.NetWebApi.Models
{
    public class City
    {

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 9999999)]
        public int Zip { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Population { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}