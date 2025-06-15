﻿//-----------------------------------------------------------------------
// <copyright file="CancelEventArgs.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Tests.Unit.Mocks
{
    public class CancelEventArgs : EventArgs
    {
        public Boolean Cancel { get; set; }
    }
}
