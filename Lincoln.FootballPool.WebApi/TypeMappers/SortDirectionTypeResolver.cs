using System;

using AutoMapper;

using Lincoln.FootballPool.Persistence;
using Lincoln.FootballPool.WebApi.Model.RequestModels;

namespace Lincoln.FootballPool.WebApi.TypeMappers
{
    public class SortDirectionTypeResolver : ValueResolver<PaginatedRequest, SortDirection>
    {
        #region Protected Methods

        protected override SortDirection ResolveCore(PaginatedRequest source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "source cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(source.SortDirection))
            {
                return SortDirection.Asc;
            }

            return (SortDirection)Enum.Parse(typeof(SortDirection), source.SortDirection, true);
        }

        #endregion
    }
}