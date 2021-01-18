using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using CitiesApp.Asp.NetWebApi.Controllers;
using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CitiesApp.Asp.NetWebApiTests
{
    [TestClass]
    public class CitiesControllerTest
    {
        [TestMethod]
        public void GetReturnsProductWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<ICityRepo>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new City { Id = 42 });

            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<City>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ICityRepo>();
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<ICityRepo>();
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<ICityRepo>();
            mockRepository.Setup(x => x.GetById(10)).Returns(new City { Id = 10 });
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ICityRepo>();
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new City { Id = 9, Name = "City2" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<ICityRepo>();
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new City { Id = 10, Name = "City2" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<City>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<City> cities = new List<City>();
            cities.Add(new City { Id = 1, Name = "City1" });
            cities.Add(new City { Id = 2, Name = "City2" });

            var mockRepository = new Mock<ICityRepo>();
            mockRepository.Setup(x => x.GetAll()).Returns(cities.AsEnumerable());
            var controller = new CitiesController(mockRepository.Object);

            // Act
            IEnumerable<City> result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(cities.Count, result.ToList().Count);
            Assert.AreEqual(cities.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(cities.ElementAt(1), result.ElementAt(1));
        }
    }
}
