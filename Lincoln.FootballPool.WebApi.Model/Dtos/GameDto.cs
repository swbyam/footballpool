//-----------------------------------------------------------------------
// <copyright file="GameDto.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.Dtos
{
    using System;

    /// <summary>
    /// Data transfer object that encapsulates information related to a football game.
    /// </summary>
    public class GameDto : DtoBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city of the home team of the game.
        /// </summary>
        public string HomeTeamCity { get; set; }

        /// <summary>
        /// Gets or sets the name of the home team of the game.
        /// </summary>
        public string HomeTeamName { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the home team of the game.
        /// </summary>
        public int HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the city of the visiting team of the game.
        /// </summary>
        public string VisitingTeamCity { get; set; }

        /// <summary>
        /// Gets or sets the name of the visiting team of the game.
        /// </summary>
        public string VisitingTeamName { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the visiting team of the game.
        /// </summary>
        public int VisitingTeamId { get; set; }

        /// <summary>
        /// Gets or sets the city of the team favored to win the game.
        /// </summary>
        public string FavoriteTeamCity { get; set; }

        /// <summary>
        /// Gets or sets the name of the team favored to win the game.
        /// </summary>
        public string FavoriteTeamName { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the team favored to win the game.
        /// </summary>
        public int FavoriteTeamId { get; set; }

        /// <summary>
        /// Gets or sets the date and start time of the game.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the week number in which the game is played.
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the betting line of the game.
        /// </summary>
        public float Line { get; set; }

        /// <summary>
        /// Gets or sets the Over/Under of the game.
        /// </summary>
        public float OverUnder { get; set; }

        /// <summary>
        /// Gets or sets the version of the game.
        /// </summary>
        public byte[] Version { get; set; }

        #endregion

    }
}