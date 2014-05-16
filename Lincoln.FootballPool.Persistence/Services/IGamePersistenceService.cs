//-----------------------------------------------------------------------
// <copyright file="IGamePersistenceService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    public interface IGamePersistenceService
    {
        #region Methods

        /// <summary>
        /// Saves a game in the persistence store based on information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot containing information needed to save a game instance to the persistence store.</param>
        /// <returns>Result of save operation.</returns>
        PersistenceOperationResult<Game> SaveGame(GameSnapshot gameSnapshot);

        /// <summary>
        /// Updates a game that currently exists in the persistence store with information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot containing information needed to update a game in the persistence store.</param>
        /// <param name="gameId">Unique id of the game that is to be updated.</param>
        /// <returns>Result of update operation.</returns>
        PersistenceOperationResult<Game> UpdateGame(int gameId, GameSnapshot gameSnapshot);

        #endregion
    }
}
