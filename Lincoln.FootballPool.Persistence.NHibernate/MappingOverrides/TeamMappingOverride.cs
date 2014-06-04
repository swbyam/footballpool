
namespace Lincoln.FootballPool.Persistence.NHibernateFramework.MappingOverrides
{
    using System;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using Lincoln.FootballPool.Domain.Entities;

    public class TeamMappingOverride : IAutoMappingOverride<Team>
    {
        #region Public Methods

        public void Override(AutoMapping<Team> mapping)
        {
            mapping.Id(team => team.Id).Column("TeamID");

            ////Do not map the "TeamFullName" property as it derives a value from existing property values and only has a getter defined.
            mapping.IgnoreProperty(team => team.FullName);

            ////Map the "Name" property to a column in the database named "TeamName".
            mapping.Map(team => team.Name, "TeamName");
        }

        #endregion
    }
}
