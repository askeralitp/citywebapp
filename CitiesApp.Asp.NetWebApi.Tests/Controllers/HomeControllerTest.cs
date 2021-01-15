using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CitiesApp.Asp.NetWebApi;
using CitiesApp.Asp.NetWebApi.Controllers;

namespace CitiesApp.Asp.NetWebApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
