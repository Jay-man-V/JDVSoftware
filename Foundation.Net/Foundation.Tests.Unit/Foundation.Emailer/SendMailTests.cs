//-----------------------------------------------------------------------
// <copyright file="SendMailTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Emailer;
using Foundation.Interfaces;

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
        protected override void StartTest()
        {
            base.StartTest();

            String smtpMailPath = Path.Combine(BaseTemporaryOutputsPath, "SmtpMail");
            DirectoryInfo smtpMailPathDirectoryInfo = new DirectoryInfo(smtpMailPath);
            if (!smtpMailPathDirectoryInfo.Exists) { smtpMailPathDirectoryInfo.Create(); }

            List<FileInfo> allFiles = smtpMailPathDirectoryInfo.GetFiles().ToList();
            allFiles.ForEach(f => f.Delete());
        }

        private IEmailServices CreateBusinessProcess()
        {
            IEmailServices retVal = new SmtpMailer(CoreInstance, ApplicationConfigurationProcess);

            return retVal;
        }

        [TestCase]
        public void Test_SendTestMail()
        {
            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendTestMail(EmailToAddress);
        }

        [TestCase]
        public void Test_SendMail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendMail(mailMessage);
        }

        [TestCase]
        public void Test_SendMail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);
            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();
            mailMessage.Attachments.AddRange(mailAttachments);

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendMail(mailMessage);
        }
 
        [TestCase]
        public void Test_SendFormalEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendFormalEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody);
        }

        [TestCase]
        public void Test_SendFormalEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendFormalEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody, mailAttachments);
        }

        [TestCase]
        public void Test_SendSimpleEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendSimpleEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody);
        }

        [TestCase]
        public void Test_SendSimpleEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = CreateBusinessProcess();
            emailServices.SendSimpleEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody, mailAttachments);
        }

        private MailMessage CreateMailMessageForTests(String functionName)
        {
            MailMessage mailMessage = new MailMessage
            {
                FromAddress = EmailFromAddress,
                FromAddressDisplayName = EmailFromDisplayName,
                Subject = EmailSubject + " " + functionName,
                Body = EmailBody,
            };
            mailMessage.ToAddress.Add(EmailToAddress);

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
