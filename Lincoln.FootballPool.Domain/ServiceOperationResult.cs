//-----------------------------------------------------------------------
// <copyright file="ServiceOperationResult.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    /// <summary>
    /// Enum contains possible values of a save operation for a particular entity in the persistence store.
    /// </summary>
    public enum OperationResult
    {
        /// <summary>
        /// Entity has been saved successfully.
        /// </summary>
        Succeeded,

        /// <summary>
        /// Save of the entity failed.
        /// </summary>
        Failed
    }

    /// <summary>
    /// Class represents the result of a service-related operation involving an entity against the persistence store.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity on which the service operation result is based.</typeparam>
    public class ServiceOperationResult<TEntity>
        where TEntity : class, new()
    {
        #region Member Variables

        /// <summary>
        /// Broken rules for operation result.
        /// </summary>
        private IList<string> brokenRules;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceOperationResult"/> instance.
        /// </summary>
        public ServiceOperationResult()
        {
            this.brokenRules = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of the service operation.
        /// </summary>
        /// <remarks>The service operation result is considered successful if there are no broken rules associated with the operation.  If there is at least one broken rule, the operation is deemed failed.</remarks>
        public OperationResult OperationResult
        {
            get
            {
                return this.brokenRules.Count > 0 ? OperationResult.Failed : OperationResult.Succeeded;
            }
        }

        /// <summary>
        /// Gets or sets a message associated with the service operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets a read-only collection of the broken rules associated with the execution of the service operation.
        /// </summary>
        ReadOnlyCollection<string> BrokenRulesRegardingOperation
        {
            get { return new ReadOnlyCollection<string>(this.brokenRules); }
        }

        /// <summary>
        /// Gets or sets the entity on which the service operation is based.
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the service operation can be executed.
        /// </summary>
        /// <remarks>The operation cannot be executed if there is at least one broken rule regarding execution.</remarks>
        public bool CanServiceOperationBeExecuted
        {
            get { return this.brokenRules.Count == 0; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the supplied <paramref name="brokenRule"/> to the list of broken rules associated with the service operation.
        /// </summary>
        /// <param name="brokenRule">Broken rule to add to service operation result.  If an empty string is passed, it will be ignored and the method will return.</param>
        public void AddBrokenRule(string brokenRule)
        {
            if (brokenRule == null)
            {
                throw new ArgumentNullException("brokenRule", "brokenRule cannot be null or empty string");
            }

            if (brokenRule.Length == 0)
            {
                return;
            }

            this.brokenRules.Add(brokenRule);
        }

        /// <summary>
        /// Gets a textual summary of all the broken service rules associated with the operation result.
        /// </summary>
        /// <returns>Summary of broken rules.  If operation result does not have any broken rules, an empty string is returned.</returns>
        public string GetBrokenRulesSummary()
        {
            StringBuilder builder = new StringBuilder();
            
            foreach(string brokenRule in this.brokenRules)
            {
                builder.AppendLine(brokenRule);
            }

            return builder.ToString();
        }

        #endregion
    }
}
