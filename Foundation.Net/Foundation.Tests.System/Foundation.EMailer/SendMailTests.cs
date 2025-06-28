//-----------------------------------------------------------------------
// <copyright file="SendMailTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Emailer;
using Foundation.Interfaces;
using Foundation.Resources;

using Foundation.Tests.Unit.Support;
using Foundation.Tests.System.Support;

namespace Foundation.Tests.System.Foundation.Emailer
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
    public class SendMailTests : SystemTestBase
    {
        [TestCase]
        public void Test_SendTestMail()
        {
            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendTestMail(EmailToAddress);
        }

        [TestCase]
        public void Test_SendMail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendMail(mailMessage);
        }

        [TestCase]
        public void Test_SendMail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            MailMessage mailMessage = CreateMailMessageForTests(functionName);
            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();
            mailMessage.Attachments.AddRange(mailAttachments);

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendMail(mailMessage);
        }
 
        [TestCase]
        //[DeploymentItem(@".ExpectedResults\Foundation.Resources\Images\Logos\JDV Software Logo.png", @".ExpectedResults\Foundation.Resources\Images\Logos\")]
        public void Test_SendFormalEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendFormalEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody);
        }

        [TestCase]
        public void Test_SendFormalEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendFormalEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody, mailAttachments);
        }

        [TestCase]
        public void Test_SendSimpleEmail_NoAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
            emailServices.SendSimpleEmail(EmailToAddress, EmailFromAddress, EmailFromDisplayName, EmailSubject + " " + functionName, EmailBody);
        }

        [TestCase]
        public void Test_SendSimpleEmail_WithAttachment()
        {
            String functionName = LocationUtils.GetFunctionName();

            List<IMailAttachment> mailAttachments = CreateMailMessageAttachments();

            IEmailServices emailServices = Core.Core.Instance.Container.Get<IEmailServices>();
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

            IFileApi fileApi = Core.Core.Instance.Container.Get<IFileApi>();

            foreach (String fileToAttach in filesToAttach)
            {
                FileInfo fileInfo = new FileInfo(fileToAttach);

                IMailAttachment mailAttachment = Core.Core.Instance.Container.Get<IMailAttachment>();

                mailAttachment.Filename = fileInfo.Name;
                mailAttachment.Content = fileApi.GetFileContentsAsByteArray(fileToAttach);

                retVal.Add(mailAttachment);
            }

            return retVal;
        }
    }
}
