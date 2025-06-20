//-----------------------------------------------------------------------
// <copyright file="app.xaml.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.ViewModels;
using Foundation.Views;

//using NationalLottery.ViewModels;

namespace NationalLottery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        private ICore Core { get; set; }

        /// <summary>
        /// Gets or sets this application.
        /// </summary>
        /// <value>The application.</value>
        private static MainWindowForm ThisApplication { get; set; }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private static IMainViewModel ViewModel { get; set; }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup">Startup</see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs">StartupEventArgs</see> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Foundation.Core.Core.Initialise(ApplicationSettings.ApplicationId);
            Core = Foundation.Core.Core.Instance;

            IMainApplicationProcess mainApplicationProcess = Core.Container.Get<IMainApplicationProcess>();

            ApplicationDefinition applicationDefinition = mainApplicationProcess.LoadApplicationDefinition();
            Core.Container.Initialise("NationalLottery.*.dll");

            LoggingHelpers.TraceCallEnter(e);

            // For catching Global uncaught exception
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionOccurred;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(UserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(UserControl))
            });

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Control), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Control))
            });

            LoggingHelpers.TraceMessage("Initialising");

            // Initialize the splash screen and set it as the application main window
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings = Core.Container.Get<IRunTimeEnvironmentSettings>();
            IDateTimeService dateTimeService = Core.Container.Get<IDateTimeService>();
            IWpfApplicationObjects wpfApplicationObjects = Core.Container.Get<IWpfApplicationObjects>();
            IFileApi fileApi = Core.Container.Get<IFileApi>();

            AboutSplashScreenForm splashScreen = new AboutSplashScreenForm();
            this.MainWindow = splashScreen;
            const Boolean isSplashScreen = true;
            AboutSplashScreenFormViewModel splashScreenViewModel = new AboutSplashScreenFormViewModel(Core, runTimeEnvironmentSettings, dateTimeService, wpfApplicationObjects, isSplashScreen);
            splashScreen.DataContext = splashScreenViewModel;
            splashScreen.Show();

            // In order to ensure the UI stays responsive, we need to do the work on a different thread
            Task.Run(() =>
            {
                // Simulate some work being done
                Thread.Sleep(500);

                // Since we're not on the UI thread once we're done we need to use the Dispatcher
                // to create and show the main window
                this.Dispatcher.Invoke(() =>
                {
                    LoggingHelpers.TraceMessage("Starting App");

                    ILoggedOnUserProcess loggedOnUserProcess = Core.Container.Get<ILoggedOnUserProcess>();

                    ThisApplication = new MainWindowForm();
                    MainWindow = ThisApplication;
                    ViewModel = new MainViewModel(Core, runTimeEnvironmentSettings, dateTimeService, wpfApplicationObjects, fileApi, ThisApplication, applicationDefinition, loggedOnUserProcess);
                    ThisApplication.DataContext = ViewModel;
                    ThisApplication.Show();

                    Thread.Sleep(750);
                    splashScreen.Close();
                });
            });

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Displays the unhandled exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static void DisplayUnhandledExceptionMessage(Exception exception)
        {
            ViewModel.LastException = exception;

            ViewModel.DisplayUnhandledExceptionMessage(exception);

            ApplicationControl.LogUnhandledExceptionMessage(exception);
        }

        /// <summary>
        /// Handles the UnobservedTaskException event of the TaskScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="UnobservedTaskExceptionEventArgs" /> instance containing the event data.</param>
        private void TaskScheduler_UnobservedTaskException(Object sender, UnobservedTaskExceptionEventArgs args)
        {
            Exception exception = args.Exception;

            DisplayUnhandledExceptionMessage(exception);
        }

        /// <summary>
        /// Handles the UnhandledException event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="DispatcherUnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private void Dispatcher_UnhandledException(Object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Exception exception = args.Exception;

            DisplayUnhandledExceptionMessage(exception);
            args.Handled = true;
        }

        /// <summary>
        /// Handles any other unhandled exceptions
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="UnhandledExceptionEventArgs" /> instance containing the event data.</param>
        static void UnhandledExceptionOccurred(Object sender, UnhandledExceptionEventArgs args)
        {
            Exception exception = (Exception)args.ExceptionObject;

            DisplayUnhandledExceptionMessage(exception);
        }
    }
}
