using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.DataAccess.IntegrationTests.Contexts
{
    [TestClass]
    public class EntityDatabaseContextIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var db = new EntityDatabaseContext())
            {
                db.Database.ExecuteSqlCommand("delete from Orders");
                db.Database.ExecuteSqlCommand("delete from Products");
            }
        }


        [TestMethod]
        public void CreateOrder()
        {
            using (var db = new EntityDatabaseContext())
            {
                var now = new DateTime();
                var order = new Order
                {
                    OrderNumber = string.Format("L{0}", now.Ticks),
                    OrderDateTime = now
                };
                db.Orders.Add(order);
                db.SaveChanges();

                // retrieve all orders 
                IOrderedQueryable<Order> query = from o in db.Orders orderby o.OrderDateTime select o;

                Assert.AreEqual(1, query.Count());
            }
        }

        [TestMethod]
        public void GetProducts()
        {
            using (var db = new EntityDatabaseContext())
            {
                var product = new Product {Name = "Bob's your uncle", Description = "Ain't that enough?"};
                db.Products.Add(product);
                db.SaveChanges();
            }

            using (var db = new EntityDatabaseContext())
            {
                DbSet<Product> products = db.Products;
                Assert.AreEqual(1, products.Count());
            }
        }
    }
}