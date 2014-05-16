//-----------------------------------------------------------------------
// <copyright file="BetDto.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.Dtos
{
    using System;

    /// <summary>
    /// Data transfer object that encapsulates information related to a bet placed against the point spread of a game.
    /// </summary>
    public class BetDto : DtoBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the bet.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the game against which the bet was placed.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the amount of money wagered on the bet.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the id of the pool user whom placed the bet.
        /// </summary>
        public int PlacedById { get; set; }

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
