//-----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Exceptions;
    using NHibernate.Linq;

    /// <summary>
    /// Repository abstract base class that utilizes NHibernate ORM tool for providing basic database persistence related operations that all derived classes can leverage.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity upon which repository operations are based.</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity, int>
        where TEntity : Entity<int>, new()
    {
        #region Member Variables

        /// <summary>
        /// NHibernate session instance.
        /// </summary>
        private ISession session;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="session">NHibernate session instance.</param>
        protected Repository(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session", "session cannot be null.");
            }

            this.session = session;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the NHibernate Session instance.
        /// </summary>
        protected ISession Session
        {
            get { return this.session; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves an entity with the supplied <paramref name="id"/> from the database.
        /// </summary>
        /// <param name="gameId">Unique id of the entity to retrieve.</param>
        /// <returns>Entity with the specified id.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving the entity from the database.</exception>
        public TEntity GetById(int id)
        {
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    TEntity entity = this.session
                        .QueryOver<TEntity>()
                        .Where(ent => ent.Id == id).SingleOrDefault();

                    transaction.Commit();

                    return entity;
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException(string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving an entity with id {0}", id), hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Retrieves all entities from the database.
        /// </summary>
        /// <returns>List of entities.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving entities from the database.</exception>
        public IEnumerable<TEntity> GetAll()
        {
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    return this.session.QueryOver<TEntity>().List<TEntity>();
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException("An error occurred retrieving entities from the database.", hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Retrieves all entities from the database as a paginated list based on the supplied 
        /// </summary>
        /// <param name="pageNumber">Page number of the result set to be returned.</param>
        /// <param name="pageSize">Number entities to be returned per page.</param>
        /// <returns>Paginated list of entities.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving a paginated list of entities from the database.</exception>
        public PaginatedList<TEntity, int> GetAllAsPaginatedList(PagingInfo pagingInfo)
        {
            if (pagingInfo == null)
            {
                throw new ArgumentNullException("pagingInfo", "pagingInfo cannot be null.");
            }

            int totalCount = 0;

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    IQueryOver<TEntity> rowCount = this.session.QueryOver<TEntity>().ToRowCountQuery();
                    IQueryOver<TEntity> result = this.Session.QueryOver<TEntity>()
                                                          .OrderBy(entity => entity.Id).Asc
                                                          .Take(pagingInfo.PageSize)
                                                          .Skip(pagingInfo.PageSize);

                    totalCount = rowCount.FutureValue<int>().Value;

                    transaction.Commit();

                    return new PaginatedList<TEntity, int>(result.Future<TEntity>(), totalCount, pagingInfo.PageNumber, pagingInfo.PageSize, pagingInfo.SortField);
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException("An error occurred retrieving a paginated list of entities", hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Updates the supplied <paramref name="entity"/> in the persistence store.
        /// </summary>
        /// <param name="entity">Entity to update in the persistence store.</param>
        /// <returns>Entity instance that was saved.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.ConcurrencyException">A concurrency error occurred saving the entity as it was already updated by another transaction.</exception>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred saving the entity to the database.</exception>
        public TEntity Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null.");
            }

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    ////Save entity in database.
                    this.session.Save(entity);

                    ////Commit transaction.
                    transaction.Commit();

                    return entity;
                }
                catch (StaleObjectStateException ssoExcp)
                {
                    throw new ConcurrencyException(string.Format(CultureInfo.CurrentCulture, "Save operation failed on entity {0} with id {1} as it has already been updated by another transaction in the database.", ssoExcp.EntityName, entity.Id), ssoExcp, ssoExcp.EntityName);
                }
                catch (HibernateException)
                {
                    throw new PersistenceException("An error occurred saving the entity.", entity.GetType().Name);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Retrieves entities from the database according to criteria contained in the supplied expression <paramref name="filterCriteria"/> and returns them as a paged result set.
        /// </summary>
        /// <typeparam name="TSortField">Type of the sort field by which the result set is to be sorted.</typeparam>
        /// <param name="pagingInfo">Paging info containing information needed to create the paged the result set.</param>
        /// <param name="filterCriteria">Expression containing criteria used to filter entities in the database.</param>
        /// <param name="orderBy">Expression containing field by which result set should be sorted.</param>
        /// <returns>Paginated list of entities.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving a paginated list of entities.</exception>
        protected PaginatedList<TEntity, int> GetPaginatedList<TSortField>(PagingInfo pagingInfo, Expression<Func<TEntity, bool>> filterCriteria, Expression<Func<TEntity, TSortField>> orderBy)
        {
            if (pagingInfo == null)
            {
                throw new ArgumentNullException("pagingInfo", "pagingInfo cannot be null.");
            }

            if (filterCriteria == null)
            {
                throw new ArgumentNullException("filterCriteria", "filterCriteria cannot be null.");
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException("orderBy", "orderBy cannot be null.");
            }

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    IQueryable<TEntity> query = pagingInfo.SortDirection == SortDirection.Asc ?
                        this.session.Query<TEntity>()
                        .OrderBy(orderBy)
                        .Where(filterCriteria) :
                        this.session
                        .Query<TEntity>()
                        .OrderByDescending(orderBy)
                        .Where(filterCriteria);

                    int totalCount = query.Count();
                    IEnumerable<TEntity> pagedResultSet = query
                        .Skip(pagingInfo.StartItemIndex - 1)
                        .Take(pagingInfo.PageSize).ToList<TEntity>();

                    transaction.Commit();

                    return new PaginatedList<TEntity, int>(pagedResultSet, totalCount, pagingInfo.PageNumber, pagingInfo.PageSize, pagingInfo.SortField);
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException("An error occurred retrieving a paginated list of entities.", hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Retrieves entities from the database according to criteria contained in the supplied expression <paramref name="filterCriteria"/>.
        /// </summary>
        /// <param name="filterCriteria">Expression containing criteria used to filter entities in the database.</param>
        /// <returns>List of entities.</returns>
        protected IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filterCriteria)
        {
            if (filterCriteria == null)
            {
                throw new ArgumentNullException("filterCriteria", "filterCriteria cannot be null.");
            }

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                IEnumerable<TEntity> resultSet = this.session
                    .Query<TEntity>()
                    .Where(filterCriteria).ToList();

                transaction.Commit();

                return resultSet;
            }
        }

        #endregion
    }
}
