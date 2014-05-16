//-----------------------------------------------------------------------
// <copyright file="BetMappingOverride.cs" company="Optum">
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
    /// NHibernate auto-mapping mapping override to the <see cref="Bet"/>
    /// </summary> entity.
    public class BetMappingOverride : IAutoMappingOverride<Bet>
    {
        #region Public Methods

        /// <summary>
        /// Overiddes mappings defined in the supplied <paramref name="mapping"/>.
        /// </summary>
        /// <param name="mapping">Auto mapping context information for the <see cref="Bet"/> entity.</param>
        public void Override(AutoMapping<Bet> mapping)
        {
            mapping.Id(bet => bet.Id).Column("BetID");

            mapping.Version(bet => bet.Version).CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

        #endregion
    }
}
