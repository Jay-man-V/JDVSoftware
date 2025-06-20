//-----------------------------------------------------------------------
// <copyright file="WpfApplicationObjects.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// </summary>
    [DependencyInjectionSingleton]
    public class WpfApplicationObjects : IWpfApplicationObjects
    {
        /// <inheritdoc cref="IWpfApplicationObjects.ApplicationWrapper"/>
        public IApplicationWrapper ApplicationWrapper { get; }

        /// <inheritdoc cref="IWpfApplicationObjects.ClipBoardWrapper"/>
        public IClipBoardWrapper ClipBoardWrapper { get; }

        /// <inheritdoc cref="IWpfApplicationObjects.DialogService"/>
        public IDialogService DialogService { get; }

        /// <inheritdoc cref="IWpfApplicationObjects.DispatcherTimerWrapper"/>
        public IDispatcherTimerWrapper DispatcherTimerWrapper { get; }

        /// <inheritdoc cref="IWpfApplicationObjects.DispatcherWrapper"/>
        public IDispatcherWrapper DispatcherWrapper { get; }
    }
}