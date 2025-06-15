﻿//-----------------------------------------------------------------------
// <copyright file="EntityLife.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The Entity Life Enumeration
    /// </summary>
    public enum EntityLife
    {
        /// <summary>
        /// The entity has been created
        /// </summary>
        [Id(0), Display(Order = 1, Name = "Created")]
        Created = 0,

        /// <summary>
        /// The entity has been deleted
        /// </summary>
        [Id(1), Display(Order = 1, Name = "Deleted")]
        Deleted = 1,

        /// <summary>
        /// The entity has been updated
        /// </summary>
        [Id(2), Display(Order = 3, Name = "Updated")]
        Updated = 2,

        /// <summary>
        /// The entity has been loaded
        /// </summary>
        [Id(3), Display(Order = 4, Name = "Loaded")]
        Loaded = 3,
    }
}
