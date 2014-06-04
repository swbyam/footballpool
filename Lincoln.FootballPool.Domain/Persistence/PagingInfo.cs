//-----------------------------------------------------------------------
// <copyright file="PagingInfo.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence
{
    using System;

    /// <summary>
    /// Class that contains information needed to page a result set from a persistence store such as the page number and the page size.
    /// </summary>
    public class PagingInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the page number.
        /// <remarks>Page number is not zero-based.  So the first page of a result set would have a page number of 1.</remarks>
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the field name by which the result set is ordered or sorted by.
        /// </summary>
        /// <remarks>This is an optional field.  To specify that the list is not sorted, set this property to an empty string.</remarks>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the direction of the sort.
        /// </summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets start index of an item in the result set based on the page number and page size.
        /// </summary>
        /// <remarks>Page size is not zero-based.  For example, a page of 2 and a page size of 10 yields a start index of 11.</remarks>
        public int StartItemIndex
        {
            get { return (this.PageNumber - 1) * this.PageSize + 1; }
        }

        #endregion
    }
}
