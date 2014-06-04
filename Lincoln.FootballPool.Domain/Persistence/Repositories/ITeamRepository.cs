//-----------------------------------------------------------------------
// <copyright file="ITeamRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence.Repositories
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for Team repository that defines operations for retrieving team instances from the persistence store.
    /// </summary>
    public interface ITeamRepository : IRepository<Team, int>
    {
        #region Methods

        /// <summary>
        /// Retrieves a team with the supplied <paramref name="teamId"/> from the persistence store.
        /// </summary>
        /// <param name="teamId">Unique id of the team to retrieve.</param>
        /// <returns>Team with the specified team id.</returns>
        ////Team GetTeam(int teamId);

        #endregion
    }
}
