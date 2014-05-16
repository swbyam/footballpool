//-----------------------------------------------------------------------
// <copyright file="IPoolRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Repositories
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for Pool repository that provides basic CRUD functionality against a persistence store including retrieving, creating, updating, and deleting pools.
    /// </summary>
    public interface IPoolRepository : IRepository<Pool, int>
    {
        #region Methods

        /// <summary>
        /// Retrieves a pool with the supplied <paramref name="poolId"/> from the persistence store.
        /// </summary>
        /// <param name="gameId">Unique id of the pool to retrieve.</param>
        /// <returns>Pool with the specified pool id.</returns>
        //Pool GetPool(int poolId);

        #endregion
    }
}
