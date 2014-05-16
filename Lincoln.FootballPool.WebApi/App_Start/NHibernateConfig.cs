//-----------------------------------------------------------------------
// <copyright file="NHibernateConfig.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.AppStart
{
    using System;

    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Conventions.Helpers;

    using Inflector;

    using Lincoln.FootballPool.Domain.Entities;
    using Lincoln.FootballPool.Persistence.NHibernateFramework;
    using Lincoln.FootballPool.Persistence.NHibernateFramework.MappingOverrides;

    using NHibernate;
    using NHibernate.Cfg;

    public static class NHibernateConfig
    {
        #region Public Methods

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringBuilder => connectionStringBuilder.FromConnectionStringWithKey("FootballPoolDb")))
                .CurrentSessionContext("web")
                .Mappings(mappingConfig => mappingConfig.AutoMappings.Add(
                    AutoMap.AssemblyOf<Bet>(new CustomAutoMappingConfiguration())
                    //.Where(type => type.Namespace == "entity namespace")
                    .Conventions.Add(
                    Table.Is(inspector => Inflector.Pluralize(inspector.EntityType.Name)),
                    ForeignKey.Format((member, type) => string.Format("{0}{1}", member.Name, "ID")))
                    .UseOverridesFromAssemblyOf<TeamMappingOverride>()))
                .BuildSessionFactory();
        }

        #endregion
    }
}