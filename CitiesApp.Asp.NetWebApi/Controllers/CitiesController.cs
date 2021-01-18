using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CitiesApp.Asp.NetWebApi.Controllers
{
    public class CitiesController : ApiController
    {

        ICityRepo _repository { get; set; }

        public CitiesController(ICityRepo repository)
        {
            _repository = repository;
        }


        public IEnumerable<City> GetAll()
        {
            return _repository.GetAll();
        }

        [Route("api/search")]
        public IEnumerable<City> Search(Filter filter)
        {
            return _repository.Search(filter);
        }

        [ResponseType(typeof(City))]
        public IHttpActionResult GetById(int id)
        {
            var city = _repository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }




        [ResponseType(typeof(City))]
        public IHttpActionResult Post(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(city);
            return CreatedAtRoute("DefaultApi", new { id = city.Id }, city);
        }

        [ResponseType(typeof(City))]
        public IHttpActionResult Put(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(city);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(city);
        }

        [ResponseType(typeof(City))]
        public IHttpActionResult Delete(int id)
        {
            var city = _repository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }

            _repository.Delete(city);
            return Ok();
        }
    }
}
