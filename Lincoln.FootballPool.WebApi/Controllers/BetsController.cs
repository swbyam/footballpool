//-----------------------------------------------------------------------
// <copyright file="BetsController.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;
    using Lincoln.FootballPool.WebApi.ActionFilters;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;
    using Lincoln.FootballPool.WebApi.TypeMappers;

    /// <summary>
    /// API controller class that contains action methods related to retrieving bets from the persistence store.
    /// </summary>
    public class BetsController : ApiController
    {
        #region Member Variables

        /// <summary>
        /// Repository that provides basic CRUD operations related to point spread bets in the persistence store.
        /// </summary>
        private IBetRepository betRepository;

        /// <summary>
        /// Type mapper that performs mapping between request objects and DTO's and their domain counterparts.
        /// </summary>
        private IPagingTypeMapper<BetDto, Bet, int> pagingTypeMapper;

        /// <summary>
        /// Type mapper that maps instances of the Bet domain objects to their corresponding DTO type.
        /// </summary>
        private IBetTypeMapper betTypeMapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BetsController"/> class.
        /// </summary>
        /// <param name="betRepository">Repository that provides basic CRUD operations related to point spread bets in the persistence store.</param>
        /// <param name="pagingTypeMapper">Type mapper that performs mapping between request objects and DTO's and their domain counterparts.</param>
        /// <param name="betTypeMapper">Type mapper that maps instances of the Bet domain objects to their corresponding DTO type.</param>
        public BetsController(IBetRepository betRepository, IPagingTypeMapper<BetDto, Bet, int> pagingTypeMapper, IBetTypeMapper betTypeMapper)
        {
            if (betRepository == null)
            {
                throw new ArgumentNullException("betRepository", "betRepository cannot be null.");
            }

            if (pagingTypeMapper == null)
            {
                throw new ArgumentNullException("pagingTypeMapper", "pagingTypeMapper cannot be null.");
            }

            if (betTypeMapper == null)
            {
                throw new ArgumentNullException("betTypeMapper", "betTypeMapper cannot be null.");
            }

            this.betRepository = betRepository;
            this.pagingTypeMapper = pagingTypeMapper;
            this.betTypeMapper = betTypeMapper;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Processes GET requests that retrieve bets placed by a pool user with the supplied <paramref name="poolUserId"/> during the entire season.
        /// </summary>
        /// <param name="poolUserId">Id of the pool user for whom bets placed are to be returned.</param>
        /// <param name="poolId">Id of the pool to which the user belongs.</param>
        /// <param name="paginatedRequest">Request object containing information needed to paginate the result set, including the current page number and number of items per page.</param>
        /// <param name="requestMessage">Request message representing the GET operation.</param>
        /// <returns>Http response message containing a paginated list of bet DTO instances representing bets place during the entire season by a pool user.</returns>
        public HttpResponseMessage Get(int poolUserId, int poolId, [FromUri]PaginatedRequest paginatedRequest, HttpRequestMessage requestMessage)
        {
            if (poolUserId <= 0)
            {
                throw new ArgumentException("poolUserId cannot be less than or equal to zero.", "poolUserId");
            }

            if (poolId <= 0)
            {
                throw new ArgumentException("poolId cannot be less than or equal to zero.", "poolId");
            }

            if (paginatedRequest == null)
            {
                throw new ArgumentNullException("paginatedRequest", "paginatedRequest cannot be null.");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            PaginatedList<Bet, int> paginatedList = this.betRepository.GetBets(poolUserId, this.pagingTypeMapper.GetPagingInfo(paginatedRequest));

            PaginatedListDto<BetDto> paginatedListDto = this.pagingTypeMapper.GetPaginatedListDto(paginatedList, this.betTypeMapper, BetsController.GetBetUri(requestMessage, poolId));

            return requestMessage.CreateResponse(HttpStatusCode.OK, paginatedListDto);
        }

        /// <summary>
        /// Processes GET requests that retrieve bets placed on games during the supplied week number <paramref name="weekNumber"/> by a pool user with id <paramref name="poolUserId"/>.
        /// </summary>
        /// <param name="weekNumber">Week number of the season for which bets are to be retrieved.</param>
        /// <param name="poolUserId">Id of the pool user for whom bets are to be returned.</param>
        /// <param name="poolId">Id of the pool to which the user belongs.</param>
        /// <param name="requestMessage">Request message representing the GET operation.</param>
        /// <returns>Http response message containing list of bet DTO instances representing placed bets.</returns>
        public HttpResponseMessage Get(int weekNumber, int poolUserId, int poolId, HttpRequestMessage requestMessage)
        {
            if (weekNumber <= 0)
            {
                throw new ArgumentException("weekNumber cannot be less than or equal to zero.", "weekNumber");
            }

            if (poolUserId <= 0)
            {
                throw new ArgumentException("poolUserId cannot be less than or equal to zero.", "poolUserId");
            }

            if (poolId <= 0)
            {
                throw new ArgumentException("poolId cannot be less than or equal to zero.", "poolId");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            ////TODO: Where is week number validated?

            ////Retrieve bets from the persistence store.
            IEnumerable<Bet> bets = this.betRepository.GetBets(weekNumber, poolUserId);

            ////If no bets were found, return HTTP status code of "Not Found".
            if (bets.Count() == 0)
            {
                return requestMessage.CreateResponse(HttpStatusCode.NotFound);
            }

            ////Convert bets to their DTO counterparts.
            IEnumerable<BetDto> betDtos = this.betTypeMapper.GetEntityDtos(bets, new UrlHelper(requestMessage));

            return requestMessage.CreateResponse(HttpStatusCode.OK, betDtos);
        }

        /// <summary>
        /// Processes GET requests that retrieve bets placed on games during the supplied week number <paramref name="weekNumber"/> in a pool with the pool id <paramref name="poolId"/>
        /// </summary>
        /// <param name="weekNumber">Week number of the season for which bets are to be retrieved.</param>
        /// <param name="poolId">Id of the pool to which the user belongs.</param>
        /// <param name="paginatedRequest">Request object containing information needed to paginate the result set, including the current page number and number of items per page.</param>
        /// <param name="requestMessage">Request message representing the GET operation.</param>
        /// <returns>Http response message containing a paginated list of bet DTO instances representing bets place during the entire season by a pool user.</returns>
        public HttpResponseMessage GetByWeek(int weekNumber, int poolId, [FromUri]PaginatedRequest paginatedRequest, HttpRequestMessage requestMessage)
        {
            if (weekNumber <= 0)
            {
                throw new ArgumentException("weekNumber cannot be less than or equal to zero.", "weekNumber");
            }

            if (poolId <= 0)
            {
                throw new ArgumentException("poolId cannot be less than or equal to zero.", "poolId");
            }

            if (paginatedRequest == null)
            {
                throw new ArgumentNullException("paginatedRequest", "paginatedRequest cannot be null.");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            ////TODO: Where is week number validated?

            PaginatedList<Bet, int> paginatedList = this.betRepository.GetBetsForWeek(weekNumber, poolId, this.pagingTypeMapper.GetPagingInfo(paginatedRequest));

            PaginatedListDto<BetDto> paginatedListDto = this.pagingTypeMapper.GetPaginatedListDto(paginatedList, this.betTypeMapper, BetsController.GetBetUri(requestMessage, poolId));

            return requestMessage.CreateResponse(HttpStatusCode.OK, paginatedListDto);
        }

        [EmptyParameterFilterAttribute("gameRequest")]
        public HttpResponseMessage Post(BetBaseRequestModel betRequest, HttpRequestMessage requestMessage)
        {
            if (betRequest == null)
            {
                throw new ArgumentNullException("betRequest", "betRequest cannot be null.");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the URI of a game according to information contained in the supplied requestMessage <paramref name="requestMessage"/> and the unique id of the pool user who placed the bet <paramref name="poolId"/>.
        /// </summary>
        /// <param name="requestMessage">Request message containing information needed to create the bet URI.</param>
        /// <param name="poolId">Unique id of a pool in which the bet was placed.</param>
        /// <returns>URI for the bet.</returns>
        private static string GetBetUri(HttpRequestMessage requestMessage, int poolId)
        {
            ////TODO: Once route has been established for getting a single bet, use that to get link.
            return new UrlHelper(requestMessage).Link("BetsForPool", new { poolId = poolId });
        }

        #endregion
    }
}
