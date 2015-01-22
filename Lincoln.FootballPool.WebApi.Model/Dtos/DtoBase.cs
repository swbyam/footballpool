//-----------------------------------------------------------------------
// <copyright file="DtoBase.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Abstract base class from which all DTO's (Data Transfer Objects) derive.
    /// </summary>
    public abstract class DtoBase
    {
        #region Member Variables

        /// <summary>
        /// List of hypermedia links associated with the DTO.
        /// </summary>
        /// <remarks>These are used to provide a HATEOAS-like capability to consumers of the corresponding web service.</remarks>
        private IList<Link> hypermediaLinks;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoBase"/> class.
        /// </summary>
        public DtoBase()
        {
            this.hypermediaLinks = new List<Link>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only collection of links that represent hypermedia related to the game.
        /// </summary>
        /// <remarks>These are used to provide a HATEOAS-like capability to consumers of the corresponding web service.</remarks>
        public ReadOnlyCollection<Link> HypermediaLinks
        {
            get { return new ReadOnlyCollection<Link>(this.hypermediaLinks); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the supplied <paramref name="hypermediaLink"/> to the DTO's list of links.
        /// </summary>
        /// <param name="hypermediaLink">Hypermedia link to add.</param>
        public void AddHypermediaLink(Link hypermediaLink)
        {
            if (hypermediaLink == null)
            {
                throw new ArgumentNullException("hypermediaLink", "hypermediaLink cannot be null.");
            }

            ////TODO: Determine how to check whether the link is already in the hypermedia links list.

            this.hypermediaLinks.Add(hypermediaLink);
        }

        #endregion
    }
}
