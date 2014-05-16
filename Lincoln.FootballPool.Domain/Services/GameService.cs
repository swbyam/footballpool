//-----------------------------------------------------------------------
// <copyright file="GameService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    public class GameService : IGameService
    {
        #region Public Methods

        /// <summary>
        /// Determines whether or not the favored team is actually part of the game i.e. the favored team is either the home or visiting team in the game.
        /// </summary>
        /// <param name="game">Game on which to verify the favored team is part of it.</param>
        /// <returns>True if the favored team is part of the game.  Otherwise, false.</returns>
        public bool IsFavoredTeamIsPartOfGame(GameSnapshot game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game", "game cannot be null.");
            }

            return game.FavoriteTeamId == game.HomeTeamId || game.FavoriteTeamId == game.VisitingTeamId;
        }

        /// <summary>
        /// Determines whether or not the home and visiting teams of the supplied <paramref name="game"/> are not the same team.
        /// </summary>
        /// <param name="game">Game on which to verify the home and visiting teams are not the same.</param>
        /// <returns>True if the home and visiting teams are different teams.  Otherwise, false.</returns>
        public bool AreHomeAndVisitingTeamsDifferent(GameSnapshot game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game", "game cannot be null.");
            }

            return game.HomeTeamId != game.VisitingTeamId;
        }

        #endregion
    }
}
