//-----------------------------------------------------------------------
// <copyright file="IBetTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.WebApi.Model.Dtos;

    /// <summary>
    /// Abstraction for mapper that converts instances of the domain class <see cref="Bet"/> to its corresponding Data Transfer Object (DTO) implementation: <see cref="BetDto"/>.
    /// </summary>
    /// <remarks>The resulting <paramref name="BetDto"/> instance is suitable for use in building RESTful web services as it contains hypermedia links that are used to represent current state per the (Hypermedia as the Engine of Application State) HATEOAS paradigm.</remarks>
    public interface IBetTypeMapper : IEntityTypeMapper<BetDto, Bet, int>
    {
        #region Methods

        /// <summary>
        /// Maps the supplied list of bets <paramref name="bets"/> into a list of their <see cref="BetDto"/> counterparts.
        /// </summary>
        /// <param name="games">List of bets to convert.</param>
        /// <param name="urlHelper">Url helper used to assist in creating DTO's for each of the supplied bets.</param>
        /// <returns>List of bet DTO instances.</returns>
        //IEnumerable<GameDto> GetBetDtos(IEnumerable<Bet> bets, UrlHelper urlHelper);

        #endregion
    }
}
