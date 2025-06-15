//-----------------------------------------------------------------------
// <copyright file="ContactDetailProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ContactDetailProcessTests
    /// </summary>
    [TestFixture]
    public class ContactDetailProcessTests : CommonBusinessProcessTestBaseClass<IContactDetail, IContactDetailProcess, IContactDetailRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 24;
        protected override String ExpectedScreenTitle => "Contacts";
        protected override String ExpectedStatusBarText => "Number of Contacts:";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Contact Type:";
        protected override string ExpectedFilter1DisplayMemberPath => FDC.ContactType.Name;

        protected override Boolean ExpectedHasOptionalDropDownParameter2 => true;
        protected override String ExpectedFilter2Name => "Parent Contact:";
        protected override string ExpectedFilter2DisplayMemberPath => FDC.ContactDetail.DisplayName;

        protected override String ExpectedComboBoxDisplayMember => FDC.ContactDetail.DisplayName;

        protected override IContactDetailRepository CreateDataAccess()
        {
            IContactDetailRepository dataAccess = Substitute.For<IContactDetailRepository>();

            return dataAccess;
        }

        protected override IContactDetailProcess CreateBusinessProcess()
        {
            IContactDetailProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IContactDetailProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IContractProcess contractProcess = Substitute.For<IContractProcess>();
            IContactTypeProcess contactTypeProcess = Substitute.For<IContactTypeProcess>();
            INationalRegionProcess nationalRegionProcess = Substitute.For<INationalRegionProcess>();
            ICountryProcess countryProcess = Substitute.For<ICountryProcess>();

            CopyProperties(contactTypeProcess, CoreInstance.Container.Get<IContactTypeProcess>());

            IContactDetailProcess process = new ContactDetailProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, contractProcess, contactTypeProcess, nationalRegionProcess, countryProcess);

            return process;
        }

        protected override IContactDetail CreateBlankEntity(IContactDetailProcess process)
        {
            IContactDetail retVal = CoreInstance.Container.Get<IContactDetail>();

            return retVal;
        }

        protected override IContactDetail CreateEntity(IContactDetailProcess process)
        {
            IContactDetail retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ParentContactId = new EntityId(10);
            retVal.ContractId = new EntityId(2);
            retVal.ContactTypeId = new EntityId(3);
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.DisplayName = Guid.NewGuid().ToString();
            retVal.LegalName = Guid.NewGuid().ToString();
            retVal.BuildingName = Guid.NewGuid().ToString();
            retVal.Street1 = Guid.NewGuid().ToString();
            retVal.Street2 = Guid.NewGuid().ToString();
            retVal.Town = Guid.NewGuid().ToString();
            retVal.County = Guid.NewGuid().ToString();
            retVal.PostCode = Guid.NewGuid().ToString();
            retVal.NationalRegionId = new EntityId(4);
            retVal.CountryId = new EntityId(5);
            retVal.Telephone1 = Guid.NewGuid().ToString();
            retVal.Telephone2 = Guid.NewGuid().ToString();
            retVal.EmailAddress = "a@b.co";

            return retVal;
        }

        protected override void CheckBlankEntry(IContactDetail entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IContactDetail entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IContactDetail entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IContactDetail entity1, IContactDetail entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.DisplayName, Is.EqualTo(entity1.DisplayName));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
        }

        protected override void UpdateEntityProperties(IContactDetail entity)
        {
            entity.DisplayName += "Updated";
            entity.ShortName += "Updated";
        }

        [TestCase]
        public void Test_ValidateEntity()
        {
            IContactDetailProcess process = CreateBusinessProcess();
            IContactDetail contactDetail = CreateEntity(process);

            contactDetail.EmailAddress = new String('A', 150) + '@' + new String('B', 250);

            Exception actualException = null;

            try
            {
                const Boolean validateAllProperties = true;
                process.ValidateEntity(contactDetail, validateAllProperties);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.Null);
            Assert.That(actualException, Is.TypeOf<AggregateException>());

            AggregateException aggregateException = actualException as AggregateException;
            Assert.That(aggregateException.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(aggregateException.InnerExceptions[0], Is.TypeOf<ValidationException>());

            ValidationException validationException = aggregateException.InnerExceptions[0] as ValidationException;
            Assert.That(validationException.Message, Is.EqualTo("The field EmailAddress must be a string or array type with a maximum length of '320'."));
        }

        [TestCase]
        public void Test_MakeListOfParentContacts()
        {
            IContactDetailProcess process = CreateBusinessProcess();
            List<IContactDetail> contacts = new List<IContactDetail>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            contacts[0].ParentContactId = new EntityId(0);
            contacts[1].ParentContactId = new EntityId(0);
            contacts[2].ParentContactId = new EntityId(0);
            contacts[3].ParentContactId = new EntityId(0);
            contacts[4].ParentContactId = new EntityId(0);

            List<IContactDetail> parentContactDetails = process.MakeListOfParentContacts(contacts);
            Assert.That(parentContactDetails.Count, Is.EqualTo(0));

            Int32 counter = 1;
            foreach (IContactDetail contact in contacts)
            {
                contact.Id = new EntityId(counter);

                counter++;
                contact.ParentContactId = new EntityId(counter);
            }

            parentContactDetails = process.MakeListOfParentContacts(contacts);
            Assert.That(parentContactDetails.Count, Is.EqualTo(4));

            contacts[1].ParentContactId = new EntityId(1);
            contacts[2].ParentContactId = new EntityId(2);
            contacts[3].ParentContactId = new EntityId(3);
            contacts[4].ParentContactId = new EntityId(1);

            parentContactDetails = process.MakeListOfParentContacts(contacts);
            Assert.That(parentContactDetails.Count, Is.EqualTo(3));
        }

        [TestCase]
        public void Test_ApplyFilter_ContactType()
        {
            IContactDetailProcess process = CreateBusinessProcess();

            IContactType contactType1 = CoreInstance.Container.Get<IContactType>();
            contactType1.Id = new EntityId(1);

            IContactType contactType2 = CoreInstance.Container.Get<IContactType>();
            contactType2.Id = new EntityId(2);

            const IContactDetail parentContactDetail = null;

            List<IContactDetail> contacts = new List<IContactDetail>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            contacts[0].ParentContactId = new EntityId(0);
            contacts[1].ParentContactId = new EntityId(0);
            contacts[2].ParentContactId = new EntityId(0);
            contacts[3].ParentContactId = new EntityId(0);
            contacts[4].ParentContactId = new EntityId(0);

            contacts[0].Id = new EntityId(0);
            contacts[0].ContactTypeId = contactType1.Id;
            contacts[0].ParentContactId = new EntityId(1);

            contacts[1].Id = new EntityId(1);
            contacts[1].ContactTypeId = contactType2.Id;
            contacts[1].ParentContactId = new EntityId(2);

            contacts[2].Id = new EntityId(2);
            contacts[2].ContactTypeId = contactType1.Id;
            contacts[2].ParentContactId = new EntityId(3);

            contacts[3].Id = new EntityId(3);
            contacts[3].ContactTypeId = contactType2.Id;
            contacts[3].ParentContactId = new EntityId(4);

            contacts[4].Id = new EntityId(4);
            contacts[4].ContactTypeId = contactType1.Id;
            contacts[4].ParentContactId = new EntityId(5);

            List<IContactDetail> filteredContacts1 = process.ApplyFilter(contacts, contactType1, parentContactDetail);
            Assert.That(filteredContacts1.Count, Is.EqualTo(3));

            List<IContactDetail> filteredContacts2 = process.ApplyFilter(contacts, contactType2, parentContactDetail);
            Assert.That(filteredContacts2.Count, Is.EqualTo(2));
        }

        [TestCase]
        public void Test_ApplyFilter_ParentContact()
        {
            IContactDetailProcess process = CreateBusinessProcess();

            const IContactType contactType1 = null;

            IContactDetail parentContactDetail1 = CreateEntity(process);
            parentContactDetail1.Id = new EntityId(1);

            IContactDetail parentContactDetail2 = CreateEntity(process);
            parentContactDetail2.Id = new EntityId(2);

            List<IContactDetail> contacts = new List<IContactDetail>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            contacts[0].ParentContactId = new EntityId(0);
            contacts[1].ParentContactId = new EntityId(0);
            contacts[2].ParentContactId = new EntityId(0);
            contacts[3].ParentContactId = new EntityId(0);
            contacts[4].ParentContactId = new EntityId(0);

            contacts[0].Id = new EntityId(0);
            contacts[0].ContactTypeId = new EntityId(1);
            contacts[0].ParentContactId = parentContactDetail1.Id;

            contacts[1].Id = new EntityId(1);
            contacts[1].ContactTypeId = new EntityId(2);
            contacts[1].ParentContactId = parentContactDetail2.Id;

            contacts[2].Id = new EntityId(2);
            contacts[2].ContactTypeId = new EntityId(1);
            contacts[2].ParentContactId = parentContactDetail1.Id;

            contacts[3].Id = new EntityId(3);
            contacts[3].ContactTypeId = new EntityId(2);
            contacts[3].ParentContactId = parentContactDetail2.Id;

            contacts[4].Id = new EntityId(4);
            contacts[4].ContactTypeId = new EntityId(1);
            contacts[4].ParentContactId = parentContactDetail1.Id;

            List<IContactDetail> filteredContacts1 = process.ApplyFilter(contacts, contactType1, parentContactDetail1);
            Assert.That(filteredContacts1.Count, Is.EqualTo(3));

            List<IContactDetail> filteredContacts2 = process.ApplyFilter(contacts, contactType1, parentContactDetail2);
            Assert.That(filteredContacts2.Count, Is.EqualTo(2));
        }
    }
}
