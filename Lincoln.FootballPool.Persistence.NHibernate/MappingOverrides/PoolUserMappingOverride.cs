//-----------------------------------------------------------------------
// <copyright file="PoolUserMappingOverride.cs" company="Optum">
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
    /// NHibernate auto-mapping mapping override to the <see cref="PoolUser"/>
    /// </summary> entity.
    public class PoolUserMappingOverride : IAutoMappingOverride<PoolUser>
    {
        /// <summary>
        /// Overiddes mappings defined in the supplied <paramref name="mapping"/>.
        /// </summary>
        /// <param name="mapping">Auto mapping context information for the <see cref="PoolUser"/> entity.</param>
        public void Override(AutoMapping<PoolUser> mapping)
        {
            mapping.Id(poolUser => poolUser.Id).Column("PoolUserID");

            mapping.Version(poolUser => poolUser.Version).CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

    }
}
