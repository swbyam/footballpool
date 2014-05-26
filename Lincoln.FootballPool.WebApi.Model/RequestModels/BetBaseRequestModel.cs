//-----------------------------------------------------------------------
// <copyright file="BetBaseRequestModel.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    /// <summary>
    /// Request model class that contains information needed to create new instances of bets via web service calls.
    /// </summary>
    /// <remarks>This class is intended for requests that create new instances of the <see cref="Bet "/> class sent with the POST HTTP verb where the game has not yet been assigned a bet ID.</remarks>
    public class BetBaseRequestModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the game upon which the bet was placed.
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "GameId must be greater than 0.")]
        public int? GameId { get; set; }

        [Required]
        [Min(1, ErrorMessage = "Amount must be greater than 0.")]
        /// <summary>
        /// Gets or sets the amount placed on the bet.
        /// </summary>
        public int? Amount { get; set; }

        [Required]
        [Min(1, ErrorMessage = "TeamToCoverBetId must be greater than 0.")]
        /// <summary>
        /// Gets or sets the id of the team picked to cover the bet.
        /// </summary>
        public int? TeamToCoverBetId { get; set; }

        [Required]
        [Min(1, ErrorMessage = "Points must be greater than 0.")]
        /// <summary>
        /// Gets or sets the the number of points taken in the bet for the team picked to win or cover the bet.
        /// </summary>
        public float? Points { get; set; }

        #endregion
    }
}
