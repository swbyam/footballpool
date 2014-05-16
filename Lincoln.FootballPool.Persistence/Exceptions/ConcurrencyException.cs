//-----------------------------------------------------------------------
// <copyright file="ConcurrencyException.cs" company="Optum">
//     Copyright (c) Optum. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Persistence
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom exception class that is thrown when a concurrency error occurs at the persistence layer.
    /// </summary>
    [Serializable]
    public class ConcurrencyException : Exception
    {
        #region Member Variables

        /// <summary>
        /// Name of the type of entity that was the subject of the concurrency exception.
        /// </summary>
        private string entityTypeName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        public ConcurrencyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="entityTypeName">Name of the type of entity that was the subject of the concurrency exception.</param>
        public ConcurrencyException(string message, string entityTypeName)
            : base(message)
        {
            if (string.IsNullOrEmpty(entityTypeName))
            {
                throw new ArgumentNullException("entityTypeName", "entityTypeName cannot be null.");
            }

            this.entityTypeName = entityTypeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="innerException">Exception to be included in this exception.</param>
        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="innerException">Exception to be included in this exception.</param>
        /// <param name="entityTypeName">Name of the type of entity that was the subject of the concurrency exception.</param>
        public ConcurrencyException(string message, Exception innerException, string entityTypeName)
            : base(message, innerException)
        {
            if (string.IsNullOrEmpty(entityTypeName))
            {
                throw new ArgumentNullException("entityTypeName", "entityTypeName cannot be null.");
            }

            this.entityTypeName = entityTypeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        public ConcurrencyException()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="info">Serialization info that contains al data that needs to be serialized with the exception.</param>
        /// <param name="context">SteamingContext that defines source and destination of serialized info.</param>
        protected ConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the type of entity that was the subject of the concurrency exception.
        /// </summary>
        public string EntityTypeName
        {
            get { return this.entityTypeName; }
        }

        #endregion
    }
}
