//-----------------------------------------------------------------------
// <copyright file="PoolUser.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Class represents a user in a football pool.
    /// </summary>
    public class PoolUser : Entity<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user name of a pool user.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the pool to which the user belongs.
        /// </summary>
        public virtual Pool Pool { get; set; }

        #endregion
    }
}
