//-----------------------------------------------------------------------
// <copyright file="FullyQualifiedTypeNameOperatorEqualsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.AssemblyHelpersTests.FullyQualifiedTypeNameTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class FullyQualifiedTypeNameOperatorEqualsTests : UnitTestBase
    {
        private String FullyQualifiedTypeNameString => @"<TaskImplementation assembly=""Foundation.BusinessProcess"" type=""Foundation.BusinessProcess.ScheduledJobProcess"" />";

        /// <summary>
        /// Tests the implicit cast from string.
        /// </summary>
        [TestCase]
        public void Test_ImplicitCastFromString()
        {
            FullyQualifiedTypeName fullyQualifiedTypeName = FullyQualifiedTypeNameString;

            Assert.That(fullyQualifiedTypeName, Is.Not.EqualTo(null));
            String fullyQualifiedTypeNameString = fullyQualifiedTypeName;
            Assert.That(fullyQualifiedTypeNameString, Is.EqualTo(FullyQualifiedTypeNameString), $"Fully Qualified Type Name '{FullyQualifiedTypeNameString}' did not work");
        }

        /// <summary>
        /// Tests the implicit cast from null string.
        /// </summary>
        [TestCase]
        public void Test_ImplicitCastFromNullString()
        {
            //const String nullEmailAddressString = null;
            //EmailAddress emailAddress = nullEmailAddressString;

            //Assert.That(emailAddress, Is.Not.EqualTo(null));
            //String emailAddressString = emailAddress;
            //Assert.That(emailAddressString, Is.EqualTo(null), "Email Address from null String did not work");
        }
    }
}
