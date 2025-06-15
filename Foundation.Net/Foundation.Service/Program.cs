//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Service
{
    internal class Program
    {
        static void Main(String[] args)
        {
            // For catching Global uncaught exception
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionOccurred;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Core.Core.Initialise(ApplicationSettings.ApplicationId);
            ICore core = Core.Core.Instance;
            core.Container.Initialise("Foundation", "Foundation.*.exe");

            ConfigureService.Configure(core);
        }

        /// <summary>
        /// Handles the UnobservedTaskException event of the TaskScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="UnobservedTaskExceptionEventArgs" /> instance containing the event data.</param>
        private static void TaskScheduler_UnobservedTaskException(Object sender, UnobservedTaskExceptionEventArgs args)
        {
            Exception exception = args.Exception;

            ApplicationControl.LogUnhandledExceptionMessage(exception);
        }

        /// <summary>
        /// Catches any unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="UnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private static void UnhandledExceptionOccurred(Object sender, UnhandledExceptionEventArgs args)
        {
            Exception exception = (Exception)args.ExceptionObject;

            ApplicationControl.LogUnhandledExceptionMessage(exception);
        }
    }
}
