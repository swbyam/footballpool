//-----------------------------------------------------------------------
// <copyright file="SuppressedRequiredMemberSelector.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.FootballPool.WebApi.Formatting
{
    using System;
    using System.Net.Http.Formatting;
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="System.Net.Http.Formatting.IRequiredMemberSelector"/> interface that indicates that every member of an object being passed in an HTTP request does not need a value in order to be deserialized.
    /// </summary>
    public class SuppressedRequiredMemberSelector : IRequiredMemberSelector 
    {
        /// <summary>
        /// Determines that every member of an object being sent to in an HTTP request does not need to be populated in order for deserialization to occur.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo to be deserialized.</param>
        /// <returns>False indicating that any member of an object does not need to be deserialized.</returns>
        public bool IsRequiredMember(MemberInfo member) 
        {
            if (member == null)
            {
                throw new ArgumentNullException("member", "member cannot be null.");
            }

            return false;
        }
    }
}