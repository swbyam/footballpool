//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.Logging
{
    using System;

    /// <summary>
    /// Abstraction for logging implementation that provides basic logging functionality.
    /// </summary>
    public interface ILogger
    {
        #region Public Methods

        void Log();

        #endregion
    }
}
