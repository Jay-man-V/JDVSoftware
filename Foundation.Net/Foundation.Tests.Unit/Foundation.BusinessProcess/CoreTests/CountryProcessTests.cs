//-----------------------------------------------------------------------
// <copyright file="CountryProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.Tests.Unit.Support;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for CountryProcessTests
    /// </summary>
    [TestFixture]
    [DeploymentItem(@".Support\SampleDocuments\United Kingdom.png", @".Support\SampleDocuments\")]
    public class CountryProcessTests : CommonBusinessProcessTestBaseClass<ICountry, ICountryProcess, ICountryRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 18;
        protected override String ExpectedScreenTitle => "Countries";
        protected override String ExpectedStatusBarText => "Number of Countries:";

        protected override string ExpectedComboBoxDisplayMember => FDC.Country.AbbreviatedName;

        protected override ICountryRepository CreateDataAccess()
        {
            ICountryRepository dataAccess = Substitute.For<ICountryRepository>();

            return dataAccess;
        }

        protected override ICountryProcess CreateBusinessProcess()
        {
            ICountryProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ICountryProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ICurrencyProcess currencyProcess = Substitute.For<ICurrencyProcess>();
            ILanguageProcess languageProcess = Substitute.For<ILanguageProcess>();
            ITimeZoneProcess timeZoneProcess = Substitute.For<ITimeZoneProcess>();
            IWorldRegionProcess worldRegionProcess = Substitute.For<IWorldRegionProcess>();

            ICountryProcess process = new CountryProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, currencyProcess, languageProcess, timeZoneProcess, worldRegionProcess);

            return process;
        }

        protected override ICountry CreateBlankEntity(ICountryProcess process)
        {
            ICountry retVal = CoreInstance.Container.Get<ICountry>();

            return retVal;
        }

        protected override ICountry CreateEntity(ICountryProcess process)
        {
            ICountry retVal = CreateBlankEntity(process);

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.IsoCode = Guid.NewGuid().ToString();
            retVal.AbbreviatedName = Guid.NewGuid().ToString();
            retVal.FullName = Guid.NewGuid().ToString();
            retVal.NativeName = Guid.NewGuid().ToString();
            retVal.DialingCode = Guid.NewGuid().ToString();
            retVal.PostCodeFormat = Guid.NewGuid().ToString();
            retVal.CurrencyId = new EntityId(1);
            retVal.LanguageId = new EntityId(1);
            retVal.TimeZoneId = new EntityId(1);
            retVal.WorldRegionId = new EntityId(1);
            retVal.CountryFlag = fileApi.GetFileContentsAsByteArray(@".Support\SampleDocuments\United Kingdom.png");

            return retVal;
        }

        protected override void CheckBlankEntry(ICountry entity)
        {
            Assert.That(entity.AbbreviatedName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ICountry entity)
        {
            Assert.That(entity.AbbreviatedName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ICountry entity)
        {
            Assert.That(entity.AbbreviatedName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ICountry entity1, ICountry entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.IsoCode, Is.EqualTo(entity1.IsoCode));
            Assert.That(entity2.AbbreviatedName, Is.EqualTo(entity1.AbbreviatedName));
            Assert.That(entity2.FullName, Is.EqualTo(entity1.FullName));
            Assert.That(entity2.NativeName, Is.EqualTo(entity1.NativeName));
            Assert.That(entity2.DialingCode, Is.EqualTo(entity1.DialingCode));
            Assert.That(entity2.PostCodeFormat, Is.EqualTo(entity1.PostCodeFormat));
            Assert.That(entity2.CountryFlag, Is.EquivalentTo(entity1.CountryFlag));
        }

        protected override void UpdateEntityProperties(ICountry entity)
        {
            entity.AbbreviatedName += "Updated";
            entity.FullName += "Updated";
        }
    }
}
