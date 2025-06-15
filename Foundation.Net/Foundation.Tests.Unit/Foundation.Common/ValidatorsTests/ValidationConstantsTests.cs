//-----------------------------------------------------------------------
// <copyright file="ValidationConstantsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Resources
{
    /// <summary>
    /// Unit Tests for the Validation Constants class
    /// </summary>
    [TestFixture]
    public class ValidationConstantsTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            Type thisType = this.GetType();
            MethodInfo[] testMethods = thisType.GetMethods();
            Int32 testMethodCount = testMethods.Count(m => m.Name.StartsWith("Test_"));

            // This test exists to ensure all the Properties are tested/checked in the next test
            Type theType = typeof(ValidationConstants);
            PropertyInfo[] propertyInfos = theType.GetProperties();

            Assert.That(propertyInfos.Length, Is.EqualTo(testMethodCount - 1));

            Int32 index = 0;
            //Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ValidationConstants.Name)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        //[TestCase]
        //public void Test_Name()
        //{
        //    String expected = "JDV Software Ltd";
        //    String actual = CompanyDetails.Name;

        //    Assert.That(actual, Is.EqualTo(expected));
        //}
    }
}
