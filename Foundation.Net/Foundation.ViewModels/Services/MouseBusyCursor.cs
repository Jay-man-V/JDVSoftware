//-----------------------------------------------------------------------
// <copyright file="MouseBusyCursor.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    ///  Contains helper methods for UI, so far just one for showing a wait cursor
    /// </summary>
    [DependencyInjectionTransient]
    public class MouseBusyCursor : IMouseBusyCursor
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MouseBusyCursor" /> class.
        /// </summary>
        public MouseBusyCursor(IDispatcherTimerWrapper dispatcherTimerWrapper = null)
        {
            DispatcherTimerWrapper = dispatcherTimerWrapper;
            SetBusyState();
        }

        private IDispatcherTimerWrapper DispatcherTimerWrapper { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            SetBusyState(false);
        }

        /// <summary>
        /// A value indicating whether the UI is currently busy
        /// </summary>
        internal Boolean IsBusy { get; set; }

        /// <inheritdoc cref="SetBusyState()"/>
        public void SetBusyState()
        {
            SetBusyState(true);
        }

        /// <summary>
        /// Sets the busy state to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private void SetBusyState(Boolean busy)
        {
            if (busy != IsBusy)
            {
                IsBusy = busy;

                if (Application.Current.IsNotNull())
                {
                    Mouse.OverrideCursor = null;
                }

                if (IsBusy &&
                    Application.Current.IsNotNull())
                {
                    Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                    TimeSpan interval = TimeSpan.FromSeconds(0);
                    DispatcherPriority dispatcherPriority = DispatcherPriority.ApplicationIdle;
                    Dispatcher currentDispatcher = Application.Current.Dispatcher;
                    IDispatcherTimerWrapper timer = DispatcherTimerWrapper.NewTimer(interval, dispatcherPriority, OnDispatcherTimer_Tick, currentDispatcher);
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDispatcherTimer_Tick(Object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dispatcherTimer)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }
    }
}
