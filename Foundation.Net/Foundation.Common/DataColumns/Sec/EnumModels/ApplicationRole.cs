﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Common.DataColumns
{
    /// <summary>
    /// Application Role data columns
    /// </summary>
    /// <seealso cref="FoundationEntity" />
    public abstract class ApplicationRole : FoundationEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract class Lengths
        {
            //public const Int32 Name = 25;
            //public const Int32 Description = 150;
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public static String EntityName => "ApplicationRole";

        /// <summary>
        /// Gets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public static String ApplicationId => "ApplicationId";

        /// <summary>
        /// Gets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public static String RoleId => "RoleId";
    }
}
