//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents game or athletic contest played between a home team and a visiting team.
    /// </summary>
    public class Game : Entity<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the home team of the game.
        /// </summary>
        public virtual Team HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the visiting team of the game.
        /// </summary>
        public virtual Team VisitingTeam { get; set; }

        /// <summary>
        /// Gets or sets the team favored to win the game.
        /// </summary>
        public virtual Team FavoriteTeam { get; set; }

        /// <summary>
        /// Gets or sets the date and start time of the game.
        /// </summary>
        public virtual DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the week number in which the game is played.
        /// </summary>
        public virtual int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the betting line of the game.
        /// </summary>
        public virtual float Line { get; set; }

        /// <summary>
        /// Gets or sets the Over/Under of the game.
        /// </summary>
        public virtual float OverUnder { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override of the ToString method that returns a textual description of the game in the format: [Visiting Team] @ [Home Team].
        /// </summary>
        /// <returns>String containing description of game.</returns>
        public override string ToString()
        {
            return this.VisitingTeam.FullName + " @ " + this.HomeTeam.FullName;
        }

        #endregion
    }
}
