﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationWrapper.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Windows;

using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// </summary>
    [DependencyInjectionSingleton]
    public class ApplicationWrapper : IApplicationWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationWrapper(IWindowWrapper windowWrapper)
        {
            MainWindow = windowWrapper;
            CurrentApplication = Application.Current; ;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Application" /> object for the current <see cref="T:System.AppDomain" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Windows.Application" /> object for the current <see cref="T:System.AppDomain" />.</returns>
        private Application CurrentApplication { get; }

        /// <inheritdoc cref="IApplicationWrapper.MainWindow"/>
        public IWindowWrapper MainWindow { get; }
    }
}
