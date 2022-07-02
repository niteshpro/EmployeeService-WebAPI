using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmployeeService
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //     name: "Action",
            //     routeTemplate: "api/{controller}/{action}/{loadallemployee}"
            //);
            config.Formatters.Remove(config.Formatters.XmlFormatter); // returns only in JSON format
        }
    }
}
