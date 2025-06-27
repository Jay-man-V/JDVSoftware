//-----------------------------------------------------------------------
// <copyright file="MenuItemProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using NSubstitute;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ConfigurationScopeProcessTests
    /// </summary>
    [TestFixture]
    public class MenuItemProcessTests : CommonBusinessProcessTestBaseClass<IMenuItem, IMenuItemProcess, IMenuItemRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 20;
        protected override String ExpectedScreenTitle => "Menu Items";
        protected override String ExpectedStatusBarText => "Number of Menu Items:";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Application:";
        protected override string ExpectedFilter1DisplayMemberPath => FDC.Application.Name;

        protected override Boolean ExpectedHasOptionalDropDownParameter2 => true;
        protected override String ExpectedFilter2Name => "Parent:";
        protected override string ExpectedFilter2DisplayMemberPath => FDC.MenuItem.Caption;

        protected override string ExpectedComboBoxDisplayMember => FDC.MenuItem.Caption;

        protected override IMenuItemRepository CreateRepository()
        {
            IMenuItemRepository dataAccess = Substitute.For<IMenuItemRepository>();

            return dataAccess;
        }

        protected override IMenuItemProcess CreateBusinessProcess()
        {
            IMenuItemProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IMenuItemProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();

            CopyProperties(applicationProcess, CoreInstance.Container.Get<IApplicationProcess>());

            IMenuItemProcess process = new MenuItemProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, applicationProcess);

            return process;
        }

        protected override IMenuItem CreateBlankEntity(IMenuItemProcess process)
        {
            IMenuItem retVal = CoreInstance.Container.Get<IMenuItem>();

            return retVal;
        }

        protected override IMenuItem CreateEntity(IMenuItemProcess process)
        {
            IMenuItem retVal = CreateBlankEntity(process);

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

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
            retVal.Icon = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 10};

            return retVal;
        }

        protected override void CheckBlankEntry(IMenuItem entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IMenuItem entity)
        {
            Assert.That(entity.Caption, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IMenuItem entity)
        {
            Assert.That(entity.Caption, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IMenuItem entity1, IMenuItem entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.ParentMenuItemId, Is.EqualTo(entity1.ParentMenuItemId));
            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Caption, Is.EqualTo(entity1.Caption));
            Assert.That(entity2.ControllerAssembly, Is.EqualTo(entity1.ControllerAssembly));
            Assert.That(entity2.ControllerType, Is.EqualTo(entity1.ControllerType));
            Assert.That(entity2.ViewAssembly, Is.EqualTo(entity1.ViewAssembly));
            Assert.That(entity2.ViewType, Is.EqualTo(entity1.ViewType));
            Assert.That(entity2.HelpText, Is.EqualTo(entity1.HelpText));
            Assert.That(entity2.MultiInstance, Is.EqualTo(entity1.MultiInstance));
            Assert.That(entity2.ShowInTab, Is.EqualTo(entity1.ShowInTab));
            Assert.That(entity2.Icon, Is.EqualTo(entity1.Icon));
        }

        protected override void UpdateEntityProperties(IMenuItem entity)
        {
            entity.Name += "Updated";
            entity.Caption += "Updated";
            entity.ControllerAssembly+= "Updated";
            entity.ControllerType += "Updated";
            entity.ViewAssembly += "Updated";
            entity.ViewType += "Updated";
            entity.HelpText += "Updated";
            entity.MultiInstance = false;
            entity.ShowInTab = false;
            entity.Icon = new Byte[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        }

        [TestCase]
        public void Test_ValidateEntity()
        {
            IMenuItemProcess process = CreateBusinessProcess();
            IMenuItem menuitem = CreateEntity(process);

            menuitem.ControllerAssembly = String.Empty;

            Exception actualException = null;

            try
            {
                const Boolean validateAllProperties = true;
                process.ValidateEntity(menuitem, validateAllProperties);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.Null);
            Assert.That(actualException, Is.TypeOf<AggregateException>());

            AggregateException aggregateException = actualException as AggregateException;
            Assert.That(aggregateException.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(aggregateException.InnerExceptions[0], Is.TypeOf<ValidationException>());

            ValidationException validationException = aggregateException.InnerExceptions[0] as ValidationException;
            Assert.That(validationException.Message, Is.EqualTo("ControllerAssembly must be provided"));
        }

        [TestCase]
        public void Test_MakeListOfParentMenuItems()
        {
            IMenuItemProcess process = CreateBusinessProcess();
            List<IMenuItem> menuItems = new List<IMenuItem>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            menuItems[0].ParentMenuItemId = new EntityId(0);
            menuItems[1].ParentMenuItemId = new EntityId(0);
            menuItems[2].ParentMenuItemId = new EntityId(0);
            menuItems[3].ParentMenuItemId = new EntityId(0);
            menuItems[4].ParentMenuItemId = new EntityId(0);

            List<IMenuItem> parentMenuItems = process.MakeListOfParentMenuItems(menuItems);
            Assert.That(parentMenuItems.Count, Is.EqualTo(0));

            Int32 counter = 1;
            foreach (IMenuItem menuitem in menuItems)
            {
                menuitem.Id = new EntityId(counter);

                counter++;
                menuitem.ParentMenuItemId = new EntityId(counter);
            }

            parentMenuItems = process.MakeListOfParentMenuItems(menuItems);
            Assert.That(parentMenuItems.Count, Is.EqualTo(4));

            menuItems[1].ParentMenuItemId = new EntityId(1);
            menuItems[2].ParentMenuItemId = new EntityId(2);
            menuItems[3].ParentMenuItemId = new EntityId(3);
            menuItems[4].ParentMenuItemId = new EntityId(1);

            parentMenuItems = process.MakeListOfParentMenuItems(menuItems);
            Assert.That(parentMenuItems.Count, Is.EqualTo(3));
        }

        [TestCase]
        public void Test_ApplyFilter_Application()
        {
            IMenuItemProcess process = CreateBusinessProcess();

            IApplication application1 = CoreInstance.Container.Get<IApplication>();
            application1.Id = new AppId(1);

            IApplication application2 = CoreInstance.Container.Get<IApplication>();
            application2.Id = new AppId(2);

            const IMenuItem parentMenuItem = null;

            List<IMenuItem> menuItems = new List<IMenuItem>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            menuItems[0].ParentMenuItemId = new EntityId(0);
            menuItems[1].ParentMenuItemId = new EntityId(0);
            menuItems[2].ParentMenuItemId = new EntityId(0);
            menuItems[3].ParentMenuItemId = new EntityId(0);
            menuItems[4].ParentMenuItemId = new EntityId(0);

            menuItems[0].Id = new EntityId(0);
            menuItems[0].ApplicationId = application1.Id;
            menuItems[0].ParentMenuItemId = new EntityId(1);

            menuItems[1].Id = new EntityId(1);
            menuItems[1].ApplicationId = application2.Id;
            menuItems[1].ParentMenuItemId = new EntityId(2);

            menuItems[2].Id = new EntityId(2);
            menuItems[2].ApplicationId = application1.Id;
            menuItems[2].ParentMenuItemId = new EntityId(3);

            menuItems[3].Id = new EntityId(3);
            menuItems[3].ApplicationId = application2.Id;
            menuItems[3].ParentMenuItemId = new EntityId(4);

            menuItems[4].Id = new EntityId(4);
            menuItems[4].ApplicationId = application1.Id;
            menuItems[4].ParentMenuItemId = new EntityId(5);

            List<IMenuItem> filteredMenuItems1 = process.ApplyFilter(menuItems, application1, parentMenuItem);
            Assert.That(filteredMenuItems1.Count, Is.EqualTo(3));

            List<IMenuItem> filteredMenuItems2 = process.ApplyFilter(menuItems, application2, parentMenuItem);
            Assert.That(filteredMenuItems2.Count, Is.EqualTo(2));
        }

        [TestCase]
        public void Test_ApplyFilter_ParentContact()
        {
            IMenuItemProcess process = CreateBusinessProcess();

            const IApplication application = null;

            IMenuItem parentMenuItem1 = CreateEntity(process);
            parentMenuItem1.Id = new EntityId(1);

            IMenuItem parentMenuItem2 = CreateEntity(process);
            parentMenuItem2.Id = new EntityId(2);

            List<IMenuItem> menuItems = new List<IMenuItem>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            menuItems[0].ParentMenuItemId = new EntityId(0);
            menuItems[1].ParentMenuItemId = new EntityId(0);
            menuItems[2].ParentMenuItemId = new EntityId(0);
            menuItems[3].ParentMenuItemId = new EntityId(0);
            menuItems[4].ParentMenuItemId = new EntityId(0);

            menuItems[0].Id = new EntityId(0);
            menuItems[0].ApplicationId = new AppId(1);
            menuItems[0].ParentMenuItemId = parentMenuItem1.Id;

            menuItems[1].Id = new EntityId(1);
            menuItems[1].ApplicationId = new AppId(2);
            menuItems[1].ParentMenuItemId = parentMenuItem2.Id;

            menuItems[2].Id = new EntityId(2);
            menuItems[2].ApplicationId = new AppId(1);
            menuItems[2].ParentMenuItemId = parentMenuItem1.Id;

            menuItems[3].Id = new EntityId(3);
            menuItems[3].ApplicationId = new AppId(2);
            menuItems[3].ParentMenuItemId = parentMenuItem2.Id;

            menuItems[4].Id = new EntityId(4);
            menuItems[4].ApplicationId = new AppId(1);
            menuItems[4].ParentMenuItemId = parentMenuItem1.Id;

            List<IMenuItem> filteredContacts1 = process.ApplyFilter(menuItems, application, parentMenuItem1);
            Assert.That(filteredContacts1.Count, Is.EqualTo(3));

            List<IMenuItem> filteredContacts2 = process.ApplyFilter(menuItems, application, parentMenuItem2);
            Assert.That(filteredContacts2.Count, Is.EqualTo(2));
        }
    }
}
