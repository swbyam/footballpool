//-----------------------------------------------------------------------
// <copyright file="PersistenceException.cs" company="Optum">
//     Copyright (c) Optum. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain.Persistence
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom exception class that is thrown when an issues occurs at the data layer of CRM performing an operation on the persistence store such as a read, update, delete, add etc.
    /// </summary>
    [Serializable]
    public class PersistenceException : Exception
    {
        #region Member Variables

        /// <summary>
        /// Name of the type of entity that was the subject of the concurrency exception.
        /// </summary>
        private string entityTypeName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        public PersistenceException(string message)
            : base(message)
        {
        }

         /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="entityTypeName">Name of the type of entity that was the subject of the concurrency exception.</param>
        public PersistenceException(string message, string entityTypeName)
            : base(message)
        {
            if (string.IsNullOrEmpty(entityTypeName))
            {
                throw new ArgumentNullException("entityTypeName", "entityTypeName cannot be null.");
            }

            this.entityTypeName = entityTypeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="innerException">Exception to be included in this exception.</param>
        public PersistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

         /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">Message to include in the exception.</param>
        /// <param name="innerException">Exception to be included in this exception.</param>
        /// <param name="entityTypeName">Name of the type of entity that was the subject of the concurrency exception.</param>
        public PersistenceException(string message, Exception innerException, string entityTypeName)
            : base(message, innerException)
        {
            if (string.IsNullOrEmpty(entityTypeName))
            {
                throw new ArgumentNullException("entityTypeName", "entityTypeName cannot be null.");
            }

            this.entityTypeName = entityTypeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        public PersistenceException()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="info">Serialization info that contains al data that needs to be serialized with the exception.</param>
        /// <param name="context">SteamingContext that defines source and destination of serialized info.</param>
        protected PersistenceException(SerializationInfo info, StreamingContext context)
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
