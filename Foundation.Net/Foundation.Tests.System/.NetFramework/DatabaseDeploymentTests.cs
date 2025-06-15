//-----------------------------------------------------------------------
// <copyright file="DatabaseDeploymentTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Text;

//using Microsoft.SqlServer.Dac;

using NUnit.Framework;

using Foundation.Tests.Unit.Support;

using Foundation.Common;

namespace Foundation.Tests.System.NetFramework
{
    /// <summary>
    /// The DatabaseDeploymentTests class
    /// </summary>
    [TestFixture]
    //[DeploymentItem("Microsoft.Data.SqlClient.SNI.x86.dll")]
    //[DeploymentItem("Microsoft.Data.SqlClient.SNI.x64.dll")]
    //[DeploymentItem("System.Resources.Extensions.dll")]
    public class DatabaseDeploymentTests
    {
        [TearDown]
        public virtual void TestCleanup()
        {
            //DeleteDatabase(DatabaseName);
        }

        //[TestCase]
        [DeploymentItem(@".Support\Sql\0 - Drop Unit Test Database.sql", @".Support\Sql\")]
        public void Test_DatabaseProjectDeployment()
        {
            //String masterDbConnectionName = "UnitTestingMasterDB";
            //String dropDatabaseScript = @".Support\Sql\0 - Drop Unit Test Database.sql";
            //ExecuteSqlFile(masterDbConnectionName, dropDatabaseScript);

            //String dataConnectionName = ApplicationSettings.DataConnectionName;
            //String dataConnectionString = ApplicationSettings.GetConnectionString(dataConnectionName);

            //DacServices instance = new DacServices(dataConnectionString);
            //String path = Path.GetFullPath(@"..\..\..\Foundation.CustomerContact\bin\Debug\Foundation.CustomerContact.dacpac");

            //String databaseName = "UnitTesting";

            //using (DacPackage dacPackage = DacPackage.Load(path))
            //{
            //    instance.Deploy(dacPackage, databaseName);
            //}
        }

        /// <summary>
        /// Executes the SQL file.
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string</param>
        /// <param name="filePath">The file path.</param>
        protected void ExecuteSqlFile(String connectionStringName, String filePath)
        {
            // TODO: re-enable this using generic connection details
            //String sql = File.ReadAllText(filePath, Encoding.UTF8);

            //String connectionString = ApplicationSettings.GetConnectionString(connectionStringName);
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    using (SqlCommand command = new SqlCommand(sql, conn))
            //    {
            //        command.ExecuteNonQuery();
            //    }

            //    conn.Close();
            //}
        }
    }
}
