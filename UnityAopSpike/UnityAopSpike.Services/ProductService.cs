using System.Linq;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.Services
{
    public class ProductService
    {
        private readonly IEntityDatabaseContext _entityDatabaseContext;

        public ProductService(IEntityDatabaseContext entityDatabaseContext)
        {
            _entityDatabaseContext = entityDatabaseContext;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _entityDatabaseContext.Products;
        }
    }
}