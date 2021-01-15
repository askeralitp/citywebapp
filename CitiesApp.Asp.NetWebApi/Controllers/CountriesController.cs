using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitiesApp.Asp.NetWebApi.Controllers
{
    public class CountriesController : ApiController
    {
        ICountryRepo _repository { get; set; }

        public CountriesController(ICountryRepo repository)
        {
            _repository = repository;
        }


        public IEnumerable<Country> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            var country = _repository.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        public IHttpActionResult Post(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(country);
            return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
        }

        public IHttpActionResult Put(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(country);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(country);
        }

        public IHttpActionResult Delete(int id)
        {
            var country = _repository.GetById(id);
            if (country == null)
            {
                return NotFound();
            }

            _repository.Delete(country);
            return Ok();
        }
    }
}
