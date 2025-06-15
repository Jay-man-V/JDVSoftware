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

namespace Foundation.Tests.System.Foundation.Services.WebApi
{
    /// <summary>
    /// Summary description for HttpWebApiTests
    /// https://webbrowsertools.com/test-download-with/
    /// </summary>
    [TestFixture]
    public class HttpWebApiTests : UnitTestBase
    {
        [TestCase("https://cdn.jsdelivr.net/gh/belaviyo/download-with/samples/sample.html", @".ExpectedResults\SampleDocuments\sample.html")]
        [DeploymentItem(@".ExpectedResults\SampleDocuments\sample.html", @".ExpectedResults\SampleDocuments\")]
        public void Test_DownloadString(String fileToDownload, String fileToCompareWith)
        {
            IFileApi fileApi = Core.Core.Instance.Container.Get<IFileApi>();
            String response = fileApi.GetFileContentsAsText(fileToCompareWith, Encoding.ASCII);

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = fileToDownload,
            };

            IHttpApi httpWebApi = Core.Core.Instance.Container.Get<IHttpApi>();
            String actualResponse = httpWebApi.DownloadString(fileTransferSettings);

            Assert.That(actualResponse, Is.EqualTo(response));
        }

        [TestCase("https://cdn.jsdelivr.net/gh/belaviyo/download-with/samples/sample.html", @".ExpectedResults\SampleDocuments\sample.html")]
        [TestCase("https://cdn.jsdelivr.net/gh/belaviyo/download-with/samples/sample.pdf", @".ExpectedResults\SampleDocuments\sample.pdf")]
        [DeploymentItem(@".ExpectedResults\SampleDocuments\sample.html", @".ExpectedResults\SampleDocuments\")]
        [DeploymentItem(@".ExpectedResults\SampleDocuments\sample.pdf", @".ExpectedResults\SampleDocuments\")]
        public void Test_DownloadFile(String fileToDownload, String fileToCompareWith)
        {
            IFileApi fileApi = Core.Core.Instance.Container.Get<IFileApi>();
            Stream response = fileApi.GetFileContentsAsStream(fileToCompareWith);
            response.Position = 0;

            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = fileToDownload,
            };

            IHttpApi httpWebApi = Core.Core.Instance.Container.Get<IHttpApi>();
            Stream actualResponse = httpWebApi.DownloadFile(fileTransferSettings);
            actualResponse.Position = 0;

            Assert.That(actualResponse, Is.EqualTo(response));
        }

        [TestCase("https://cdn.jsdelivr.net/gh/belaviyo/download-with/samples/sample.html", @".Support\SampleDocuments\WebResponse.json")]
        [DeploymentItem(@".Support\SampleDocuments\WebResponse.json", @".Support\SampleDocuments\")]
        public void Test_DownloadString_JsonData(String fileToDownload, String fileToCompareWith)
        {
            Encoding encoding = Encoding.UTF8;
            IFileApi fileApi = Core.Core.Instance.Container.Get<IFileApi>();
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
        public void Test_DownloadStringAsync_JsonData()
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
    }
}
