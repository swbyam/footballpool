//-----------------------------------------------------------------------
// <copyright file="BetBase.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Base class of a bet made against a game.
    /// </summary>
    public abstract class BetBase : Entity<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the game upon which the bet has been placed.
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Gets or sets the amount placed on the bet.
        /// </summary>
        public virtual int Amount { get; set; }

        /// <summary>
        /// Gets or sets the pool user whom placed the bet.
        /// </summary>
        public virtual PoolUser PlacedBy { get; set; }

        #endregion
    }
}
