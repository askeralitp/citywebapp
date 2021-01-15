using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CitiesApp.Asp.NetWebApi.Repository
{
    public class CityRepository :IDisposable, ICityRepo
    {


        private CityDbContext db = new CityDbContext();

        public IEnumerable<City> GetAll()
        {
            return db.Cities.Include(x => x.Country).OrderBy(x => x.Zip);
        }

        public City GetById(int id)
        {
            return db.Cities.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<City> Search(Filter filter)
        {
            var cities = db.Cities.Include(x => x.Country).Where(x => x.Population >= filter.Min & x.Population <= filter.Max).OrderBy(x => x.Population);
            return cities;
        }

        public void Add(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
        }

        public void Update(City city)
        {
            db.Entry(city).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete (City city)
        {
            db.Cities.Remove(city);
            db.SaveChanges();
        }


        public void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(db !=null)
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