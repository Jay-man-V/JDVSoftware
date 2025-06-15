﻿//-----------------------------------------------------------------------
// <copyright file="StdContainerWindow.xaml.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows.Controls;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Views
{
    /// <summary>
    /// Interaction logic for StdContainerWindow.xaml
    /// </summary>
    public partial class StdContainerWindow : IWindow
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StdContainerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentViewModel"></param>
        /// <param name="userControlToDisplay"></param>
        /// <param name="controlViewModel"></param>
        public StdContainerWindow(IViewModel parentViewModel, Type userControlToDisplay, Type controlViewModel)
        {
            Object[] parameters = { this, parentViewModel };
            IViewModel viewModel = Activator.CreateInstance(controlViewModel, parameters) as IViewModel;
            Initialise(parentViewModel, userControlToDisplay, viewModel);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentViewModel"></param>
        /// <param name="userControlToDisplay"></param>
        /// <param name="controlViewModel"></param>
        public StdContainerWindow(IViewModel parentViewModel, Type userControlToDisplay, IViewModel controlViewModel)
        {
            Initialise(parentViewModel, userControlToDisplay, controlViewModel);
        }

        /// <summary>
        /// Initialises the view
        /// </summary>
        /// <param name="parentViewModel"></param>
        /// <param name="userControlToDisplay"></param>
        /// <param name="controlViewModel"></param>
        private void Initialise(IViewModel parentViewModel, Type userControlToDisplay, IViewModel controlViewModel)
        {
            UserControl userControl = Activator.CreateInstance(userControlToDisplay) as UserControl;
            if (userControl.IsNotNull())
            {
                userControl.DataContext = controlViewModel;

                this.Content = userControl;
            }
        }
    }
}
