//-----------------------------------------------------------------------
// <copyright file="CustomAutoMappingConfiguration.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.NHibernateFramework
{
    using System;
    using System.Globalization;

    using FluentNHibernate;
    using FluentNHibernate.Automapping;

    using Lincoln.FootballPool.Domain.Entities;

    public class CustomAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        #region Public Methods

        public override bool ShouldMap(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "type cannot be null.");
            }

            ////TODO: Come up with better way to do this!
            return type == typeof(Game) || type == typeof(Team) || type == typeof(Bet) || type == typeof(Pool) || type == typeof(PoolUser);
        }

        public override bool IsId(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member", "member cannot be null.");
            }

            return member.Name.Equals("Id", StringComparison.InvariantCulture);
        }

        public override bool IsVersion(Member member)
        {
            return base.IsVersion(member);
        }

        #endregion
    }
}
