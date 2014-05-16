//-----------------------------------------------------------------------
// <copyright file="SortingInfo.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence
{
    using System;

    /// <summary>
    /// Enum contains possible sort direction values.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending sort direction.
        /// </summary>
        Asc,

        /// <summary>
        /// Descending sort direction.
        /// </summary>
        Desc
    }

    /// <summary>
    /// Class contains information needed to sort the result set from a persistence store such as the name of the sort field and sort direction.
    /// </summary>
    public class SortingInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the field name by which the result set is ordered or sorted by.
        /// </summary>
        /// <remarks>This is an optional field.  To specify that the list is not sorted, set this property to an empty string.</remarks>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the direction of the sort.
        /// </summary>
        public SortDirection SortDirection { get; set; }

        #endregion
    }
}
