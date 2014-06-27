using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.DataAccess.IntegrationTests.Contexts
{
    [TestClass]
    public class CreateOrderDbContextIntegrationTest
    {
        [TestCleanup]
        public void CleanUp()
        {
            using (var db = new CreateOrderDbContext())
            {
                db.Database.ExecuteSqlCommand("delete from Orders");
            }
        }


        [TestMethod]
        public void CreateOrder()
        {
            using (var db = new CreateOrderDbContext())
            {
                var now = new DateTime();
                var order = new Order {OrderNumber = string.Format("L{0}", now.Ticks), 
                    OrderDateTime = now};
                db.Orders.Add(order);
                db.SaveChanges();

                // retrieve all orders 
                var query = from o in db.Orders orderby o.OrderDateTime select o;

                Assert.AreEqual(1, query.Count());
            } 
        }
    }
}
