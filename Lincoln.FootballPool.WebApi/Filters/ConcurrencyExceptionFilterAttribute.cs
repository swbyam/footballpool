//-----------------------------------------------------------------------
// <copyright file="ConcurrencyExceptionFilterAttribute.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Filters
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Lincoln.FootballPool.Persistence;

    /// <summary>
    /// Custom exception filter attribute that handles exceptions of type <see cref="Lincoln.FootballPool.Persistence.ConcurrencyExceptionFilterAttribute"/> by returning an error HTTP response message.
    /// </summary>
    /// <remarks>The exception filter attribute provides additional error information to a user including the type name of the entity involved in the exception if one is provided.</remarks>
    public class ConcurrencyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Overrides the <c>OnException</c> method of the <see cref="ExceptionFilterAttribute"/> base class and contains logic that handles exceptions of type <see cref="Lincoln.FootballPool.Persistence.PersistenceException"/>.
        /// </summary>
        /// <param name="actionExecutedContext">Action of the HTTP executed context.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new ArgumentNullException("actionExecutedContext", "actionExecutedContext cannot be null.");
            }

            string message = "Changes to the {0} could not be saved due modification from another transaction.  Retrieve the {0} again and resubmit changes.";
            string entityTypeName = string.Empty;

            if (actionExecutedContext.Exception is ConcurrencyException)
            {
                ////If one exists, get the entity type name associated with the exception.
                entityTypeName = string.IsNullOrWhiteSpace(((ConcurrencyException)actionExecutedContext.Exception).EntityTypeName) ? "entity" : ((ConcurrencyException)actionExecutedContext.Exception).EntityTypeName;

                actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.Conflict, string.Format(CultureInfo.CurrentCulture, message, entityTypeName));
            }
        }

        #endregion
    }
}