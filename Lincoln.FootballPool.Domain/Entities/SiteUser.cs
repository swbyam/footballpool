//-----------------------------------------------------------------------
// <copyright file="SiteUser.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents user that has registered with the football site.  A site user may have one or more pool users assosciated with it that each belong to a single football pool.
    /// </summary>
    public class SiteUser : Entity<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user name of the site user.
        /// </summary>
        public int UserName { get; set; }

        #endregion
    }
}
