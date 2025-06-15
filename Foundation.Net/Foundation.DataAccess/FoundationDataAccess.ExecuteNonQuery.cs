//-----------------------------------------------------------------------
// <copyright file="FoundationDataAccess.ExecuteNonQuery.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.DataAccess.Database
{
    /// <summary>
    /// Defines the FoundationDataAccess class
    /// </summary>
    public partial class FoundationDataAccess
    {
        /// <inheritdoc cref="IFoundationDataAccess.ExecuteScalar(String, CommandType, IDatabaseParameters)"/>
        public Int32 ExecuteNonQuery(String sql, CommandType commandType = CommandType.Text, IDatabaseParameters databaseParameters = null)
        {
            LoggingHelpers.TraceCallEnter(sql, commandType, databaseParameters);

            Int32 retVal = -1;

            using (IDbConnection conn = GetConnection())
            {
                retVal = ExecuteNonQuery(conn, sql, commandType, databaseParameters);
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IFoundationDataAccess.ExecuteScalar(IDbConnection, String, CommandType, IDatabaseParameters)"/>
        public Int32 ExecuteNonQuery(IDbConnection conn, String sql, CommandType commandType = CommandType.Text, IDatabaseParameters databaseParameters = null)
        {
            LoggingHelpers.TraceCallEnter(conn, sql, commandType, databaseParameters);

            Int32 retVal = -1;
            
            using (IDbCommand command = conn.CreateCommand())
            {
                if (DatabaseTransaction.IsNotNull())
                {
                    command.Transaction = DatabaseTransaction;
                }

                if (databaseParameters.HasItems())
                {
                    databaseParameters.ToList().ForEach(p => command.Parameters.Add(p));
                }

                command.CommandText = sql;
                command.CommandType = commandType;

                retVal = command.ExecuteNonQuery();
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
