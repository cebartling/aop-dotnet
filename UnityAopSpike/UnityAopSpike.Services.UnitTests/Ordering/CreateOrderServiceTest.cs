using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;
using UnityAopSpike.Services.Ordering;

namespace UnityAopSpike.Services.UnitTests.Ordering
{
    [TestClass]
    public class CreateOrderServiceTest
    {
        private Mock<DbSet<Order>> _mockSet;
        private Mock<IEntityDatabaseContext> _mockContext;

        [TestInitialize]
        public void DoBefore()
        {
            _mockSet = new Mock<DbSet<Order>>();
            _mockContext = new Mock<IEntityDatabaseContext>();
        }

        [TestMethod]
        public void Execute_AddsOrderToDbSet()
        {
            _mockContext.Setup(m => m.Orders).Returns(_mockSet.Object);
            var service = new CreateOrderService(_mockContext.Object);

            service.Execute();

            _mockSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once());
        }

        [TestMethod]
        public void Execute_SavesChangesOnDbContext()
        {
            _mockContext.Setup(m => m.Orders).Returns(_mockSet.Object);
            var service = new CreateOrderService(_mockContext.Object);

            service.Execute();

            _mockContext.Verify(m => m.SaveChanges(), Times.Once()); 
        }
    }
}
