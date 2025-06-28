﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Configuration;

using Foundation.Common.Properties;
using Foundation.Interfaces;

namespace Foundation.Common
{
    /// <summary>
    /// Defines the behaviours for the Application Settings
    /// </summary>
    [Serializable]
    public class ApplicationSettings
    {
        /// <summary>
        /// Initialises the <see cref="ApplicationSettings"/> class.
        /// </summary>
        static ApplicationSettings()
        {
            ApplicationName = Settings.Default.ApplicationName;
            ApplicationId = new AppId(Settings.Default.ApplicationId);

            TraceLevel = new TraceSwitch("TraceLevelSwitch", "Default Trace Level").Level;

            Initialise();
        }

        /// <summary>
        /// The default Valid To date/time that is used throughout the application
        /// <para>
        /// This is normally the '2199-Dec-31 23:59:59'
        /// </para>
        /// </summary>
        public static DateTime DefaultValidToDateTime => new DateTime(2199, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        /// <summary>
        /// Gets the Application Name.
        /// </summary>
        /// <value>
        /// The Application Name.
        /// </value>
        public static String ApplicationName { get; internal set; }

        /// <summary>
        /// Gets the Application Id Number.
        /// </summary>
        /// <value>
        /// The Application Id Number.
        /// </value>
        public static AppId ApplicationId { get; internal set; }

        /// <summary>
        /// Gets the Trace Level.
        /// </summary>
        /// <value>
        /// The Trace Level.
        /// </value>
        public static TraceLevel TraceLevel { get; }

        /// <summary>
        /// Gets the Logging Configuration.
        /// </summary>
        /// <value>
        /// The Logging Configuration.
        /// </value>
        public static LoggingConfiguration LoggingConfiguration { get; set; }

        /// <summary>
        /// Initialises:
        /// <see cref="LoggingConfiguration"/>
        /// </summary>
        public static void Initialise()
        {
            LoggingConfiguration = ConfigurationManager.GetSection(LoggingConstants.EventLoggingSection) as LoggingConfiguration;

            if (LoggingConfiguration.IsNull())
            {
                String errorMessage = $"Application configuration not found. Missing section '{LoggingConstants.EventLoggingSection}'";

                ConfigurationErrorsException configurationErrorsException = new ConfigurationErrorsException(errorMessage);

                throw configurationErrorsException;
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="dataConnectionName">Name of the data connection.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">dataConnectionName</exception>
        public static String GetConnectionString(String dataConnectionName)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[dataConnectionName];

            if (connectionStringSettings.IsNull())
            {
                String errorMessage = $"Cannot load Connection named '{dataConnectionName}'. Check to make sure the connection is defined in the Configuration File.";

                throw new ArgumentNullException(nameof(dataConnectionName), errorMessage);
            }

            String retVal = connectionStringSettings.ConnectionString;
            return retVal;
        }

        /// <summary>
        /// Gets the name of the data provider.
        /// </summary>
        /// <param name="dataConnectionName">Name of the data connection.</param>
        /// <returns></returns>
        public static String GetDataProviderName(String dataConnectionName)
        {
            String retVal = ConfigurationManager.ConnectionStrings[dataConnectionName].ProviderName;
            return retVal;
        }
    }
}
