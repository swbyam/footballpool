//-----------------------------------------------------------------------
// <copyright file="Team.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents a team that plays in a game.
    /// </summary>
    public class Team : Entity<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the city in which the team plays.
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Gets the full name of the team which includes the city followed by the name of the team i.e. New England Patriots etc.
        /// </summary>
        public virtual string FullName 
        {
            get
            {
                return this.City + " " + this.Name;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override of Object.Equals method which determines whether or not the supplied obj has the same team id as the team id of this instance (assuming the supplied object is an instance of <see cref="Team"/>).
        /// </summary>
        /// <param name="obj">Object to compare with the current Team instance.</param>
        /// <returns>True if the team id of the objects are the same.  Otherwise, false.</returns>
        public virtual bool Equals(Object obj)
        {
            if (obj == null || !(obj is Team))
            {
                return false;
            }

            return this.Id == ((Game)obj).Id;
        }

        /// <summary>
        /// Override of Object.Equals method which determines whether or not the supplied <paramref name="team"/> has the same team id as the team id of this instance.
        /// </summary>
        /// <param name="team">Team to compare with the current Team instance.</param>
        /// <returns>True if the team id of the teams are the same.  Otherwise, false.</returns>
        public virtual bool Equals(Team team)
        {
            if (team == null)
            {
                return false;
            }

            return this.Id == team.Id;
        }

        /// <summary>
        /// Override of GetHashCode that serves as hash function for type.
        /// </summary>
        /// <returns>Hash value for type instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
