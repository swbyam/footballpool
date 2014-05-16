//-----------------------------------------------------------------------
// <copyright file="PickEmBet.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Bet placed on a game where the bet is evaluated according to whether or not the team on which the bet is placed won or lost the game.
    /// </summary>
    public class PickEmBet : BetBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the team picked to win the game.
        /// </summary>
        public virtual Team TeamToCoverBet { get; set; }

        #endregion
    }
}
