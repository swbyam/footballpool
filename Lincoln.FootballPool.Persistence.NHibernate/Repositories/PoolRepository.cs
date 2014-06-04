//-----------------------------------------------------------------------
// <copyright file="PoolRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.Repositories
{
    using System;
    using System.Collections.Generic;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Exceptions;

    /// <summary>
    /// Pool repository that provides basic CRUD functionality against a database including retrieving, creating, updating, and deleting pools.
    /// </summary>
    public class PoolRepository : Repository<Pool>, IPoolRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolRepository"/> class.
        /// </summary>
        /// <param name="session">NHibernate ISession implementation representing connection between objects and the database.</param>
        public PoolRepository(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a pool with the supplied <paramref name="poolId"/> from the database.
        /// </summary>
        /// <param name="gameId">Unique id of the pool to retrieve.</param>
        /// <returns>Pool with the specified pool id.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving the entity from the database.</exception>
        ////public Pool GetPool(int poolId)
        ////{
        ////    return base.GetById(poolId);
        ////}

        #endregion
    }
}
