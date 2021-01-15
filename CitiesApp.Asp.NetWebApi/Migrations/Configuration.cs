namespace CitiesApp.Asp.NetWebApi.Migrations
{
    using CitiesApp.Asp.NetWebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CitiesApp.Asp.NetWebApi.Models.CityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CitiesApp.Asp.NetWebApi.Models.CityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Countries.AddOrUpdate(p => p.Id,
                new Country() { Id = 1, Name = "Serbia", Code = "RS" },
                new Country() { Id = 2, Name = "Russia", Code = "RU" },
                new Country() { Id = 3, Name = "China ", Code = "CN" }
                );

            context.Cities.AddOrUpdate(p => p.Id,
                new City() { Id = 1, Name = "Belgrade", Zip = 11000, Population= 1400000, CountryId=1 },
                new City() { Id = 2, Name = "Moscow", Zip = 101000, Population= 11920000, CountryId=2 },
                new City() { Id = 3, Name = "Beijing ", Zip = 100000, Population=21540000, CountryId=3 }
                );

        }
    }
}
