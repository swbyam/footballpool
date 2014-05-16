//-----------------------------------------------------------------------
// <copyright file="PaginatedListDto.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data Transfer Object (DTO) that represents a paginated list of a particular entity.
    /// </summary>
    /// <typeparam name="TEntityDto">Type of entity DTO that is contained in the paginated list.</typeparam>
    /// <remarks>There currently is not the ability based on the design to constrain the entity DTO type such that it is actually a DTO type.  Since it could be any type, it is up to the consumer of this class to use it with DTO types.</remarks>
    public class PaginatedListDto<TEntityDto>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the page number representing the items contained in the paginated list.
        /// <remarks>Page number is not zero-based.  So the first page of a result set would have a page number of 1.</remarks>
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page contained in the paginated list.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page contained in the paginated list.
        /// </summary>
        public int NumberPages { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether or not the result set has a previous page.
        /// </summary>
        /// <remarks>This property will return false when the paginated list is at the first page.</remarks>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the result set has a next page.
        /// </summary>
        /// <remarks>This property will return false when the paginated list is at the last page of the result set.</remarks>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Gets or sets a collection DTO's representing entities contained in the paginated list.
        /// </summary>
        public IEnumerable<TEntityDto> EntityDtos { get; set; }

        #endregion
    }
}
