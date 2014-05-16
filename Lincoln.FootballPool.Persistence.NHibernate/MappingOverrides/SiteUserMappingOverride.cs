//-----------------------------------------------------------------------
// <copyright file="SiteUserMappingOverride.cs" company="Optum">
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
    /// NHibernate auto-mapping mapping override to the <see cref="SiteUser"/>
    /// </summary> entity.
    public class SiteUserMappingOverride : IAutoMappingOverride<SiteUser>
    {
        /// <summary>
        /// Overiddes mappings defined in the supplied <paramref name="mapping"/>.
        /// </summary>
        /// <param name="mapping">Auto mapping context information for the <see cref="SiteUser"/> entity.</param>
        public void Override(AutoMapping<SiteUser> mapping)
        {
            mapping.Id(siteUser => siteUser.Id).Column("SiteUserID");

            mapping.Version(siteUser => siteUser.Version).CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

    }
}
