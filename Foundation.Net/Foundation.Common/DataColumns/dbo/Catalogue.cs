﻿//-----------------------------------------------------------------------
// <copyright file="Catalogue.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Common.DataColumns
{
    /// <summary>
    /// Catalogue data columns
    /// </summary>
    /// <seealso cref="FoundationEntity" />
    public abstract class Catalogue : FoundationEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract class Lengths
        {
            /// <summary>
            /// The name
            /// </summary>
            public const Int32 Name = 500;

            /// <summary>
            /// The description
            /// </summary>
            public const Int32 Description = Int32.MaxValue;
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public static String EntityName => nameof(Catalogue);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public static String Name => "Name";

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public static String Description => "Description";

        /// <summary>
        /// Gets the front cover.
        /// </summary>
        /// <value>
        /// The front cover.
        /// </value>
        public static String FrontCover => "FrontCover";
    }
}
