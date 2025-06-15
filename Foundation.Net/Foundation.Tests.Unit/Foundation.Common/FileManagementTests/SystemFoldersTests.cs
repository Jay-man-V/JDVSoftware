//-----------------------------------------------------------------------
// <copyright file="SystemFoldersTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.FileManagementTests
{
    /// <summary>
    /// Unit Tests for System Folders class
    /// </summary>
    [TestFixture]
    public class SystemFoldersTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ReportParameterKeys"/>
        /// </summary>
        [TestCase]
        public void Test_AllMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetStaticPropertyInfosForType(typeof(SystemFolders));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(SystemFolders.TempDirectory)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Verify that the keys contain the values expected
        /// </summary>
        [TestCase]
        public void Test_SystemFoldersProperties()
        {
            String tempDirectory = $@"C:\WINDOWS\TEMP";

            String actualTempDirectory = SystemFolders.TempDirectory.ToUpper();

            Assert.That(actualTempDirectory, Is.EqualTo(tempDirectory));
        }
    }
}
