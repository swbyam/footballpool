//-----------------------------------------------------------------------
// <copyright file="GameSnapshot.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Snapshots
{
    using System;

    /// <summary>
    /// Snapshot class that contains an abbreviated set of properties for a <see cref="Game"/> instance.
    /// </summary>

    public class GameSnapshot
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the game upon which the snapshot is based.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the home team participating in the game.
        /// </summary>
        public int HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the visiting team participating in the game.
        /// </summary>
        public int VisitingTeamId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the visiting team participating in the game.
        /// </summary>
        public int FavoriteTeamId { get; set; }

        /// <summary>
        /// Gets or sets the date and start time of the game.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the week number in which the game is played.
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the betting line of the game.
        /// </summary>
        public float Line { get; set; }

        /// <summary>
        /// Gets or sets the Over/Under of the game.
        /// </summary>
        public float OverUnder { get; set; }

        #endregion
    }
}
