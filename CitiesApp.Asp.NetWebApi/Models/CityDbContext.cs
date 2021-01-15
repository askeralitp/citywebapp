using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CitiesApp.Asp.NetWebApi.Models
{
    public class CityDbContext : DbContext
    {
        public CityDbContext(): base("name=CityContext")
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}