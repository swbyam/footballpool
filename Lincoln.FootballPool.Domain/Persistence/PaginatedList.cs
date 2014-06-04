//-----------------------------------------------------------------------
// <copyright file="PaginatedList.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence
{
    using System;
    using System.Collections.Generic;

    using Lincoln.FootballPool.Domain.Entities;

    public class PaginatedList<TEntity, TEntityId> : List<TEntity>
        where TEntity : Entity<TEntityId>, new()
        where TEntityId : struct
    {
        #region Member Variables

        /// <summary>
        /// Page number representing the items contained in the paginated list.
        /// </summary>
        private int pageNumber;

        /// <summary>
        /// Number of items per page contained in the paginated list.
        /// </summary>
        private int pageSize;

        /// <summary>
        // Total number of items in the persistence store.
        /// </summary>
        private int totalNumberItems;

        /// <summary>
        /// Field name by which the result set is ordered or sorted by.
        /// </summary>
        private string sortField;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{TEntity,TEntityId}"/> class.
        /// </summary>
        /// <param name="items">Items or entities that are to be included in the paginated list.</param>
        /// <param name="totalNumberItems">Total number of items in the persistence store.</param>
        /// <param name="pageNumber">Page number representing the items contained in the paginated list.</param>
        /// <param name="pageSize">Number of items per page contained in the paginated list.</param>
        /// <param name="sortField">Field name by which the result set is ordered or sorted by.</param>
        public PaginatedList(IEnumerable<TEntity> items, int totalNumberItems, int pageNumber, int pageSize, string sortField)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items", "items cannot be null.");
            }

            if (totalNumberItems <= 0)
            {
                throw new ArgumentException("totalNumberItems cannot be less than or equal to zero.", "totalNumberItems");
            }

            if (pageNumber <= 0)
            {
                throw new ArgumentException("pageNumber cannot be less than or equal to zero.", "pageNumber");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("pageSize cannot be less than or equal to zero.", "pageSize");
            }

            if (sortField == null)
            {
                throw new ArgumentNullException("sortField", "sortField cannot be null.");
            }

            ////TODO: Conduct any additonal parameter validation here?

            this.AddRange(items);
            this.totalNumberItems = totalNumberItems;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.sortField = sortField;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the page number representing the items contained in the paginated list.
        /// <remarks>Page number is not zero-based.  So the first page of a result set would have a page number of 1.</remarks>
        /// </summary>
        public int PageNumber
        {
            get { return this.pageNumber; }
        }

        /// <summary>
        /// Gets the number of items per page contained in the paginated list.
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
        }

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
        /// Gets the number of items per page contained in the paginated list.
        /// </summary>
        public int NumberPages
        {
            get 
            {
                ////If there are no items, return 0 as there are no pages in this case.
                if (this.totalNumberItems == 0)
                {
                    return 0;
                }

                int numberPages = (int)Math.Ceiling(this.totalNumberItems / (double)this.pageSize);

                ////If the calculated number of pages comes out to zero (which will happen when there are less items than the page size), return 1.
                return numberPages > 0 ? numberPages : 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the result set has a previous page.
        /// </summary>
        /// <remarks>This property will return false when the paginated list is at the first page.</remarks>
        public bool HasPreviousPage
        {
            get { return this.pageNumber > 1; }
        }

        /// <summary>
        /// Gets a value indicating whether or not the result set has a next page.
        /// </summary>
        /// <remarks>This property will return false when the paginated list is at the last page of the result set.</remarks>
        public bool HasNextPage
        {
            get { return this.pageNumber < this.NumberPages; }
        }

        #endregion
    }
}
