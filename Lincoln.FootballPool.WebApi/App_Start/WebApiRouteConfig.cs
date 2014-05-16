//-----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.AppStart
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using Lincoln.FootballPool.WebApi.MessageHandlers;

    /// <summary>
    /// Static Class that is responsible for registering http routes on behalf of the Web API framework.
    /// </summary>
    public static class WebApiRouteConfig
    {
        #region Public Methods

        /// <summary>
        /// Registers the http routes for Web API in the supplied <paramref name="config"/> instance.
        /// </summary>
        /// <param name="config">HttpConfiguration instance with which routes are to be registered.</param>
        public static void RegiserRoutes(HttpConfiguration config)
        {
            ////Pipelines
            HttpMessageHandler poolExistsPipeline = 
                HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new[] { new PoolExistsMessageHandler() });

            ////Web API routes.
            config.MapHttpAttributeRoutes();

            ////TODO: Figure out how the 2 routes below can be differentiated from one another i.e. the URI segments are the same.  Perhaps 2nd route can be dropped?

            config.Routes.MapHttpRoute(
                name: "BetsForWeekRoute",
                routeTemplate: "api/{poolId}/bets/{weekNumber}",
                defaults: new { controller = "Bets", action = "GetByWeek" },
                constraints: null,
                handler: poolExistsPipeline);

            config.Routes.MapHttpRoute(
                name: "BetsForUserRoute",
                routeTemplate: "api/{poolId}/bets/{poolUserId}",
                defaults: new { controller = "Bets" },
                constraints: null,
                handler: poolExistsPipeline);

            config.Routes.MapHttpRoute(
                name: "BetsForUserByWeekRoute",
                routeTemplate: "api/{poolId}/bets/{poolUserId}/{weekNumber}",
                defaults: new { controller = "Bets" },
                constraints: null,
                handler: poolExistsPipeline);

            config.Routes.MapHttpRoute(
                name: "BetsForPool",
                routeTemplate: "api/{poolId}/bets",
                defaults: new { controller = "Bets" },
                constraints: null,
                handler: poolExistsPipeline);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        #endregion
    }
}