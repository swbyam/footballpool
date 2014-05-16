//-----------------------------------------------------------------------
// <copyright file="IBetRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for bet repository that provides basic CRUD operations related to point spread bets in the persistence store.
    /// </summary>
    public interface IBetRepository : IRepository<Bet, int>
    {
        #region Methods

        /// <summary>
        /// Gets a paged result set of all points spread bets placed by the pool user with the supplied <paramref name="poolUserId"/> from the persistence store.
        /// </summary>
        /// <param name="poolUserId">Unique id of the pool user for whom bets are to be returned.</param>
        /// <param name="pagingInfo">Paging info instance containing information needed to page the result such as the current page number and the number of bets per page.</param>
        /// <returns>Paged set of point spread bets.</returns>
        PaginatedList<Bet, int> GetBets(int poolUserId, PagingInfo pagingInfo);

        /// <summary>
        /// Gets bets that made during week <paramref name="weekNumber"/> that were made by a pool user with the supplied id <paramref name="poolUserId"/> from the persistence store.
        /// </summary>
        /// <param name="weekNumber">Week number for which bets were placed.</param>
        /// <param name="poolUserId">Id of the pool user who placed the bets.</param>
        /// <returns>List of bets.</returns>
        IEnumerable<Bet> GetBets(int weekNumber, int poolUserId);

        PaginatedList<Bet, int> GetBetsForWeek(int weekNumber, int poolId, PagingInfo pagingInfo);

        #endregion
    }
}
