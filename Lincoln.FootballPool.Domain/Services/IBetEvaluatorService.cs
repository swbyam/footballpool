//-----------------------------------------------------------------------
// <copyright file="IBetEvaluatorService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for service that evaluates various types of bets placed on games as to whether they have been won or lost.
    /// </summary>
    public interface IBetEvaluatorService
    {
        /// <summary>
        /// Evaluates the supplied bet and returns a bet result that contains information including whether or not the bet was won and the amount won or lost.
        /// </summary>
        /// <param name="betToEvaluate">Bet to be evaluated.</param>
        /// <returns>Bet result indicating the outcome of the bet.</returns>
        BetResult EvaluateBet(BetBase betToEvaluate);
    }
}
