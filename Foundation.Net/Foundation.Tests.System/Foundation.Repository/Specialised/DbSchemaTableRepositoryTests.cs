//-----------------------------------------------------------------------
// <copyright file="DbSchemaTableRepositoryTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.System.Support;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.System.Foundation.Repository.Specialised
{
    [TestFixture]
    public class DbSchemaTableRepositoryTests : DataAccessSystemTestBase
    {
        [TestCase]
        public void Test_GetAllTables()
        {
            IDbSchemaTableRepository dataAccess = Core.Core.Instance.Container.Get<IDbSchemaTableRepository>();

            List<IDbSchemaTable> allTables = dataAccess.GetAllTables();

            Int32 databaseTableCount = allTables.Count;
            databaseTableCount--; // UnitTesting.dbo.TestEntity

            foreach (IDbSchemaTable dbSchemaTable in allTables)
            {
                Debug.WriteLine($"{dbSchemaTable.TableCatalog}.{dbSchemaTable.TableSchema}.{dbSchemaTable.TableName}");
            }

            Type theType = typeof(FDC.TableNames);
            PropertyInfo[] propertyInfos = theType.GetProperties();

            Int32 tablesMemberCount = propertyInfos.Length;

            Assert.That(databaseTableCount, Is.EqualTo(tablesMemberCount));
        }
    }
}
