using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CitiesApp.Asp.NetWebApi.Repository
{
    public class CountryRepository: IDisposable, ICountryRepo
    {

        private CityDbContext db = new CityDbContext();

        public IEnumerable<Country> GetAll()
        {
            return db.Countries;
        }

        public Country GetById(int id)
        {
            return db.Countries.FirstOrDefault(p => p.Id == id);

        }
        public void Add(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        public void Update(Country country)
        {
            db.Entry(country).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public void Delete(Country country)
        {
            db.Countries.Remove(country);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}