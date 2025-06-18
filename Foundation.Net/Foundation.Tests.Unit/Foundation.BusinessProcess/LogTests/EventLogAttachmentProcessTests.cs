//-----------------------------------------------------------------------
// <copyright file="EventLogAttachmentProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.LogTests
{
    /// <summary>
    /// Summary description for EventLogAttachmentProcessTests
    /// </summary>
    [TestFixture]
    public class EventLogAttachmentProcessTests : CommonBusinessProcessTestBaseClass<IEventLogAttachment, IEventLogAttachmentProcess, IEventLogAttachmentRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 7;
        protected override String ExpectedScreenTitle => "Event Log Attachments";
        protected override String ExpectedStatusBarText => "Number of Event Log Attachments:";

        protected override String ExpectedComboBoxDisplayMember => FDC.EventLogAttachment.AttachmentFileName;

        protected override IEventLogAttachmentRepository CreateRepository()
        {
            IEventLogAttachmentRepository dataAccess = Substitute.For<IEventLogAttachmentRepository>();

            return dataAccess;
        }

        protected override IEventLogAttachmentProcess CreateBusinessProcess()
        {
            IEventLogAttachmentProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IEventLogAttachmentProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IEventLogAttachmentProcess process = new EventLogAttachmentProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IEventLogAttachment CreateBlankEntity(IEventLogAttachmentProcess process)
        {
            IEventLogAttachment retVal = CoreInstance.Container.Get<IEventLogAttachment>();

            return retVal;
        }

        protected override IEventLogAttachment CreateEntity(IEventLogAttachmentProcess process)
        {
            IEventLogAttachment retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.EventLogId = new LogId(1);
            retVal.AttachmentFileName = Guid.NewGuid().ToString();
            retVal.Attachment = Guid.NewGuid().ToByteArray();

            return retVal;
        }

        protected override void CheckBlankEntry(IEventLogAttachment entity)
        {
            Assert.That(entity.AttachmentFileName, Is.EqualTo(null));
            Assert.That(entity.Attachment, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IEventLogAttachment entity)
        {
            Assert.That(entity.AttachmentFileName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IEventLogAttachment entity)
        {
            Assert.That(entity.AttachmentFileName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IEventLogAttachment entity1, IEventLogAttachment entity2)
        {
            Assert.That(entity2.EventLogId, Is.EqualTo(entity1.EventLogId));
            Assert.That(entity2.AttachmentFileName, Is.EqualTo(entity1.AttachmentFileName));
            Assert.That(entity2.Attachment, Is.EqualTo(entity1.Attachment));

            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));
        }

        protected override void UpdateEntityProperties(IEventLogAttachment entity)
        {
            entity.EventLogId = new LogId(456);
            entity.AttachmentFileName += "Updated";
            entity.Attachment = Guid.NewGuid().ToByteArray();
        }

        [TestCase]
        public override void Test_Delete_Entity_Id()
        {
            IEventLogAttachmentProcess process = CreateBusinessProcess();

            Repository
                .When(da => da.Delete(Arg.Any<EntityId>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                process.Delete(new EntityId(1));
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }

        [TestCase]
        public override void Test_Delete_Entity_Object()
        {
            IEventLogAttachmentProcess process = CreateBusinessProcess();

            Repository
                .When(da => da.Delete(Arg.Any<IEventLogAttachment>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                IEventLogAttachment entity = CoreInstance.Container.Get<IEventLogAttachment>();
                process.Delete(entity);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }

        [TestCase]
        public override void Test_Delete_MultipleEntities()
        {
            IEventLogAttachmentProcess process = CreateBusinessProcess();

            List<IEventLogAttachment> EventLogAttachments = new List<IEventLogAttachment>
            {
                CoreInstance.Container.Get<IEventLogAttachment>(),
                CoreInstance.Container.Get<IEventLogAttachment>(),
            };

            Repository
                .When(da => da.Delete(Arg.Any<List<IEventLogAttachment>>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                process.Delete(EventLogAttachments);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }
    }
}
