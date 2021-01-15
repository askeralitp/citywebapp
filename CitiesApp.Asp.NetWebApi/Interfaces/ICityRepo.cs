using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesApp.Asp.NetWebApi.Interfaces
{
    public interface ICityRepo
    {
        IEnumerable<City> GetAll();
        IEnumerable<City> Search(Filter filter);
        City GetById(int id);
        void Add(City city);
        void Update(City city);
        void Delete(City city);
    }
}
