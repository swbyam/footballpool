//-----------------------------------------------------------------------
// <copyright file="IGameService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain;
    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Snapshots;

    /// <summary>
    /// Type of team in the context of a game.
    /// </summary>
    public enum GameTeamType
    {
        /// <summary>
        /// Team is home team of a game.
        /// </summary>
        Home,

        /// <summary>
        /// Team is visiting team of a game.
        /// </summary>
        Visiting,

        /// <summary>
        /// Team is favorite team of a game.
        /// </summary>
        Favorite
    }

    /// <summary>
    /// Abstraction for service that provides business-related functionality related to games.
    /// </summary>
    public interface IGameService
    {
        #region Methods

        /// <summary>
        /// Saves a game in the persistence store based on information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot containing information needed to save a game instance to the persistence store.</param>
        /// <returns>Result of save operation.</returns>
        ServiceOperationResult<Game> SaveGame(GameSnapshot gameSnapshot);

        /// <summary>
        /// Updates a game that currently exists in the persistence store with information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot containing information needed to update a game in the persistence store.</param>
        /// <param name="gameId">Unique id of the game that is to be updated.</param>
        /// <returns>Result of update operation.</returns>
        ServiceOperationResult<Game> UpdateGame(int gameId, GameSnapshot gameSnapshot);

        /// <summary>
        /// Determines whether or not the favored team is actually part of the game i.e. the favored team is either the home or visiting team in the game.
        /// </summary>
        /// <param name="game">Game on which to verify the favored team is part of it.</param>
        /// <returns>True if the favored team is part of the game.  Otherwise, false.</returns>
        bool IsFavoredTeamIsPartOfGame(GameSnapshot game);

        /// <summary>
        /// Determines whether or not the home and visiting teams of the supplied <paramref name="game"/> are not the same team.
        /// </summary>
        /// <param name="game">Game on which to verify the home and visiting teams are not the same.</param>
        /// <returns>True if the home and visiting teams are different teams.  Otherwise, false.</returns>
        bool AreHomeAndVisitingTeamsDifferent(GameSnapshot game);

        #endregion
    }
}
