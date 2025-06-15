//-----------------------------------------------------------------------
// <copyright file="EncryptionServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Text;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// The Encryption Tests
    /// </summary>
    [TestFixture]
    public class EncryptionServiceTests : UnitTestBase
    {
        private const string Password = "tHiSiSmYpAsSwOrD!£$%^";
        private static Byte[] Salt => Encoding.UTF8.GetBytes(Password);

        private const string SourceValueString = "AbCdEfGhIjKlMnOpQrStUvWxYz1234567980!£$%^&*()_+";
        private Byte[] SourceValueBytes => Encoding.UTF8.GetBytes(SourceValueString);

        private static IEncryptionService _encryptionService = null;

        protected override void StartTest()
        {
            base.StartTest();

            _encryptionService = CoreInstance.Container.Get<IEncryptionService>();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateSalt_RepeatedCalls_Default()
        {
            byte[] salt1 = _encryptionService.GenerateSalt();
            byte[] salt2 = _encryptionService.GenerateSalt();

            Assert.That(salt2, Is.Not.EquivalentTo(salt1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateSalt_RepeatedCalls_SmallSize()
        {
            byte[] salt1 = _encryptionService.GenerateSalt(8);
            byte[] salt2 = _encryptionService.GenerateSalt(8);

            Assert.That(salt2, Is.Not.EquivalentTo(salt1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateKeys_File_Default()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(".", keyName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GenerateKeys_File_Password()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(Password, Salt, ".", keyName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_String_Default()
        {
            _encryptionService.GenerateKeys(out Byte[] key, out Byte[] iv);

            string encrypted = _encryptionService.EncryptData(key, iv, SourceValueString);

            string decrypted = _encryptionService.DecryptData(key, iv, encrypted);

            Assert.That(decrypted, Is.EqualTo(SourceValueString));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_String_File()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(".", keyName);

            string encrypted = _encryptionService.EncryptData(keyName, SourceValueString);

            string decrypted = _encryptionService.DecryptData(keyName, encrypted);

            Assert.That(decrypted, Is.EqualTo(SourceValueString));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_String_Password()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(Password, Salt, ".", keyName);

            string encrypted = _encryptionService.EncryptData(keyName, SourceValueString);

            string decrypted = _encryptionService.DecryptData(keyName, encrypted);

            Assert.That(decrypted, Is.EqualTo(SourceValueString));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_Byte_Default()
        {
            _encryptionService.GenerateKeys(out Byte[] key, out Byte[] iv);

            byte[] encrypted = _encryptionService.EncryptData(key, iv, SourceValueBytes);

            byte[] decrypted = _encryptionService.DecryptData(key, iv, encrypted);

            Assert.That(encrypted, Is.Not.EquivalentTo(SourceValueBytes));
            Assert.That(decrypted, Is.Not.EquivalentTo(encrypted));
            Assert.That(decrypted, Is.EquivalentTo(SourceValueBytes));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_File()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(".", keyName);

            byte[] encrypted = _encryptionService.EncryptData(keyName, SourceValueBytes);

            byte[] decrypted = _encryptionService.DecryptData(keyName, encrypted);

            Assert.That(encrypted, Is.Not.EquivalentTo(SourceValueBytes));
            Assert.That(decrypted, Is.Not.EquivalentTo(encrypted));
            Assert.That(decrypted, Is.EquivalentTo(SourceValueBytes));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EncryptionDecryption_Password()
        {
            string functionName = LocationUtils.GetFunctionName();
            string className = LocationUtils.GetClassName();
            string keyName = $"{className}.{functionName}";

            _encryptionService.GenerateKeys(Password, Salt, ".", keyName);

            byte[] encrypted = _encryptionService.EncryptData(keyName, SourceValueBytes);

            byte[] decrypted = _encryptionService.DecryptData(keyName, encrypted);

            Assert.That(encrypted, Is.Not.EquivalentTo(SourceValueBytes));
            Assert.That(decrypted, Is.Not.EquivalentTo(encrypted));
            Assert.That(decrypted, Is.EquivalentTo(SourceValueBytes));
        }
    }
}
