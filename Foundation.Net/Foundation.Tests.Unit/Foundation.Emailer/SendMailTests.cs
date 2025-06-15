//-----------------------------------------------------------------------
// <copyright file="SendMailTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Emailer;
using Foundation.Interfaces;
using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Emailer
{
    /// <summary>
    /// Summary description for SendMailTests
    /// </summary>
    [TestFixture]
    [DeploymentItem(@".Support\SampleDocuments\Sample Image.jpg", @".Support\SampleDocuments\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample Excel Document.xlsx", @".Support\SampleDocuments\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample Word Document.docx", @".Support\SampleDocuments\")]
    public class SendMailTests : UnitTestBase
    {
        private readonly String _toAddress = "Jayesh.Varsani@datalore.me.uk";
        private readonly String _fromAddress = ApplicationSettings.SmtpConfiguration.FromAddress;
        private readonly String _fromAddressDisplayName = "Automated Unit Test Program";
        private readonly String _subject = $"Test Email Subject - {DateTime.Now.ToString(Formats.DotNet.DateTimeSeconds)}";
        private readonly String _body = $"Test Email Body - {DateTime.Now.ToString(Formats.DotNet.DateTimeSeconds)}";

        protected override void StartTest()
        {
            base.StartTest();

            String smtpMailPath = Path.Combine(BaseTemporaryOutputsPath, "SmtpMail");
            DirectoryInfo smtpMailPathDirectoryInfo = new DirectoryInfo(smtpMailPath);
            if (!smtpMailPathDirectoryInfo.Exists) { smtpMailPathDirectoryInfo.Create(); }

            List<FileInfo> allFiles = smtpMailPathDirectoryInfo.GetFiles().ToList();
            allFiles.ForEach(f => f.Delete());
        }

        [TestCase]
        public void Test_SendTestMail()
        {
            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendTestMail(_toAddress);
        }

        [TestCase]
        public void Test_SendMail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendMail(mailMessage);
        }

        [TestCase]
        public void Test_SendMail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);
            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();
            mailMessage.Attachments.AddRange(mailAttachments);

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendMail(mailMessage);
        }
 
        [TestCase]
        //[DeploymentItem(@".ExpectedResults\Foundation.Resources\Images\Logos\JDV Software Logo.png", @".ExpectedResults\Foundation.Resources\Images\Logos\")]
        public void Test_SendFormalEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendFormalEmail(_toAddress, _fromAddress, _fromAddressDisplayName, _subject + " " + functionName, _body);
        }

        [TestCase]
        public void Test_SendFormalEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendFormalEmail(_toAddress, _fromAddress, _fromAddressDisplayName, _subject + " " + functionName, _body, mailAttachments);
        }

        [TestCase]
        public void Test_SendSimpleEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendSimpleEmail(_toAddress, _fromAddress, _fromAddressDisplayName, _subject + " " + functionName, _body);
        }

        [TestCase]
        public void Test_SendSimpleEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = CoreInstance.Container.Get<IEmailServices>();
            emailServices.SendSimpleEmail(_toAddress, _fromAddress, _fromAddressDisplayName, _subject + " " + functionName, _body, mailAttachments);
        }

        private MailMessage CreateMailMessageForTests(String functionName)
        {
            MailMessage mailMessage = new MailMessage
            {
                FromAddress = _fromAddress,
                FromAddressDisplayName = _fromAddressDisplayName,
                Subject = _subject + " " + functionName,
                Body = _body,
            };
            mailMessage.ToAddress.Add(_toAddress);

            return mailMessage;
        }

        private List<IMailAttachment> CreateMailMessageAttachments()
        {
            List<IMailAttachment> retVal = new List<IMailAttachment>();

            String[] filesToAttach =
            {
                @".Support\SampleDocuments\Sample Image.jpg",
                @".Support\SampleDocuments\Sample Excel Document.xlsx",
                @".Support\SampleDocuments\Sample PDF Document.pdf",
                @".Support\SampleDocuments\Sample Text Document.txt",
                @".Support\SampleDocuments\Sample Word Document.docx",
            };

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();

            foreach (String fileToAttach in filesToAttach)
            {
                FileInfo fileInfo = new FileInfo(fileToAttach);

                IMailAttachment mailAttachment = CoreInstance.Container.Get<IMailAttachment>();

                mailAttachment.Filename = fileInfo.Name;
                mailAttachment.Content = fileApi.GetFileContentsAsByteArray(fileToAttach);

                retVal.Add(mailAttachment);
            }

            return retVal;
        }
    }
}
