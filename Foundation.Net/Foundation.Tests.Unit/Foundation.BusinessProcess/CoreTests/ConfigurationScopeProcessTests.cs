//-----------------------------------------------------------------------
// <copyright file="ConfigurationScopeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ConfigurationScopeProcessTests
    /// </summary>
    [TestFixture]
    public class ConfigurationScopeProcessTests : CommonBusinessProcessTestBaseClass<IConfigurationScope, IConfigurationScopeProcess, IConfigurationScopeRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 8;
        protected override String ExpectedScreenTitle => "Configuration Scopes";
        protected override String ExpectedStatusBarText => "Number of Configuration Scopes:";

        protected override string ExpectedComboBoxDisplayMember => FDC.ConfigurationScope.Name;

        protected override IConfigurationScopeRepository CreateDataAccess()
        {
            IConfigurationScopeRepository dataAccess = Substitute.For<IConfigurationScopeRepository>();

            return dataAccess;
        }

        protected override IConfigurationScopeProcess CreateBusinessProcess()
        {
            IConfigurationScopeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IConfigurationScopeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IConfigurationScopeProcess process = new ConfigurationScopeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IConfigurationScope CreateBlankEntity(IConfigurationScopeProcess process)
        {
            IConfigurationScope retVal = CoreInstance.Container.Get<IConfigurationScope>();

            return retVal;
        }

        protected override IConfigurationScope CreateEntity(IConfigurationScopeProcess process)
        {
            IConfigurationScope retVal = CreateBlankEntity(process);

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IConfigurationScope entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IConfigurationScope entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IConfigurationScope entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IConfigurationScope entity1, IConfigurationScope entity2)
        {
            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));
        }

        protected override void UpdateEntityProperties(IConfigurationScope entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
