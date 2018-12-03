using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UptownZoo;
using UptownZoo.Controllers;

namespace UptownZoo.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index_RetrurnsNonNullViewResult() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result =  controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ReturnsNonNullViewResult() {
            HomeController home = new HomeController();

            ViewResult result = home.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ShouldHaveViewBagMessage() {
            HomeController home = new HomeController();

            ViewResult result = home.About() as ViewResult;

            Assert.IsNotNull(result.ViewBag.Message);

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
