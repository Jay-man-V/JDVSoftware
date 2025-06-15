//-----------------------------------------------------------------------
// <copyright file="ResourceLoaderTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Resources
{
    /// <summary>
    /// Unit Tests for the Resource Loader class
    /// </summary>
    [TestFixture]
    [DeploymentItem(@".ExpectedResults\Foundation.Resources\EMailTemplates\Formal Template.html", @".ExpectedResults\Foundation.Resources\EMailTemplates\")]
    [DeploymentItem(@".ExpectedResults\Foundation.Resources\Images\Logos\JDV Software Logo.png", @".ExpectedResults\Foundation.Resources\Images\Logos\")]
    public class ResourceLoaderTests : UnitTestBase
    {
        [TestCase]
        public void Test_GetResourceFileAsStream()
        {
            String expectedContent;
            using (StreamReader expected = File.OpenText(@".ExpectedResults\Foundation.Resources\EMailTemplates\Formal Template.html"))
            {
                expectedContent = expected.ReadToEnd();
            }

            String actualContent;
            using (Stream actual = ResourceLoader.GetResourceFileAsStream(ResourceNames.EMailTemplates.FormalEmailTemplate))
            {
                using (TextReader tr = new StreamReader(actual))
                {
                    actualContent = tr.ReadToEnd();
                }
            }

            Assert.That(actualContent, Is.EqualTo(expectedContent));
        }

        [TestCase]
        public void Test_GetResourceFileAsText()
        {
            String expected = File.ReadAllText(@".ExpectedResults\Foundation.Resources\EMailTemplates\Formal Template.html");
            String actual = ResourceLoader.GetResourceFileAsText(ResourceNames.EMailTemplates.FormalEmailTemplate);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetResourceImage()
        {
            Image expected = Image.FromFile(@".ExpectedResults\Foundation.Resources\Images\Logos\JDV Software Logo.png");
            Image actual = ResourceLoader.GetResourceImage(ResourceNames.Logos.CompanyLogo);

            Assert.That(actual.ToByteArray(), Is.EquivalentTo(expected.ToByteArray()));
        }

        [TestCase]
        public void Test_GetResource_Exception()
        {
            String resourceName = "Made up resource name that doesn't exist";

            Assembly targetAssembly = Assembly.GetAssembly(typeof(ResourceLoader));
            String errorMessage = $@"Resource File '{resourceName}' does not exist in the Assembly '{targetAssembly.FullName}'";
            MissingManifestResourceException actualException = null;

            try
            {
                ResourceLoader.GetResourceFileAsStream(resourceName);
            }
            catch (MissingManifestResourceException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            String actualExceptionMessage = actualException.Message;

            Assert.That(actualExceptionMessage, Is.EqualTo(errorMessage));
        }
    }
}
