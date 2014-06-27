using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.Services;
using UnityAopSpike.Web.Controllers;

namespace UnityAopSpike.Web.UnitTests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        private ProductsController _controller;
        private Mock<IProductService> _productServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [TestMethod]
        public void GetProducts()
        {
            IEnumerable<Product> expectedProducts = new List<Product>
            {
                new Product {Name = "Shoe 1", Description = "Hiking boot"}
            };
            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(expectedProducts);

            IEnumerable<Product> products = _controller.GetAllProducts();

            _productServiceMock.Verify(x => x.GetAllProducts());
            Assert.AreEqual(expectedProducts, products);
        }
    }
}