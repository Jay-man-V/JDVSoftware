//-----------------------------------------------------------------------
// <copyright file="MainApplicationProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;

using NUnit.Framework;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.MainTests
{
    /// <summary>
    /// The Main Application Process Tests class
    /// </summary>
    [TestFixture]
    public class MainApplicationProcessTests : UnitTestBase
    {
        [TestCase]
        [DeploymentItem(@".Support\Configs\MenuConfigSample.xml", @".Support\Configs\")]
        public void Test_Deserialisation()
        {
            String applicationDefinitionFile = @".Support\Configs\MenuConfigSample.xml";

            IMainApplicationProcess mainApplicationProcess = CoreInstance.Container.Get<IMainApplicationProcess>();

            ApplicationDefinition applicationDefinition = mainApplicationProcess.LoadApplicationDefinition(applicationDefinitionFile);

            Assert.That(applicationDefinition.Name, Is.EqualTo("Menu Config Testing"));

            // 3 Menu Items in the Main Menu
            Assert.That(applicationDefinition.ViewMenuItems.Count, Is.EqualTo(3));

            // Menu Item #1
            ValidateMenuItem_1(applicationDefinition.ViewMenuItems[0]);

            // Menu Item #2
            ValidateMenuItem_2(applicationDefinition.ViewMenuItems[1]);

            // Menu Item #3
            ValidateMenuItem_3(applicationDefinition.ViewMenuItems[2]);
        }

        [TestCase]
        public void Test_Deserialisation_FileNotExist()
        {
            String applicationDefinitionFile = "MadeUpFileDoesNotExist.xml";
            String message = $"The file '{applicationDefinitionFile}' does not exist or access to it is denied";
            String expectedFile = $"{applicationDefinitionFile}";

            FileNotFoundException actualException = null;

            try
            {
                IMainApplicationProcess mainApplicationProcess = CoreInstance.Container.Get<IMainApplicationProcess>();

                _ = mainApplicationProcess.LoadApplicationDefinition(applicationDefinitionFile);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualMessage = ReplaceFilePathWithConstant(actualException.Message);
            Assert.That(actualMessage, Is.EqualTo(message));

            String actualFile = ReplaceFilePathWithConstant(actualException.FileName);
            Assert.That(actualFile, Is.EqualTo(expectedFile));
        }

        private void ValidateMenuItem_1(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Test Form"));
            Assert.That(menuItem.Name, Is.EqualTo("Test Form"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.TestFormController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }

        private void ValidateMenuItem_2(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Logs"));
            Assert.That(menuItem.Name, Is.EqualTo("Logs"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(false));
            Assert.That(menuItem.Controller, Is.EqualTo(null));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(1));

            // Menu Item #2.1
            ValidateMenuItem_2_1(menuItem.MenuItems[0]);
        }

        private void ValidateMenuItem_2_1(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Event Log"));
            Assert.That(menuItem.Name, Is.EqualTo("Event Log"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.EventLogController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }

        private void ValidateMenuItem_3(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Security"));
            Assert.That(menuItem.Name, Is.EqualTo("Security"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(false));
            Assert.That(menuItem.Controller, Is.EqualTo(null));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(4));

            // Menu Item #3.1
            ValidateMenuItem_3_1(menuItem.MenuItems[0]);

            // Menu Item #3.2
            ValidateMenuItem_3_2(menuItem.MenuItems[1]);

            // Menu Item #3.3
            ValidateMenuItem_3_3(menuItem.MenuItems[2]);

            // Menu Item #3.4
            ValidateMenuItem_3_4(menuItem.MenuItems[3]);
        }

        private void ValidateMenuItem_3_1(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Applications"));
            Assert.That(menuItem.Name, Is.EqualTo("Applications"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.ApplicationController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }

        private void ValidateMenuItem_3_2(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Application Types"));
            Assert.That(menuItem.Name, Is.EqualTo("ApplicationTypes"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.ApplicationTypeController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }

        private void ValidateMenuItem_3_3(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("Roles"));
            Assert.That(menuItem.Name, Is.EqualTo("Roles"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.RoleController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }

        private void ValidateMenuItem_3_4(ViewMenuItem menuItem)
        {
            Assert.That(menuItem.Caption, Is.EqualTo("User Profiles"));
            Assert.That(menuItem.Name, Is.EqualTo("UserProfiles"));
            Assert.That(menuItem.Icon, Is.EqualTo(String.Empty));
            Assert.That(menuItem.HelpText, Is.EqualTo(String.Empty));
            Assert.That(menuItem.MultiInstance, Is.EqualTo(false));
            Assert.That(menuItem.ShowInTab, Is.EqualTo(true));
            Assert.That(menuItem.Controller.AssemblyName, Is.EqualTo("Foundation.WinForms.Controllers"));
            Assert.That(menuItem.Controller.AssemblyType, Is.EqualTo("Foundation.WinForms.Controllers.UserProfileController"));
            Assert.That(menuItem.MenuItems.Count, Is.EqualTo(0));
        }
    }
}
