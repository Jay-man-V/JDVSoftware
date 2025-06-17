//-----------------------------------------------------------------------
// <copyright file="MenuItemViewModelTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.BusinessProcess;
using Foundation.Interfaces;
using Foundation.Tests.Unit.Foundation.ViewModels.Support;
using Foundation.ViewModels;

using NSubstitute;

using NUnit.Framework;

using System;
using System.Collections.Generic;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.ViewModels.CoreTests
{
    /// <summary>
    /// Summary description for MenuItemViewModelTests
    /// </summary>
    [TestFixture]
    public class MenuItemViewModelTests : GenericDataGridViewModelTestBaseClass<IMenuItem, IMenuItemViewModel, IMenuItemProcess>
    {
        protected override String ExpectedScreenTitle => "Menu Items";
        protected override String ExpectedStatusBarText => "Number of Menu Items:";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Application:";
        protected override string ExpectedFilter1DisplayMemberPath => FDC.Application.Name;

        protected override Boolean ExpectedHasOptionalDropDownParameter2 => true;
        protected override String ExpectedFilter2Name => "Parent:";
        protected override string ExpectedFilter2DisplayMemberPath => FDC.MenuItem.Name;

        private IApplicationProcess ApplicationProcess { get; set; }

        protected override IMenuItemViewModel CreateViewModel(IDateTimeService dateTimeService)
        {
            ApplicationProcess = Substitute.For<IApplicationProcess>();

            IMenuItemViewModel viewModel = new MenuItemViewModel(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DialogService, ClipBoardWrapper, FileApi, BusinessProcess, ApplicationProcess);
            GenericDataGridViewModelBase<IMenuItem> genericDataGridViewModel = (GenericDataGridViewModelBase<IMenuItem>)viewModel;

            genericDataGridViewModel.MouseBusyCursor = Substitute.For<IMouseBusyCursor>();

            return viewModel;
        }

        protected override IMenuItemProcess CreateBusinessProcess()
        {
            IMenuItemProcess process = Substitute.For<IMenuItemProcess>();

            return process;
        }

        protected override IMenuItem CreateModel()
        {
            IMenuItem retVal = base.CreateModel();

            retVal.ApplicationId = new AppId(1);
            retVal.ParentMenuItemId = new EntityId(1);
            retVal.Name = Guid.NewGuid().ToString();
            retVal.Caption = Guid.NewGuid().ToString();
            retVal.ControllerAssembly = Guid.NewGuid().ToString();
            retVal.ControllerType = Guid.NewGuid().ToString();
            retVal.ViewAssembly = Guid.NewGuid().ToString();
            retVal.ViewType = Guid.NewGuid().ToString();
            retVal.HelpText = Guid.NewGuid().ToString();
            retVal.MultiInstance = true;
            retVal.ShowInTab = true;
            retVal.Icon = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 10 };

            return retVal;
        }

        protected override void SetupForRefreshData()
        {
            base.SetupForRefreshData();

            List<IApplication> applications = new List<IApplication>
            {
                CoreInstance.Container.Get<IApplication>(),
                CoreInstance.Container.Get<IApplication>(),
            };
            ApplicationProcess.GetAll().Returns(applications);

            //List<IMenuItem> menuItems = new List<IMenuItem>
            //{
            //    CoreInstance.Container.Get<IMenuItem>(),
            //    CoreInstance.Container.Get<IMenuItem>(),
            //};
            //BusinessProcess.GetAll().Returns(menuItems);

            List<IMenuItem> parentMenuItems = new List<IMenuItem>
            {
                CreateModel(),
                CreateModel(),
            };
            BusinessProcess.MakeListOfParentMenuItems(Arg.Any<List<IMenuItem>>()).Returns(parentMenuItems);

            List<IMenuItem> filteredData = new List<IMenuItem>();
            BusinessProcess.ApplyFilter(Arg.Any<List<IMenuItem>>(), Arg.Any<IApplication>(), Arg.Any<IMenuItem>()).Returns(filteredData);
        }

        protected override Object CreateModelForDropDown1()
        {
            IApplication retVal = CoreInstance.Container.Get<IApplication>();

            return retVal;
        }

        protected override Object CreateModelForDropDown2()
        {
            IMenuItem retVal = CoreInstance.Container.Get<IMenuItem>();

            return retVal;
        }
    }
}
