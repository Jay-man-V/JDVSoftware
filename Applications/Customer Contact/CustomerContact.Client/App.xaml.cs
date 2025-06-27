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
using System.Windows.Input;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.ViewModels;
using Foundation.Views;

namespace CustomerContact
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

            LoggingHelpers.TraceCallEnter(e);

            // For catching Global uncaught exception
            ApplicationControl.ApplicationStart(DisplayUnhandledExceptionMessage);
            Dispatcher.UnhandledException += ApplicationControl.Dispatcher_UnhandledException;

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
            //IAboutSplashScreenFormViewModel splashScreenViewModel = Core.Core.Instance.Container.Get<AboutSplashScreenFormViewModel>(isSplashScreen);

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

                    IApplicationProcess applicationProcess = Core.Container.Get<IApplicationProcess>();
                    IApplication application = applicationProcess.Get(Core.ApplicationId);
                    IMenuItemProcess menuItemProcess = Core.Container.Get<IMenuItemProcess>();

                    ThisApplication = new MainWindowForm();
                    MainWindow = ThisApplication;
                    ViewModel = new MainViewModel(Core, runTimeEnvironmentSettings, dateTimeService, wpfApplicationObjects, fileApi, ThisApplication, applicationProcess, menuItemProcess);
                    ViewModel.Initialise(ThisApplication, null, application.Name);
                    ThisApplication.DataContext = ViewModel;
                    ThisApplication.Show();

                    Thread.Sleep(750);
                    splashScreen.Close();

                    Mouse.OverrideCursor = null;
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
    }
}
