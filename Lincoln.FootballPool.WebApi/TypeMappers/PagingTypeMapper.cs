﻿//-----------------------------------------------------------------------
// <copyright file="PagingTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Persistence;
    using Lincoln.FootballPool.WebApi.Model.Dtos;
    using Lincoln.FootballPool.WebApi.Model.RequestModels;

    /// <summary>
    /// Mapping class that creates instances of <see cref="PagingInfo"/> from the class's request counterpart <see cref="PaginatedRequest"/>.
    /// </summary>
    public class PagingTypeMapper<TEntityDto, TEntity, TEntityId> : IPagingTypeMapper<TEntityDto, TEntity, TEntityId>
        where TEntityDto : DtoBase, new()
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        public PagingInfo GetPagingInfo(PaginatedRequest paginatedRequest)
        {
            if (paginatedRequest == null)
            {
                throw new ArgumentNullException("paginatedRequest", "paginatedRequest cannot be null.");
            }

            PagingInfo pagingInfo = Mapper.Map<PaginatedRequest, PagingInfo>(paginatedRequest);

            ////Get sort direction and convert to SortDirection enum.
            SortDirection sortDirection;

            ////If sort field is not valid name, throw expception.
            if (!Enum.TryParse(paginatedRequest.SortDirection, true, out sortDirection))
            {
                throw new InvalidSortExpressionException(string.Format("{0} is not a valid sort direction.  Please provide one of the supported values: \"asc\" or \"desc\" (case-insensitive).", paginatedRequest.SortDirection));
            }

            ////Map the SortInfo property manually as AutoMapper tends not to support object graph expanding well.
            pagingInfo.SortInfo = new SortingInfo { SortField = paginatedRequest.SortField, SortDirection = sortDirection };

            return pagingInfo;
        }

        public PaginatedListDto<TEntityDto> GetPaginatedListDto(PaginatedList<TEntity, TEntityId> paginatedList, IEntityTypeMapper<TEntityDto, TEntity, TEntityId> entityMapper, string entityUri)
        {
            if (paginatedList == null)
            {
                throw new ArgumentNullException("paginatedList", "paginatedList cannot be null.");
            }

            if (entityMapper == null)
            {
                throw new ArgumentNullException("entityMapper", "entityMapper cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(entityUri))
            {
                throw new ArgumentException("entityUri cannot be null or empty string", "entityUri");
            }

            return new PaginatedListDto<TEntityDto>
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = paginatedList.PageSize,
                NumberPages = paginatedList.NumberPages,
                HasPreviousPage = paginatedList.HasPreviousPage,
                HasNextPage = paginatedList.HasNextPage,
                EntityDtos = PagingTypeMapper<TEntityDto, TEntity, TEntityId>.GetDtosFromEntities(paginatedList.AsEnumerable(), entityMapper, entityUri)
            };
        }

        #region Private Methods

        private static IEnumerable<TEntityDto> GetDtosFromEntities(IEnumerable<TEntity> entities, IEntityTypeMapper<TEntityDto, TEntity, TEntityId> entityMapper, string entityUri)
        {
            return entities.Select(entity => entityMapper.GetEntityDto(entity, entityUri));
        }

        #endregion
    }
}