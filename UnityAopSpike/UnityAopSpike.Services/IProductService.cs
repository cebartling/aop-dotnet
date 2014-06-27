using System.Collections.Generic;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
    }
}