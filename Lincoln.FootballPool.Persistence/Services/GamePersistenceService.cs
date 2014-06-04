//-----------------------------------------------------------------------
// <copyright file="GamePersistenceService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Services
{
    using System;
    using System.Globalization;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Services;
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.Persistence.Repositories;
    using Lincoln.FootballPool.Persistence.Services;

    /// <summary>
    /// Services that provides persistence-related operations relevant to games.
    /// </summary>
    public class GamePersistenceService : IGamePersistenceService
    {
        #region Member Variables

        /// <summary>
        /// Game service that provides game-specific business-related functionality.
        /// </summary>
        private IGameService gameService;

        /// <summary>
        /// Team repository that provides basic CRUD operations against the database.
        /// </summary>
        private ITeamRepository teamRepository;

        /// <summary>
        /// Game repository that provides basic CRUD operations against the database.
        /// </summary>
        private IGameRepository gameRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePersistenceService"/> class.
        /// </summary>
        /// <param name="gameService">Game service that provides game-specific business-related functionality.</param>
        /// <param name="teamRepository">Team repository that provides basic CRUD operations against the database.</param>
        /// <param name="gameRepository">Game repository that provides basic CRUD operations against the database.</param>
        public GamePersistenceService(IGameService gameService, ITeamRepository teamRepository, IGameRepository gameRepository)
        {
            if (gameService == null)
            {
                throw new ArgumentNullException("gameService", "gameService cannot be null.");
            }

            if (teamRepository == null)
            {
                throw new ArgumentNullException("teamRepository", "teamRepository cannot be null.");
            }

            if (gameRepository == null)
            {
                throw new ArgumentNullException("gameRepository", "gameRepository cannot be null.");
            }

            this.gameService = gameService;
            this.teamRepository = teamRepository;
            this.gameRepository = gameRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves a game in the persistence store based on information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot containing information needed to save a game instance to the persistence store.</param>
        /// <returns>Result of save operation.</returns>
        public PersistenceOperationResult<Game> SaveGame(GameSnapshot gameSnapshot)
        {
            if (gameSnapshot == null)
            {
                throw new ArgumentNullException("gameSnapshot", "gameSnapshot cannot be null.");
            }

            PersistenceOperationResult<Game> operationResult = new PersistenceOperationResult<Game>();
            Game gameToSave = null;

            ////Verify that the favorite team is a part of the game.
            operationResult.AddBrokenRule(this.CheckIfFavoredTeamIsPartOfGame(gameSnapshot));

            ////Verify that home and visiting teams of game are different.
            operationResult.AddBrokenRule(this.CheckIfHomeAndVisitingTeamsAreDifferent(gameSnapshot));

            ////If there is at least one broken rule regarding saving the game, return.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////Verify that home team exists in the persistence store.
            Team homeTeam = this.CheckTeamExistInPersistenceStore(gameSnapshot.HomeTeamId, 0, GameTeamType.Home, operationResult);

            ////Verify that visiting team exists in the persistence store.
            Team visitingTeam = this.CheckTeamExistInPersistenceStore(gameSnapshot.VisitingTeamId, 0, GameTeamType.Visiting, operationResult);

            ////Verify that favorite team exists in the persistence store.
            Team favoriteTeam = this.CheckTeamExistInPersistenceStore(gameSnapshot.FavoriteTeamId, 0, GameTeamType.Favorite, operationResult);

            ////If either the home team, visiting team, or favored team do not exist in the persistence store, return.
            ////NOTE: The following rules cannot be executed if any one of these teams does not exist.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////Verify that a games does not already exist between the home and visiting teams i.e. a home team cannot play the same team during the regular season more than once as the home team.
            this.CheckIfGameExistBetweenHomeAndVisitingTeams(homeTeam, visitingTeam, operationResult);

            ////Verify that a game does not already exist that week with the same teams.
            this.CheckIfGameExistsBetweenTeamsDuringWeek(homeTeam, visitingTeam, gameSnapshot.WeekNumber, operationResult);

            ////If there is at least one broken rule regarding saving the game, return.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////Create instance of game that is to be saved.
            gameToSave = new Game()
            {
                HomeTeam = homeTeam,
                VisitingTeam = visitingTeam,
                FavoriteTeam = favoriteTeam,
                StartDateTime = gameSnapshot.StartDateTime,
                WeekNumber = gameSnapshot.WeekNumber,
                Line = gameSnapshot.Line,
                OverUnder = gameSnapshot.OverUnder
            };

            ////Save the game in the persistence store.
            Game savedGame = this.gameRepository.SaveGame(gameToSave);
            operationResult.Entity = savedGame;

            return operationResult;
        }

        /// <summary>
        /// Updates a game that currently exists in the persistence store with information contained in the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameId">Unique id of the game that is to be updated.</param>
        /// <param name="gameSnapshot">Game snapshot containing information needed to update a game in the persistence store.</param>
        /// <returns>Result of update operation.</returns>
        public PersistenceOperationResult<Game> UpdateGame(int gameId, GameSnapshot gameSnapshot)
        {
            if (gameSnapshot == null)
            {
                throw new ArgumentNullException("gameSnapshot", "gameSnapshot cannot be null.");
            }

            PersistenceOperationResult<Game> operationResult = new PersistenceOperationResult<Game>();

            ////Verify that the favorite team is a part of the game.
            operationResult.AddBrokenRule(this.CheckIfFavoredTeamIsPartOfGame(gameSnapshot));

            ////Verify that home and visiting teams of game are different.
            operationResult.AddBrokenRule(this.CheckIfHomeAndVisitingTeamsAreDifferent(gameSnapshot));

            ////If there is at least one broken rule regarding updating the game, return.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////Verify that game exists in the persistence store.
            Game gameToUpdate = this.gameRepository.GetById(gameId);

            ////Game to update cannot be found in the persistence store.
            if (gameToUpdate == null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "A game with id {0} does not exist in the persistence store.  Update operations can only occur on games that exist in the persistence store.", gameId));

                return operationResult;
            }

            ////If the game's home team has been edited, check that the new home team exists in the persistence store.
            if (gameSnapshot.HomeTeamId != gameToUpdate.HomeTeam.Id)
            {
                ////Check to see if team with new home team id exists in the persistence store.
                Team newHomeTeam = CheckTeamExistInPersistenceStore(gameSnapshot.HomeTeamId, gameToUpdate.Id, GameTeamType.Home, operationResult);

                ////If team exists in persistence store, update home team of the game.
                if (newHomeTeam != null)
                {
                    gameToUpdate.HomeTeam = newHomeTeam;
                }
            }

            ////If the game's visiting team has been edited, check that the new visiting team exists in the persistence store.
            if (gameSnapshot.VisitingTeamId != gameToUpdate.VisitingTeam.Id)
            {
                ////Check to see if team with new visiting team id exists in the persistence store.
                Team newVisitingTeam = CheckTeamExistInPersistenceStore(gameSnapshot.VisitingTeamId, gameToUpdate.Id, GameTeamType.Visiting, operationResult);

                ////If team exists in persistence store, update visiting team of the game.
                if (newVisitingTeam != null)
                {
                    gameToUpdate.VisitingTeam = newVisitingTeam;
                }
            }

            ////If the game's favorite team has been edited, check that the new favorite team exists in the persistence store.
            if (gameSnapshot.FavoriteTeamId != gameToUpdate.FavoriteTeam.Id)
            {
                ////Check to see if team with new favorite team id exists in the persistence store.
                Team newFavoriteTeam = CheckTeamExistInPersistenceStore(gameSnapshot.FavoriteTeamId, gameToUpdate.Id, GameTeamType.Favorite, operationResult);

                ////If team exists in persistence store, update favorite team of the game.
                if (newFavoriteTeam != null)
                {
                    gameToUpdate.FavoriteTeam = newFavoriteTeam;
                }
            }

            ////If there is at least one broken rule regarding updating the game, return.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////Verify that a games does not already exist between the home and visiting teams i.e. a home team cannot play the same team during the regular season more than once as the home team.
            this.CheckIfGameExistBetweenHomeAndVisitingTeams(gameToUpdate.HomeTeam, gameToUpdate.VisitingTeam, operationResult);

            ////Verify that a game does not already exist that week with the same teams.
            this.CheckIfGameExistsBetweenTeamsDuringWeek(gameToUpdate.HomeTeam, gameToUpdate.VisitingTeam, gameSnapshot.WeekNumber, operationResult);

            ////If there is at least one broken rule regarding saving the game, return.
            if (!operationResult.CanPersistenceOperationBeExecuted)
            {
                return operationResult;
            }

            ////The operation result is successful, so update rest of game properties with information contained in game snapshot and save to persistence store.
            gameToUpdate.StartDateTime = gameSnapshot.StartDateTime;
            gameToUpdate.WeekNumber = gameSnapshot.WeekNumber;
            gameToUpdate.Line = gameSnapshot.Line;
            gameToUpdate.OverUnder = gameSnapshot.OverUnder;

            ////Save game in persistence store.
            Game updatedGame = this.gameRepository.SaveGame(gameToUpdate);
            operationResult.Entity = updatedGame;

            return operationResult;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a rule evaluation description indicating whether the favored team is part of the supplied game snapshot <paramref name="gameSnapshot"/>.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot on which to evaluate the rule.</param>
        /// <returns>Description of broken rule.  If the favored team is part of the game, an empty string is returned.</returns>
        private string CheckIfFavoredTeamIsPartOfGame(GameSnapshot gameSnapshot)
        {
            return this.gameService.IsFavoredTeamIsPartOfGame(gameSnapshot) ? string.Empty : string.Format(CultureInfo.CurrentCulture, "The favored team {0} is neither the home ({1}) nor visiting ({2}) team of the game.", gameSnapshot.FavoriteTeamId, gameSnapshot.HomeTeamId, gameSnapshot.VisitingTeamId);
        }

        /// <summary>
        /// Returns a rule evaluation description indicating whether the home and visiting teams that are a part of the supplied game snapshot <paramref name="gameSnapshot"/> are different teams or the same team.
        /// </summary>
        /// <param name="gameSnapshot">Game snapshot on which to evaluate the rule.</param>
        /// <returns>Description of broken rule.  If the home and visiting teams are different, an empty string is returned.</returns>
        private string CheckIfHomeAndVisitingTeamsAreDifferent(GameSnapshot gameSnapshot)
        {
            return this.gameService.AreHomeAndVisitingTeamsDifferent(gameSnapshot) ? string.Empty : string.Format(CultureInfo.CurrentCulture, "The home ({0}) and visiting teams ({1}) are the same for the game.  The home and visiting teams must be different.", gameSnapshot.HomeTeamId, gameSnapshot.VisitingTeamId);
        }

        /// <summary>
        /// Verifies whether or not a team exists in the persistence store with id <paramref name="teamdId"/>.
        /// </summary>
        /// <param name="teamId">Id of the team to check for its existence in the persistence store.</param>
        /// <param name="gameId">Id of the game to which the team may be associated.</param>
        /// <param name="gameTeamType">Type of the team in the context of the game.  Possible values include "Home", "Visiting", and "Favorite".</param>
        /// <param name="operationResult">OperationResult instance that stores information on the result of the persistence-based operation involving a game.</param>
        /// <returns></returns>
        private Team CheckTeamExistInPersistenceStore(int teamId, int? gameId, GameTeamType gameTeamType, PersistenceOperationResult<Game> operationResult)
        {
            ////Retrieve team from the persistence store based on its unique id to verify that it exists.
            Team team = this.teamRepository.GetById(teamId);

            ////Team does not exist in the persistence store.
            if (team == null)
            {
                string gameDescription = gameId.HasValue ? "game with id " + gameId.Value.ToString() : "new game to be created";

                operationResult.AddBrokenRule(string.Format("The {0} team with id {1} specified for {2} does not exist in the persistence store.", gameTeamType.ToString().ToLower(), teamId, gameDescription));
            }

            return team;
        }

        /// <summary>
        /// Verifies whether or note a game exists between the <paramref name="homeTeam"/> and <paramref name="visitingTeam"/> during the season.  This check satisfies the requirement that a team may not play another team as the home team more than once during the regular season.
        /// </summary>
        /// <param name="homeTeam">Home team of a game.</param>
        /// <param name="visitingTeam">Visiting team of a game.</param>
        /// <param name="operationResult">OperationResult instance that stores information on the result of the persistence-based operation involving a game.</param>
        private void CheckIfGameExistBetweenHomeAndVisitingTeams(Team homeTeam, Team visitingTeam, PersistenceOperationResult<Game> operationResult)
        {
            if (this.gameRepository.GetGame(homeTeam, visitingTeam) != null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "A game already exists between the home team {0} and visiting team {1}.", homeTeam.Id, visitingTeam.Id));
            }
        }

        /// <summary>
        /// Verifies whether or not a game exists between the <paramref name="homeTeam"/> and <paramref name="visitingTeam"/> during the supplied week number <paramref name="weekNumber"/>.  This check satisfies the requirement that the same two teams cannot play one another more than once during the same week.
        /// </summary>
        /// <param name="homeTeam">Home team of a game.</param>
        /// <param name="visitingTeam">Visiting team of a game.</param>
        /// <param name="weekNumber">Week number of the season.</param>
        /// <param name="operationResult">OperationResult instance that stores information on the result of the persistence-based operation involving a game.</param>
        private void CheckIfGameExistsBetweenTeamsDuringWeek(Team homeTeam, Team visitingTeam, int weekNumber, PersistenceOperationResult<Game> operationResult)
        {
            if (this.gameRepository.GetGame(weekNumber, homeTeam, visitingTeam) != null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "A game in week {0} already exists between teams {1} and {2}.", weekNumber, homeTeam.Id, visitingTeam.Id));
            }
        }

        #endregion
    }
}
