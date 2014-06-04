//-----------------------------------------------------------------------
// <copyright file="IGameTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;

    /// <summary>
    /// Abstraction for mapping class that converts instances of the domain class <see cref="Game"/> to its corresponding Data Transfer Object (DTO) implementation: <see cref="GameDto"/>.
    /// </summary>
    /// <remarks>The resulting <paramref name="GameDto"/> instance is suitable for use in building RESTful web services as it contains hypermedia links that are used to represent current state per the (Hypermedia as the Engine of Application State) HATEOAS paradigm.</remarks>
    public interface IGameTypeMapper : IEntityTypeMapper<GameDto, Game, int>
    {
        #region Methods

        /// <summary>
        /// Maps the supplied Game Request Model instance <paramref name="gameRequestModel"/> into an instance o the domain model class <see cref="GameSnapshot"/>.
        /// </summary>
        /// <param name="gameRequestModel">Game request model to convert.</param>
        /// <returns>GameSnapshot domain model instance.</returns>
        GameSnapshot GetGameSnapshot(GameBaseRequestModel gameRequestModel);

        /// <summary>
        /// Maps the supplied Game Request Model instance <paramref name="gameRequestModel"/> into an instance o the domain model class <see cref="Game"/>.
        /// </summary>
        /// <param name="gameRequestModel">Game request model to convert.</param>
        /// <returns>Game domain model instance.</returns>
        Game GetGame(GameBaseRequestModel gameRequestModel);

        #endregion
    }
}
