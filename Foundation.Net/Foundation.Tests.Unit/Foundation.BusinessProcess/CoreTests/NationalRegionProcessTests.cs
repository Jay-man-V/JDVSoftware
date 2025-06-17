//-----------------------------------------------------------------------
// <copyright file="NationalRegionProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for NationalRegionProcessTests
    /// </summary>
    [TestFixture]
    public class NationalRegionProcessTests : CommonBusinessProcessTestBaseClass<INationalRegion, INationalRegionProcess, INationalRegionRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "National Regions";
        protected override String ExpectedStatusBarText => "Number of National Regions:";

        protected override String ExpectedComboBoxDisplayMember => FDC.NationalRegion.ShortName;

        protected override INationalRegionRepository CreateDataAccess()
        {
            INationalRegionRepository dataAccess = Substitute.For<INationalRegionRepository>();

            return dataAccess;
        }

        protected override INationalRegionProcess CreateBusinessProcess()
        {
            INationalRegionProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override INationalRegionProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ICountryProcess countryProcess = Substitute.For<ICountryProcess>();

            INationalRegionProcess process = new NationalRegionProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, countryProcess);

            return process;
        }

        protected override INationalRegion CreateBlankEntity(INationalRegionProcess process)
        {
            INationalRegion retVal = CoreInstance.Container.Get<INationalRegion>();

            return retVal;
        }

        protected override INationalRegion CreateEntity(INationalRegionProcess process)
        {
            INationalRegion retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.CountryId = new EntityId(1);
            retVal.Abbreviation = Guid.NewGuid().ToString();
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.FullName = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(INationalRegion entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(INationalRegion entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(INationalRegion entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(INationalRegion entity1, INationalRegion entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.CountryId, Is.EqualTo(entity1.CountryId));
            Assert.That(entity2.Abbreviation, Is.EqualTo(entity1.Abbreviation));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.FullName, Is.EqualTo(entity1.FullName));
        }

        protected override void UpdateEntityProperties(INationalRegion entity)
        {
            entity.Abbreviation += "Updated";
            entity.ShortName += "Updated";
            entity.FullName += "Updated";
        }
    }
}
