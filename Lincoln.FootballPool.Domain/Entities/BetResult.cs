//-----------------------------------------------------------------------
// <copyright file="BetResult.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents the result of evaluating a bet.  Information it contains include whether the bet was won or lost and the amount of money won or lost on the bet.
    /// </summary>
    public class BetResult
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the bet result.
        /// </summary>
        public int BetResultId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the bet was won.
        /// </summary>
        public bool BetWon { get; set; }

        /// <summary>
        /// Gets or sets the amount of money won or lost on the bet depending on whether the bet was won or lost.
        /// </summary>
        public int AmountWonLost { get; set; }

        /// <summary>
        /// Gets or sets the bet that was evaluated and generated the bet result.
        /// </summary>
        public Bet Bet { get; set; }

        #endregion
    }
}
