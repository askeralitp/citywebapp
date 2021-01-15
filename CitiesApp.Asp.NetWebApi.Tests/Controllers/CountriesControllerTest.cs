using CitiesApp.Asp.NetWebApi.Controllers;
using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace CitiesApp.Asp.NetWebApi.Tests.Controllers
{
    [TestClass]
    class CountriesControllerTest
    {
        [TestMethod]
        public void GetReturnsCountryWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<ICountryRepo>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new Country { Id = 42 });

            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Country>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        //---------------------------------------------------------

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ICountryRepo>();
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        //---------------------------------------------------------

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<ICountryRepo>();
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        //---------------------------------------------------------

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<ICountryRepo>();
            mockRepository.Setup(x => x.GetById(10)).Returns(new Country { Id = 10 });
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }


        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<ICountryRepo>();
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Country { Id = 9, Name = "Country2" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        // -------------------------------------------------------------------------------------

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<ICountryRepo>();
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Country { Id = 10, Name = "Country1" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Country>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }

        // ------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Country> products = new List<Country>();
            products.Add(new Country { Id = 1, Name = "Country1" });
            products.Add(new Country { Id = 2, Name = "Country2" });

            var mockRepository = new Mock<ICountryRepo>();
            mockRepository.Setup(x => x.GetAll()).Returns(products.AsEnumerable());
            var controller = new CountriesController(mockRepository.Object);

            // Act
            IEnumerable<Country> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(products.Count, result.ToList().Count);
            Assert.AreEqual(products.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(products.ElementAt(1), result.ElementAt(1));
        }
    }
}
