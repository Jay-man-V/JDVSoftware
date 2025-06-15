﻿//-----------------------------------------------------------------------
// <copyright file="IApplication.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The Application model interface
    /// </summary>
    public interface IApplication : IFoundationModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        new AppId Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        String Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        String Description { get; set; }
    }
}
