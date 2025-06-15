//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Views;

using FEnums = Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Main application
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IApprovalStatus}" />
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private Boolean _enabled;

        /// <summary>
        /// Initialises a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="targetWindow">The target window.</param>
        /// <param name="applicationDefinition">The application definition.</param>
        /// <param name="loggedOnUserProcess">The logged on user process.</param>
        public MainViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IWindow targetWindow,
            ApplicationDefinition applicationDefinition,
            ILoggedOnUserProcess loggedOnUserProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                applicationDefinition.Name
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, targetWindow, applicationDefinition, loggedOnUserProcess);

            FileApi = fileApi;
            ApplicationDefinition = applicationDefinition;

            LoggedOnUsername = Core.CurrentLoggedOnUser.Username;
            LoggedOnUserDisplayName = Core.CurrentLoggedOnUser.DisplayName;
            Version = "<None>";
            UserRole = "<None>";
            Environment = "Dev";

            foreach (IRole role in Core.CurrentLoggedOnUser.UserProfile.Roles)
            {
                if (UserRole.Length > 0) UserRole += "|";

                UserRole += role.Name;
            }

            LoggedOnUsersViewModel = new LoggedOnUserViewModel(Core, RunTimeEnvironmentSettings, DateTimeService, DialogService, ClipBoardWrapper, FileApi, loggedOnUserProcess);
            LoggedOnUsersViewModel.Initialise(targetWindow, this, "Logged on Users");

            InitialiseMenuItems();

            TabItems = new ObservableCollection<TabItem>();

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="Initialise()"/>
        public override void Initialise()
        {
            LoggingHelpers.TraceCallEnter();

            // Nothing to do

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Gets or sets the child window counter.
        /// </summary>
        /// <value>
        /// The child window counter.
        /// </value>
        private Int32 ChildWindowCounter { get; set; }

        /// <summary>
        /// Gets the logged on users view model.
        /// </summary>
        /// <value>
        /// The logged on users view model.
        /// </value>
        private LoggedOnUserViewModel LoggedOnUsersViewModel { get; }

        /// <summary>
        /// Gets the file service.
        /// </summary>
        /// <value>
        /// The file service.
        /// </value>
        protected IFileApi FileApi { get; }

        /// <summary>
        /// Gets the application definition.
        /// </summary>
        /// <value>
        /// The application definition.
        /// </value>
        protected ApplicationDefinition ApplicationDefinition { get; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        public String ApplicationName => ApplicationDefinition.Name;

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public String Version { get; }

        /// <summary>
        /// Gets the logged on username.
        /// </summary>
        /// <value>
        /// The logged on username.
        /// </value>
        public String LoggedOnUsername { get; }

        /// <summary>
        /// Gets the logged on user display name.
        /// </summary>
        /// <value>
        /// The logged on user display name.
        /// </value>
        public String LoggedOnUserDisplayName { get; }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public String UserRole { get; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public String Database { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public String Environment { get; }

        /// <summary>
        /// Gets the re show last exception error dialog.
        /// </summary>
        /// <value>
        /// The re show last exception error dialog.
        /// </value>
        public ICommand ReShowLastExceptionErrorDialogCommand { get { return RelayCommandFactory.New(ReShowLastExceptionErrorDialog_Click, () => LastException.IsNotNull()); } }

        /// <summary>
        /// Gets the logged on user click command.
        /// </summary>
        /// <value>
        /// The logged on user click command.
        /// </value>
        public ICommand LoggedOnUserClickCommand => RelayCommandFactory.New(LoggedOnUserClickCommand_Click);

        /// <summary>
        /// Gets the sample command with argument.
        /// </summary>
        /// <value>
        /// The sample command with argument.
        /// </value>
        public ICommand SampleCmdWithArgument => RelayCommandFactory.New<Object>(OnSampleCmdWithArgument_Click);

        /// <summary>
        /// Gets the show about dialog command.
        /// </summary>
        /// <value>
        /// The show about dialog command.
        /// </value>
        public ICommand ShowAboutDialogCommand => RelayCommandFactory.New(OnShowAboutDialog_Click);

        /// <summary>
        /// Gets the open test form command.
        /// </summary>
        /// <value>
        /// The open test form command.
        /// </value>
        public ICommand OpenTestFormCommand => RelayCommandFactory.New(OnOpenTestForm_Click, RelayCommandFactory.AlwaysTrue);

        /// <summary>
        /// Gets the open new notification command.
        /// </summary>
        /// <value>
        /// The open new notification command.
        /// </value>
        public ICommand OpenNewNotificationCommand => RelayCommandFactory.New(OnOpenNewNotification_Click);

        /// <summary>
        /// Gets the open error dialog command.
        /// </summary>
        /// <value>
        /// The open error dialog command.
        /// </value>
        public ICommand OpenErrorDialogCommand => RelayCommandFactory.New(OnOpenErrorDialog_Click);

        /// <summary>
        /// Gets the TreeView selection left click command.
        /// </summary>
        /// <value>
        /// The TreeView selection left click command.
        /// </value>
        public ICommand TreeViewSelectionLeftClickCommand => RelayCommandFactory.New<ViewMenuItem>(OnMenuSelection_LeftClick);

        /// <summary>
        /// Gets the TreeView selection right click command.
        /// </summary>
        /// <value>
        /// The TreeView selection right click command.
        /// </value>
        public ICommand TreeViewSelectionRightClickCommand => RelayCommandFactory.New<ViewMenuItem>(OnMenuSelection_RightClick);

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand { get { return RelayCommandFactory.New<ViewMenuItem>(OnSave_Click, () => _enabled); } }

        /// <summary>
        /// Gets the TreeView selected item changed.
        /// </summary>
        /// <value>
        /// The TreeView selected item changed.
        /// </value>
        public ICommand TreeViewSelectedItemChanged => RelayCommandFactory.New<ViewMenuItem>(OnTreeViewSelectedItemChanged_Click, RelayCommandFactory.AlwaysTrue);

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public Object SelectedItem { get; set; }

        private ObservableCollection<TabItem> _tabItems;
        /// <summary>
        /// Gets or sets the tab items.
        /// </summary>
        /// <value>
        /// The tab items.
        /// </value>
        public ObservableCollection<TabItem> TabItems
        {
            get => _tabItems;
            set => SetPropertyValue(ref _tabItems, value);
        }

        private ObservableCollection<ViewMenuItem> _applicationDefinitions;
        /// <summary>
        /// Gets or sets the application definitions.
        /// </summary>
        /// <value>
        /// The application definitions.
        /// </value>
        public ObservableCollection<ViewMenuItem> ApplicationDefinitions
        {
            get => _applicationDefinitions;
            set => SetPropertyValue(ref _applicationDefinitions, value);
        }

        private ViewMenuItem _selectedApplicationDefinition;
        /// <summary>
        /// Gets or sets the selected application definition.
        /// </summary>
        /// <value>
        /// The selected application definition.
        /// </value>
        public ViewMenuItem SelectedApplicationDefinition
        {
            get => _selectedApplicationDefinition;
            set
            {
                SetPropertyValue(ref _selectedApplicationDefinition, value);
                TreeViewItems = new ObservableCollection<ViewMenuItem>(_selectedApplicationDefinition.MenuItems);
            }
        }

        private ObservableCollection<ViewMenuItem> _treeViewItems;
        /// <summary>
        /// Gets or sets the TreeView items.
        /// </summary>
        /// <value>
        /// The TreeView items.
        /// </value>
        public ObservableCollection<ViewMenuItem> TreeViewItems
        {
            get => _treeViewItems;
            set => SetPropertyValue(ref _treeViewItems, value);
        }

        private BindingList<ViewMenuItem> _menuBarItems;
        /// <summary>
        /// Gets or sets the menu bar items.
        /// </summary>
        /// <value>
        /// The menu bar items.
        /// </value>
        public BindingList<ViewMenuItem> MenuBarItems
        {
            get => _menuBarItems;
            set => SetPropertyValue(ref _menuBarItems, value);
        }

        /// <summary>
        /// Displays a message box to the user containing details of the <paramref name="exception" />
        /// </summary>
        /// <param name="exception"></param>
        public void DisplayUnhandledExceptionMessage(Exception exception)
        {
            Visibility continueVisibility = Visibility.Visible;
            MessageDialogForm theForm = new MessageDialogForm();
            ErrorDialogViewModel errorDialogViewModel = new ErrorDialogViewModel(Core, RunTimeEnvironmentSettings, DateTimeService, DialogService, ClipBoardWrapper, theForm, this, exception, continueVisibility);

            // Show a message before closing application
            theForm.DataContext = errorDialogViewModel;
            theForm.Owner = ThisWindow as Window;
            theForm.Show();
        }

        /// <summary>
        /// Initialises the menu items.
        /// </summary>
        private void InitialiseMenuItems()
        {
            LoggingHelpers.TraceCallEnter();

            List<ViewMenuItem> menuBarItems = ApplicationDefinition.ViewMenuItems.Where(mi => !String.IsNullOrEmpty(mi.Menu)).ToList();

            ApplicationDefinitions = new ObservableCollection<ViewMenuItem>(ApplicationDefinition.ViewMenuItems.Where(mi => mi.MenuItems.Any()));
            SelectedApplicationDefinition = ApplicationDefinitions[0];

            MenuBarItems = new BindingList<ViewMenuItem>(menuBarItems);

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Logged on user click command click.
        /// </summary>
        private void LoggedOnUserClickCommand_Click()
        {
            LoggingHelpers.TraceCallEnter();

            StdContainerWindow theForm = new StdContainerWindow(this, typeof(LoggedOnUsersControl), LoggedOnUsersViewModel)
            {
                Owner = ThisWindow as Window
            };

            theForm.ShowDialog();

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [save click].
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        private void OnSave_Click(ViewMenuItem menuItem)
        {
            LoggingHelpers.TraceCallEnter(menuItem);

            Debug.Print($"{LocationUtils.GetFunctionName()} Called");

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [TreeView selected item changed click].
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        private void OnTreeViewSelectedItemChanged_Click(ViewMenuItem menuItem)
        {
            LoggingHelpers.TraceCallEnter(menuItem);

            using (new MouseBusyCursor())
            {
                Debug.Print($"{LocationUtils.GetFunctionName()} - {menuItem.Caption}");
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [menu selection left click].
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        private void OnMenuSelection_LeftClick(ViewMenuItem menuItem)
        {
            LoggingHelpers.TraceCallEnter(menuItem);

            using (new MouseBusyCursor())
            {
                if (menuItem.IsNotNull() &&
                    menuItem.Controller.IsNotNull() &&
                    menuItem.ViewScreen.IsNotNull())
                {
                    ChildWindowCounter++;

                    ViewController controller = menuItem.Controller;
                    ViewScreen view = menuItem.ViewScreen;
                    Object[] parameters = { DateTimeService, DialogService, ThisWindow, this };
                    IViewModel viewModel;
                    try
                    {
                        viewModel = Core.Container.Get<IViewModel>(controller.AssemblyName, controller.AssemblyType, parameters);

                        if (menuItem.Parameters.Any())
                        {
                            menuItem.Parameters.ForEach(p => viewModel.Parameters.Add(p.Name, p.Value));
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"Unable to create View Model: '{controller.AssemblyType}' from '{controller.AssemblyName}'", exception);
                    }

                    ContentControl contentControl;
                    try
                    {
                        contentControl = Core.Container.Get<ContentControl>(view.AssemblyName, view.AssemblyType);
                        IWindow targetWindow = contentControl as IWindow;
                        viewModel.Initialise(targetWindow, this, menuItem.Caption);
                        contentControl.DataContext = viewModel;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"Unable to View - {menuItem.Caption}: '{view.AssemblyType}' from '{view.AssemblyName}'", exception);
                    }

                    TabItem tabItem = null;
                    Window window = null;

                    if (menuItem.ShowInTab)
                    {
                        // See if the view has been created already
                        tabItem = TabItems.FirstOrDefault(ti => menuItem.Name.Equals(ti.Tag));

                        if (tabItem.IsNull() ||
                            menuItem.MultiInstance)
                        {
                            tabItem = new TabItem
                            {
                                Tag = menuItem.Name,
                                Header = menuItem.Caption,
                                Content = contentControl,
                            };
                        }
                    }
                    else
                    {
                        foreach (Window openWindow in Application.Current.Windows)
                        {
                            if (menuItem.Name.Equals(openWindow.Tag))
                            {
                                window = openWindow;
                            }
                        }

                        if (window.IsNull() ||
                            menuItem.MultiInstance)
                        {
                            if (contentControl is Window control)
                            {
                                window = control;
                            }
                            else
                            {
                                window = new StdContainerWindow
                                {
                                    Tag = menuItem.Name,
                                    Title = menuItem.Caption,
                                    Content = contentControl,
                                };
                            }

                            window.Owner = Application.Current.MainWindow;
                        }
                    }

                    if (tabItem.IsNotNull())
                    {
                        tabItem.IsSelected = true;
                        if (!TabItems.Any(ti => menuItem.Name.Equals(ti.Tag)))
                        {
                            TabItems.Add(tabItem);
                        }

                        if (menuItem.MultiInstance)
                        {
                            TabItems.Add(tabItem);
                        }
                    }
                    else
                    {
                        window.Show();
                        window.Activate();
                        window.Topmost = true;
                        window.Topmost = false;
                        window.Focus();
                    }
                }
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [menu selection right click].
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        private void OnMenuSelection_RightClick(ViewMenuItem menuItem)
        {
            LoggingHelpers.TraceCallEnter(menuItem);

            using (new MouseBusyCursor())
            {
                Debug.Print($"{LocationUtils.GetFunctionName()} - {menuItem.Caption}");
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Res the show last exception error dialog click.
        /// </summary>
        private void ReShowLastExceptionErrorDialog_Click()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                if (LastException.IsNotNull())
                {
                    DisplayUnhandledExceptionMessage(LastException);
                }
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [sample command with argument click].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnSampleCmdWithArgument_Click(Object obj)
        {
            LoggingHelpers.TraceCallEnter(obj);

            using (new MouseBusyCursor())
            {
                // TODO
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [show about dialog click].
        /// </summary>
        private void OnShowAboutDialog_Click()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                LoggingHelpers.TraceMessage("Opening About dialog");
                IViewModel parentViewModel = this;
                AboutSplashScreenForm theForm = new AboutSplashScreenForm();
                AboutSplashScreenFormViewModel viewModel = new AboutSplashScreenFormViewModel(Core, RunTimeEnvironmentSettings, DateTimeService, DialogService, ClipBoardWrapper, false);
                theForm.DataContext = viewModel;
                theForm.Show();
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [open new notification click].
        /// </summary>
        private void OnOpenNewNotification_Click()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                IRandomService randomService = Core.Container.Get<IRandomService>();
                FEnums.MessageType messageType = (FEnums.MessageType)randomService.NextInt32(1, 6);
                String messageHeader = messageType.ToString();
                String message = $"{messageType} Content";
                ShowNotificationMessage(messageType, messageHeader, message);
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [open test form click].
        /// </summary>
        private void OnOpenTestForm_Click()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                //TestForm theForm = new TestForm();
                //theForm.DataContext = new TestFormViewModel();

                //IOpenFolderDialogSettings openFolderDialogSettings = new OpenFolderDialogSettings();
                //IDialogService ds = Core.Core.Instance.Container.Get<IDialogService>();
                //DialogResult dialogResult = ds.ShowOpenFolderDialog(this, openFolderDialogSettings);

                //if (dialogResult == DialogResult.Ok)
                //{
                //    Debug.WriteLine(openFolderDialogSettings.FolderName);
                //}

                IRandomService randomService = Core.Container.Get<IRandomService>();
                Int32[] iconIds = { 0, 16, 32, 48, 64, 16, 16, 48, 64 };
                FEnums.MessageBoxImage messageBoxImage = (FEnums.MessageBoxImage)iconIds[randomService.NextInt32(1, 9)];

                MessageDialogForm theForm = new MessageDialogForm();
                MessageDialogViewModel messageDialogViewModel = new MessageDialogViewModel(Core, RunTimeEnvironmentSettings, DateTimeService, DialogService, ClipBoardWrapper, theForm, this);

                messageDialogViewModel.MessageBoxImage = messageBoxImage;
                messageDialogViewModel.FormTitle = $"Form Title - {messageDialogViewModel.MessageBoxImage}";
                messageDialogViewModel.ScreenInstructions = $"Screen instructions - {messageDialogViewModel.MessageBoxImage}";

                theForm.DataContext = messageDialogViewModel;
                theForm.Owner = ThisWindow as Window;
                theForm.ShowDialog();
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [open error dialog click].
        /// </summary>
        private void OnOpenErrorDialog_Click()
        {
            LoggingHelpers.TraceCallEnter();

            using (new MouseBusyCursor())
            {
                _enabled = !_enabled;

                const Boolean throwNew = false;
                const Boolean throwException = true;
                ExceptionManagementDemo d = new ExceptionManagementDemo();
                d.Method1(throwException, throwNew);
            }

            LoggingHelpers.TraceCallReturn();
        }
    }
}
