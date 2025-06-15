//-----------------------------------------------------------------------
// <copyright file="SerialisationHelpers.CustomTypes.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.HelpersTests
{
    /// <summary>
    /// The Serialisation Helpers Tests class
    /// </summary>
    [TestFixture]
    public class SerialisationHelpersCustomTypes : UnitTestBase
    {
        private SerialiseTest CreateObjectForTesting()
        {
            SerialiseTest retVal = new SerialiseTest
            {
                StringList = new List<String> { "One", "Two", "Three", "Four" },
                Int32List = new List<Int32> { 1, 2, 3, 4 }
            };

            return retVal;
        }

        [DeploymentItem(@"\.ExpectedResults\Foundation.Common\HelpersTests\Test_Serialise_CustomType.txt")]
        [TestCase]
        public void Test_Serialise_CustomType()
        {
            SerialiseTest value = CreateObjectForTesting();

            String actual = SerialisationHelpers.Serialise(value);

            String sourceFile = @".ExpectedResults\Foundation.Common\HelpersTests\Test_Serialise_CustomType.txt";
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();

            String expected = fileApi.GetFileContentsAsText(sourceFile, Encoding.Default);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [DeploymentItem(@"\.ExpectedResults\Foundation.Common\HelpersTests\Test_Serialise_CustomType.txt")]
        [TestCase]
        public void Test_Deserialise_CustomType()
        {
            SerialiseTest expected = CreateObjectForTesting();

            String sourceFile = @".ExpectedResults\Foundation.Common\HelpersTests\Test_Serialise_CustomType.txt";
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String fileContent = fileApi.GetFileContentsAsText(sourceFile, Encoding.Default);

            SerialiseTest actual = SerialisationHelpers.Deserialise<SerialiseTest>(fileContent);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
