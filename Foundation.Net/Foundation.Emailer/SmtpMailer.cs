//-----------------------------------------------------------------------
// <copyright file="SmtpEmailer.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;

using Mail = System.Net.Mail;

namespace Foundation.Emailer
{
    /// <summary>
    /// The SMTP Emailer class
    /// </summary>
    [DependencyInjectionTransient]
    public class SmtpEmailer : IEmailServices
    {
        /// <inheritdoc cref="IEmailServices.SendTestMail(String)"/>
        public void SendTestMail(String toAddress)
        {
            LoggingHelpers.TraceCallEnter(toAddress);

            String mailFrom = ApplicationSettings.SmtpConfiguration.FromAddress;
            String mailSubject = "Test email";
            String mailBody = "Test email";

            String textAttachmentBody = "Sample text file";
            Byte[] fileAttachment = Encoding.ASCII.GetBytes(textAttachmentBody);

            using (Mail.SmtpClient client = new Mail.SmtpClient())
            {
                String username = ApplicationSettings.SmtpConfiguration.Username;
                String password = ApplicationSettings.SmtpConfiguration.Password;

                client.Port = ApplicationSettings.SmtpConfiguration.Port;
                client.Host = ApplicationSettings.SmtpConfiguration.Server;
                client.EnableSsl = ApplicationSettings.SmtpConfiguration.EnableSsl;
                client.Credentials = new NetworkCredential(username, password);

                using (Mail.MailMessage mailMessage = new Mail.MailMessage())
                {
                    mailMessage.From = new Mail.MailAddress(mailFrom);
                    toAddress.Split(';').ToList().ForEach(s => mailMessage.To.Add(s));
                    mailMessage.Subject = mailSubject;
                    mailMessage.Body = mailBody;

                    MemoryStream ms = new MemoryStream(fileAttachment);

                    mailMessage.Attachments.Add(new Mail.Attachment(ms, "Sample File.txt"));

                    client.Send(mailMessage);
                }
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IEmailServices.SendSimpleEmail(String, String, String, String, String, List{IMailAttachment})"/>
        public void SendSimpleEmail(String toAddress, String fromAddress, String fromAddressDisplayName, String subject, String body, List<IMailAttachment> mailAttachments = null)
        {
            LoggingHelpers.TraceCallEnter(toAddress, fromAddress, subject, body, mailAttachments);

            MailMessage mailMessage = new MailMessage
            {
                FromAddress = fromAddress,
                FromAddressDisplayName = fromAddressDisplayName,
                Subject = subject,
                Body = body,
            };
            mailMessage.ToAddress.AddRange(toAddress.Split(';'));

            if (mailAttachments.HasItems())
            {
                mailAttachments.ForEach(ma => mailMessage.Attachments.Add(ma));
            }

            SendMail(mailMessage);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IEmailServices.SendFormalEmail(String, String, String, String, String, List{IMailAttachment})"/>
        public void SendFormalEmail(String toAddress, String fromAddress, String fromAddressDisplayName, String subject, String body, List<IMailAttachment> mailAttachments = null)
        {
            LoggingHelpers.TraceCallEnter(toAddress, fromAddress, subject, body, mailAttachments);

            String mailTemplateHtml = ResourceLoader.GetResourceFileAsText(ResourceNames.EMailTemplates.FormalEmailTemplate);
            String newBody = mailTemplateHtml;
            newBody = newBody.Replace("$$SUBJECT$$", subject);
            newBody = newBody.Replace("$$BODY$$", body);

            IMailMessage mailMessage = new MailMessage
            {
                FromAddress = fromAddress,
                FromAddressDisplayName = fromAddressDisplayName,
                Subject = subject,
                Body = newBody,
                IsBodyHtml = true,
            };
            mailMessage.ToAddress.AddRange(toAddress.Split(';'));

            if (mailAttachments.HasItems())
            {
                mailAttachments.ForEach(ma => mailMessage.Attachments.Add(ma));
            }

            SendMail(mailMessage);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IEmailServices.SendMail(IMailMessage)"/>
        public void SendMail(IMailMessage mailMessage)
        {
            LoggingHelpers.TraceCallEnter(mailMessage);

            using (Mail.SmtpClient client = new Mail.SmtpClient())
            {
                String username = ApplicationSettings.SmtpConfiguration.Username;
                String password = ApplicationSettings.SmtpConfiguration.Password;

                client.Port = ApplicationSettings.SmtpConfiguration.Port;
                client.Host = ApplicationSettings.SmtpConfiguration.Server;
                client.EnableSsl = ApplicationSettings.SmtpConfiguration.EnableSsl;
                client.Credentials = new NetworkCredential(username, password);

                using (Mail.MailMessage message = CreateSmtpMailMessage(mailMessage))
                {
                    client.Send(message);
                }
            }

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Creates the mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        /// <returns>
        /// The Mail Message
        /// </returns>
        private Mail.MailMessage CreateSmtpMailMessage(IMailMessage mailMessage)
        {
            LoggingHelpers.TraceCallEnter(mailMessage);

            Mail.MailMessage retVal = new Mail.MailMessage();
            Mail.MailAddress from = new Mail.MailAddress(mailMessage.FromAddress, mailMessage.FromAddressDisplayName);

            foreach (String toMailAddress in mailMessage.ToAddress)
            {
                Mail.MailAddress to = new Mail.MailAddress(toMailAddress);
                retVal.To.Add(to);
            }

            foreach (IMailAttachment attachment in mailMessage.Attachments)
            {
                MemoryStream ms = new MemoryStream(attachment.Content);

                Mail.Attachment item = new Mail.Attachment(ms, attachment.Filename);
                retVal.Attachments.Add(item);
            }

            retVal.From = from;

            retVal.IsBodyHtml = mailMessage.IsBodyHtml;
            retVal.Body = mailMessage.Body;
            retVal.Subject = mailMessage.Subject;

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
