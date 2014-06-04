//-----------------------------------------------------------------------
// <copyright file="AutoMapperConfig.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.AppStart
{
    using System;

    using AutoMapper;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence;
    using Lincoln.FootballPool.Domain.Snapshots;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;

    /// <summary>
    /// Static class that configures the AutoMapper api and sets up mappings between domain types in the Lincoln.FootballPool.Domain project and their corresponding DTO counterparts.  Mappings are also set up between request objects that are sent as part of web service requests and corresponding domain constructs.
    /// </summary>
    public static class AutoMapperConfig
    {
        #region Public Methods

        /// <summary>
        /// Configures the mappings between domain types in the Lincoln.FootballPool.Domain project and their corresponding DTO counterparts.  Mappings are also set up between request objects that are sent as part of web service requests and corresponding domain constructs.
        /// </summary>
        public static void Configure()
        {
            ////Create AutoMapper mappings.

            ////Map entities to DTOs.
            Mapper.CreateMap<Game, GameDto>()
                ////Do not map to HypermediaLinks property of GameDto.
                .ForMember(gameDto => gameDto.HypermediaLinks, expr => expr.Ignore());
            Mapper.CreateMap<Bet, BetDto>()
                ////Do not map to HypermediaLinks property of BetDto.
                .ForMember(betDto => betDto.HypermediaLinks, expr => expr.Ignore());

            ////Map request objects to domain objects.
            Mapper.CreateMap<GameBaseRequestModel, GameSnapshot>()
                .ForMember(gameSnapshot => gameSnapshot.GameId, expr => expr.Ignore());
            Mapper.CreateMap<PaginatedRequest, PagingInfo>()
                .ForMember(pagingInfo => pagingInfo.StartItemIndex, expr => expr.Ignore());
                ////.ForMember(pagingInfo => pagingInfo.SortInfo, expr => expr.Ignore());
                ////.ForMember(pagingInfo => pagingInfo.SortInfo, expr => expr.ResolveUsing<SortDirectionTypeResolver>());
                
            ////Check that AutoMapper configuration is valid.
            Mapper.AssertConfigurationIsValid();
        }

        #endregion
    }
}