//-----------------------------------------------------------------------
// <copyright file="DbSchemaColumnRepositoryTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.System.Support;

namespace Foundation.Tests.System.Foundation.Repository.Specialised
{
    [TestFixture]
    public class DbSchemaColumnRepositoryTests : DataAccessSystemTestBase
    {
        [TestCase]
        public void Test_GetAll_Object()
        {
            IDbSchemaTableRepository schemaTableDataAccess = Core.Core.Instance.Container.Get<IDbSchemaTableRepository>();

            List<IDbSchemaTable> allTables = schemaTableDataAccess.GetAllTables();

            IDbSchemaColumnRepository dataAccess = Core.Core.Instance.Container.Get<IDbSchemaColumnRepository>();

            foreach (IDbSchemaTable dbSchemaTable in allTables)
            {
                Debug.WriteLine($"{dbSchemaTable.TableCatalog}.{dbSchemaTable.TableSchema}.{dbSchemaTable.TableName}");

                List<IDbSchemaColumn> allTableColumns = dataAccess.GetAllColumns(dbSchemaTable);
                foreach (IDbSchemaColumn dbSchemaColumn in allTableColumns)
                {
                    Debug.WriteLine($"{dbSchemaColumn.TableName}.{dbSchemaColumn.ColumnName} ({dbSchemaColumn.DataType})");
                }
            }
        }

        [TestCase]
        public void Test_GetAll_String()
        {
            IDbSchemaTableRepository schemaTableDataAccess = Core.Core.Instance.Container.Get<IDbSchemaTableRepository>();

            List<IDbSchemaTable> allTables = schemaTableDataAccess.GetAllTables();

            IDbSchemaColumnRepository dataAccess = Core.Core.Instance.Container.Get<IDbSchemaColumnRepository>();

            foreach (IDbSchemaTable dbSchemaTable in allTables)
            {
                Debug.WriteLine($"{dbSchemaTable.TableCatalog}.{dbSchemaTable.TableSchema}.{dbSchemaTable.TableName}");

                List<IDbSchemaColumn> allTableColumns = dataAccess.GetAllColumns(dbSchemaTable.TableCatalog, dbSchemaTable.TableSchema, dbSchemaTable.TableName);
                foreach (IDbSchemaColumn dbSchemaColumn in allTableColumns)
                {
                    Debug.WriteLine($"{dbSchemaColumn.TableName}.{dbSchemaColumn.ColumnName} ({dbSchemaColumn.DataType})");
                }
            }
        }
    }
}
