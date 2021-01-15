using CitiesApp.Asp.NetWebApi.Interfaces;
using CitiesApp.Asp.NetWebApi.Repository;
using Microsoft.Practices.Unity;
using ProductService.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CitiesApp.Asp.NetWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //unity 
            var container = new UnityContainer();
            container.RegisterType<ICountryRepo, CountryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICityRepo, CityRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
