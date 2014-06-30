using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Unity.WebApi;
using UnityAopSpike.Core.Interceptors;
using UnityAopSpike.DataAccess.Contexts;
using UnityAopSpike.Services.Products;

namespace UnityAopSpike.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            // All components that implement IDisposable should be registered with the 
            // HierarchicalLifetimeManager to ensure that they are properly disposed at 
            // the end of the request.
            container.RegisterType<IEntityDatabaseContext, EntityDatabaseContext>(new HierarchicalLifetimeManager(),
                CreateInterfaceInterceptor(), 
                CreateLoggingInterceptor(),
                CreateTimingInterceptor());
            container.RegisterType<IProductService, ProductService>(CreateInterfaceInterceptor(), 
                CreateLoggingInterceptor(), 
                CreateTimingInterceptor());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static Interceptor<InterfaceInterceptor> CreateInterfaceInterceptor()
        {
            return new Interceptor<InterfaceInterceptor>();
        }

        private static InterceptionBehavior<LoggingInterceptionBehavior> CreateLoggingInterceptor()
        {
            return new InterceptionBehavior<LoggingInterceptionBehavior>();
        }

        private static InterceptionBehavior<TimingInterceptionBehavior> CreateTimingInterceptor()
        {
            return new InterceptionBehavior<TimingInterceptionBehavior>();
        }
    }
}