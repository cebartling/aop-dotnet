﻿using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityAopSpike.Web.Controllers;

namespace UnityAopSpike.Web.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}