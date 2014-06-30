using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using UnityAopSpike.DataAccess.Contexts;
using UnityAopSpike.Services.Products;

namespace UnityAopSpike.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            // All components that implement IDisposable should be registered with the 
            // HierarchicalLifetimeManager to ensure that they are properly disposed at 
            // the end of the request.
            container.RegisterType<IEntityDatabaseContext, EntityDatabaseContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductService, ProductService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}