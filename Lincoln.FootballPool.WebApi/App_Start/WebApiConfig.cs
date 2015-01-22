//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.AppStart
{
    using System;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Validation;
    using System.Web.Http.Validation.Providers;

    using Lincoln.FootballPool.WebApi.Filters;
    using Lincoln.FootballPool.WebApi.Formatting;
    using Lincoln.FootballPool.WebApi.MessageHandlers;

    using WebApiDoodle.Web.Filters;

    /// <summary>
    /// Static class that performs setup and registration operations on an instance of <see cref="HttpConfiguration"/>.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers all routes associated with the web api project using the supplied <see cref="HttpConfiguration"/> instance <paramref name="config"/>.
        /// </summary>
        /// <param name="config">HttpConfiguration instance to which routes are to be registered.</param>
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config", "Parameter config cannot be null.");
            }

            ////Register routes.
            WebApiRouteConfig.RegiserRoutes(config);

            ////Configure AutoMapper.
            AutoMapperConfig.Configure();

            // Web API configuration and services.

            ////Add HTTP message handler that prevents non-SSL requests from being handled.
            ////config.MessageHandlers.Add(new RequireHttpsMessageHandler());

            ////Register exception filter attribute that handles exceptions related to concurrency.
            config.Filters.Add(new ConcurrencyExceptionFilterAttribute());

            ////Register exception filter attribute that handles exceptions related to general database operation errors.
            config.Filters.Add(new PersistenceExceptionFilterAttribute());

            ////Remove formatters that will not be used.
            ////NOTE: These formatters are being removed because they can only read the "application/x-www-form-urlencoded" media type.
            MediaTypeFormatter jqueryFormatter = config.Formatters.FirstOrDefault(
                x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);

            ////Replace IRequiredMemberSelector implementation of each media type formatter such that every member of an object being sent over the wire does not have to have a value in order to be deserialized.
            ////NOTE: This prevents an additional redundant validation message appearing in ModelState when a property has a [Required] data annotation.
            foreach (MediaTypeFormatter formatter in config.Formatters)
            {
                formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
            }

            ////Register action filter from Web API Doodle API which performs validation against complex types after the model binding process has completed.  If a complex type is found to be invalid, an HTTP status code of 400 is returned (bad request).
            config.Filters.Add(new InvalidModelStateFilterAttribute());

            ////Replace content negogiator with new instance of DefaultContentNegogiator with the "excludeMatchOnTypeOnly" parameter set to true.  Doing this results in the web api methods strictly enforcing the media types that are accepted.
            config.Services.Replace(typeof(IContentNegotiator), new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));

            ////Remove all validation providers except the DataAnnotationsModelValidatorProvider implementation.
            config.Services.RemoveAll(
                typeof(ModelValidatorProvider), 
                validator => !(validator is DataAnnotationsModelValidatorProvider));

            ////Set error detail policy such that detailed error messages cannot be retrieved from users not directly on the server.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
        }
    }
}
