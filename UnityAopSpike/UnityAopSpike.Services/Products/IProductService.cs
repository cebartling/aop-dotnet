using System.Collections.Generic;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.Services.Products
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
    }
}