//-----------------------------------------------------------------------
// <copyright file="TestMapModelsToDatabase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Interfaces;
using Foundation.Repository;
using Foundation.Tests.System.Support;

using NSubstitute;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Foundation.Tests.System.Foundation.Models
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class TestMapModelsToDatabase : SystemTestBase
    {
        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_CheckAllTablesAreMapped()
        {
            ISchemaDatabaseProvider schemaDatabaseProvider = Substitute.For<ISchemaDatabaseProvider>();
            DbSchemaTableRepository dbSchemaTableDataAccess = new DbSchemaTableRepository(CoreInstance, schemaDatabaseProvider);

            List<Type> modelTypes = GetListOfValidTypes();
            IEnumerable<IDbSchemaTable> dbSchemaTables = dbSchemaTableDataAccess.GetAllTables();

            Boolean allExists = dbSchemaTables.Any(t => modelTypes.All(m => m.Name != t.TableName));

            Assert.That(allExists, Is.EqualTo(true));
        }

        private List<Type> GetListOfValidTypes()
        {
            String sourceAssembly = "Foundation.Models.dll";
            Assembly modelAssembly = Assembly.LoadFrom(sourceAssembly);
            Type[] allModelTypes = modelAssembly.GetTypes();

            List<Type> retVal = allModelTypes.Where(t => t.Namespace == "Foundation.Models" &&
                                                         !String.IsNullOrEmpty(t.FullName) && !t.FullName.Contains("DisplayClass") &&
                                                         !t.Attributes.HasFlag(TypeAttributes.Abstract)
            ).OrderBy(t2 => t2.Name).ToList();

            return retVal;
        }
    }
}
