//-----------------------------------------------------------------------
// <copyright file="BetSnapshot.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Snapshots
{
    using System;

    /// <summary>
    /// Snapshot class that contains an abbreviated set of properties for a <see cref="Bet"/> instance.
    /// </summary>
    public class BetSnapshot
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the game upon which the bet was placed.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the amount placed on the bet.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the id of the team picked to cover the bet.
        /// </summary>
        public int TeamToCoverBetId { get; set; }

        /// <summary>
        /// Gets or sets the the number of points taken in the bet for the team picked to win or cover the bet.
        /// </summary>
        public float Points { get; set; }

        #endregion
    }
}
