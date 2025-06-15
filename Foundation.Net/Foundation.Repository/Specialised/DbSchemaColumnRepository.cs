//-----------------------------------------------------------------------
// <copyright file="DbSchemaColumnRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Foundation.Common;
using Foundation.DataAccess.Database;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Repository
{
    /// <summary>
    /// Defines the Db Schema Column Data Access class
    /// </summary>
    /// <see cref="IDbSchemaColumn" />
    [DependencyInjectionTransient]
    public class DbSchemaColumnRepository : FoundationDataAccess, IDbSchemaColumnRepository
    {
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        protected String EntityName => FDC.Specialised.DbSchemaColumn.EntityName;

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        protected String TableName => FDC.TableNames.Specialised.DbSchemaColumn;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbSchemaColumnRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="databaseProvider"></param>
        public DbSchemaColumnRepository
        (
            ICore core,
            ISchemaDatabaseProvider databaseProvider
        ) :
            base
            (
                core,
                databaseProvider
            )
        {
            LoggingHelpers.TraceCallEnter(core, databaseProvider);

            // Does nothing

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IDbSchemaColumnRepository.GetAllColumns(IDbSchemaTable)"/>
        public List<IDbSchemaColumn> GetAllColumns(IDbSchemaTable dbSchemaTable)
        {
            LoggingHelpers.TraceCallEnter(dbSchemaTable);

            List<IDbSchemaColumn> retVal = GetAllColumns(dbSchemaTable.TableCatalog, dbSchemaTable.TableSchema, dbSchemaTable.TableName);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IDbSchemaColumnRepository.GetAllColumns(String, String, String)"/>
        public List<IDbSchemaColumn> GetAllColumns(String tableCatalog, String tableSchema, String tableName)
        {
            LoggingHelpers.TraceCallEnter(tableCatalog, tableSchema, tableName);

            List<IDbSchemaColumn> retVal = new List<IDbSchemaColumn>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    *");
            sql.AppendLine("FROM");
            sql.AppendLine(TableName);
            sql.AppendLine("WHERE");
            sql.AppendLine($"    {FDC.Specialised.DbSchemaColumn.TableCatalog} = {DataLogicProvider.DatabaseParameterPrefix}{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableCatalog} AND");
            sql.AppendLine($"    {FDC.Specialised.DbSchemaColumn.TableSchema} = {DataLogicProvider.DatabaseParameterPrefix}{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableSchema} AND");
            sql.AppendLine($"    {FDC.Specialised.DbSchemaColumn.TableName} = {DataLogicProvider.DatabaseParameterPrefix}{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableName}");

            DatabaseParameters databaseParameters = new DatabaseParameters
            {
                CreateParameter($"{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableCatalog}", tableCatalog),
                CreateParameter($"{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableSchema}", tableSchema),
                CreateParameter($"{FDC.Specialised.DbSchemaColumn.EntityName}{FDC.Specialised.DbSchemaColumn.TableName}", tableName),
            };

            using (IDataReader dataReader = ExecuteReader(sql.ToString(), CommandType.Text, databaseParameters))
            {
                while (dataReader.Read())
                {
                    IDbSchemaColumn entity = PopulateEntity(dataReader);
                    retVal.Add(entity);
                }

                dataReader.Close();
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <summary>
        /// Populates the entity.
        /// </summary>
        /// <param name="dataRecord">The data record.</param>
        /// <returns></returns>
        private IDbSchemaColumn PopulateEntity(IDataRecord dataRecord)
        {
            LoggingHelpers.TraceCallEnter(dataRecord);

            IDbSchemaColumn retVal = Core.Container.Get<IDbSchemaColumn>();

            retVal.TableName = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaColumn.TableName]);
            retVal.ColumnName = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaColumn.ColumnName]);

            String dbType = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaColumn.DataType]);
            retVal.DataType = DataLogicProvider.MapDbTypeToDotNetType(dbType);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
