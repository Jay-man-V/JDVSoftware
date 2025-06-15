//-----------------------------------------------------------------------
// <copyright file="HttpWebApiTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Services.WebApi;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.WebApi
{
    /// <summary>
    /// Summary description for HttpWebApiTests
    /// </summary>
    [TestFixture]
    public class HttpWebApiTests : UnitTestBase
    {
        [TestCase]
        public void Test_DownloadString()
        {
            String inputValue = LocationUtils.GetFunctionName();
            Byte[] byteArray = Encoding.UTF8.GetBytes(inputValue);
            Stream response = new MemoryStream(byteArray);

            MockHttpMessageHandler mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = "https://tempuri.org",
            };

            HttpApi httpWebApi = new HttpApi
            {
                HttpMessageHandler = mockHttpMessageHandler
            };

            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(response),
            };
            mockHttpMessageHandler.Send(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(mockResponse);

            String actualResponse = httpWebApi.DownloadString(fileTransferSettings);

            Assert.That(actualResponse, Is.EqualTo(inputValue));
        }

        [TestCase( @".ExpectedResults\SampleDocuments\sample.html")]
        [TestCase(@".ExpectedResults\SampleDocuments\sample.pdf")]
        [DeploymentItem(@".ExpectedResults\SampleDocuments\sample.html", @".ExpectedResults\SampleDocuments\")]
        [DeploymentItem(@".ExpectedResults\SampleDocuments\sample.pdf", @".ExpectedResults\SampleDocuments\")]
        public void Test_DownloadFile(String fileToCompareWith)
        {
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();

            Stream response = fileApi.GetFileContentsAsStream(fileToCompareWith);
            MockHttpMessageHandler mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = "https://tempuri.org",
            };

            HttpApi httpWebApi = new HttpApi
            {
                HttpMessageHandler = mockHttpMessageHandler
            };

            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(fileApi.GetFileContentsAsStream(fileToCompareWith)),
            };
            mockHttpMessageHandler.Send(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(mockResponse);

            Stream actualResponse = httpWebApi.DownloadFile(fileTransferSettings);

            Assert.That(actualResponse, Is.EqualTo(response));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\WebResponse.json", @".Support\SampleDocuments\")]
        public void Test_DownloadString_JsonData()
        {
            Encoding encoding = Encoding.UTF8;
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String response = fileApi.GetFileContentsAsText(@".Support\SampleDocuments\WebResponse.json", encoding);
            MockHttpMessageHandler mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = "https://tempuri.org",
            };

            HttpApi httpWebApi = new HttpApi
            {
                HttpMessageHandler = mockHttpMessageHandler
            };

            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response),
            };
            mockHttpMessageHandler.Send(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(mockResponse);

            String actualResponse = httpWebApi.DownloadString(fileTransferSettings);

            Assert.That(actualResponse, Is.EqualTo(response));
        }

        [TestCase]
        public void Test_DownloadStringAsync()
        {
            String response = LocationUtils.GetFunctionName();
            MockHttpMessageHandler mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = "https://tempuri.org",
            };

            HttpApi httpWebApi = new HttpApi
            {
                HttpMessageHandler = mockHttpMessageHandler
            };

            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response),
            };
            mockHttpMessageHandler.Send(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(mockResponse);

            Task<String> t = httpWebApi.DownloadStringAsync(fileTransferSettings);
            t.Wait();
            String actualResponse = t.Result;

            Assert.That(actualResponse, Is.EqualTo(response));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\WebResponse.json", @".Support\SampleDocuments\")]
        public void Test_DownloadStringAsync_JsonData()
        {
            Encoding encoding = Encoding.UTF8;
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String response = fileApi.GetFileContentsAsText(@".Support\SampleDocuments\WebResponse.json", encoding);
            MockHttpMessageHandler mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = "https://tempuri.org",
            };

            HttpApi httpWebApi = new HttpApi
            {
                HttpMessageHandler = mockHttpMessageHandler
            };

            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response),
            };
            mockHttpMessageHandler.Send(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(mockResponse);

            Task<String> t = httpWebApi.DownloadStringAsync(fileTransferSettings);
            t.Wait();
            String actualResponse = t.Result;

            Assert.That(actualResponse, Is.EqualTo(response));
        }
    }
}
