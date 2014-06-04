//-----------------------------------------------------------------------
// <copyright file="PoolExistsMessageHandler.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.MessageHandlers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;

    public class PoolExistsMessageHandler : DelegatingHandler
    {
        #region Public Methods

        /// <summary>
        /// Web API custom message handler that verifies whether or not a pool exists in the persistence store that has a pool id included in a request URI.  If a pool with the id does not exist, an HTTP status code of "Not Found" is returned.
        /// </summary>
        /// <param name="request">HTTP request message.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>Task representing an asynchronous invocation of the message being sent.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "request cannot be null.");
            }

            if (cancellationToken == null)
            {
                throw new ArgumentNullException("cancellationToken", "cancellationToken cannot be null.");
            }

            ////Verify that pool exists in persistence store.
            ////TODO: When creating route constraint, be sure to constrain value to int.
            int poolId = Convert.ToInt32(request.GetRouteData().Values["poolId"]);
            IPoolRepository poolRepository = (IPoolRepository)request.GetDependencyScope().GetService(typeof(IPoolRepository));

            ////Retrieve pool entity from persistence store.
            Pool poolToCheckExists = poolRepository.GetById(poolId);

            ////If pool does not exist in persistence store, return HTTP status code of "Not Found"
            ////NOTE: This return value is left intentionally vague so a potential hacker does not know what is missing from the URI.
            if (poolToCheckExists == null)
            {
                Task.FromResult(request.CreateResponse(HttpStatusCode.NotFound));
            }

            return base.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}