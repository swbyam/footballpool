//-----------------------------------------------------------------------
// <copyright file="PoolMappingOverride.cs" company="Optum">
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
    /// NHibernate auto-mapping mapping override to the <see cref="Pool"/>
    /// </summary> entity.
    public class PoolMappingOverride : IAutoMappingOverride<Pool>
    {
        /// <summary>
        /// Overiddes mappings defined in the supplied <paramref name="mapping"/>.
        /// </summary>
        /// <param name="mapping">Auto mapping context information for the <see cref="Pool"/> entity.</param>
        public void Override(AutoMapping<Pool> mapping)
        {
            mapping.Id(pool => pool.Id).Column("PoolID");

            mapping.Version(pool => pool.Version).CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

    }
}
