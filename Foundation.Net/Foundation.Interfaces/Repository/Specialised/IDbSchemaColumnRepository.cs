//-----------------------------------------------------------------------
// <copyright file="IDbSchemaColumnRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The Database Schema Column Data Access interface
    /// </summary>
    public interface IDbSchemaColumnRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbSchemaTable"></param>
        /// <returns></returns>
        List<IDbSchemaColumn> GetAllColumns(IDbSchemaTable dbSchemaTable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableCatalog"></param>
        /// <param name="tableSchema"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<IDbSchemaColumn> GetAllColumns(String tableCatalog, String tableSchema, String tableName);
    }
}
