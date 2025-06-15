//-----------------------------------------------------------------------
// <copyright file="UserFoldersTests.cs" company="JDV Software Ltd">
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
    /// Unit Tests for User Folders class
    /// </summary>
    [TestFixture]
    public class UserFoldersTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ReportParameterKeys"/>
        /// </summary>
        [TestCase]
        public void Test_AllMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetStaticPropertyInfosForType(typeof(UserFolders));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(UserFolders.MyDocuments)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(UserFolders.TempDirectory)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Verify that the keys contain the values expected
        /// </summary>
        [TestCase]
        public void Test_UserFoldersProperties()
        {
            String loggedOnUser = Environment.UserName;

            String myDocuments = $@"C:\Users\{loggedOnUser}\OneDrive\Documents";
            String tempDirectory = $@"C:\Users\{loggedOnUser}\AppData\Local\Temp";

            String actualMyDocuments = UserFolders.MyDocuments;
            String actualTempDirectory = UserFolders.TempDirectory;

            Assert.That(actualMyDocuments, Is.EqualTo(myDocuments));
            Assert.That(actualTempDirectory, Is.EqualTo(tempDirectory));
        }
    }
}
