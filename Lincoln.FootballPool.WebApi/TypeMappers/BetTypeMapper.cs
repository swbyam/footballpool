//-----------------------------------------------------------------------
// <copyright file="BetTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;
    using System.Collections.Generic;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.WebApi.Model;
    using Lincoln.FootballPool.WebApi.Model.Dtos;

    /// <summary>
    /// Mapping class that converts instances of the domain class <see cref="Bet"/> to its corresponding Data Transfer Object (DTO) implementation: <see cref="BetDto"/>.
    /// </summary>
    /// <remarks>The resulting <paramref name="BetDto"/> instance is suitable for use in building RESTful web services as it contains hypermedia links that are used to represent current state per the (Hypermedia as the Engine of Application State) HATEOAS paradigm.</remarks>
    public class BetTypeMapper : EntityTypeMapper<BetDto, Bet, int>, IBetTypeMapper
    {
        /// <summary>
        /// Maps the supplied list of bets <paramref name="bets"/> into a list of their <see cref="BetDto"/> counterparts.
        /// </summary>
        /// <param name="games">List of bets to convert.</param>
        /// <param name="urlHelper">Url helper used to assist in creating DTO's for each of the supplied bets.</param>
        /// <returns>List of bet DTO instances.</returns>
        ////public IEnumerable<BetDto> GetBetDtos(IEnumerable<Bet> bets, UrlHelper urlHelper)
        ////{
        ////    if (bets == null)
        ////    {
        ////        throw new ArgumentNullException("bets", "bets cannot be null.");
        ////    }

        ////    if (urlHelper == null)
        ////    {
        ////        throw new ArgumentNullException("urlHelper", "urlHelper cannot be null.");
        ////    }

        ////    return bets.Select(bet => this.GetEntityDto(bet, urlHelper.Link("DefaultApi", new { id = bet.Id })));
        ////}

        #region Protected Methods

        /// <summary>
        /// Creates and returns a list of hypermedia links related to a bet based on information contained in the supplied bet <paramref name="entity"/> and the uri to the bet resource <paramref name="entityUri"/>.
        /// </summary>
        /// <param name="entity">Bet containing information needed to create the hypermedia links.</param>
        /// <param name="entityUri">Uri to a bet resource.</param>
        /// <returns>List of hypermedia links.</returns>
        protected override IEnumerable<Link> CreateHypermediaLinks(Bet entity, string entityUri)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(entityUri))
            {
                throw new ArgumentException("entityUri cannot be null or empty string", "entityUri");
            }

            List<Link> hypermediaLinks = new List<Link>();

            ////Add hypermedia links.

            ////Link to bet resource itself.
            hypermediaLinks.Add(new Link
            {
                Title = "self",
                Rel = "self",
                Href = entityUri + "/" + entity.Id
            });

            ////Link to the game resource upon which the bet was placed.
            hypermediaLinks.Add(new Link
            {
                Title = "Game",
                Rel = "game",
                Href = "api/Games/" + entity.Game.Id,
            });

            ////Link to the team resource that was picked to cover the bet.
            hypermediaLinks.Add(new Link
            {
                Title = "TeamToCoverBet",
                Rel = "teamtocoverbet",
                Href = "api/Teams/" + entity.TeamToCoverBet.Id,
            });
            ////TODO: How can league id be obtained?
            ////betDto.AddHypermediaLinks(new Link
            ////{
            ////    Title = "PlacedByUser",
            ////    Rel = "placedbyuser",
            ////    Href = "api/<league id>/poolusers/<pool user id>"
            ////});

            return hypermediaLinks;
        }

        #endregion
    }
}