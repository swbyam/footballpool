//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Entities
{
    using System;

    /// <summary>
    /// Abstract base class from which all entities derive.
    /// </summary>
    /// <typeparam name="TEntityId">Type of the entity id.</typeparam>
    public abstract class Entity<TEntityId>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the entity.
        /// </summary>
        public virtual TEntityId Id { get; set; }

        /// <summary>
        /// Gets or sets the version of the entity.
        /// </summary>
        public virtual byte[] Version { get; set; }

        #endregion
    }
}
