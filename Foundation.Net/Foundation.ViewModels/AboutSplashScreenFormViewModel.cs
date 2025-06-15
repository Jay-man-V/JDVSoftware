﻿//-----------------------------------------------------------------------
// <copyright file="AboutSplashScreenFormViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for About/Splash Screen
    /// </summary>
    public class AboutSplashScreenFormViewModel : ViewModelBase //, IAboutSplashScreenFormViewModel
    {
        /// <summary>Initialises a new instance of the <see cref="AboutSplashScreenFormViewModel" /> class.</summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="isSplashScreen">if set to <c>true</c> [is splash screen].</param>
        public AboutSplashScreenFormViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            Boolean isSplashScreen
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                ApplicationSettings.ApplicationName
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, isSplashScreen);

            Product = "<not set>";
            CompanyName = "<not set>";
            Copyright = "<not set>";
            Trademark = "<not set>";
            Configuration = "<not set>";
            Version = "<not set>";
            ApplicationServer = "<not set>";
            DatabaseServer = "<not set>";
            DatabaseDetails = "<not set>";
            Username = "<not set>";
            Name = "<not set>";
            Email = "<not set>";
            Roles = "<not set>";

            Assembly entryAssembly = Assembly.GetEntryAssembly();

            if (entryAssembly.IsNotNull())
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(entryAssembly.Location);

                CompanyName = fileVersionInfo.CompanyName ?? String.Empty;
                Product = fileVersionInfo.ProductName ?? String.Empty;
                Copyright = fileVersionInfo.LegalCopyright ?? String.Empty;
                Trademark = fileVersionInfo.LegalTrademarks ?? String.Empty;
                Version = $"{fileVersionInfo.ProductVersion} ({fileVersionInfo.FileVersion})";

                Configuration = "<Not set>";
                Object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)customAttributes[0];
                    Configuration = aca.Configuration;
                }
            }

            ApplicationServer = String.Empty;
            DatabaseServer = String.Empty;
            DatabaseDetails = String.Empty;

            Username = Core.CurrentLoggedOnUser.Username;
            Name = Core.CurrentLoggedOnUser.DisplayName;
            Email = "<Todo>";

            Roles = String.Empty;
            foreach (IRole role in Core.CurrentLoggedOnUser.UserProfile.Roles)
            {
                if (Roles.Length > 0) Roles += "|";

                Roles += role.Name;
            }

            if (isSplashScreen)
            {
                CloseButtonVisibility = Visibility.Hidden;
                UserInformationExpanded = false;
                ProgressBarVisibility = Visibility.Visible;
            }
            else
            {
                CloseButtonVisibility = Visibility.Visible;
                UserInformationExpanded = true;
                ProgressBarVisibility = Visibility.Hidden;
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="Initialise()"/>
        public override void Initialise()
        {
            LoggingHelpers.TraceCallEnter();

            // Nothing to do

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>Gets the close button visibility.</summary>
        /// <value>The close button visibility.</value>
        public Visibility CloseButtonVisibility { get; }

        /// <summary>Gets the progress bar visibility.</summary>
        /// <value>The progress bar visibility.</value>
        public Visibility ProgressBarVisibility { get; }

        /// <summary>Gets a value indicating whether [user information expanded].</summary>
        /// <value>
        ///   <c>true</c> if [user information expanded]; otherwise, <c>false</c>.</value>
        public Boolean UserInformationExpanded { get; }

        /// <summary>Gets a value indicating whether [dialog result].</summary>
        /// <value>
        ///   <c>true</c> if [dialog result]; otherwise, <c>false</c>.</value>
        public Boolean DialogResult => false;

        /// <summary>Gets the product.</summary>
        /// <value>The product.</value>
        public String Product { get; }

        /// <summary>Gets the name of the company.</summary>
        /// <value>The name of the company.</value>
        public String CompanyName { get; }

        /// <summary>Gets the copyright.</summary>
        /// <value>The copyright.</value>
        public String Copyright { get; }

        /// <summary>Gets the trademark.</summary>
        /// <value>The trademark.</value>
        public String Trademark { get; }

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public String Configuration { get; }

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        public String Version { get; }

        /// <summary>Gets the application server.</summary>
        /// <value>The application server.</value>
        public String ApplicationServer { get; }

        /// <summary>Gets the database server.</summary>
        /// <value>The database server.</value>
        public String DatabaseServer { get; }

        /// <summary>Gets the database details.</summary>
        /// <value>The database details.</value>
        public String DatabaseDetails { get; }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        public String Username { get; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public String Name { get; }

        /// <summary>Gets the email.</summary>
        /// <value>The email.</value>
        public String Email { get; }

        /// <summary>Gets the roles.</summary>
        /// <value>The roles.</value>
        public String Roles { get; }
    }
}
