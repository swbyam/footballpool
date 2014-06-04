//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for repository that provides basic persistence-related operations against a persistence store for a particular type that has a particular type of id.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TEntityId">Type of the entity's id.</typeparam>
    public interface IRepository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        #region Methods

        /// <summary>
        /// Retrieves an entity with the supplied <paramref name="id"/> from the persistence store.
        /// </summary>
        /// <param name="gameId">Unique id of the entity to retrieve.</param>
        /// <returns>Entity with the specified id.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving the entity from the persistence store.</exception>
        TEntity GetById(TEntityId id);

        /// <summary>
        /// Retrieves all entities from the persistence store.
        /// </summary>
        /// <returns>List of entities.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving entities from the persistence store.</exception>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Retrieves all entities from the persistence store as a paginated list based on the supplied 
        /// </summary>
        /// <param name="pagingInfo">Paging info containing information needed to create the paged the result set.</param>
        /// <returns>Paginated list of entities.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving a paginated list of entities from the persistence store.</exception>
        PaginatedList<TEntity, TEntityId> GetAllAsPaginatedList(PagingInfo pagingInfo);

        /// <summary>
        /// Updates the supplied <paramref name="entity"/> in the persistence store.
        /// </summary>
        /// <param name="entity">Entity to update in the persistence store.</param>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.ConcurrencyException">A concurrency error occurred saving the entity as it was already updated by another transaction.</exception>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred saving the entity to the persistence store.</exception>
        TEntity Save(TEntity entity);

        #endregion
    }
}
