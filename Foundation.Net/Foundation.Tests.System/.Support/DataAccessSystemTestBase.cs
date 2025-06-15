//-----------------------------------------------------------------------
// <copyright file="DataAccessSystemTestBase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

//using Microsoft.SqlServer.Dac;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.System.Support
{
    /// <summary>
    /// The Automated Unit Test Base class
    /// </summary>
    [TestFixture]
    [DeploymentItem(@".Support\Sql\0 - Drop Unit Test Database.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\0 - Create Unit Test Database.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1 - Create Generic Test Tables.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1 - Reset all tables.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.1.1 - Create - ufn_GetListOfCalendarDates.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.1.2 - Create - ufn_CheckIsWorkingDayOrGetNextWorkingDay.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.1.3 - Create - ufn_GetNextWorkingDay.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.1.4 - Create - ufn_CheckIsWorkingDayOrGetNextWorkingDay.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.2.1 - Create - usp_NonWorkingDays_GetWorkingDays.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.2.2 - Create - usp_NonWorkingDays_GetWorkingDaysByMonth.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\1.2.3 - Create - usp_UserProfile_LoadFromActiveDirectoryUsersFromStaging.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\2 - Business Tables - Initial Data.sql", @".Support\Sql\")]
    [DeploymentItem(@".Support\Sql\3 - Business Tables - Additional Data.sql", @".Support\Sql\")]
    public abstract class DataAccessSystemTestBase : SystemTestBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DataAccessSystemTestBase"/> class.
        /// </summary>
        protected DataAccessSystemTestBase() : this("SystemTestBase") { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SystemTestBase"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        protected DataAccessSystemTestBase(String connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        /// <summary>
        /// Gets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        protected String ConnectionStringName { get; set; }

        /// <summary>
        /// Initialises the test.
        /// </summary>
        protected override void StartTest()
        {
            /*
             *
             * Part 1 - Remove existing Test Database and create a new empty Test Database
             *
             */

            //ConnectionStringName = "UnitTestingMasterDB";
            //ApplicationSettings.ApplicationName = ConnectionStringName;

            //String dropDatabaseScript = @".Support\Sql\0 - Drop Unit Test Database.sql";
            //ExecuteSqlFile(dropDatabaseScript);

            //String createDatabaseScript = @".Support\Sql\0 - Create Unit Test Database.sql";
            //ExecuteSqlFile(createDatabaseScript);

            //String dataConnectionName = ApplicationSettings.DataConnectionName;
            //String connectionString = ApplicationSettings.GetConnectionString(dataConnectionName);

            //DacServices instance = new DacServices(connectionString);
            //String path = Path.GetFullPath(@"..\..\..\Foundation.Net\Foundation.CustomerContact\bin\Debug\Foundation.CustomerContact.dacpac");

            //String databaseName = "UnitTesting";

            //using (DacPackage dacPackage = DacPackage.Load(path))
            //{
            //    instance.Deploy(dacPackage, databaseName);
            //}

            /*
             *
             * Part 2 - Create the Test Tables
             *
             */

            ConnectionStringName = "UnitTestingCore";
            //ApplicationSettings.ApplicationName = ConnectionStringName;

            String[] createTablesScript =
            {
                @".Support\Sql\1 - Create Generic Test Tables.sql",
                @".Support\Sql\1 - Reset all tables.sql",

                @".Support\Sql\1.1.1 - Create - ufn_GetListOfCalendarDates.sql",
                @".Support\Sql\1.1.2 - Create - ufn_CheckIsWorkingDayOrGetNextWorkingDay.sql",
                @".Support\Sql\1.1.3 - Create - ufn_GetNextWorkingDay.sql",
                @".Support\Sql\1.2.1 - Create - usp_NonWorkingDays_GetWorkingDays.sql",
                @".Support\Sql\1.2.2 - Create - usp_NonWorkingDays_GetWorkingDaysByMonth.sql",
                @".Support\Sql\1.2.3 - Create - usp_UserProfile_LoadFromActiveDirectoryUsersFromStaging.sql",

                //@".Support\Sql\2 - Business Tables - Initial Data.sql",
                @".Support\Sql\3 - Business Tables - Additional Data.sql",
            };

            foreach (String createTableScript in createTablesScript)
            {
                ExecuteSqlFile(createTableScript);
            }

            /*
             *
             * Part 3 - Unit Testing setup
             *
             */

            //ApplicationSettings.ApplicationName = ConnectionStringName;
            //ApplicationSettings.DataConnectionName = ConnectionStringName;

            base.StartTest();
        }

        /// <summary>
        /// Executes the SQL file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        protected void ExecuteSqlFile(String filePath)
        {
            String sql = File.ReadAllText(filePath, Encoding.UTF8);

            ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// Executes the SQL file.
        /// </summary>
        /// <param name="sql">The sql.</param>
        protected void ExecuteSqlCommand(String sql)
        {
            String connectionString = ApplicationSettings.GetConnectionString(ConnectionStringName);
            String databaseProviderName = ApplicationSettings.GetDataProviderName(ConnectionStringName);

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(databaseProviderName);

            using (IDbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                using (IDbCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
