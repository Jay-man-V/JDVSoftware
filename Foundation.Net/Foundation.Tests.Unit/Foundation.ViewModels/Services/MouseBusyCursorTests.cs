//-----------------------------------------------------------------------
// <copyright file="MouseBusyCursorTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using NUnit.Framework;

using Foundation.ViewModels;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.ViewModels.Services
{
    /// <summary>
    /// Summary description for MouseBusyCursorTests
    /// </summary>
    [TestFixture]
    public class MouseBusyCursorTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor()
        {
            MouseBusyCursor mouseBusyCursor = new MouseBusyCursor();
            Assert.That(mouseBusyCursor.IsBusy, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Constructor_And_Disposed()
        {
            MouseBusyCursor mouseBusyCursor = null;
            using (mouseBusyCursor = new MouseBusyCursor())
            {
                Assert.That(mouseBusyCursor.IsBusy, Is.EqualTo(true));
            }

            mouseBusyCursor.Dispose();
            Assert.That(mouseBusyCursor.IsBusy, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_Constructor_And_UsingBlock()
        {
            using (MouseBusyCursor mouseBusyCursor = new MouseBusyCursor())
            {
                Assert.That(mouseBusyCursor.IsBusy, Is.EqualTo(true));
            }
        }
    }
}
