//-----------------------------------------------------------------------
// <copyright file="GameRepository.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence;
    using Lincoln.FootballPool.Domain.Persistence.Repositories;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Exceptions;

    /// <summary>
    /// Game repository that provides basic CRUD functionality against a database including retrieving, creating, updating, and deleting games.
    /// </summary>
    public class GameRepository : Repository<Game>, IGameRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="session">NHibernate ISession implementation representing connection between objects and the database.</param>
        public GameRepository(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves all games for the supplied week number <paramref name="week"/> from the database.
        /// </summary>
        /// <param name="week">Week number for which games are to be returned.</param>
        /// <returns>List of games for the week specified.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving games from the database.</exception>
        public IEnumerable<Game> GetGames(int week)
        {
            using (ITransaction transaction = this.Session.BeginTransaction())
            {
                try
                {
                    IList<Game> games = this.Session
                        .CreateCriteria<Game>()
                        .Add(Restrictions.Eq("WeekNumber", week))
                        .List<Game>();
                    transaction.Commit();

                    return games;
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException(string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving games that take place during week {0}", week), hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Retrieves a game that takes place between the supplied home team <paramref name="homeTeam"/> and visiting team <paramref name="visitingTeam"/> from the database.
        /// </summary>
        /// <param name="homeTeam">Home team of the game.</param>
        /// <param name="visitingTeam">Visiting team of the game.</param>
        /// <returns>Game between home and visiting team.  If game could not be found, null is returned.</returns>
        /// <remarks>This database retrieval operation makes assumptions on which team is the home team and which is the visiting team based on supplied parameters.</remarks>
        public Game GetGame(Team homeTeam, Team visitingTeam)
        {
            if (homeTeam == null)
            {
                throw new ArgumentNullException("homeTeam", "homeTeam cannot be null.");
            }

            if (visitingTeam == null)
            {
                throw new ArgumentNullException("visitingTeam", "visitingTeam cannot be null.");
            }

            using (ITransaction transaction = this.Session.BeginTransaction())
            {
                try
                {
                    Game retrievedGame = this.Session
                        .QueryOver<Game>()
                        .Where(game => game.HomeTeam.Id == homeTeam.Id && game.VisitingTeam.Id == visitingTeam.Id).SingleOrDefault();

                    transaction.Commit();

                    return retrievedGame;
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException(string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving a game that takes place between teams: {1} and {2}", homeTeam, visitingTeam), hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Retrieves a game that takes place during the supplied week number <paramref name="weekNumber"/> between the two supplied teams: <paramref name="team1"/> and <paramref name="team2"/> from the database.
        /// </summary>
        /// <param name="weekNumber">Week number of the game.</param>
        /// <param name="team1">First team of the game.</param>
        /// <param name="team1">Second team of the game.</param>
        /// <returns>Game that occurs during the specified week number between the supplied teams.  If game cannot be found, null is returned.</returns>
        /// <exception cref="Lincoln.FootballPool.Domain.Persistence.PersistenceException">An error occurred retrieving the game from the database.</exception>
        /// <remarks>  This database retrieval operation does not assume which team is the home team and which team is the away team.  It simply returns a game that takes place between the 2 teams for the given week.</remarks>
        public Game GetGame(int weekNumber, Team team1, Team team2)
        {
            if (team1 == null)
            {
                throw new ArgumentNullException("team1", "team1 cannot be null.");
            }

            if (team2 == null)
            {
                throw new ArgumentNullException("team2", "team2 cannot be null.");
            }

            using (ITransaction transaction = this.Session.BeginTransaction())
            {
                try
                {
                    Game retrievedGame = this.Session
                        .QueryOver<Game>()
                        .Where(game => (game.HomeTeam.Id == team1.Id || game.VisitingTeam.Id == team1.Id) && (game.HomeTeam.Id == team2.Id || game.VisitingTeam.Id == team2.Id) && game.WeekNumber == weekNumber).SingleOrDefault();

                    transaction.Commit();

                    return retrievedGame;
                }
                catch (HibernateException hibernateExcp)
                {
                    throw new PersistenceException(string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving a game during week {0} that takes place between teams: {1} and {2}", weekNumber, team1, team2), hibernateExcp);
                }
            }
        }

        /// <summary>
        /// Saves the supplied game to the database.  If the game does not already exist in the persistence store, it will be created.
        /// </summary>
        /// <param name="game">Game to be saved.</param>
        /// <returns>Game that was saved to the database.</returns>
        public Game SaveGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game", "game cannot be null.");
            }

            return this.Save(game);
        }

        #endregion
    }
}
