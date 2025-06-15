//-----------------------------------------------------------------------
// <copyright file="ParameterTests.cs" company="JDV Software Ltd">
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
    /// The Parameter Tests class
    /// </summary>
    [TestFixture]
    public class ParameterTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ViewParameter"/>
        /// </summary>
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetInstancePropertyInfosForType(typeof(ViewParameter));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewParameter.Name)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewParameter.Value)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Constructor()
        {
            ViewParameter obj = new ViewParameter();

            Assert.That(obj.Name, Is.EqualTo(String.Empty));
            Assert.That(obj.Value, Is.EqualTo(String.Empty));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Clone()
        {
            ViewParameter obj = new ViewParameter
            {
                Name = "Name",
                Value = "Value",
            };

            ViewParameter clone = obj.Clone() as ViewParameter;

            Assert.That(obj, Is.Not.EqualTo(clone));

            Assert.That(obj.Name, Is.EqualTo(clone.Name));
            Assert.That(obj.Value, Is.EqualTo(clone.Value));
        }
    }
}
