//-----------------------------------------------------------------------
// <copyright file="IPagingInfoTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Domain.Persistence;
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

        /// <summary>
        /// Converts the supplied <paramref name="paginatedRequest"/> instance to an instance of <see cref="PagingInfo"/>.
        /// </summary>
        /// <param name="paginatedRequest">Paginated request to convert or map.</param>
        /// <returns>PagingInfo instance.</returns>
        PagingInfo GetPagingInfo(PaginatedRequest paginatedRequest);

        /// <summary>
        /// Converts the supplied <paramref name="paginatedList"/> instance to an instance of <see cref="PaginatedListDto"/> using the supplied <paramref name="entityMapper"/>.
        /// </summary>
        /// <param name="paginatedList">Paginated list to convert.</param>
        /// <param name="entityMapper">Entity mapper used to convert entities contained in the paginated list to their DTP counterpart.</param>
        /// <param name="entityUri">Entity URI used to create hyperlinks for the entities per the HATEOAS paradigm.</param>
        /// <returns>Paginated list DTO.</returns>
        PaginatedListDto<TEntityDto> GetPaginatedListDto(PaginatedList<TEntity, TEntityId> paginatedList, IEntityTypeMapper<TEntityDto, TEntity, TEntityId> entityMapper, string entityUri);

        #endregion
    }
}
