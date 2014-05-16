//-----------------------------------------------------------------------
// <copyright file="Pool.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class represents a pool that contains pool users competing against each other making picks on football games.
    /// </summary>
    public class Pool : Entity<int>
    {
        #region Constructors

        public Pool()
        {
            //this.poolUsers = new List<PoolUser>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the pool.
        /// </summary>
        public virtual string PoolName { get; set; }

        /// <summary>
        /// Gets or sets a collection of pool users that belong to the pool.
        /// </summary>
        //public virtual ICollection<PoolUser> PoolUsers { get; set; }

        #endregion
    }
}
