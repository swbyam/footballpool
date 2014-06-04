//-----------------------------------------------------------------------
// <copyright file="TeamRepository.cs" company="Lincoln">
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
    /// Repository class that provides basic CRUD functionality against a database using the NHibernate ORM tool.
    /// </summary>
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class.
        /// </summary>
        /// <param name="session">NHibernate ISession implementation representing connection between objects and the database.</param>
        public TeamRepository(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a team with the supplied <paramref name="teamId"/> from the database.
        /// </summary>
        /// <param name="gameId">Unique id of the team to retrieve.</param>
        /// <returns>Team with the specified team id.</returns>
        ////public Team GetTeam(int teamId)
        ////{
        ////    return base.GetById(teamId);
        ////}

        #endregion
    }
}
