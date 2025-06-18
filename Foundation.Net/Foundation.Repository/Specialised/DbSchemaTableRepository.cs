//-----------------------------------------------------------------------
// <copyright file="DbSchemaTableRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Foundation.Common;
using Foundation.DataAccess.Database;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Repository
{
    /// <summary>
    /// Defines the Db Schema Table Data Access class
    /// </summary>
    /// <see cref="IDbSchemaTable" />
    [DependencyInjectionTransient]
    public class DbSchemaTableRepository : FoundationDataAccess, IDbSchemaTableRepository
    {
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        protected String EntityName => FDC.Specialised.DbSchemaTable.EntityName;

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        protected String TableName => FDC.TableNames.Specialised.DbSchemaTable;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbSchemaTableRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="databaseProvider"></param>
        public DbSchemaTableRepository
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

            DbSchemaColumnRepository = new DbSchemaColumnRepository(core, databaseProvider);

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Gets the database schema column repository.
        /// </summary>
        /// <value>
        /// The database schema column repository.
        /// </value>
        private IDbSchemaColumnRepository DbSchemaColumnRepository { get; }

        /// <inheritdoc cref="IDbSchemaTableRepository.GetAllTables()"/>
        public List<IDbSchemaTable> GetAllTables()
        {
            LoggingHelpers.TraceCallEnter();

            List<IDbSchemaTable> retVal = new List<IDbSchemaTable>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    *");
            sql.AppendLine("FROM");
            sql.AppendLine(TableName);
            sql.AppendLine("ORDER BY");
            sql.AppendLine($"    {FDC.Specialised.DbSchemaTable.TableName}");

            using (IDataReader dataReader = ExecuteReader(sql.ToString()))
            {
                while (dataReader.Read())
                {
                    IDbSchemaTable entity = PopulateEntity(dataReader);
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
        private IDbSchemaTable PopulateEntity(IDataRecord dataRecord)
        {
            LoggingHelpers.TraceCallEnter(dataRecord);

            IDbSchemaTable retVal = Core.Container.Get<IDbSchemaTable>();

            retVal.TableCatalog = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaTable.TableCatalog]);
            retVal.TableSchema = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaTable.TableSchema]);
            retVal.TableName = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaTable.TableName]);
            retVal.TableType = Convert.ToString(dataRecord[FDC.Specialised.DbSchemaTable.TableType]);

            IEnumerable<IDbSchemaColumn> dbSchemaColumns = DbSchemaColumnRepository.GetAllColumns(retVal);
            retVal.SchemaColumns.ToList().AddRange(dbSchemaColumns);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
