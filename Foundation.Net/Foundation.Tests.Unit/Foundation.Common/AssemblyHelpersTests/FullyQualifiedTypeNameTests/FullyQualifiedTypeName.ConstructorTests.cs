//-----------------------------------------------------------------------
// <copyright file="FullyQualifiedTypeNameConstructorTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Xml;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.AssemblyHelpersTests.FullyQualifiedTypeNameTests
{
    /// <summary>
    /// Email Address tests
    /// </summary>
    [TestFixture]
    public class FullyQualifiedTypeNameConstructorTests : UnitTestBase
    {
        /// <summary>
        /// Tests the default constructor.
        /// </summary>
        [TestCase]
        public void Test_ConstructorDefault()
        {
            Object fullyQualifiedTypeName = new FullyQualifiedTypeName();

            Assert.That(fullyQualifiedTypeName, Is.Not.EqualTo(null));
            Assert.That(fullyQualifiedTypeName, Is.InstanceOf<FullyQualifiedTypeName>());
        }

        /// <summary>
        /// Tests the string constructor.
        /// </summary>
        [TestCase]
        public void Test_ConstructorString()
        {
            String fullyQualifiedTypeNameString = @"<TaskImplementation assembly=""Foundation.BusinessProcess"" type=""Foundation.BusinessProcess.ScheduledJobProcess"" />";
            Object fullyQualifiedTypeNameObject = new FullyQualifiedTypeName(fullyQualifiedTypeNameString);

            Assert.That(fullyQualifiedTypeNameObject, Is.Not.EqualTo(null), $"Failed to create FullyQualifiedTypeName object for '{fullyQualifiedTypeNameString}'");

            String generatedObjectType = fullyQualifiedTypeNameObject.GetType().ToString();
            Assert.That(fullyQualifiedTypeNameObject, Is.InstanceOf<FullyQualifiedTypeName>(), $"Generated object '{generatedObjectType}' does not match expected FullyQualifiedTypeName type");

            String fullyQualifiedTypeObjectValue = fullyQualifiedTypeNameObject.ToString();
            Assert.That(fullyQualifiedTypeObjectValue, Is.EqualTo(fullyQualifiedTypeNameString), $"Fully Qualified Type Name do not match input: '{fullyQualifiedTypeNameString}', from object: '{fullyQualifiedTypeObjectValue}'");
        }

        /// <summary>
        /// Tests the constructor with an empty string.
        /// </summary>
        [TestCase]
        public void Test_ConstructorEmptyString()
        {
            Exception actualException = null;

            try
            {
                String xmlTypeName = String.Empty;
                _ = new FullyQualifiedTypeName(xmlTypeName);
            }
            catch(Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<XmlException>());
        }

        /// <summary>
        /// Tests the constructor with a null string.
        /// </summary>
        [TestCase]
        public void Test_ConstructorNullString()
        {
            Exception actualException = null;

            try
            {
                const String xmlTypeName = null;
                _ = new FullyQualifiedTypeName(xmlTypeName);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());
        }

        /// <summary>
        /// Tests the constructor with a null string.
        /// </summary>
        [TestCase]
        public void Test_Properties()
        {
            String fullyQualifiedTypeNameString = @"<TaskImplementation assembly=""Foundation.BusinessProcess"" type=""Foundation.BusinessProcess.ScheduledJobProcess"" />";
            FullyQualifiedTypeName fullyQualifiedTypeNameObject = new FullyQualifiedTypeName(fullyQualifiedTypeNameString);

            Assert.That(fullyQualifiedTypeNameObject.AssemblyName, Is.EqualTo("Foundation.BusinessProcess"));
            Assert.That(fullyQualifiedTypeNameObject.TypeName, Is.EqualTo("Foundation.BusinessProcess.ScheduledJobProcess"));
        }
    }
}
