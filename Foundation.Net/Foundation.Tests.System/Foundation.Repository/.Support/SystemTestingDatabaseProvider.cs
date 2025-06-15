//-----------------------------------------------------------------------
// <copyright file="SystemTestingDatabaseProvider.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Interfaces;

namespace Foundation.Tests.System.Foundation.Repository.Support
{
    public interface ISystemTestingDatabaseProvider : IDatabaseProvider
    {

    }

    [DependencyInjectionTransient]
    public class SystemTestingDatabaseProvider : ISystemTestingDatabaseProvider
    {
        public String ConnectionName => "SystemTesting";
    }
}
