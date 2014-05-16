//-----------------------------------------------------------------------
// <copyright file="GameRequestModel.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Request model class that contains information needed to modify existing instances of games.
    /// </summary>
    /// <remarks>Existing game instances have a unique id (GameId) which is needed when updating them via PUT or PATCH requests.</remarks>
    public class GameRequestModel : GameBaseRequestModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of a game.
        /// </summary>
        [Required]
        public int? GameId { get; set; }

        #endregion
    }
}
