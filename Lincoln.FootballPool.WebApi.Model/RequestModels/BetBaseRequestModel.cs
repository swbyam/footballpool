
namespace Lincoln.FootballPool.WebApi.Model.RequestModels
{
    using System;

    public class BetBaseRequestModel
    {
        #region Properties

        public int TeamToCoverBetId { get; set; }

        public int Amount { get; set; }

        #endregion
    }
}
