//-----------------------------------------------------------------------
// <copyright file="IMouseBusyCursor.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Interfaces
{
    /// <summary>
    /// The interface Mouse Busy Cursor
    /// </summary>
    public interface IMouseBusyCursor : IDisposable
    {
        /// <summary>
        /// Sets the busy state as busy.
        /// </summary>
        void SetBusyState();
    }
}
