//-----------------------------------------------------------------------
// <copyright file="PointSpreadBetEvaluationStrategy.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Bet evaluation strategy that evaluates bets made against the point spread of a game.
    /// </summary>
    internal class PointSpreadBetEvaluationStrategy : IBetEvaluationStrategy
    {
        /// <summary>
        /// Evaluates the supplied bet <paramref name="betToEvaluate"/> made against the point spread of a game.
        /// </summary>
        /// <param name="betToEvaluate">Point spread bet to evaluate.</param>
        /// <returns>Result of the point spread bet evaluation.</returns>
        public BetResult EvaluateBet(BetBase betToEvaluate)
        {
            if (betToEvaluate == null)
            {
                throw new ArgumentNullException("betToEvaluate", "betToEvaluate cannot be null.");
            }

            ////Cont. here.  Validate that parameter is of PointSpreadBet type!!

            return null;
        }
    }
}
