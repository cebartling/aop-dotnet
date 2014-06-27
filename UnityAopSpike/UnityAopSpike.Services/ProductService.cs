using System.Collections.Generic;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.Services
{
    public class ProductService : IProductService
    {
        private readonly IEntityDatabaseContext _entityDatabaseContext;

        public ProductService(IEntityDatabaseContext entityDatabaseContext)
        {
            _entityDatabaseContext = entityDatabaseContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _entityDatabaseContext.Products;
        }
    }
}