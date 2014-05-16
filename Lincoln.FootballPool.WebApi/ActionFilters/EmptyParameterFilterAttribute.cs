//-----------------------------------------------------------------------
// <copyright file="EmptyParameterFilterAttribute.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.ActionFilters 
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// Action filter attribute that verifies whether a request has been sent with an empty message body.  If this is the case, the corresponding complex type action method parameter will be passed to an action method as null.
    /// </summary>
    /// <remarks>If a request is sent with an empty message body, an HTTP status code of 400 is returned (bad request).</remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EmptyParameterFilterAttribute : ActionFilterAttribute
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyParameterFilterAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter for which the attribute is to check for being null.</param>
        public EmptyParameterFilterAttribute(string parameterName) 
        {
            if (string.IsNullOrEmpty(parameterName)) 
            {
                throw new ArgumentNullException("parameterName", "parameterName cannot be null.");
            }

            this.ParameterName = parameterName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the parameter for which the attribute is to check for being null.
        /// </summary>
        public string ParameterName { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override of OnActionExecuting method from base <see cref="ActionFilterAttribute"/> class.
        /// </summary>
        /// <param name="actionContext">Http action context instance.</param>
        public override void OnActionExecuting(HttpActionContext actionContext) 
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext", "actionContext cannot be null.");
            }

            object parameterValue;

            if (actionContext.ActionArguments.TryGetValue(this.ParameterName, out parameterValue)) 
            {
                if (parameterValue == null) 
                {
                    actionContext.ModelState.AddModelError(this.ParameterName, EmptyParameterFilterAttribute.FormatErrorMessage(this.ParameterName));

                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats and returns an error message in the event the action method parameter is null.
        /// </summary>
        /// <param name="parameterName">Name of the action method parameter.</param>
        /// <returns>Formatted error message.</returns>
        private static string FormatErrorMessage(string parameterName) 
        {
            return string.Format(CultureInfo.CurrentCulture, "The {0} parameter cannot be null.", parameterName);
        }

        #endregion
    }
}