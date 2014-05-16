//-----------------------------------------------------------------------
// <copyright file="PointSpreadBet.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents a bet placed on a team against the point spread of a game.
    /// </summary>
    public class PointSpreadBet : PickEmBet
    {
        #region Properties

        /// <summary>
        /// Gets or sets the points obtained in the bet for the team picked to cover the bet.
        /// </summary>
        public float Points { get; set; }

        #endregion
    }
}
