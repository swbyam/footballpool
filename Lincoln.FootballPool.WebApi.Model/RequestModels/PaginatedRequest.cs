//-----------------------------------------------------------------------
// <copyright file="PaginatedRequest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    /// <summary>
    /// Request model class that contains information needed to page the result set returned from a web service call.
    /// </summary>
    public class PaginatedRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the page number.
        /// <remarks>Page number is not zero-based.  So the first page of a result set would have a page number of 1.</remarks>
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "PageNumber must be greater than 0.")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        [Required]
        [Min(1, ErrorMessage = "PageSize must be greater than 0.")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the field name by which the result set is ordered or sorted by.
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the direction of the sort.
        /// </summary>
        /// <remarks>Supported values include "Asc" and "Desc" in a case-insensitive manner.</remarks>
        public string SortDirection { get; set; }

        ////TODO: Create custom data annotation that verifies the either "asc" or "desc" were supplied for SortDirection.

        #endregion
    }
}
