using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesApp.Asp.NetWebApi.Interfaces
{
    public interface ICountryRepo
    {
        IEnumerable<Country> GetAll();
        Country GetById(int id);
        void Add(Country country);
        void Update(Country country);
        void Delete(Country country);
    }
}
