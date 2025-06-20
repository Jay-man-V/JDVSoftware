//-----------------------------------------------------------------------
// <copyright file="MockDispatcherTimerWrapper.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows.Threading;

using Foundation.Interfaces;

namespace Foundation.Tests.Unit.Mocks
{
    public class MockDispatcherTimerWrapper : IDispatcherTimerWrapper
    {
        public MockDispatcherTimerWrapper() { }

        private MockDispatcherTimerWrapper(TimeSpan interval, DispatcherPriority priority, EventHandler callback, Dispatcher dispatcher)
        {
            DispatcherTimer = new MockDispatcherTimerWrapper();
        }

        private MockDispatcherTimerWrapper DispatcherTimer { get; }

        public IDispatcherTimerWrapper NewTimer(TimeSpan interval, DispatcherPriority priority, EventHandler callback, Dispatcher dispatcher)
        {
            IDispatcherTimerWrapper retVal = new MockDispatcherTimerWrapper(interval, priority, callback, dispatcher);

            return retVal;
        }

        public void Start()
        {
            DispatcherTimer.Start();
        }
    }
}
