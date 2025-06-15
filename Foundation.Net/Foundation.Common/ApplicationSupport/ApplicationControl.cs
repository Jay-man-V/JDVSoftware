//-----------------------------------------------------------------------
// <copyright file="ApplicationControl.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Common
{
    /// <summary>
    /// Application Control contains basic functionality that all applications need to call
    /// </summary>
    public abstract class ApplicationControl
    {
        /// <summary>
        /// 
        /// </summary>
        public void ApplicationStart()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void LogUnhandledExceptionMessage(Exception exception)
        {
            LoggingHelpers.LogErrorMessage(exception);

            //Core.Core.Instance.Container.Reset();
            //Core.Core.Instance.Container.Initialise();
        }
    }
}
