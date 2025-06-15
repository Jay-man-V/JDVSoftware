//-----------------------------------------------------------------------
// <copyright file="RandomServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// The Random Tests
    /// </summary>
    [TestFixture]
    public class RandomServicesTests : UnitTestBase
    {
        private IRandomService _randomService;

        protected override void StartTest()
        {
            base.StartTest();

            _randomService = CoreInstance.Container.Get<IRandomService>();
        }
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_NextInt32()
        {
            Int32 aNumber1 = _randomService.NextInt32();
            Int32 aNumber2 = _randomService.NextInt32();
            Assert.That(aNumber2, Is.Not.EqualTo(aNumber1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_NextInt32_MaxValue()
        {
            Int32 maxValue = 100;
            Int32 aNumber1 = _randomService.NextInt32(maxValue);
            Assert.That(aNumber1 <= maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_NextInt32_Min_MaxValue()
        {
            Int32 minValue = 50;
            Int32 maxValue = 100;
            Int32 aNumber1 = _randomService.NextInt32(minValue, maxValue);

            Assert.That(aNumber1 >= minValue);
            Assert.That(aNumber1 <= maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_SimpleRandomString()
        {
            String aString1 = _randomService.SimpleRandomString(10, _randomService.AlphaNumeric);
            String aString2 = _randomService.SimpleRandomString(10, _randomService.AlphaNumeric);
            Assert.That(aString2, Is.Not.EqualTo(aString1));

            String aString3 = _randomService.SimpleRandomString(1000, _randomService.AlphaNumeric);
            String aString4 = _randomService.SimpleRandomString(1000, _randomService.AlphaNumeric);
            Assert.That(aString4, Is.Not.EqualTo(aString3));

            String aString5 = _randomService.SimpleRandomString(15, _randomService.AllChars);
            String aString6 = _randomService.SimpleRandomString(15, _randomService.AllChars);
            Assert.That(aString6, Is.Not.EqualTo(aString5));

            String aString7 = _randomService.SimpleRandomString(200, _randomService.AlphaUpperCaseOnly);
            String aString8 = _randomService.SimpleRandomString(200, _randomService.AlphaUpperCaseOnly);
            Assert.That(aString8, Is.Not.EqualTo(aString7));

            String aString9 = _randomService.SimpleRandomString(200, _randomService.AlphaLowerCaseOnly);
            String aString10 = _randomService.SimpleRandomString(200, _randomService.AlphaLowerCaseOnly);
            Assert.That(aString10, Is.Not.EqualTo(aString9));

            String aString11 = _randomService.SimpleRandomString(200, _randomService.NonAlphaChars);
            String aString12 = _randomService.SimpleRandomString(200, _randomService.NonAlphaChars);
            Assert.That(aString12, Is.Not.EqualTo(aString11));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_RandomPassword()
        {
            String password1 = _randomService.RandomPassword(10, _randomService.AlphaNumeric);
            String password2 = _randomService.RandomPassword(10, _randomService.AlphaNumeric);
            Assert.That(password2, Is.Not.EqualTo(password1));

            String password3 = _randomService.RandomPassword(1000, _randomService.AlphaNumeric);
            String password4 = _randomService.RandomPassword(1000, _randomService.AlphaNumeric);
            Assert.That(password4, Is.Not.EqualTo(password3));

            String password5 = _randomService.RandomPassword(15, _randomService.AlphaNumeric);
            String password6 = _randomService.RandomPassword(15, _randomService.AlphaNumeric);
            Assert.That(password6, Is.Not.EqualTo(password5));

            String password7 = _randomService.RandomPassword(20, _randomService.AllChars);
            String password8 = _randomService.RandomPassword(20, _randomService.AllChars);
            Assert.That(password7, Is.Not.EqualTo(password8));
        }
    }
}
