//-----------------------------------------------------------------------
// <copyright file="ContactTypeProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ContactTypeProcessTests
    /// </summary>
    [TestFixture]
    public class ContactTypeProcessTests : CommonBusinessProcessTestBaseClass<IContactType, IContactTypeProcess, IContactTypeRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Contact Types";
        protected override String ExpectedStatusBarText => "Number of Contact Types:";

        protected override string ExpectedComboBoxDisplayMember => FDC.ContactType.Name;

        protected override IContactTypeRepository CreateRepository()
        {
            IContactTypeRepository dataAccess = Substitute.For<IContactTypeRepository>();

            return dataAccess;
        }

        protected override IContactTypeProcess CreateBusinessProcess()
        {
            IContactTypeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IContactTypeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IContactTypeProcess process = new ContactTypeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IContactType CreateBlankEntity(IContactTypeProcess process)
        {
            IContactType retVal = CoreInstance.Container.Get<IContactType>();

            return retVal;
        }

        protected override IContactType CreateEntity(IContactTypeProcess process)
        {
            IContactType retVal = CreateBlankEntity(process);

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IContactType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IContactType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IContactType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IContactType entity1, IContactType entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IContactType entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
