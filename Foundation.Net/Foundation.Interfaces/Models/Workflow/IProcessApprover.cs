//-----------------------------------------------------------------------
// <copyright file="IApprovalTask.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The Approval Task model interface
    /// </summary>
    public interface IProcessApprover : IFoundationModel
    {
        /// <summary>
        /// 
        /// </summary>
        String ProcessName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        EntityId ApproverUserProfileId { get; set; }
    }
}
