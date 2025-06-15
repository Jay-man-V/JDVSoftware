//-----------------------------------------------------------------------
// <copyright file="HashingUtilsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Text;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// The Hashing Tests
    /// </summary>
    [TestFixture]
    public class HashingServiceTests : UnitTestBase
    {
        private const String SourceValueString = "AbCdEfGhIjKlMnOpQrStUvWxYz1234567980!£$%^&*()_+";

        private readonly Byte[] _sourceValueBytes = Encoding.UTF8.GetBytes(SourceValueString);
        private IHashingService _hashingService;

        protected override void StartTest()
        {
            base.StartTest();

            _hashingService = CoreInstance.Container.Get<IHashingService>();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateSalt_RepeatedCalls_Default()
        {
            Byte[] salt1 = _hashingService.GenerateSalt();
            Byte[] salt2 = _hashingService.GenerateSalt();

            Debug.WriteLine("salt1 -> " + ByteArrayToStringRepresentationForCode(salt1));
            Debug.WriteLine("salt2 -> " + ByteArrayToStringRepresentationForCode(salt2));

            Assert.That(salt2, Is.Not.EquivalentTo(salt1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateSalt_RepeatedCalls_SmallSize()
        {
            Byte[] salt1 = _hashingService.GenerateSalt(8);
            Byte[] salt2 = _hashingService.GenerateSalt(8);

            Assert.That(salt2, Is.Not.EquivalentTo(salt1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateHash_String()
        {
            String expectedValue = "cb5fafcad365ddf38cc7c67eade2b28af878eb2c44670b4f06da1a4426d5539cf98b2fe387904efa6f4fe970fa5e1749";
            String actualValue = _hashingService.GenerateHash(SourceValueString, _sourceValueBytes);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateHash_Bytes()
        {
            Byte[] expectedValue = { 203, 95, 175, 202, 211, 101, 221, 243, 140, 199, 198, 126, 173, 226, 178, 138, 248, 120, 235, 44, 68, 103, 11, 79, 6, 218, 26, 68, 38, 213, 83, 156, 249, 139, 47, 227, 135, 144, 78, 250, 111, 79, 233, 112, 250, 94, 23, 73 };
            Byte[] actualValue = _hashingService.GenerateHash(_sourceValueBytes, _sourceValueBytes);

            Assert.That(actualValue, Is.EquivalentTo(expectedValue));
        }
    }
}
