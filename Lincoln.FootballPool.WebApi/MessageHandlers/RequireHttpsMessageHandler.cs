//-----------------------------------------------------------------------
// <copyright file="RequireHttpsMessageHandler.cs" company="Lincoln">
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

    /// <summary>
    /// Web API custom message handler that verifies whether or not a request message passing through the Web API HTTP message handler chain was sent as an HTTPS request.  If this is not the case, a return message is sent with the HTTP status of "Forbidden".
    /// </summary>
    public class RequireHttpsMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Sends the supplied HTTP request message <paramref name="request"/> to the inner HTTP message handler.
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

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                HttpResponseMessage forbiddenResponse = request.CreateResponse(HttpStatusCode.Forbidden);
                forbiddenResponse.ReasonPhrase = "SSL Required";

                return Task.FromResult<HttpResponseMessage>(forbiddenResponse);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}