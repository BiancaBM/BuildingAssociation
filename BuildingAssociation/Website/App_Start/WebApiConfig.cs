using Repositories.Contracts;
using Repositories.Repositories;
using Services.Contracts;
using Services.Services;
using System.Web.Http;
using Unity;
using Website.Helpers;

namespace Website
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API configuration and services
            var container = new UnityContainer();
            RegisterRepositories(container);
            RegisterServices(container);

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterRepositories(UnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IProviderBillRepository, ProviderBillRepository>();
            container.RegisterType<IProviderRepository, ProviderRepository>();
            container.RegisterType<IApartmentRepository, ApartmentRepository>();
            container.RegisterType<IWaterConsumptionRepository, WaterConsumptionRepository>();
            container.RegisterType<IMansionRepository, MansionRepository>();

        }

        private static void RegisterServices(UnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IProviderBillService, ProviderBillService>();
            container.RegisterType<IProviderService, ProviderService>();
            container.RegisterType<IApartmentService, ApartmentService>();
            container.RegisterType<IWaterConsumptionService, WaterConsumptionService>();
            container.RegisterType<IMansionService, MansionService>();
        }
    }
}
