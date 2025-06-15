//-----------------------------------------------------------------------
// <copyright file="StatusViewModelTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.Interfaces;
using Foundation.ViewModels;

using Foundation.Tests.Unit.Foundation.ViewModels.Support;

namespace Foundation.Tests.Unit.Foundation.ViewModels.CoreTests
{
    /// <summary>
    /// Summary description for StatusViewModelTests
    /// </summary>
    [TestFixture]
    public class StatusViewModelTests : GenericDataGridViewModelTestBaseClass<IStatus, IStatusViewModel, IStatusProcess>
    {
        protected override String ExpectedScreenTitle => "Statuses";
        protected override String ExpectedStatusBarText => "Number of Statuses:";

        protected override IStatusViewModel CreateViewModel(IDateTimeService dateTimeService)
        {
            IStatusViewModel viewModel = new StatusViewModel(CoreInstance, RunTimeEnvironmentSettings, dateTimeService,DialogService, ClipBoardWrapper, FileApi, BusinessProcess);
            GenericDataGridViewModelBase<IStatus> genericDataGridViewModel = (GenericDataGridViewModelBase<IStatus>)viewModel;

            genericDataGridViewModel.MouseBusyCursor = Substitute.For<IMouseBusyCursor>();

            return viewModel;
        }

        protected override IStatusProcess CreateBusinessProcess()
        {
            IStatusProcess process = Substitute.For<IStatusProcess>();

            return process;
        }

        protected override IStatus CreateModel()
        {
            IStatus retVal = base.CreateModel();

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }
    }
}
