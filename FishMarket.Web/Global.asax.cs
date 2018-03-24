using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using FishMarket.Repository.DataContext;
using System.Data.Entity;
using Newtonsoft.Json;
using NLog;

namespace FishMarket.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new FishDbInitializer());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Exceptions logged using with Nlog. 
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(exc, JsonConvert.SerializeObject(exc));

            Server.ClearError();

            Response.Redirect("~/Error/Index");
        }
    }
}