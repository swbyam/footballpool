//-----------------------------------------------------------------------
// <copyright file="Link.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Model
{
    using System;

    /// <summary>
    /// Class represents link that is contained in an HTTP request used to reference media types or resources
    /// contained in the request per the Hypermedia As The Engine Of Application State (HATEOAS) paradigm.
    /// </summary>
    public class Link
    {
        #region Properties

        /// <summary>
        /// Gets or sets the "rel" attribute of the link which specifies the relationship between the resource and the resource identified in the link.
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// Gets or sets the "href" attribute of the link which references the linked resource's address.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the "title" attribute of the link which describes the action i.e. delete, update, next etc.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the "type" attribute of the link which is the MIME type of the linked resource.
        /// </summary>
        public string Type { get; set; }

        #endregion
    }
}
