////-----------------------------------------------------------------------
//// <copyright file="ConfigPropertiesConfigurationTests.cs" company="JDV Software Ltd">
////     Copyright (c) JDV Software Ltd. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------

//using System;
//using System.Reflection;

//using NUnit.Framework;

//using Foundation.Tests.Unit.Support;

//using Foundation.Common;
//using Foundation.Interfaces;

//namespace Foundation.Tests.Unit.Foundation.Common.ConfigurationTests
//{
//    /// <summary>
//    /// The Config Properties Configuration Tests class
//    /// </summary>
//    [TestFixture]
//    public class ConfigPropertiesConfigurationTests : UnitTestBase
//    {
//        [TestCase]
//        public void Test_CountMembers()
//        {
//            // This test exists to ensure all the class are tested/checked in the next test
//            Type theType = typeof(ConfigPropertiesSection);
//            PropertyInfo[] propertyInfos = theType.GetProperties();

//            Int32 index = 0;

//            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ConfigPropertiesSection.ApplicationId)));
//            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ConfigPropertiesSection.ApplicationName)));
//            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ConfigPropertiesSection.UserDataPath)));
//            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ConfigPropertiesSection.SystemDataPath)));
//            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ConfigPropertiesSection.DataConnectionName)));

//            Assert.That(propertyInfos.Length, Is.EqualTo(index));
//        }

//        /// <summary>
//        /// Tests the configuration properties.
//        /// </summary>
//        [TestCase]
//        public void Test_ConfigurationProperties()
//        {
//            EntityId expectedApplicationId = new EntityId(1);
//            const String expectedApplicationName = "applicationName";
//            const String expectedUserDataPath = ".\\UserData";
//            const String expectedSystemDataPath = "\\SystemData\\";
//            const String expectedDataConnectionName = "UnitTesting";

//            Int32 actualApplicationId = ApplicationSettings.ApplicationId;
//            String actualApplicationName = ApplicationSettings.ApplicationName;
//            String actualUserDataPath = ApplicationSettings.UserDataPath;
//            String actualSystemDataPath = ApplicationSettings.SystemDataPath;
//            String actualDataConnectionName = ApplicationSettings.DataConnectionName;

//            Assert.That(actualApplicationId, Is.EqualTo(expectedApplicationId.TheEntityId));
//            Assert.That(actualApplicationName, Is.EqualTo(expectedApplicationName));
//            Assert.That(actualUserDataPath, Is.EqualTo(expectedUserDataPath));
//            Assert.That(actualSystemDataPath, Is.EqualTo(expectedSystemDataPath));
//            Assert.That(actualDataConnectionName, Is.EqualTo(expectedDataConnectionName));
//        }
//    }
//}
