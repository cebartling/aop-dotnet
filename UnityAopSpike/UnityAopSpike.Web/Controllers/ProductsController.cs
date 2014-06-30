using System.Collections.Generic;
using System.Web.Http;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.Services.Products;

namespace UnityAopSpike.Web.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }
    }
}