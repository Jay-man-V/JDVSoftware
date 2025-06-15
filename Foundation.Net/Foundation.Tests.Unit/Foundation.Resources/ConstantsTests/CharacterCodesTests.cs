//-----------------------------------------------------------------------
// <copyright file="CharacterCodesTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Resources.ConstantsTests
{
    /// <summary>
    /// Unit Tests for the Character Codes Tests class
    /// </summary>
    [TestFixture]
    public class CharacterCodesTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="CharacterCodes"/>
        /// </summary>
        [TestCase]
        public void Test_AllMembers()
        {
            // This test exists to ensure all the class members are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetStaticPropertyInfosForType(typeof(CharacterCodes));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(CharacterCodes.CarriageReturn)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(CharacterCodes.DoubleQuote)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(CharacterCodes.FieldDelimiter)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(CharacterCodes.NewLine)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(CharacterCodes.SingleQuote)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_Keys()
        {
            PropertyInfo[] propertyInfos = GetStaticPropertyInfosForType(typeof(CharacterCodes));
            Int32 index = 0;

            index++; Assert.That(CharacterCodes.CarriageReturn, Is.EqualTo('\r'));
            index++; Assert.That(CharacterCodes.DoubleQuote, Is.EqualTo('"'));
            index++; Assert.That(CharacterCodes.FieldDelimiter, Is.EqualTo(','));
            index++; Assert.That(CharacterCodes.NewLine, Is.EqualTo('\n'));
            index++; Assert.That(CharacterCodes.SingleQuote, Is.EqualTo('\''));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }
    }
}
