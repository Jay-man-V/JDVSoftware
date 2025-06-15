﻿//-----------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Foundation.Common;
using Foundation.Interfaces;

using FEnums = Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// Implements generic routines for all view models
    /// Implements INotifyPropertyChanged for all ViewModels
    /// </summary>
    public abstract class ViewModelBase : IViewModel
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialises the <see cref="ViewModelBase" /> class.
        /// </summary>
        static ViewModelBase()
        {
            LoggingHelpers.TraceCallEnter();

            /* These steps must be performed as soon as possible before other actions run */

            StatusProcess = Foundation.Core.Core.Instance.Container.Get<IStatusProcess>();
            UserProfileProcess = Foundation.Core.Core.Instance.Container.Get<IUserProfileProcess>();
            LoggedOnUserProcess = Foundation.Core.Core.Instance.Container.Get<ILoggedOnUserProcess>();

            /******************************************************************************/

            LoggingHelpers.TraceCallReturn();
        }

        private static List<IStatus> _statusesList;
        private static List<IUserProfile> _userProfilesList;
        private static List<ILoggedOnUser> _loggedOnUsersList;

        /// <summary>
        /// Gets the Status process
        /// </summary>
        protected internal static IStatusProcess StatusProcess { get; internal set; }

        /// <summary>
        /// Gets the User profile process
        /// </summary>
        protected internal static IUserProfileProcess UserProfileProcess { get; internal set; }

        /// <summary>
        /// Gets the Logged on user process
        /// </summary>
        protected internal static ILoggedOnUserProcess LoggedOnUserProcess { get; internal set; }

        /// <summary>
        /// List of <see cref="IStatus"/>. This is a list ALL the Status entities within the system.
        /// <para>
        /// This is not a list of all the current active Status entities
        /// </para>
        /// </summary>
        public static List<IStatus> StatusesList
        {
            get
            {
                if (_statusesList.IsNull())
                {
                    _statusesList = StatusProcess.GetAll(false);
                }

                return _statusesList;
            }
            internal set => _statusesList = value;
        }

        /// <summary>
        /// List of <see cref="IUserProfile"/>. This is a list ALL the User Profile entities within the system. 
        /// <para>
        /// This is not a list of all the current active User Profile entities
        /// </para>
        /// </summary>
        public static List<IUserProfile> UserProfilesList
        {
            get
            {
                if (_userProfilesList.IsNull())
                {
                    _userProfilesList = UserProfileProcess.GetAll(false);
                }

                return _userProfilesList;
            }
            internal set => _userProfilesList = value;
        }

        /// <summary>
        /// Gets the logged on users list.
        /// </summary>
        /// <value>
        /// The logged on users list.
        /// </value>
        public static List<ILoggedOnUser> LoggedOnUsersList
        {
            get
            {
                if (_loggedOnUsersList.IsNull())
                {
                    _loggedOnUsersList = LoggedOnUserProcess.GetLoggedOnUsers(ApplicationSettings.ApplicationId);
                }

                return _loggedOnUsersList;
            }
            internal set => _loggedOnUsersList = value;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewModelBase" /> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="formTitle">The form title</param>
        protected ViewModelBase
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            String formTitle
        )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, formTitle);

            MessageBoxImage = FEnums.MessageBoxImage.None;

            _formTitle = formTitle;

            Core = core;
            RunTimeEnvironmentSettings = runTimeEnvironmentSettings;
            DateTimeService = dateTimeService;
            DialogService = dialogService;
            ClipBoardWrapper = clipBoardWrapper;

            Parameters = new Dictionary<String, Object>();

            LoggingHelpers.TraceCallReturn();
        }

        private IMouseBusyCursor _mouseBusyCursor;

        /// <inheritdoc cref="MouseBusyCursor"/>
        public IMouseBusyCursor MouseBusyCursor
        {
            get
            {
                IMouseBusyCursor retVal = _mouseBusyCursor ?? Core.Container.Get<IMouseBusyCursor>();

                return retVal;
            }
            internal set => _mouseBusyCursor = value;
        }

        private Application _currentApplication;
        /// <inheritdoc cref="CurrentApplication"/>
        public Application CurrentApplication
        {
            get
            {
                Application retVal = _currentApplication ?? Application.Current;

                return retVal;
            }
            internal set => _currentApplication = value;
        }

        private Dispatcher _currentDispatcher;
        /// <inheritdoc cref="CurrentDispatcher"/>
        public Dispatcher CurrentDispatcher
        {
            get
            {
                Dispatcher retVal = _currentDispatcher ?? CurrentApplication.Dispatcher;

                return retVal;
            }
            internal set => _currentDispatcher = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetWindow">The target window.</param>
        /// <param name="parentViewModel">The parent view model.</param>
        /// <param name="formTitle">The form title.</param>
        public virtual void Initialise(IWindow targetWindow, IViewModel parentViewModel, String formTitle)
        {
            LoggingHelpers.TraceCallEnter(targetWindow, parentViewModel, formTitle);

            if (StatusesList.Any()) { /* Do nothing - Forces the static constructor to run */ }
            if (UserProfilesList.Any()) { /* Do nothing - Forces the static constructor to run */ }
            if (LoggedOnUsersList.Any()) { /* Do nothing - Forces the static constructor to run */ }
            
            ThisWindow = targetWindow;
            ParentViewModel = parentViewModel;

            FormTitle = formTitle;

            Initialise();

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>Gets the message box image.</summary>
        /// <value>The message box image.</value>
        public FEnums.MessageBoxImage MessageBoxImage { get; set; }

        /// <summary>
        /// Gets This window.
        /// </summary>
        /// <value>This window.</value>
        public IWindow ThisWindow { get; private set; }

        /// <summary>
        /// Gets or sets the last exception.
        /// </summary>
        /// <value>The last exception.</value>
        public Exception LastException { get; set; }

        /// <summary>
        /// The Parent View Model
        /// </summary>
        public IViewModel ParentViewModel { get; private set; }

        /// <summary>
        /// Generic property to hold parameters for the View Model
        /// </summary>
        public Dictionary<String, Object> Parameters { get; }

        /// <summary>
        /// The Foundation Core service
        /// </summary>
        protected ICore Core { get; }

        /// <summary>
        /// Gets the run time environment settings
        /// </summary>
        /// <value>
        /// The run time environment settings.
        /// </value>
        protected internal IRunTimeEnvironmentSettings RunTimeEnvironmentSettings { get; }

        /// <summary>
        /// Gets the date time service.
        /// </summary>
        /// <value>The date time service.</value>
        protected internal IDateTimeService DateTimeService { get; }

        /// <summary>
        /// Gets the dialog service.
        /// </summary>
        /// <value>The dialog service.</value>
        protected IDialogService DialogService { get; }

        /// <summary>
        /// Gets the clip board wrapper.
        /// </summary>
        /// <value>The clip board wrapper.</value>
        protected IClipBoardWrapper ClipBoardWrapper { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has previous notification message.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous notification message; otherwise, <c>false</c>.</value>
        protected internal Boolean HasPreviousNotificationMessage { get; set; }
        
        /// <summary>
        /// Gets or sets the last type of the message.
        /// </summary>
        /// <value>The last type of the message.</value>
        protected internal MessageType LastMessageType { get; set; }
        
        /// <summary>
        /// Gets or sets the last message header.
        /// </summary>
        /// <value>The last message header.</value>
        protected internal String LastMessageHeader { get; set; }

        /// <summary>
        /// Gets or sets the last message.
        /// </summary>
        /// <value>The last message.</value>
        protected internal String LastMessage { get; set; }

        /// <summary>
        /// Gets the open last notification command.
        /// </summary>
        /// <value>
        /// The open last notification command.
        /// </value>
        public ICommand OpenLastNotificationCommand { get { return RelayCommandFactory.New(OnOpenLastNotification_Execute, () => HasPreviousNotificationMessage); } }

        /// <summary
        /// >Gets the close window command.
        /// </summary>
        /// <value>The close window command.</value>
        public ICommand CloseWindowCommand => RelayCommandFactory.New<IWindow>(OnCloseWindowCommand_Execute);

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        /// <value>The exit command.</value>
        public ICommand ExitApplicationCommand => RelayCommandFactory.New(OnExitApplicationCommand_Execute);

        private String _formTitle;
        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <value>The form title.</value>
        public String FormTitle
        {
            get => _formTitle;
            protected internal set => SetPropertyValue(ref _formTitle, value);
        }

        private String _screenInstructions;
        /// <summary>
        /// Gets the screen instructions.
        /// </summary>
        /// <value>The screen instructions.</value>
        public String ScreenInstructions
        {
            get => _screenInstructions;
            protected internal set => SetPropertyValue(ref _screenInstructions, value);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is system support.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is system support; otherwise, <c>false</c>.</value>
        public Boolean IsSystemSupport => Core.CurrentLoggedOnUser.IsSystemSupport;

        /// <summary>
        /// Called when [open last notification click].
        /// </summary>
        private void OnOpenLastNotification_Execute()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                ReShowNotificationMessage();
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [close window command].
        /// </summary>
        /// <param name="window">The window.</param>
        protected virtual void OnCloseWindowCommand_Execute(IWindow window)
        {
            LoggingHelpers.TraceCallEnter(window);

            using (MouseBusyCursor)
            {
                if (window.IsNotNull())
                {
                    window.Close();
                }
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [exit application command].
        /// </summary>
        protected virtual void OnExitApplicationCommand_Execute()
        {
            LoggingHelpers.TraceCallEnter();

            using (MouseBusyCursor)
            {
                CurrentApplication?.MainWindow?.Close();
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Shows the notification message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="messageHeader">The message header.</param>
        /// <param name="message">The message.</param>
        protected internal virtual void ShowNotificationMessage(MessageType messageType, String messageHeader, String message)
        {
            LastMessageType = messageType;
            LastMessageHeader = messageHeader;
            LastMessage = message;
            HasPreviousNotificationMessage = true;

            DialogService.ShowNotificationMessage(messageType, messageHeader, message);
        }

        /// <summary>
        /// Re-shows notification message.
        /// </summary>
        protected virtual void ReShowNotificationMessage()
        {
            if (HasPreviousNotificationMessage)
            {
                DialogService.ShowNotificationMessage(LastMessageType, LastMessageHeader, LastMessage);
            }
        }

        /// <summary>
        /// Raises the PropertyChanged Event, derives the Property name from the Property calling this function
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            LoggingHelpers.TraceCallEnter(propertyName);

            CommandManager.InvalidateRequerySuggested();

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler.IsNotNull())
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        protected Boolean SetPropertyValue<TValue>(ref TValue storage, TValue newValue, [CallerMemberName] String propertyName = "")
        {
            LoggingHelpers.TraceCallEnter(storage, newValue, propertyName);

            Boolean retVal = false;

            if (!EqualityComparer<TValue>.Default.Equals(storage, newValue))
            {
                storage = newValue;
                NotifyPropertyChanged(propertyName);
                retVal = true;
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <summary>
        /// Checks to see if the parameter is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected internal virtual Boolean CanExecuteParamIsNotNull(Object obj)
        {
            LoggingHelpers.TraceCallEnter(obj);

            Boolean retVal = obj.IsNotNull();

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <summary>
        /// Initialise function will be called once the Parameters have been set by the framework
        /// </summary>
        public abstract void Initialise();
    }
}
