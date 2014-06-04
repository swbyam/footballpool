//-----------------------------------------------------------------------
// <copyright file="GameMappingOverride.cs" company="Optum">
//     Copyright (c) Optum. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.MappingOverrides
{
    using System;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using Lincoln.FootballPool.Domain.Entities;

    /// <summary>
    /// NHibernate auto-mapping mapping override to the <see cref="Game"/>
    /// </summary> entity.
    public class GameMappingOverride : IAutoMappingOverride<Game>
    {
        #region Public Methods

        /// <summary>
        /// Overrides mappings defined in the supplied <paramref name="mapping"/>.
        /// </summary>
        /// <param name="mapping">Auto mapping context information for the <see cref="Game"/> entity.</param>
        public void Override(AutoMapping<Game> mapping)
        {
            mapping.Id(game => game.Id).GeneratedBy.Identity();
            mapping.Map(game => game.StartDateTime, "GameDate");

            mapping.Version(game => game.Version).CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

        #endregion
    }
}
