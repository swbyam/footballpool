//-----------------------------------------------------------------------
// <copyright file="IPagingInfoTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Persistence;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;

    /// <summary>
    /// Abstraction for mapping class that creates instances of the persistence class <see cref="PagingInfo"/> from its request form <see cref="PaginatedRequest"/> and also generates instances of <see cref="PaginatedListDto<TEntityDto>"/> from <see cref="PaginatedList<TEntity, TEntityId>"/>.  
    /// </summary>
    /// <typeparam name="TEntityDto">Type of entity DTO contained in the paginated DTO list.</typeparam>
    /// <typeparam name="TEntity">Type of entity contained in a paginated list that is to be convert to an entity DTO and placed in a paginated DTO list.</typeparam>
    /// <typeparam name="TEntityId">Type of id used by the entity.</typeparam>
    public interface IPagingTypeMapper<TEntityDto, TEntity, TEntityId>
        where TEntityDto : DtoBase, new()
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        #region Methods

        PagingInfo GetPagingInfo(PaginatedRequest paginatedRequest);

        PaginatedListDto<TEntityDto> GetPaginatedListDto(PaginatedList<TEntity, TEntityId> paginatedList, IEntityTypeMapper<TEntityDto, TEntity, TEntityId> entityMapper, string entityUri);

        #endregion
    }
}
