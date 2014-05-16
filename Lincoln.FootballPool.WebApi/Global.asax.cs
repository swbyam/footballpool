//-----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi
{
    using System;
    using System.Web.Http;

    using Lincoln.FootballPool.WebApi.AppStart;

    /// <summary>
    /// WebApiApplication type for the application.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Method that is called on application start up.
        /// </summary>
        protected void Application_Start()
        {
            ////TODO: This file may be needed in a web host project and not this one!!

            ////Perform set up and registration on HttpConfiguration instance.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ////Initialize Autofac IoC container.
            AutofacIocConfig.Configure(GlobalConfiguration.Configuration);
        }
    }
}
