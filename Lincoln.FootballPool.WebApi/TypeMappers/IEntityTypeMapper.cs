//-----------------------------------------------------------------------
// <copyright file="IEntityTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Routing;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.WebApi.Model.Dtos;

    public interface IEntityTypeMapper<TEntityDto, TEntity, TEntityId>
        where TEntityDto : DtoBase, new()
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        #region Methods

        /// <summary>
        /// Maps the supplied <paramref name="entity"/> instance to its DTO counterpart.
        /// </summary>
        /// <param name="entity">Entity to convert.</param>
        /// <param name="entityUri">Uri of the entity that is to be converted to its DTO counterpart.</param>
        /// <returns>Entity DTO instance.</returns>
        TEntityDto GetEntityDto(TEntity entity, string entityUri);

         /// <summary>
        /// Maps the supplied list of entities <paramref name="entities"/> into a list of their DTO counterparts <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">List of entities to convert.</param>
        /// <param name="urlHelper">Url helper used to assist in creating DTO's for each of the supplied entities.</param>
        /// <returns>List of entity DTO instances.</returns>
        IEnumerable<TEntityDto> GetEntityDtos(IEnumerable<TEntity> entities, UrlHelper urlHelper);

        #endregion
    }
}
