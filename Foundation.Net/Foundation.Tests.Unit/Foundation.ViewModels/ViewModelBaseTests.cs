//-----------------------------------------------------------------------
// <copyright file="ViewModelBaseTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using NUnit.Framework;

using NSubstitute;

using Foundation.Interfaces;
using Foundation.ViewModels;

using Foundation.Tests.Unit.Foundation.ViewModels.Support;
using Foundation.Tests.Unit.Mocks;

namespace Foundation.Tests.Unit.Foundation.ViewModels
{
    /// <summary>
    /// Summary description for ViewModelBaseTests
    /// </summary>
    [TestFixture]
    public class ViewModelBaseTests : ViewModelTestBaseClass<IMockViewModel>
    {
        protected override String ExpectedScreenTitle { get; set; } = "Mock View";

        protected override IMockViewModel CreateViewModel(IDateTimeService dateTimeService)
        {
            IMockViewModel viewModel = new MockViewModel(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DialogService, ClipBoardWrapper);
            ViewModelBase viewModelBase = (ViewModelBase)viewModel;

            viewModelBase.MouseBusyCursor = Substitute.For<IMouseBusyCursor>();

            return viewModel;
        }
    }
}
