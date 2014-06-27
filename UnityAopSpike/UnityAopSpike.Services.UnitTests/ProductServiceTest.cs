using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.Services.UnitTests
{
    [TestClass]
    public class ProductServiceTest
    {
        private Mock<IEntityDatabaseContext> _mockContext;
        private Mock<DbSet<Product>> _mockSet;

        [TestInitialize]
        public void DoBefore()
        {
            _mockSet = new Mock<DbSet<Product>>();
            _mockContext = new Mock<IEntityDatabaseContext>();
        }


        [TestMethod]
        public void GetAllProducts_RetrieveProductsFromDbContext()
        {
            _mockContext.Setup(m => m.Products).Returns(_mockSet.Object);
            var service = new ProductService(_mockContext.Object);

            service.GetAllProducts();

            _mockContext.Verify(m => m.Products, Times.Once());
        }
    }
}