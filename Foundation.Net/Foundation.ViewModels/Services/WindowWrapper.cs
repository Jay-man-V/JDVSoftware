﻿//-----------------------------------------------------------------------
// <copyright file="WindowWrapper.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Windows;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// </summary>
    [DependencyInjectionSingleton]
    internal class WindowWrapper : IWindowWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public WindowWrapper()
        {
            Window = Application.Current.MainWindow;
        }

        private Window Window { get; }

        /// <inheritdoc cref="IWindowWrapper.Close()"/>
        public void Close()
        {
            if (Window.IsNotNull())
            {
                Window.Close();
            }
        }
    }
}