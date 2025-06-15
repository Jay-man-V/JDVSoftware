//-----------------------------------------------------------------------
// <copyright file="WorldRegionProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for WorldRegionProcessTests
    /// </summary>
    [TestFixture]
    public class WorldRegionProcessTests : CommonBusinessProcessTestBaseClass<IWorldRegion, IWorldRegionProcess, IWorldRegionRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 8;
        protected override String ExpectedScreenTitle => "World Regions";
        protected override String ExpectedStatusBarText => "Number of World Regions:";

        protected override String ExpectedComboBoxDisplayMember => FDC.WorldRegion.Name;

        protected override IWorldRegionRepository CreateDataAccess()
        {
            IWorldRegionRepository dataAccess = Substitute.For<IWorldRegionRepository>();

            return dataAccess;
        }

        protected override IWorldRegionProcess CreateBusinessProcess()
        {
            IWorldRegionProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IWorldRegionProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IWorldRegionProcess process = new WorldRegionProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IWorldRegion CreateBlankEntity(IWorldRegionProcess process)
        {
            IWorldRegion retVal = CoreInstance.Container.Get<IWorldRegion>();

            return retVal;
        }

        protected override IWorldRegion CreateEntity(IWorldRegionProcess process)
        {
            IWorldRegion retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IWorldRegion entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IWorldRegion entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IWorldRegion entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IWorldRegion entity1, IWorldRegion entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
        }

        protected override void UpdateEntityProperties(IWorldRegion entity)
        {
            entity.Name += "Updated";
        }
    }
}
