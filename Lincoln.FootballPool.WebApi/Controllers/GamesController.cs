//-----------------------------------------------------------------------
// <copyright file="GamesController.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Routing;
    using System.Web.Http;

    using Lincoln.FootballPool.Domain;
    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;
    using Lincoln.FootballPool.Domain.Services;
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.WebApi.ActionFilters;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;
    using Lincoln.FootballPool.WebApi.TypeMappers;

    /// <summary>
    /// API controller class that contains action methods related to retrieving games from the persistence store.
    /// </summary>
    public class GamesController : ApiController
    {
        #region Member Variables

        /// <summary>
        /// Game repository that is responsible retrieving games from the persistence store.
        /// </summary>
        private IGameRepository gameRepository;

        /// <summary>
        /// Type mapper that maps instances of the Game domain objects to their corresponding DTO type.
        /// </summary>
        private IGameTypeMapper gameTypeMapper;

        /// <summary>
        /// Services that provides persistence-related operations relevant to games.
        /// </summary>
        private IGameService gameService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        public GamesController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="gameRepository"> Game repository that is responsible or providing basic CRUD functionality related to games including retrieving, updating, and deleting them from the persistence store.</param>
        /// <param name="gameTypeMapper">Type mapper that maps instances of the Game domain objects to their corresponding DTO type.</param>
        /// <param name="gameService">Services that provides persistence-related operations relevant to games.</param>
        public GamesController(IGameRepository gameRepository, IGameTypeMapper gameTypeMapper, IGameService gameService)
        {
            if (gameRepository == null)
            {
                throw new ArgumentNullException("gameRepository", "gameRepository cannot be null.");
            }

            if (gameTypeMapper == null)
            {
                throw new ArgumentNullException("gameTypeMapper", "gameTypeMapper cannot be null.");
            }

            if (gameService == null)
            {
                throw new ArgumentNullException("gameService", "gameService cannot be null.");
            }

            this.gameRepository = gameRepository;
            this.gameTypeMapper = gameTypeMapper;
            this.gameService = gameService;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Action method processes GET requests and returns all games for the supplied week number <paramref name="week"/>.
        /// </summary>
        /// <param name="week">Week number for which games are to be retrieved.</param>
        /// <param name="requestMessage">Http request message.</param>
        /// <returns>Http response message containing a list of DTO instances representing games that are to be played during the supplied week.</returns>
        public HttpResponseMessage GetGameByWeek(int week, HttpRequestMessage requestMessage)
        {
            ////TODO: This method, and any others that filter games by its properties i.e. by week, by team etc., has to be handled via something like ODATA, which needs to be looked into.

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            IEnumerable<GameDto> gameDtos = null;

            ////Get games for current week.
            IEnumerable<Game> gamesForCurrentWeek = this.gameRepository.GetGames(week);

            ////If no games exist for the current week, send an error message indicating that none were found.
            if (gamesForCurrentWeek.Count() == 0)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotFound, string.Format(CultureInfo.CurrentCulture, "No games exist for week {0}.  Please enter a valid week for which games can be returned.", week));
            }

            ////Convert Game instances into Game DTO instances.
            gameDtos = this.gameTypeMapper.GetEntityDtos(gamesForCurrentWeek, new UrlHelper(requestMessage));

            ////Returns Game DTO instances in HTTP response message.
            return requestMessage.CreateResponse(HttpStatusCode.OK, gameDtos);
        }

        /// <summary>
        /// Action method that processes GET requests which retrieve a game by its supplied id <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Unique id of the game to return.</param>
        /// <param name="requestMessage">HTTP request message.</param>
        /// <returns>Http response message containing a game DTO instance representing the game being returned.</returns>
        public HttpResponseMessage Get(int id, HttpRequestMessage requestMessage)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            GameDto gameDto = null;

            ////Get games for current week.
            Game game = this.gameRepository.GetById(id);

            ////If no games exist for the current week, send an error message indicating that none were found.
            if (game == null)
            {
                requestMessage.CreateErrorResponse(HttpStatusCode.NotFound, "The specified game could not be found.");
            }

            //////Returns DTO instance for Game instance.
            gameDto = this.gameTypeMapper.GetEntityDto(game, GamesController.GetGameUri(requestMessage, game.Id));

            ////Returns Game DTO instance in HTTP response message.
            return requestMessage.CreateResponse(HttpStatusCode.OK, gameDto);
        }

        /// <summary>
        /// Processes POST requests that save and create a game in the persistence store based on information contained in the supplied game DTO instance <paramref name="gameRequest"/>.
        /// </summary>
        /// <param name="gameRequest">Game request instance containing information needed to save a game.</param>
        /// <param name="requestMessage">HTTP request message representing the POST operation call.</param>
        /// <returns>HTTP response message containing the Game object that was created, or an error message in case of a failure.</returns>
        [EmptyParameterFilterAttribute("gameRequest")]
        public HttpResponseMessage Post(GameBaseRequestModel gameRequest, HttpRequestMessage requestMessage)
        {
            if (gameRequest == null)
            {
                throw new ArgumentNullException("gameDto", "gameDto cannot be null.");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            ////Get GameSnapshot instance from GameRequest instance.
            GameSnapshot game = this.gameTypeMapper.GetGameSnapshot(gameRequest);

            ////Save game in persistence store which generates new game instance in persistence store with new id.
            ServiceOperationResult<Game> operationResult = this.gameService.SaveGame(game);

            if (operationResult.OperationResult == OperationResult.Failed)
            {
                ////TODO: Send back message indicating operation failed and why....perhaps don't include all detail in request.  Detail must be logged however.
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotFound, string.Format(CultureInfo.CurrentCulture, "Game could not be created due to the following issues:\n{0}", operationResult.GetBrokenRulesSummary()));
            }

            ////Get GameDto instance from Game instance.
            GameDto savedGameDto = this.gameTypeMapper.GetEntityDto(operationResult.Entity, GamesController.GetGameUri(requestMessage, operationResult.Entity.Id));

            ////Return HTTP response with status code indicating Game resource was created along with resource itself.
            HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.Created, savedGameDto);
            response.Headers.Add("Location", savedGameDto.HypermediaLinks.Single(link => link.Rel == "self").Href);

            return response;
        }

        /// <summary>
        /// Processes PUT requests that update a game in the persistence store with the suppied game id <paramref name="gameId"/> with information contained in the game request <paramref name="gameRequest"/>.
        /// </summary>
        /// <param name="gameId">Unique id of the game that is to be updated.</param>
        /// <param name="gameRequest">Game request instance containing information needed to update the game.</param>
        /// <param name="requestMessage">HTTP request message representing the PUT operation call.</param>
        /// <returns>HTTP response message containing the Game object that was updated, or an error message in case of a failure.</returns>
        [EmptyParameterFilterAttribute("gameRequest")]
        public HttpResponseMessage Put(int Id, GameBaseRequestModel gameRequest, HttpRequestMessage requestMessage)
        {
            if (gameRequest == null)
            {
                throw new ArgumentNullException("gameRequest", "gameRequest cannot be null.");
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage", "requestMessage cannot be null.");
            }

            ////Get GameSnapshot instance from GameRequest instance.
            GameSnapshot game = this.gameTypeMapper.GetGameSnapshot(gameRequest);

            ////Update game in the persistence store.
            ServiceOperationResult<Game> operationResult = this.gameService.UpdateGame(Id, game);
            if (operationResult.OperationResult == OperationResult.Failed)
            {
                ////TODO: Determine if all of this information should be returned to the caller.  Once approach is finalized, update the POST method accordingly.  Message has to be logged.
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotFound, string.Format(CultureInfo.CurrentCulture, "Game could not be saved due to the following issues:\n{0}", operationResult.GetBrokenRulesSummary()));
            }

            ////Get GameDto instance from updated Game instance.
            GameDto updatedGameDto =  this.gameTypeMapper.GetEntityDto(operationResult.Entity, GamesController.GetGameUri(requestMessage, operationResult.Entity.Id));

            ////Return HTTP response with status code indicating Game resource was created along with resource itself.
            HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, updatedGameDto);

            return response;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the URI of a game according to information contained in the supplied requestMessage <paramref name="requestMessage"/> and the unique id of the game <paramref name="gameId"/>.
        /// </summary>
        /// <param name="requestMessage">Request message containing information needed to create the game URI.</param>
        /// <param name="gameId">Unique id of a game.</param>
        /// <returns>URI for the game.</returns>
        private static string GetGameUri(HttpRequestMessage requestMessage, int gameId)
        {
            return new UrlHelper(requestMessage).Link("DefaultApi", new { id = gameId });
        }

        #endregion
    }
}
