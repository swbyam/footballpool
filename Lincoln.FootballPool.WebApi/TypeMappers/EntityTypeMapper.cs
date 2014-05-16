//-----------------------------------------------------------------------
// <copyright file="EntityTypeMapper.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Routing;

    using AutoMapper;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.WebApi.Model;
    using Lincoln.FootballPool.WebApi.Model.Dtos;

    public abstract class EntityTypeMapper<TEntityDto, TEntity, TEntityId> : IEntityTypeMapper<TEntityDto, TEntity, TEntityId>
        where TEntityDto : DtoBase, new()
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        #region Public Methods

        /// <summary>
        /// Maps the supplied <paramref name="entity"/> instance to its DTO counterpart.
        /// </summary>
        /// <param name="entity">Entity to convert.</param>
        /// <param name="entityUri">Uri of the entity that is to be converted to its DTO counterpart.</param>
        /// <returns>Entity DTO instance.</returns>
        public TEntityDto GetEntityDto(TEntity entity, string entityUri)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(entityUri))
            {
                throw new ArgumentException("entityUri cannot be null or empty string", "entityUri");
            }

            ////Map entity instance to DTO instance.
            TEntityDto entityDto = Mapper.Map<TEntity, TEntityDto>(entity);

            ////Get hypermedia links that need to be added to DTO.
            IEnumerable<Link> hypermediaLinks = this.CreateHypermediaLinks(entity, entityUri);

            ////If hypermedia links were supplied by subclass implementation, add them to DTO.
            if (hypermediaLinks != null)
            {
                foreach (Link link in hypermediaLinks)
                {
                    entityDto.AddHypermediaLink(link);
                }
            }

            return entityDto;
        }

        /// <summary>
        /// Maps the supplied list of entities <paramref name="entities"/> into a list of their DTO counterparts <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">List of entities to convert.</param>
        /// <param name="urlHelper">Url helper used to assist in creating DTO's for each of the supplied entities.</param>
        /// <returns>List of entity DTO instances.</returns>
        public IEnumerable<TEntityDto> GetEntityDtos(IEnumerable<TEntity> entities, UrlHelper urlHelper)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities", "entities cannot be null.");
            }

            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper", "urlHelper cannot be null.");
            }

            return entities.Select(entity => this.GetEntityDto(entity, urlHelper.Link("DefaultApi", new { id = entity.Id })));
        }

        #endregion

        #region Protected Methods

        protected abstract IEnumerable<Link> CreateHypermediaLinks(TEntity entity, string entityUri);

        #endregion
    }
}