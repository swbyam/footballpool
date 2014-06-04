//-----------------------------------------------------------------------
// <copyright file="InvalidSortExpressionException.cs" company="Optum">
//     Copyright (c) Optum. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom exception that represents errors that occur when creating a sort expression that is to be executed against a persistence store.  Such errors could include an invalid sort field name, a bad sort expression etc.
    /// </summary>
    [Serializable]
    public class InvalidSortExpressionException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSortExpressionException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        public InvalidSortExpressionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSortExpressionException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="innerException">Exception to be included in this exception.</param>
        public InvalidSortExpressionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSortExpressionException"/> class.
        /// </summary>
        public InvalidSortExpressionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSortExpressionException"/> class.
        /// </summary>
        /// <param name="info">Serialization info that contains al data that needs to be serialized with the exception.</param>
        /// <param name="context">SteamingContext that defines source and destination of serialized info.</param>
        protected InvalidSortExpressionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
