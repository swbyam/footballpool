
namespace Lincoln.FootballPool.Domain.Services
{
    using System;

    using Lincoln.FootballPool.Domain.Snapshots;

    public interface IBetPlacementService
    {
        #region Methods

        void PlaceBet(BetSnapshot bet);

        #endregion
    }
}
