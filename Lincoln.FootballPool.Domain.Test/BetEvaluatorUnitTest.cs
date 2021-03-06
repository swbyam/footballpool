﻿//-----------------------------------------------------------------------
// <copyright file="BetEvaluatorUnitTest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//<auto-generated/>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Test
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BetEvaluatorUnitTest
    {
        #region Unit Tests

        [TestMethod]
        public void CanEvaluatePointSpreadBet()
        {
            int amountPlacedOnBet = 200000;
            Game game = BetEvaluatorUnitTest.CreateGameInstance();
            Bet betToEvaluate = new Bet { Id = 1, Game = BetEvaluatorUnitTest.CreateGameInstance(), Amount = amountPlacedOnBet, PlacedBy = new PoolUser { Id = 1, UserName = "CMouse" }, TeamToCoverBet = new Team { Id = 1, Name = "Patriots", City = "New England" }, Points = 4.5f };
            BetResult expectedBetResult = new BetResult { BetWon = false, AmountWonLost = amountPlacedOnBet, Bet = betToEvaluate };
            BetResult actualBetResult = null;

            IBetEvaluatorService betEvaluatorService = new BetEvaluatorService();
            actualBetResult = betEvaluatorService.EvaluateBet(betToEvaluate);

            Assert.AreEqual<BetResult>(expectedBetResult, actualBetResult);
        }

        ////public void CanEvaluatePointSpreadBet()
        ////{
        ////    int amountPlacedOnBet = 200000;
        ////    Game game = BetEvaluatorUnitTest.CreateGameInstance();

        ////    Bet betToEvaluate = new Bet { BetId = 1, Game = BetEvaluatorUnitTest.CreateGameInstance(), Amount = amountPlacedOnBet, PlacedBy = new PoolUser { UserId = 1, UserName = "CMouse" }, TeamToCoverBet = new Team { TeamId = 1, TeamName = "Patriots", City = "New England" }, Points = 4.5f };
        ////    BetResult betResult = new BetResult { BetWon = false, AmountWonLost = amountPlacedOnBet, Bet = betToEvaluate };

        ////    Mock<IBetEvaluatorService> betEvaluatorServiceMock = new Mock<IBetEvaluatorService>();
        ////    betEvaluatorServiceMock.Setup(service => service.EvaluateBet(betToEvaluate)).Returns(betResult);

        ////    ////Evaluate bet.
        ////    betEvaluatorServiceMock.Object.EvaluateBet(betToEvaluate);
        ////}

        #endregion

        #region Test Helper Methods

        private static Game CreateGameInstance()
        {
            return new Game
            {
                Id = 1,
                HomeTeam = new Team
                {
                    Id = 2,
                    Name = "Broncos",
                    City = "Denver"
                },
                VisitingTeam = new Team
                {
                    Id = 1,
                    Name = "Patriots",
                    City = "New England"
                },
                FavoriteTeam = new Team
                {
                    Id = 2,
                    Name = "Broncos",
                    City = "Denver"
                },
                StartDateTime = new DateTime(2014, 1, 16, 13, 0, 0, DateTimeKind.Local),
                WeekNumber = 10,
                Line = 4.5f,
                OverUnder = 55f
            };

            //return new Game { GameId = 1, HomeTeam = new Team { TeamId = 2, Name = "Broncos", City = "Denver" }, VisitingTeam = new Team { TeamId = 1, Name = "Patriots", City = "New England" }, FavoriteTeam = new Team { TeamId = 2, Name = "Broncos", City = "Denver" }, StartDateTime = new DateTime(2014, 1, 16, 13, 0, 0, DateTimeKind.Local), WeekNumber = 10, Line = 4.5f, OverUnder = 55f };
        }

        #endregion
    }
}
