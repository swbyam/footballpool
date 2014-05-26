//-----------------------------------------------------------------------
// <copyright file="GameTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.WebApi.Model;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;

    /// <summary>
    /// Mapping class that converts instances of the domain class <see cref="Game"/> to its corresponding Data Transfer Object (DTO) implementation: <see cref="GameDto"/>.
    /// </summary>
    /// <remarks>The resulting <paramref name="GameDto"/> instance is suitable for use in building RESTful web services as it contains hypermedia links that are used to represent current state per the (Hypermedia as the Engine of Application State) HATEOAS paradigm.</remarks>
    public class GameTypeMapper : EntityTypeMapper<GameDto, Game, int>, IGameTypeMapper
    {
        #region Public Methods

        /// <summary>
        /// Maps the supplied <paramref name="game"/> instance to its <see cref="GameDto"/> counterpart.
        /// </summary>
        /// <param name="game">Game to convert.</param>
        /// <param name="gameUri">Uri of the game that is to be converted to its DTO counterpart.</param>
        /// <returns>Game DTO instance.</returns>
        //public GameDto GetGameDto(Game game, string gameUri)
        //{
        //    if (game == null)
        //    {
        //        throw new ArgumentNullException("game", "game cannot be null.");
        //    }

        //    if (string.IsNullOrWhiteSpace(gameUri))
        //    {
        //        throw new ArgumentException("gameUri cannot be null or empty string", "gameUri");
        //    }

        //    ////Map Game domain instance to GameDto instance.
        //    GameDto gameDto = Mapper.Map<Game, GameDto>(game);
            
        //    ////Add hypermedia links.
        //    gameDto.AddHypermediaLink(new Link
        //    {
        //        Title = "self",
        //        Rel = "self",
        //        Href = gameUri
        //    });
        //    gameDto.AddHypermediaLink(new Link
        //    {
        //        Title = "HomeTeamName",
        //        Rel = "hometeam",
        //        Href = gameUri + "/hometeam"
        //    });
        //    gameDto.AddHypermediaLink(new Link
        //    {
        //        Title = "VisitingTeamName",
        //        Rel = "visitingteam",
        //        Href = gameUri + "/visitingteam"
        //    });
        //    gameDto.AddHypermediaLink(new Link
        //    {
        //        Title = "FavoriteTeamName",
        //        Rel = "favoriteteam",
        //        Href = gameUri + "/favoriteteam"
        //    });

        //    return gameDto;
        //}

        /// <summary>
        /// Maps the supplied list of games <paramref name="games"/> into a list of their <see cref="GameDto"/> counterparts.
        /// </summary>
        /// <param name="games">List of games to convert.</param>
        /// <param name="urlHelper">Url helper used to assist in creating DTO's for each of the supplied games.</param>
        /// <returns>List of game DTO instances.</returns>
        //public IEnumerable<GameDto> GetGameDtos(IEnumerable<Game> games, UrlHelper urlHelper)
        //{
        //    if (games == null)
        //    {
        //        throw new ArgumentNullException("games", "games cannot be null.");
        //    }

        //    if (urlHelper == null)
        //    {
        //        throw new ArgumentNullException("urlHelper", "urlHelper cannot be null.");
        //    }

        //    return games.Select(game => this.GetEntityDto(game, urlHelper.Link("DefaultApi", new { id = game.Id })));
        //}

        /// <summary>
        /// Maps the supplied Game Request Model instance <paramref name="gameRequestModel"/> into an instance o the domain model class <see cref="GameSnapshot"/>.
        /// </summary>
        /// <param name="gameRequestModel">Game request model to convert.</param>
        /// <returns>GameSnapshot domain model instance.</returns>
        public GameSnapshot GetGameSnapshot(GameBaseRequestModel gameRequestModel)
        {
            if (gameRequestModel == null)
            {
                throw new ArgumentNullException("gameRequestModel", "gameRequestModel cannot be null.");
            }

            return Mapper.Map<GameBaseRequestModel, GameSnapshot>(gameRequestModel);
        }

        /// <summary>
        /// Maps the supplied Game Request Model instance <paramref name="gameRequestModel"/> into an instance o the domain model class <see cref="Game"/>.
        /// </summary>
        /// <param name="gameRequestModel">Game request model to convert.</param>
        /// <returns>Game domain model instance.</returns>
        public Game GetGame(GameBaseRequestModel gameRequestModel)
        {
            if (gameRequestModel == null)
            {
                throw new ArgumentNullException("gameRequestModel", "gameRequestModel cannot be null.");
            }

            return Mapper.Map<GameBaseRequestModel, Game>(gameRequestModel);
        }

        /// <summary>
        /// Maps the supplied Game DTO instance <paramref name="gameDto"/> into an instance the domain model entity <see cref="Game"/>.
        /// </summary>
        /// <param name="gameDto">Game DTO instance to convert.</param>
        /// <returns>Game domain model instance.</returns>
        //public Game GetGameSnapshot(GameDto gameDto)
        //{
        //    if (gameDto == null)
        //    {
        //        throw new ArgumentNullException("gameDto", "gameDto cannot be null.");
        //    }

        //    return new Game()
        //    {
        //        Id = gameDto.Id,
        //        HomeTeam = new Team()
        //        {
        //            Id = gameDto.HomeTeamId,
        //            City = gameDto.HomeTeamCity,
        //            Name = gameDto.HomeTeamName
        //        },
        //        VisitingTeam = new Team()
        //        {
        //            Id = gameDto.VisitingTeamId,
        //            City = gameDto.VisitingTeamCity,
        //            Name = gameDto.VisitingTeamName
        //        },
        //        FavoriteTeam = new Team()
        //        {
        //            Id = gameDto.FavoriteTeamId,
        //            City = gameDto.FavoriteTeamCity,
        //            Name = gameDto.FavoriteTeamName
        //        },
        //        StartDateTime = gameDto.StartDateTime,
        //        WeekNumber = gameDto.WeekNumber,
        //        Line = gameDto.Line,
        //        OverUnder = gameDto.OverUnder
        //    };
        //}

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates and returns a list of hypermedia links related to a bet based on information contained in the supplied game <paramref name="entity"/> and the uri to the game resource <paramref name="entityUri"/>.
        /// </summary>
        /// <param name="entity">Game containing information needed to create the hypermedia links.</param>
        /// <param name="entityUri">Uri to a game resource.</param>
        /// <returns>List of hypermedia links.</returns>
        protected override IEnumerable<Link> CreateHypermediaLinks(Game entity, string entityUri)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(entityUri))
            {
                throw new ArgumentException("entityUri cannot be null or empty string", "entityUri");
            }

            List<Link> hypermediaLinks = new List<Link>();

            ////Add hypermedia links.

            ////Link to game resource itself.
            hypermediaLinks.Add(new Link
            {
                Title = "self",
                Rel = "self",
                Href = entityUri
            });

            ////Link to the home team resource of the game.
            hypermediaLinks.Add(new Link
            {
                Title = "HomeTeamName",
                Rel = "hometeam",
                Href = entityUri + "/hometeam"
            });

            ////Link to the visiting team resource of the game.
            hypermediaLinks.Add(new Link
            {
                Title = "VisitingTeamName",
                Rel = "visitingteam",
                Href = entityUri + "/visitingteam"
            });

            ////Link to the favorite team resource of the game.
            hypermediaLinks.Add(new Link
            {
                Title = "FavoriteTeamName",
                Rel = "favoriteteam",
                Href = entityUri + "/favoriteteam"
            });

            return hypermediaLinks;
        }

        #endregion
    }
}