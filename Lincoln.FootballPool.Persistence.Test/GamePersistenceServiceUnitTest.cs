//-----------------------------------------------------------------------
// <copyright file="GamePersistenceServiceUnitTest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence.Test
{
    using System;

    using Lincoln.FootballPool.Domain.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests class that contains tests for the <see cref="Lincoln.FootballPool.Persistence.Services.GamePersistenceService"/> class.
    /// </summary>
    [TestClass]
    public class GamePersistenceServiceUnitTest
    {
        #region Method Tests
       
        /// <summary>
        /// Tests whether or not a game can succesfully be saved in the persistence store.
        /// </summary>
        [TestMethod]
        public void CanSaveGame()
        {
            ////TODO: Download moq and create mocks of repositories that need to be passed to ctor.

            //IGamePersistenceService gamePersistenceService = new GamePersistenceService()
        }

        #endregion
    }
}
