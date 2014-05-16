//-----------------------------------------------------------------------
// <copyright file="IGameRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for Game repository that provides basic CRUD functionality against a persistence store including retrieving, creating, updating, and deleting games.
    /// </summary>
    public interface IGameRepository : IRepository<Game, int>
    {
        #region Methods

        /// <summary>
        /// Returns games for the supplied week number <paramref name="week"/> from the persistence store.
        /// </summary>
        /// <param name="week">Week number for which games are to be returned.</param>
        /// <returns>List of games for the week specified.</returns>
        IEnumerable<Game> GetGames(int week);

        /// <summary>
        /// Retrieves a game that takes place between the supplied home team <paramref name="homeTeam"/> and visiting team <paramref name="visitingTeam"/> from the persistence store.
        /// </summary>
        /// <param name="homeTeam">Home team of the game.</param>
        /// <param name="visitingTeam">Visiting team of the game.</param>
        /// <returns>Game between home and visiting team.  If game cannot be found, null is returned.</returns>
        /// <remarks>This database retrieval operation makes assumptions on which team is the home team and which is the visiting team based on supplied parameters.</remarks>
        Game GetGame(Team homeTeam, Team visitingTeam);

        /// <summary>
        /// Retrieves a game that takes place during the supplied week number <paramref name="weekNumber"/> between the two supplied teams: <paramref name="team1"/> and <paramref name="team2"/> from the persistence store.
        /// </summary>
        ///<param name="weekNumber">Week number of the game.</param>
        ///<param name="team1">First team of the game.</param>
        ///<param name="team1">Second team of the game.</param>
        /// <returns>Game that occurs during the specified week number between the supplied teams.  If game cannot be found, null is returned.</returns>
        /// <exception cref="Lincoln.FootballPool.Persistence.PersistenceException">An error occurred retrieving the game from the persistence store.</exception>
        /// <remarks>This operation does not assume which team is the home team and which team is the away team.  It simply returns a game that takes place between the 2 teams for the given week.</remarks>
        Game GetGame(int weekNumber, Team team1, Team team2);

        /// <summary>
        /// Saves the supplied game to the persistence store.  If the game does not already exist in the persistence store, it will be created.
        /// </summary>
        /// <param name="game">Game to be saved.</param>
        /// <returns>Game that was saved to the persistence store.</returns>
        Game SaveGame(Game game);

        #endregion
    }
}
