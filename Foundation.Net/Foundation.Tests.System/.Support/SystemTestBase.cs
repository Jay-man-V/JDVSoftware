//-----------------------------------------------------------------------
// <copyright file="SystemTestBase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Foundation.Tests.Unit.Support;
using Foundation.Interfaces;
using System;
using Foundation.Common;

namespace Foundation.Tests.System.Support
{
    /// <summary>
    /// The System Test Base class
    /// </summary>
    [TestFixture]
    public abstract class SystemTestBase : UnitTestBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SystemTestBase"/> class.
        /// </summary>
        protected SystemTestBase() { }

        protected AppId CoreSystemApplicationId => new AppId(0);
        protected AppId JdvSoftwareApplicationId => new AppId(1);

        protected IEventLogProcess EventLogProcess { get; set; }

        protected LogId ParentLogId { get; set; }
        protected String BatchName => "System Testing";

        protected String ProcessName { get; set; }
        protected String TaskName { get; set; }

        protected override void StartTest()
        {
            EventLogProcess = Core.Core.Instance.Container.Get<IEventLogProcess>();
            ParentLogId = EventLogProcess.StartTask(ApplicationSettings.ApplicationId, BatchName, ProcessName, TaskName);
        }

        protected override void EndTest()
        {
            base.EndTest();

            EventLogProcess.EndTask(ParentLogId, LogSeverity.Information, "Test finished.");
        }
    }
}
