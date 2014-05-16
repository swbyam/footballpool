//-----------------------------------------------------------------------
// <copyright file="Bet.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents bet placed on a team against a point spread.
    /// </summary>
    public class Bet : PickEmBet
    {
        #region Properties

        /// <summary>
        /// Gets or sets the the number of points taken in the bet for the team picked to win or cover the bet.
        /// </summary>
        public virtual float Points { get; set; }

        #endregion
    }
}
