//-----------------------------------------------------------------------
// <copyright file="BetRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Persistence;
    using Lincoln.FootballPool.Persistence.Repositories;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Exceptions;

    /// <summary>
    /// Possible fields by which bets can be sorted.
    /// </summary>
    internal enum BetSortField
    {
        /// <summary>
        /// User name of the pool user who placed the bets.
        /// </summary>
        UserName,

        /// <summary>
        /// Team picked to cover bet.
        /// </summary>
        TeamToCoverBet,

        /// <summary>
        /// Amount placed on bets.
        /// </summary>
        Amount
    }

    /// <summary>
    /// Class is repository for the <see cref="Bet"/> entity domain type that provides basic CRUD-based functionality against a relational database.
    /// </summary>
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BetRepository"/> class.
        /// </summary>
        /// <param name="session">NHibernate ISession implementation representing connection between objects and the database.</param>
        public BetRepository(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all a paged result set of all bets placed by the pool user with the supplied <paramref name="poolUserId"/> from the database.
        /// </summary>
        /// <param name="poolUserId">Unique id of the pool user for whom bets are to be returned.</param>
        /// <param name="pagingInfo">Paging info instance containing information needed to page the result such as the current page number and the number of bets per page.</param>
        /// <returns>Paged set of bets.</returns>
        public PaginatedList<Bet, int> GetBets(int poolUserId, PagingInfo pagingInfo)
        {
            if (poolUserId <= 0)
            {
                throw new ArgumentException("poolUserId cannot be less than or equal to zero.", "poolUserId");
            }

            if (pagingInfo == null)
            {
                throw new ArgumentNullException("pagingInfo", "pagingInfo cannot be null.");
            }

            return base.GetPaginatedList(pagingInfo, bet => bet.PlacedBy.Id == poolUserId,
                bet => bet.PlacedBy.UserName);
        }

        /// <summary>
        /// Gets bets that made during week <paramref name="weekNumber"/> that were made by a pool user with the supplied id <paramref name="poolUserId"/> from the database.
        /// </summary>
        /// <param name="weekNumber">Week number for which bets were placed.</param>
        /// <param name="poolUserId">Id of the pool user who placed the bets.</param>
        /// <returns>List of bets.</returns>
        /// <exception cref="Lincoln.FootballPool.Persistence.PersistenceException">An error occurred retrieving bets from the persistence store.</exception>
        public IEnumerable<Bet> GetBets(int weekNumber, int poolUserId)
        {
            if (weekNumber <= 0)
            {
                throw new ArgumentException("weekNumber cannot be less than or equal to zero.", "weekNumber");
            }

            if (poolUserId <= 0)
            {
                throw new ArgumentException("poolUserId cannot be less than or equal to zero.", "poolUserId");
            }

            try
            {
                return base.GetList(bet => bet.Game.WeekNumber == weekNumber && bet.PlacedBy.Id == poolUserId);
            }
            catch (HibernateException hExcp)
            {
                throw new PersistenceException(string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving bets placed during week {0} by pool user {1}.", weekNumber, poolUserId), hExcp);
            }
        }

        /// <summary>
        /// Gets bets made during the supplied week number <paramref name="weekNumber"/> by all pool users in a pool with the id <paramref name="poolId"/> from the database.
        /// </summary>
        /// <param name="weekNumber">Week number during which bets to be retrieved were placed.</param>
        /// <param name="poolId">Id of the pool for which bets placed are to be returned.</param>
        /// <param name="pagingInfo">Paging info instance containing information needed to page the result such as the current page number and the number of bets per page.</param>
        /// <returns>Paged set of point spread bets.</returns>
        public PaginatedList<Bet, int> GetBetsForWeek(int weekNumber, int poolId, PagingInfo pagingInfo)
        {
            if (weekNumber <= 0)
            {
                throw new ArgumentException("weekNumber cannot be less than or equal to zero.", "weekNumber");
            }

            if (poolId <= 0)
            {
                throw new ArgumentException("poolId cannot be less than or equal to zero.", "poolId");
            }

            if (pagingInfo == null)
            {
                throw new ArgumentNullException("pagingInfo", "pagingInfo cannot be null.");
            }

            BetSortField betSortField;

            ////If sort field is not valid name, throw exception.
            if (!Enum.TryParse(pagingInfo.SortField, true, out betSortField))
            {
                throw new InvalidSortExpressionException(string.Format("{0} is not a supported field when sorting bets.", pagingInfo.SortField));
            }

            ////TODO: Not happy with the conditional here based on the type of sort field.  There may be more elegant design somewhere for this!

            return betSortField == BetSortField.Amount ? base.GetPaginatedList(pagingInfo, bet => bet.Game.WeekNumber == weekNumber && bet.PlacedBy.Pool.Id == poolId, bet => bet.Amount) : base.GetPaginatedList(pagingInfo, bet => bet.Game.WeekNumber == weekNumber && bet.PlacedBy.Pool.Id == poolId, BetRepository.GetSortExpression(betSortField));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and returns a sort expression according to the supplied sort field name <paramref name="sortField"/>.
        /// </summary>
        /// <param name="sortField">Name of the field that is to be sorted by.</param>
        /// <returns>System.Linq.Expression representing sort expression.</returns>
        private static Expression<Func<Bet, string>> GetSortExpression(BetSortField betSortField)
        {
            switch (betSortField)
            {
                case BetSortField.UserName:
                    return bet => bet.PlacedBy.UserName;
                case BetSortField.TeamToCoverBet:
                    return bet => bet.TeamToCoverBet.Name;
                default:
                    return bet => bet.PlacedBy.UserName;
            }
        }

        #endregion
    }
}
