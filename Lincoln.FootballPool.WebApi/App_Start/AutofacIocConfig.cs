//-----------------------------------------------------------------------
// <copyright file="AutofacIocConfig.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.AppStart
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Lincoln.FootballPool.Domain.Persistence.Repositories;
    using Lincoln.FootballPool.Domain.Services;
    using Lincoln.FootballPool.Persistence.NHibernateFramework.Repositories;
    using Lincoln.FootballPool.WebApi.TypeMappers;

    using NHibernate;

    /// <summary>
    /// Sets up and configures the IoC container of the <see cref="HttpConfiguration"/> instance associated with the web api project with the concrete types and interfaces of various constructs such as services, repositories etc. that are used throughout the project.
    /// </summary>
    /// <remarks>This implementation is specific to the Autofac open source framework.</remarks>
    public static class AutofacIocConfig
    {
        #region Public Methods
        
        /// <summary>
        /// Configures the IoC container or DependencyResolver of the supplied HttpConfiguration instance <paramref name="config"/> with services, repositories etc. and their interfaces that are used by the web api project. 
        /// </summary>
        /// <param name="config">HttpConfiguration instance to configure.</param>
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config", "Parameter config cannot be null.");
            }

            AutofacIocConfig.Configure(config, AutofacIocConfig.RegisterServices(new ContainerBuilder()));
        }

        /// <summary>
        /// Configures the IoC container or DependencyResolver of the supplied HttpConfiguration instance <paramref name="config"/> with services, repositories etc. and their interfaces that are contained in the Autofac IContainer instance <paramref name="container"/>.
        /// </summary>
        /// <param name="config">HttpConfiguration instance to configure.</param>
        /// <param name="container">Autofac IContainer instance that represents the IoC container for the application.</param>
        public static void Configure(HttpConfiguration config, IContainer container)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config", "Parameter config cannot be null.");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container", "Parameter container cannot be null.");
            }

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers services and other constructs using the supplied Autofac <see cref="containerBuilder"/>instance <paramref name="containerBuilder"/>.
        /// </summary>
        /// <param name="containerBuilder">Autofac ContainerBuilder instance to which services are to be registered.</param>
        /// <returns>Autofac IContainer instance.</returns>
        private static IContainer RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            ////Register dependencies for NHibernate.
            AutofacIocConfig.RegisterNHibernateDependencies(containerBuilder);

            ////Register services, repositories, and type mappers.
            containerBuilder.RegisterType<GameService>().As<IGameService>().InstancePerApiRequest();
            containerBuilder.RegisterType<PoolRepository>().As<IPoolRepository>().InstancePerApiRequest();
            containerBuilder.RegisterType<GameRepository>().As<IGameRepository>().InstancePerApiRequest();
            containerBuilder.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerApiRequest();
            containerBuilder.RegisterType<BetRepository>().As<IBetRepository>().InstancePerApiRequest();
            ////containerBuilder.RegisterType<GamePersistenceService>().As<IGamePersistenceService>().InstancePerApiRequest();
            containerBuilder.RegisterType<GameTypeMapper>().As<IGameTypeMapper>().InstancePerApiRequest();
            containerBuilder.RegisterType<BetTypeMapper>().As<IBetTypeMapper>().InstancePerApiRequest();
            containerBuilder.RegisterGeneric(typeof(PagingTypeMapper<,,>)).As(typeof(IPagingTypeMapper<,,>))
                   .InstancePerApiRequest();

            return containerBuilder.Build();
        }

        private static void RegisterNHibernateDependencies(ContainerBuilder containerBuilder)
        {
            ////Register NHibernate SessionFactory as singleton.
            containerBuilder.Register(compContext => NHibernateConfig.CreateSessionFactory()).SingleInstance();

            ////Register ISession as being returned with request.
            containerBuilder.Register(compContext => compContext.Resolve<ISessionFactory>().OpenSession()).InstancePerApiRequest();
        }

        #endregion
    }
}
