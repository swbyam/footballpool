﻿//-----------------------------------------------------------------------
// <copyright file="GameTypeMapperUnitTest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//<auto-generated/>
//-----------------------------------------------------------------------
using System;

using AutoMapper;

using Lincoln.FootballPool.Domain.Entities;
using Lincoln.FootballPool.WebApi.AppStart;
using Lincoln.FootballPool.WebApi.Model;
using Lincoln.FootballPool.WebApi.Model.Dtos;
using Lincoln.FootballPool.WebApi.Model.RequestModels;
using Lincoln.FootballPool.WebApi.TypeMappers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lincoln.FootballPool.WebApi.Test
{
    [TestClass]
    public class GameTypeMapperUnitTest
    {
        #region Unit Tests

        /// <summary>
        /// Test verifies whether a GameDto instance can be obtained or mapped from a Game instance.
        /// </summary>
        [TestMethod]
        public void CanGetGameDtoFromGameInstance()
        {
            Game gameToConvert = GameTypeMapperUnitTest.CreateGameInstance();
            GameTypeMapper gameTypeMapper = new GameTypeMapper();

            ////Set up AutoMapper mappings.
            AutoMapperConfig.Configure();

            ////Check that AutoMapper configuration is valid.
            //Mapper.AssertConfigurationIsValid();

            string gameUri = "api/games/12";

            GameDto gameDto = gameTypeMapper.GetEntityDto(gameToConvert, gameUri);

            Assert.IsNotNull(gameDto);
            Assert.AreEqual<int>(gameToConvert.Id, gameDto.Id);
            Assert.AreEqual<string>(gameToConvert.HomeTeam.Name, gameDto.HomeTeamName);
            Assert.AreEqual<string>(gameToConvert.VisitingTeam.Name, gameDto.VisitingTeamName);
            Assert.AreEqual<string>(gameToConvert.FavoriteTeam.Name, gameDto.FavoriteTeamName);
            Assert.AreEqual(gameToConvert.StartDateTime, gameDto.StartDateTime);
            Assert.AreEqual<float>(gameToConvert.Line, gameDto.Line);
            Assert.AreEqual<int>(4, gameDto.HypermediaLinks.Count);
            Assert.AreEqual<string>(gameUri, gameDto.HypermediaLinks[0].Href);
            Assert.AreEqual<string>(gameUri + "/hometeam", gameDto.HypermediaLinks[1].Href);
            Assert.AreEqual<string>(gameUri + "/visitingteam", gameDto.HypermediaLinks[2].Href);
            Assert.AreEqual<string>(gameUri + "/favoriteteam", gameDto.HypermediaLinks[3].Href);
        }

        /// <summary>
        /// Test verifies whether a Game instance can be obtained or mapped from a GameDto instance.
        /// </summary>
        [TestMethod]
        public void CanGetGameSnapshotFromGameDtoInstance()
        {
            //GameDto gameDtoToConvert = GameTypeMapperUnitTest.CreateGameDtoInstance();
            GameBaseRequestModel gameRequest = GameTypeMapperUnitTest.CreateGameRequestModel();
            GameTypeMapper gameTypeMapper = new GameTypeMapper();

            ////Set up AutoMapper mappings.
            AutoMapperConfig.Configure();

            GameSnapshot game = gameTypeMapper.GetGameSnapshot(gameRequest);

            Assert.IsNotNull(game);
            Assert.AreEqual<int>(gameRequest.HomeTeamId.Value, game.HomeTeamId);
            Assert.AreEqual<int>(gameRequest.VisitingTeamId.Value, game.VisitingTeamId);
            Assert.AreEqual<int>(gameRequest.FavoriteTeamId.Value, game.FavoriteTeamId);
            Assert.AreEqual<DateTime>(gameRequest.StartDateTime.Value, game.StartDateTime);
            Assert.AreEqual<int>(gameRequest.WeekNumber.Value, game.WeekNumber);
            Assert.AreEqual<float>(gameRequest.Line.Value, game.Line);
            Assert.AreEqual<float>(gameRequest.OverUnder.Value, game.OverUnder);
        }

        #endregion

        #region Test Helper Methods

        private static Game CreateGameInstance()
        {
            return new Game { Id = 1, HomeTeam = new Team { Id = 2, Name = "Broncos", City = "Denver" }, VisitingTeam = new Team { Id = 1, Name = "Patriots", City = "New England" }, FavoriteTeam = new Team { Id = 2, Name = "Broncos", City = "Denver" }, StartDateTime = new DateTime(2014, 1, 16, 13, 0, 0, DateTimeKind.Local), WeekNumber = 10, Line = 4.5f, OverUnder = 55f };
        }

        private static GameDto CreateGameDtoInstance()
        {
            return new GameDto { Id = 1, HomeTeamName = "Broncos", VisitingTeamName = "Patriots", FavoriteTeamName = "Broncos", StartDateTime = new DateTime(2014, 1, 16, 13, 0, 0, DateTimeKind.Local), WeekNumber = 10, Line = 4.5f, OverUnder = 55f };
        }

        private static GameBaseRequestModel CreateGameRequestModel()
        {
            return new GameBaseRequestModel
            {
                HomeTeamId = 1,
                VisitingTeamId = 2,
                FavoriteTeamId = 1,
                StartDateTime = new DateTime(2014, 1, 16, 13, 0, 0, DateTimeKind.Local),
                WeekNumber = 10,
                Line = 9.5f,
                OverUnder = 35.5f
            };
        }

        #endregion
    }
}