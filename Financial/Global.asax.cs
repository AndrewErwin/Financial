using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Financial.App_Start;
using WebMatrix.WebData;

namespace Financial
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.ConfigureAutoMapper();
            WebSecurity.InitializeDatabaseConnection("Financial", "Users", "Id", "Login", true);
        }
    }
}
