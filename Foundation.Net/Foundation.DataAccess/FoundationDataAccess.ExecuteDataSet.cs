//-----------------------------------------------------------------------
// <copyright file="FoundationDataAccess.ExecuteDataSet.cs" company="JDV Software Ltd">
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
        /// <inheritdoc cref="IFoundationDataAccess.ExecuteDataSet(String, CommandType, IDatabaseParameters)"/>
        public DataSet ExecuteDataSet(String sql, CommandType commandType = CommandType.Text, IDatabaseParameters databaseParameters = null)
        {
            LoggingHelpers.TraceCallEnter(sql, commandType, databaseParameters);

            DataSet retVal;

            using (IDbConnection conn = GetConnection())
            {
                retVal = ExecuteDataSet(conn, sql, commandType, databaseParameters);
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IFoundationDataAccess.ExecuteDataSet(IDbConnection, String, CommandType, IDatabaseParameters)"/>
        public DataSet ExecuteDataSet(IDbConnection conn, String sql, CommandType commandType = CommandType.Text, IDatabaseParameters databaseParameters = null)
        {
            LoggingHelpers.TraceCallEnter(conn, sql, commandType, databaseParameters);

            DataSet retVal;

            using (IDbCommand command = conn.CreateCommand())
            {
                if (DatabaseTransaction.IsNotNull())
                {
                    command.Transaction = DatabaseTransaction;
                }

                IDbDataAdapter dataAdapter = DatabaseProviderFactory.CreateDataAdapter();

                if (databaseParameters.HasItems())
                {
                    databaseParameters.ToList().ForEach(p => command.Parameters.Add(p));
                }

                command.CommandText = sql;
                command.CommandType = commandType;

                dataAdapter.SelectCommand = command;

                retVal = new DataSet();
                dataAdapter.Fill(retVal);
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
