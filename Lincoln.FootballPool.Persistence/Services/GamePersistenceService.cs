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
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.Domain.Services;
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

            ////Retrieve the home team from the database based on its unique id to verify that it exists.
            Team homeTeam = this.teamRepository.GetById(gameSnapshot.HomeTeamId);

            ////Home team does not exist in the persistence store.
            if (homeTeam == null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "Home team with id {0} does not exist in the persistence store.", gameSnapshot.HomeTeamId));
            }

            ////Retrieve the visiting team from the database based on its unique id to verify that it exists.
            Team visitingTeam = this.teamRepository.GetById(gameSnapshot.VisitingTeamId);

            ////Visiting team does not exist in the persistence store.
            if (visitingTeam == null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "Visiting team with id {0} does not exist in the persistence store.", gameSnapshot.VisitingTeamId));
            }

            ////TODO: Is this check necessary?  It is already known that the favorite team is part of the game!
            ////Retrieve the favorite team from the database based on its unique id to verify that it exists.
            Team favoriteTeam = this.teamRepository.GetById(gameSnapshot.FavoriteTeamId);

            ////Favorite team does not exist in the persistence store.
            if (favoriteTeam == null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "Favorite team with id {0} does not exist in the persistence store.", gameSnapshot.FavoriteTeamId));
            }

            ////TODO: Confirm if the next 2 validation rules are redundant.  If so, which one should be removed?

            ////Verify that a games does not already exist between the home and visiting teams i.e. a home team cannot play the same team during the regular season more than once as the home team.
            if (this.gameRepository.GetGame(homeTeam, visitingTeam) != null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "A game already exists between the home team {0} and visiting team {1}.", gameSnapshot.HomeTeamId, gameSnapshot.VisitingTeamId));
            }

            ////Verify that a game does not already exist that week with the same teams.
            if (this.gameRepository.GetGame(gameSnapshot.WeekNumber, homeTeam, visitingTeam) != null)
            {
                operationResult.AddBrokenRule(string.Format(CultureInfo.CurrentCulture, "A game in week {0} already exists between teams {1} and {2}.", gameSnapshot.WeekNumber, gameSnapshot.HomeTeamId, gameSnapshot.VisitingTeamId));
            }

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
                Team newHomeTeam = this.teamRepository.GetById(gameSnapshot.HomeTeamId);
                if (newHomeTeam == null)
                {
                    operationResult.AddBrokenRule(string.Format("The new home team with id {0} specified for game {1} does not exist in the persistence store.", gameSnapshot.HomeTeamId, gameId));
                }
                else
                {
                    ////Update the home team of the game.
                    gameToUpdate.HomeTeam = newHomeTeam;
                }
            }

            ////If the game's visiting team has been edited, check that the new visiting team exists in the persistence store.
            if (gameSnapshot.VisitingTeamId != gameToUpdate.VisitingTeam.Id)
            {
                ////Check to see if team with new visiting team id exists in the persistence store.
                Team newVisitingTeam = this.teamRepository.GetById(gameSnapshot.VisitingTeamId);
                if (newVisitingTeam == null)
                {
                    operationResult.AddBrokenRule(string.Format("The new visiting team with id {0} specified for game {1} does not exist in the persistence store.", gameSnapshot.VisitingTeamId, gameId));
                }
                else
                {
                    ////Update the visiting team of the game.
                    gameToUpdate.VisitingTeam = newVisitingTeam;
                }
            }

            ////If the game's favorite team has been edited, check that the new favorite team exists in the persistence store.
            if (gameSnapshot.FavoriteTeamId != gameToUpdate.FavoriteTeam.Id)
            {
                ////Check to see if team with new favorite team id exists in the persistence store.
                Team newFavoriteTeam = this.teamRepository.GetById(gameSnapshot.FavoriteTeamId);
                if (newFavoriteTeam == null)
                {

                    operationResult.AddBrokenRule(string.Format("The new favorite team with id {0} specified for game {1} does not exist in the persistence store.", gameSnapshot.FavoriteTeamId, gameId));
                }
                else
                {
                    ////Update the favorite team of the game.
                    gameToUpdate.FavoriteTeam = newFavoriteTeam;
                }
            }

            ////If there is at least one broken rule regarding updating the game, return.
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

        #endregion
    }
}
