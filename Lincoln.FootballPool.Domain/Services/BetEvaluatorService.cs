//-----------------------------------------------------------------------
// <copyright file="BetEvaluatorService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// Domain service that evaluates bets.
    /// </summary>
    public class BetEvaluatorService : IBetEvaluatorService
    {
        #region Public Methods
        /// <summary>
        /// Evaluates the supplied bet and returns a bet result that contains information including whether or not the bet was won and the amount won or lost.
        /// </summary>
        /// <param name="betToEvaluate">Bet to be evaluated.</param>
        /// <returns>Bet result indicating the outcome of the bet.</returns>
        public BetResult EvaluateBet(BetBase betToEvaluate)
        {
            if (betToEvaluate == null)
            {
                throw new ArgumentNullException("betToEvaluate", "betToEvaluate cannot be null.");
            }

            return BetEvaluatorService.GetBetEvaluationStrategy(betToEvaluate).EvaluateBet(betToEvaluate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a bet evaluation strategy instance according to the type of bet that is to be evaluated.
        /// </summary>
        /// <param name="betToEvaluate">Bet to be evaluated.</param>
        /// <returns>Implementation of the IBetEvaluationStrategy interface.</returns>
        private static IBetEvaluationStrategy GetBetEvaluationStrategy(BetBase betToEvaluate)
        {
            if (betToEvaluate.GetType() == typeof(PointSpreadBet))
            {
                return new PointSpreadBetEvaluationStrategy();
            }
            else
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The supplied type of bet {0} does not have a bet evaluation strategy.  Make sure a bet with an associated bet evaluation strategy is supplied.", betToEvaluate.GetType()));
            }
        }

        #endregion
    }
}
