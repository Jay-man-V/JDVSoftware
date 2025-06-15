//-----------------------------------------------------------------------
// <copyright file="IMainApplicationProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Defines the behaviour of the Main Application process
    /// </summary>
    public interface IMainApplicationProcess
    {
        /// <summary>
        /// Loads the application definition.
        /// </summary>
        /// <returns>Deserialised Application Definition</returns>
        /// <exception cref="FileNotFoundException">If the ApplicationDefinition.xml file cannot be found</exception>
        ApplicationDefinition LoadApplicationDefinition(String applicationDefinitionFile = "ApplicationDefinition.xml");
    }
}
