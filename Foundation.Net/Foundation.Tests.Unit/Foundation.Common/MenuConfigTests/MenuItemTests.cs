//-----------------------------------------------------------------------
// <copyright file="MenuItemTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.MenuConfigTests
{
    /// <summary>
    /// The Menu Item Tests class
    /// </summary>
    [TestFixture]
    public class MenuItemTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ViewMenuItem"/>
        /// </summary>
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetInstancePropertyInfosForType(typeof(ViewMenuItem));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Menu)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Name)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Caption)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Icon)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.ViewScreen)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Controller)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.HelpText)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.MultiInstance)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.ShowInTab)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.IsSelected)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.IsExpanded)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.MenuItems)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewMenuItem.Parameters)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Constructor()
        {
            ViewMenuItem obj = new ViewMenuItem();

            Assert.That(obj.Menu, Is.EqualTo(String.Empty));
            Assert.That(obj.Name, Is.EqualTo(String.Empty));
            Assert.That(obj.Caption, Is.EqualTo(String.Empty));
            Assert.That(obj.Icon, Is.EqualTo(String.Empty));
            Assert.That(obj.ViewScreen, Is.EqualTo(null));
            Assert.That(obj.Controller, Is.EqualTo(null));
            Assert.That(obj.HelpText, Is.EqualTo(String.Empty));
            Assert.That(obj.MultiInstance, Is.EqualTo(false));
            Assert.That(obj.ShowInTab, Is.EqualTo(false));
            Assert.That(obj.MenuItems, Is.Not.EqualTo(null));
            Assert.That(obj.IsSelected, Is.EqualTo(false));
            Assert.That(obj.IsExpanded, Is.EqualTo(false));
            Assert.That(obj.Parameters, Is.Not.EqualTo(null));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Clone()
        {
            ViewMenuItem obj = new ViewMenuItem
            {
                Menu = "Menu",
                Name = "Name",
                Caption = "Caption",
                Icon = "Icon",
                HelpText = "HelpText",
                MultiInstance = false,
                ShowInTab = false,
                IsSelected = false,
                IsExpanded = false,
            };

            ViewMenuItem clone = obj.Clone() as ViewMenuItem;

            Assert.That(obj, Is.Not.EqualTo(clone));

            Assert.That(obj.Menu, Is.EqualTo(clone.Menu));
            Assert.That(obj.Name, Is.EqualTo(clone.Name));
            Assert.That(obj.Caption, Is.EqualTo(clone.Caption));
            Assert.That(obj.Icon, Is.EqualTo(clone.Icon));
            Assert.That(obj.HelpText, Is.EqualTo(clone.HelpText));
            Assert.That(obj.MultiInstance, Is.EqualTo(clone.MultiInstance));
            Assert.That(obj.ShowInTab, Is.EqualTo(clone.ShowInTab));
            Assert.That(obj.IsSelected, Is.EqualTo(clone.IsSelected));
            Assert.That(obj.IsExpanded, Is.EqualTo(clone.IsExpanded));
            Assert.That(obj.ViewScreen, Is.EqualTo(null));
            Assert.That(obj.Controller, Is.EqualTo(null));

            Assert.That(obj.MenuItems, Is.Not.SameAs(clone.MenuItems));
            Assert.That(obj.Parameters, Is.Not.SameAs(clone.Parameters));
        }
    }
}
