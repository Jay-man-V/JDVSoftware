//-----------------------------------------------------------------------
// <copyright file="MailAttachmentTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Foundation.Common;
using NUnit.Framework;

using Foundation.Emailer;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;
using NUnit.Framework.Legacy;

namespace Foundation.Tests.Unit.Foundation.Emailer
{
    /// <summary>
    /// Summary description for MailAttachmentTests
    /// </summary>
    [TestFixture]
    public class MailAttachmentTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor()
        {
            IMailAttachment mailAttachment = CoreInstance.Container.Get<IMailAttachment>();

            Type[] allInterfaces = mailAttachment.GetType().GetInterfaces();

            Assert.That(2, Is.EqualTo(allInterfaces.Length));
            Assert.That(mailAttachment, Is.InstanceOf<IMailAttachment>());
            Assert.That(mailAttachment, Is.InstanceOf<ICloneable>());
        }

        [TestCase]
        public void Test_Clone()
        {
            IMailAttachment mailAttachment = CoreInstance.Container.Get<IMailAttachment>();

            mailAttachment.Filename = "Filename";
            mailAttachment.Content = new Byte[] { 0, 1, 2, 3, 5, 6, 7, 8, 9 };

            IMailAttachment clonedMailAttachment = mailAttachment.Clone() as MailAttachment;

            Assert.That(clonedMailAttachment.Filename, Is.EqualTo(mailAttachment.Filename));

            Assert.That(clonedMailAttachment.Content, Is.Not.SameAs(mailAttachment.Content));
            Assert.That(clonedMailAttachment.Content, Is.EquivalentTo(mailAttachment.Content));
        }
    }
}
