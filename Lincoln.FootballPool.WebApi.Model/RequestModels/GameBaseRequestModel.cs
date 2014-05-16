//-----------------------------------------------------------------------
// <copyright file="GameBaseRequestModel.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    /// <summary>
    /// Request model class that contains information needed to create new instances of games via web service calls.
    /// </summary>
    /// <remarks>This class is intended for requests that create new instances of Game objects sent with the POST HTTP verb where the game has not yet been assigned a game ID.</remarks>
    public class GameBaseRequestModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the home team of a game.
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "HomeTeamId must be greater than 0.")]
        public int? HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the visiting team of a game.
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "VisitingTeamId must be greater than 0.")]
        public int? VisitingTeamId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the team favored to win the game.
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "FavoriteTeamId must be greater than 0.")]
        public int? FavoriteTeamId { get; set; }

        /// <summary>
        /// Gets or sets the date and start time of a game.
        /// </summary>
        [Required]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the week number in which a game is played.
        /// </summary>
        [Required]
        [Range(1, 17, ErrorMessage = "Week number must be between 1 and 17.")]
        public int? WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the betting line of a game.
        /// </summary>
        [Required]
        [Min(0, ErrorMessage = "Line must be greater than 0.")]
        public float? Line { get; set; }

        /// <summary>
        /// Gets or sets the Over/Under of a game.
        /// </summary>
        [Required]
        [Min(0, ErrorMessage = "Over/under must be greater than 0.")]
        public float? OverUnder { get; set; }

        #endregion
    }
}
