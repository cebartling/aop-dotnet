using System.Collections.Generic;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.AcceptanceTests.Steps.Core
{
    public class TestingDomain
    {
        private static readonly IEntityDatabaseContext EntityDatabaseContextInstance = new EntityDatabaseContext();

        /// <summary>
        ///     Creates testing products for step definitions.
        /// </summary>
        /// <param name="createCount">The number of the products to create.</param>
        public static IList<Product> CreateProducts(int createCount)
        {
            var products = new List<Product>();
            for (int i = 0; i < createCount; i++)
            {
                var product = new Product
                {
                    Name = string.Format("Product name {0}", i),
                    Description = string.Format("Product description for product name {0}.", i)
                };
                EntityDatabaseContextInstance.Products.Add(product);
                products.Add(product);
            }
            EntityDatabaseContextInstance.SaveChanges();
            return products;
        }
    }
}