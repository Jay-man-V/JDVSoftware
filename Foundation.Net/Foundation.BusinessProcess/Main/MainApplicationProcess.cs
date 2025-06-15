//-----------------------------------------------------------------------
// <copyright file="MainApplicationProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Xml.Serialization;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.BusinessProcess
{
    /// <summary>
    /// The Main Application Process 
    /// </summary>
    [DependencyInjectionTransient]
    public class MainApplicationProcess : IMainApplicationProcess
    {
        /// <summary>
        /// The default application definition file
        /// </summary>
        public const String DefaultApplicationDefinitionFile = "ApplicationDefinition.xml";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="core"></param>
        public MainApplicationProcess
        (
            ICore core
        )
        {
            Core = core;
        }

        private ICore Core { get; }

        /// <inheritdoc cref="IMainApplicationProcess.LoadApplicationDefinition(String)"/>
        public ApplicationDefinition LoadApplicationDefinition(String applicationDefinitionFile = DefaultApplicationDefinitionFile)
        {
            LoggingHelpers.TraceCallEnter(applicationDefinitionFile);

            ApplicationDefinition retVal = null;

            IFileApi fileApi = Core.Container.Get<IFileApi>();

            fileApi.EnsureFileExists(applicationDefinitionFile);

            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationDefinition));

            using (StreamReader reader = new StreamReader(applicationDefinitionFile))
            {
                retVal = (ApplicationDefinition)serializer.Deserialize(reader);
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
