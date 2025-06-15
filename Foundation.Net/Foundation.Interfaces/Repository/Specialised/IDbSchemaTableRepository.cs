﻿//-----------------------------------------------------------------------
// <copyright file="IDbSchemaTableRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The Database Schema Table Data Access interface
    /// </summary>
    public interface IDbSchemaTableRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<IDbSchemaTable> GetAllTables();
    }
}
