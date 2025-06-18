//-----------------------------------------------------------------------
// <copyright file="ApplicationConfigurationProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ApplicationConfigurationProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationConfigurationProcessTests : CommonBusinessProcessTestBaseClass<IApplicationConfiguration, IApplicationConfigurationProcess, IApplicationConfigurationRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Application Configurations";
        protected override String ExpectedStatusBarText => "Number of Application Configurations:";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Configuration Scope:";
        protected override string ExpectedFilter1DisplayMemberPath => FDC.ConfigurationScope.Name;

        protected override Boolean ExpectedHasOptionalAction1 => true;
        protected override String ExpectedAction1Name => "Load group...";

        protected override Boolean ExpectedHasOptionalDropDownParameter2 => true;
        protected override String ExpectedFilter2Name => "Application Name:";
        protected override string ExpectedFilter2DisplayMemberPath => FDC.ConfigurationScope.Name;

        protected override Boolean ExpectedHasOptionalDropDownParameter3 => true;
        protected override String ExpectedFilter3Name => "User:";
        protected override string ExpectedFilter3DisplayMemberPath => FDC.UserProfile.DisplayName;

        protected override String ExpectedComboBoxDisplayMember => FDC.ApplicationConfiguration.Key;

        protected override IApplicationConfigurationRepository CreateRepository()
        {
            IApplicationConfigurationRepository dataAccess = Substitute.For<IApplicationConfigurationRepository>();

            return dataAccess;
        }

        protected override IApplicationConfigurationProcess CreateBusinessProcess()
        {
            IApplicationConfigurationProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationConfigurationProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IConfigurationScopeProcess configurationScopeProcess = Substitute.For<IConfigurationScopeProcess>();
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            CopyProperties(configurationScopeProcess, CoreInstance.Container.Get<IConfigurationScopeProcess>());
            CopyProperties(applicationProcess, CoreInstance.Container.Get<IApplicationProcess>());
            CopyProperties(userProfileProcess, CoreInstance.Container.Get<IUserProfileProcess>());

            IApplicationConfigurationProcess process = new ApplicationConfigurationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, configurationScopeProcess, applicationProcess, userProfileProcess);

            return process;
        }

        protected override IApplicationConfiguration CreateBlankEntity(IApplicationConfigurationProcess process)
        {
            IApplicationConfiguration retVal = CoreInstance.Container.Get<IApplicationConfiguration>();

            return retVal;
        }

        protected override IApplicationConfiguration CreateEntity(IApplicationConfigurationProcess process)
        {
            IApplicationConfiguration retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.ConfigurationScopeId = new EntityId(ConfigurationScope.System.Id());
            retVal.Key = Guid.NewGuid().ToString();
            retVal.Value = $"{Guid.NewGuid()},{Guid.NewGuid()}";

            return retVal;
        }

        protected override void CheckBlankEntry(IApplicationConfiguration entity)
        {
            Assert.That(entity.Key, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IApplicationConfiguration entity)
        {
            Assert.That(entity.Key, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IApplicationConfiguration entity)
        {
            Assert.That(entity.Key, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IApplicationConfiguration entity1, IApplicationConfiguration entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ConfigurationScopeId, Is.EqualTo(entity1.ConfigurationScopeId));
            Assert.That(entity2.Key, Is.EqualTo(entity1.Key));
            Assert.That(entity2.Value, Is.EqualTo(entity1.Value));
        }

        protected override void UpdateEntityProperties(IApplicationConfiguration entity)
        {
            entity.Key += "Updated";
            entity.Value += "Updated";
        }

        [TestCase]
        public void Test_ApplyFilter_ConfigurationScope()
        {
            IApplicationConfigurationProcess process = CreateBusinessProcess();

            IConfigurationScope configurationScope1 = CoreInstance.Container.Get<IConfigurationScope>();
            configurationScope1.Id = new EntityId(1);

            IConfigurationScope configurationScope2 = CoreInstance.Container.Get<IConfigurationScope>();
            configurationScope2.Id = new EntityId(2);

            IApplication application1 = CoreInstance.Container.Get<IApplication>();
            application1.Id = new AppId(0);

            IUserProfile userProfile1 = CoreInstance.Container.Get<IUserProfile>();
            userProfile1.Id = new EntityId(1);

            List<IApplicationConfiguration> applicationConfigurations = new List<IApplicationConfiguration>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            applicationConfigurations[0].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[1].ConfigurationScopeId = configurationScope2.Id;
            applicationConfigurations[2].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[3].ConfigurationScopeId = configurationScope2.Id;
            applicationConfigurations[4].ConfigurationScopeId = configurationScope1.Id;

            applicationConfigurations[0].Id = new EntityId(0);
            applicationConfigurations[0].ApplicationId = application1.Id;
            applicationConfigurations[0].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[1].Id = new EntityId(1);
            applicationConfigurations[1].ApplicationId = application1.Id;
            applicationConfigurations[1].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[2].Id = new EntityId(2);
            applicationConfigurations[2].ApplicationId = application1.Id;
            applicationConfigurations[2].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[3].Id = new EntityId(3);
            applicationConfigurations[3].ApplicationId = application1.Id;
            applicationConfigurations[3].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[4].Id = new EntityId(4);
            applicationConfigurations[4].ApplicationId = application1.Id;
            applicationConfigurations[4].CreatedByUserProfileId = userProfile1.Id;

            List<IApplicationConfiguration> filteredApplicationConfigurations1 = process.ApplyFilter(applicationConfigurations, configurationScope1, application1, userProfile1);
            Assert.That(filteredApplicationConfigurations1.Count, Is.EqualTo(3));

            List<IApplicationConfiguration> filteredApplicationConfigurations2 = process.ApplyFilter(applicationConfigurations, configurationScope2, application1, userProfile1);
            Assert.That(filteredApplicationConfigurations2.Count, Is.EqualTo(2));
        }

        [TestCase]
        public void Test_ApplyFilter_Application()
        {
            IApplicationConfigurationProcess process = CreateBusinessProcess();

            IConfigurationScope configurationScope1 = CoreInstance.Container.Get<IConfigurationScope>();
            configurationScope1.Id = new EntityId(1);

            IApplication application1 = CoreInstance.Container.Get<IApplication>();
            application1.Id = new AppId(1);

            IApplication application2 = CoreInstance.Container.Get<IApplication>();
            application2.Id = new AppId(2);

            IUserProfile userProfile1 = CoreInstance.Container.Get<IUserProfile>();
            userProfile1.Id = new EntityId(1);

            List<IApplicationConfiguration> applicationConfigurations = new List<IApplicationConfiguration>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            applicationConfigurations[0].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[1].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[2].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[3].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[4].ConfigurationScopeId = configurationScope1.Id;

            applicationConfigurations[0].Id = new EntityId(0);
            applicationConfigurations[0].ApplicationId = application1.Id;
            applicationConfigurations[0].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[1].Id = new EntityId(1);
            applicationConfigurations[1].ApplicationId = application2.Id;
            applicationConfigurations[1].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[2].Id = new EntityId(2);
            applicationConfigurations[2].ApplicationId = application1.Id;
            applicationConfigurations[2].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[3].Id = new EntityId(3);
            applicationConfigurations[3].ApplicationId = application2.Id;
            applicationConfigurations[3].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[4].Id = new EntityId(4);
            applicationConfigurations[4].ApplicationId = application1.Id;
            applicationConfigurations[4].CreatedByUserProfileId = userProfile1.Id;

            List<IApplicationConfiguration> filteredApplicationConfigurations1 = process.ApplyFilter(applicationConfigurations, configurationScope1, application1, userProfile1);
            Assert.That(filteredApplicationConfigurations1.Count, Is.EqualTo(3));

            List<IApplicationConfiguration> filteredApplicationConfigurations2 = process.ApplyFilter(applicationConfigurations, configurationScope1, application2, userProfile1);
            Assert.That(filteredApplicationConfigurations2.Count, Is.EqualTo(2));
        }

        [TestCase]
        public void Test_ApplyFilter_UserProfile()
        {
            IApplicationConfigurationProcess process = CreateBusinessProcess();

            IConfigurationScope configurationScope1 = CoreInstance.Container.Get<IConfigurationScope>();
            configurationScope1.Id = new EntityId(1);

            IApplication application1 = CoreInstance.Container.Get<IApplication>();
            application1.Id = new AppId(1);

            IUserProfile userProfile1 = CoreInstance.Container.Get<IUserProfile>();
            userProfile1.Id = new EntityId(1);

            IUserProfile userProfile2 = CoreInstance.Container.Get<IUserProfile>();
            userProfile2.Id = new EntityId(2);

            List<IApplicationConfiguration> applicationConfigurations = new List<IApplicationConfiguration>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            applicationConfigurations[0].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[1].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[2].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[3].ConfigurationScopeId = configurationScope1.Id;
            applicationConfigurations[4].ConfigurationScopeId = configurationScope1.Id;

            applicationConfigurations[0].Id = new EntityId(0);
            applicationConfigurations[0].ApplicationId = application1.Id;
            applicationConfigurations[0].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[1].Id = new EntityId(1);
            applicationConfigurations[1].ApplicationId = application1.Id;
            applicationConfigurations[1].CreatedByUserProfileId = userProfile2.Id;

            applicationConfigurations[2].Id = new EntityId(2);
            applicationConfigurations[2].ApplicationId = application1.Id;
            applicationConfigurations[2].CreatedByUserProfileId = userProfile1.Id;

            applicationConfigurations[3].Id = new EntityId(3);
            applicationConfigurations[3].ApplicationId = application1.Id;
            applicationConfigurations[3].CreatedByUserProfileId = userProfile2.Id;

            applicationConfigurations[4].Id = new EntityId(4);
            applicationConfigurations[4].ApplicationId = application1.Id;
            applicationConfigurations[4].CreatedByUserProfileId = userProfile1.Id;

            List<IApplicationConfiguration> filteredApplicationConfigurations1 = process.ApplyFilter(applicationConfigurations, configurationScope1, application1, userProfile1);
            Assert.That(filteredApplicationConfigurations1.Count, Is.EqualTo(3));

            List<IApplicationConfiguration> filteredApplicationConfigurations2 = process.ApplyFilter(applicationConfigurations, configurationScope1, application1, userProfile2);
            Assert.That(filteredApplicationConfigurations2.Count, Is.EqualTo(2));
        }
    }
}
