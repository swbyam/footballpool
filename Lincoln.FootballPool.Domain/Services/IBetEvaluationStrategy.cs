//-----------------------------------------------------------------------
// <copyright file="IBetEvaluationStrategy.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Abstraction for bet evaluation strategy that evaluates bets and returns the result of the evaluation.
    /// </summary>
    internal interface IBetEvaluationStrategy
    {
        #region Methods

        /// <summary>
        /// Evaluates the supplied bet <paramref name="betToEvaluate"/> and returns the result of the evaluation.
        /// </summary>
        /// <param name="betToEvaluate">Bet to be evaluated.</param>
        /// <returns>Result of the bet evaluation.</returns>
        BetResult EvaluateBet(BetBase betToEvaluate);
       
        #endregion
    }
}
