//-----------------------------------------------------------------------
// <copyright file="CommonBusinessProcessTestBaseClass.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess
{
    /// <summary>
    /// Summary description for CommonBusinessProcessUnitTests
    /// </summary>
    [TestFixture]
    public abstract class CommonBusinessProcessTestBaseClass<TEntity, TBusinessProcess, TRepository> : UnitTestBase
        where TEntity : IFoundationModel
        where TBusinessProcess : ICommonBusinessProcess<TEntity>
        where TRepository : IFoundationModelRepository<TEntity>
    {
        protected TRepository Repository { get; private set; }
        protected IEventLogProcess EventLogProcess { get; set; }

        protected Int32 StandardColumnDefinitionsCount
        {
            get
            {
                if (Repository.HasValidityPeriodColumns)
                {
                    return 8;
                }
                else
                {
                    return 7;
                }
            }
        }

        protected abstract TRepository CreateRepository();
        protected abstract TBusinessProcess CreateBusinessProcess();
        protected abstract TBusinessProcess CreateBusinessProcess(IDateTimeService dateTimeService);
        protected abstract TEntity CreateBlankEntity(TBusinessProcess process);
        protected abstract TEntity CreateEntity(TBusinessProcess process);
        protected abstract void UpdateEntityProperties(TEntity entity);
        protected abstract void CheckBlankEntry(TEntity entity);
        protected abstract void CheckAllEntry(TEntity entity);
        protected abstract void CheckNoneEntry(TEntity entity);
        protected abstract void CompareEntityProperties(TEntity entity1, TEntity entity2);

        protected abstract Int32 ColumnDefinitionsCount { get; }
        protected abstract String ExpectedScreenTitle { get; }
        protected abstract String ExpectedStatusBarText { get; }

        protected virtual EntityId ExpectedNullId => new EntityId(-1);
        protected virtual EntityId ExpectedAllId => new EntityId(-1);
        protected virtual EntityId ExpectedNoneId => new EntityId(-2);
        protected virtual String ExpectedAllText => "<All>";
        protected virtual String ExpectedNoneText => "<None>";

        protected virtual Boolean ExpectedHasOptionalDropDownParameter1 => false;
        protected virtual String ExpectedFilter1Name => "Filter1";
        protected virtual String ExpectedFilter1DisplayMemberPath => String.Empty;
        protected virtual String ExpectedFilter1ValueMemberPath => FDC.FoundationEntity.Id;
        protected virtual Boolean ExpectedHasOptionalAction1 => false;
        protected virtual String ExpectedAction1Name => "Action1";

        protected virtual Boolean ExpectedHasOptionalDropDownParameter2 => false;
        protected virtual String ExpectedFilter2Name => "Filter2";
        protected virtual String ExpectedFilter2DisplayMemberPath => String.Empty;
        protected virtual String ExpectedFilter2ValueMemberPath => FDC.FoundationEntity.Id;
        protected virtual Boolean ExpectedHasOptionalAction2 => false;
        protected virtual String ExpectedAction2Name => "Action2";

        protected virtual Boolean ExpectedHasOptionalDropDownParameter3 => false;
        protected virtual String ExpectedFilter3Name => "Filter3";
        protected virtual String ExpectedFilter3DisplayMemberPath => String.Empty;
        protected virtual String ExpectedFilter3ValueMemberPath => FDC.FoundationEntity.Id;
        protected virtual Boolean ExpectedHasOptionalAction3 => false;
        protected virtual String ExpectedAction3Name => "Action3";

        protected virtual Boolean ExpectedHasOptionalDropDownParameter4 => false;
        protected virtual String ExpectedFilter4Name => "Filter4";
        protected virtual String ExpectedFilter4DisplayMemberPath => String.Empty;
        protected virtual String ExpectedFilter4ValueMemberPath => FDC.FoundationEntity.Id;
        protected virtual Boolean ExpectedHasOptionalAction4 => false;
        protected virtual String ExpectedAction4Name => "Action4";

        protected virtual String ExpectedComboBoxDisplayMember => String.Empty;

        protected virtual Boolean ExpectedCanRefreshData => true;
        protected virtual Boolean ExpectedCanViewData => false;
        protected virtual Boolean ExpectedCanAddData => false;
        protected virtual Boolean ExpectedCanEditData => false;
        protected virtual Boolean ExpectedCanDeleteData => false;

        protected void CopyProperties(ICommonBusinessProcess substitute, ICommonBusinessProcess concrete)
        {
            substitute.NullId.Returns(concrete.NullId);
            substitute.AllId.Returns(concrete.AllId);
            substitute.NoneId.Returns(concrete.NoneId);
            substitute.ComboBoxDisplayMember.Returns(concrete.ComboBoxDisplayMember);
            substitute.ComboBoxValueMember.Returns(concrete.ComboBoxValueMember);
        }

        protected virtual void CheckBaseClassProperties(TBusinessProcess process)
        {
            // Does nothing
        }

        protected virtual void CompareEntityBaseProperties_Id(TEntity expectedEntity, TEntity actualEntity)
        {
            Assert.That(actualEntity.Id, Is.EqualTo(expectedEntity.Id));
        }

        private void CompareEntityBaseProperties(TRepository repository, TEntity expectedEntity, TEntity actualEntity)
        {
            CompareEntityBaseProperties_Id(expectedEntity, actualEntity);
            Assert.That(actualEntity.CreatedByUserProfileId, Is.EqualTo(expectedEntity.CreatedByUserProfileId));
            Assert.That(actualEntity.LastUpdatedByUserProfileId, Is.EqualTo(expectedEntity.LastUpdatedByUserProfileId));
            Assert.That(actualEntity.CreatedOn, Is.EqualTo(expectedEntity.CreatedOn));
            Assert.That(actualEntity.LastUpdatedOn, Is.EqualTo(expectedEntity.LastUpdatedOn));
            Assert.That(actualEntity.EntityState, Is.EqualTo(expectedEntity.EntityState));
            Assert.That(actualEntity.EntityLife, Is.EqualTo(expectedEntity.EntityLife));
            Assert.That(actualEntity.EntityStatus, Is.EqualTo(expectedEntity.EntityStatus));
            Assert.That(actualEntity.StatusId, Is.EqualTo(expectedEntity.StatusId));
            Assert.That(actualEntity.Timestamp, Is.EquivalentTo(expectedEntity.Timestamp));

            if (repository.HasValidityPeriodColumns)
            {
                Assert.That(actualEntity.ValidFrom, Is.EqualTo(expectedEntity.ValidFrom));
                Assert.That(actualEntity.ValidTo, Is.EqualTo(expectedEntity.ValidTo));
            }
        }

        protected void SetRepositoryProperties(TRepository repository)
        {
            TRepository tempRepository = CoreInstance.Container.Get<TRepository>();

            repository.HasValidityPeriodColumns.Returns(tempRepository.HasValidityPeriodColumns);
        }

        protected override void StartTest()
        {
            base.StartTest();
            EventLogProcess = Substitute.For<IEventLogProcess>();

            Repository = CreateRepository();
            SetRepositoryProperties(Repository);
        }

        protected virtual void Test_NullId(TBusinessProcess process)
        {
            Assert.That(process.NullId, Is.EqualTo(new EntityId(ExpectedNullId)));
        }

        protected virtual void Test_AllId(TBusinessProcess process)
        {
            Assert.That(process.AllId, Is.EqualTo(new EntityId(ExpectedAllId)));
        }

        protected virtual void Test_NoneId(TBusinessProcess process)
        {
            Assert.That(process.NoneId, Is.EqualTo(new EntityId(ExpectedNoneId)));
        }

        [TestCase]
        public void Test_BaseClassProperties()
        {
            TBusinessProcess process = CreateBusinessProcess(DateTimeService);

            Test_NullId(process);
            Assert.That(process.NullId.ToInteger(), Is.EqualTo(ExpectedNullId));

            Test_AllId(process);
            Assert.That(process.AllId.ToInteger(), Is.EqualTo(ExpectedAllId));
            Assert.That(process.AllText, Is.EqualTo(ExpectedAllText));

            Test_NoneId(process);
            Assert.That(process.NoneId.ToInteger(), Is.EqualTo(ExpectedNoneId));
            Assert.That(process.NoneText, Is.EqualTo(ExpectedNoneText));

            Assert.That(process.HasOptionalDropDownParameter1, Is.EqualTo(ExpectedHasOptionalDropDownParameter1));
            Assert.That(process.Filter1Name, Is.EqualTo(ExpectedFilter1Name));
            Assert.That(process.Filter1DisplayMemberPath, Is.EqualTo(ExpectedFilter1DisplayMemberPath));
            Assert.That(process.Filter1SelectedValuePath, Is.EqualTo(ExpectedFilter1ValueMemberPath));
            Assert.That(process.HasOptionalAction1, Is.EqualTo(ExpectedHasOptionalAction1));
            Assert.That(process.Action1Name, Is.EqualTo(ExpectedAction1Name));

            Assert.That(process.HasOptionalDropDownParameter2, Is.EqualTo(ExpectedHasOptionalDropDownParameter2));
            Assert.That(process.Filter2Name, Is.EqualTo(ExpectedFilter2Name));
            Assert.That(process.Filter2DisplayMemberPath, Is.EqualTo(ExpectedFilter2DisplayMemberPath));
            Assert.That(process.Filter2SelectedValuePath, Is.EqualTo(ExpectedFilter2ValueMemberPath));
            Assert.That(process.HasOptionalAction2, Is.EqualTo(ExpectedHasOptionalAction2));
            Assert.That(process.Action2Name, Is.EqualTo(ExpectedAction2Name));

            Assert.That(process.HasOptionalDropDownParameter3, Is.EqualTo(ExpectedHasOptionalDropDownParameter3));
            Assert.That(process.Filter3Name, Is.EqualTo(ExpectedFilter3Name));
            Assert.That(process.Filter3DisplayMemberPath, Is.EqualTo(ExpectedFilter3DisplayMemberPath));
            Assert.That(process.Filter3SelectedValuePath, Is.EqualTo(ExpectedFilter3ValueMemberPath));
            Assert.That(process.HasOptionalAction3, Is.EqualTo(ExpectedHasOptionalAction3));
            Assert.That(process.Action3Name, Is.EqualTo(ExpectedAction3Name));

            Assert.That(process.HasOptionalDropDownParameter4, Is.EqualTo(ExpectedHasOptionalDropDownParameter4));
            Assert.That(process.Filter4Name, Is.EqualTo(ExpectedFilter4Name));
            Assert.That(process.Filter4DisplayMemberPath, Is.EqualTo(ExpectedFilter4DisplayMemberPath));
            Assert.That(process.Filter4SelectedValuePath, Is.EqualTo(ExpectedFilter4ValueMemberPath));
            Assert.That(process.HasOptionalAction4, Is.EqualTo(ExpectedHasOptionalAction4));
            Assert.That(process.Action4Name, Is.EqualTo(ExpectedAction4Name));

            Assert.That(process.ScreenTitle, Is.EqualTo(ExpectedScreenTitle));
            Assert.That(process.StatusBarText, Is.EqualTo(ExpectedStatusBarText));

            Assert.That(process.DefaultValidFromDateTime, Is.EqualTo(SystemDateTimeMs));
            Assert.That(process.DefaultValidToDateTime, Is.EqualTo(ValidToDateTime));

            Assert.That(process.ComboBoxDisplayMember, Is.EqualTo(ExpectedComboBoxDisplayMember));
            Assert.That(process.ComboBoxValueMember, Is.EqualTo(FDC.FoundationEntity.Id));

            CheckBaseClassProperties(process);
        }

        [TestCase]
        public void Test_GetColumnDefinitions()
        {
            TBusinessProcess process = CreateBusinessProcess();

            List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitions();

            Int32 columnDefinitionsCount = ColumnDefinitionsCount;
            Int32 actualColumnDefinitionsCount = gridColumnDefinitions.Count;

            Assert.That(actualColumnDefinitionsCount, Is.EqualTo(columnDefinitionsCount));
        }

        [TestCase]
        public void Test_AddFilterOptionAll_Entity()
        {
            TBusinessProcess process = CreateBusinessProcess();

            if (String.IsNullOrEmpty(process.ComboBoxDisplayMember))
            {
                String parameterName = nameof(process.ComboBoxDisplayMember);
                Object actualType = CoreInstance.Container.Get<TBusinessProcess>();
                String errorMessage = $"Empty {parameterName} passed to {actualType.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {parameterName}";

                ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
                Assert.That(actualException.ParamName, Is.EqualTo(parameterName));
            }
            else if (process.ComboBoxDisplayMember == "Made up property name")
            {
                Object actualType = CoreInstance.Container.Get<TEntity>();
                String errorMessage = $"Member '{actualType.GetType()}.{process.ComboBoxDisplayMember}' not found.";

                MissingMemberException actualException = Assert.Throws<MissingMemberException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            }
            else
            {
                List<TEntity> listItems = new List<TEntity>();
                process.AddFilterOptionAll(listItems);

                Assert.That(listItems.Count, Is.EqualTo(1));

                Assert.That(listItems[0].Id, Is.EqualTo(ExpectedAllId));
                CheckAllEntry(listItems[0]);
            }
        }

        [TestCase]
        public void Test_AddFilterOptionNone_Entity()
        {
            TBusinessProcess process = CreateBusinessProcess();

            if (String.IsNullOrEmpty(process.ComboBoxDisplayMember))
            {
                String parameterName = nameof(process.ComboBoxDisplayMember);
                Object actualType = CoreInstance.Container.Get<TBusinessProcess>();
                String errorMessage = $"Empty {parameterName} passed to {actualType.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {parameterName}";

                ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
                Assert.That(actualException.ParamName, Is.EqualTo(parameterName));
            }
            else if (process.ComboBoxDisplayMember == "Made up property name")
            {
                Object actualType = CoreInstance.Container.Get<TEntity>();
                String errorMessage = $"Member '{actualType.GetType()}.{process.ComboBoxDisplayMember}' not found.";

                MissingMemberException actualException = Assert.Throws<MissingMemberException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            }
            else
            {
                List<TEntity> listItems = new List<TEntity>();
                process.AddFilterOptionNone(listItems);

                Assert.That(listItems.Count, Is.EqualTo(1));

                Assert.That(listItems[0].Id, Is.EqualTo(ExpectedNoneId));
                CheckNoneEntry(listItems[0]);
            }
        }

        [TestCase]
        public void Test_AddFilterOptionsAdditional_Entity()
        {
            TBusinessProcess process = CreateBusinessProcess();

            if (String.IsNullOrEmpty(process.ComboBoxDisplayMember))
            {
                String parameterName = nameof(process.ComboBoxDisplayMember);
                Object actualType = CoreInstance.Container.Get<TBusinessProcess>();
                String errorMessage = $"Empty {parameterName} passed to {actualType.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {parameterName}";

                ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
                Assert.That(actualException.ParamName, Is.EqualTo(parameterName));
            }
            else if (process.ComboBoxDisplayMember == "Made up property name")
            {
                Object actualType = CoreInstance.Container.Get<TEntity>();
                String errorMessage = $"Member '{actualType.GetType()}.{process.ComboBoxDisplayMember}' not found.";

                MissingMemberException actualException = Assert.Throws<MissingMemberException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            }
            else
            {
                List<TEntity> listItems = new List<TEntity>();
                process.AddFilterOptionsAdditional(listItems);

                Assert.That(listItems.Count, Is.EqualTo(2));

                Assert.That(listItems[0].Id, Is.EqualTo(ExpectedAllId));
                CheckAllEntry(listItems[0]);

                Assert.That(listItems[1].Id, Is.EqualTo(ExpectedNoneId));
                CheckNoneEntry(listItems[1]);
            }
        }

        [TestCase]
        public void Test_AddFilterOptionAll_String()
        {
            TBusinessProcess process = CreateBusinessProcess();

            List<String> listItems = new List<String>();
            process.AddFilterOptionAll(listItems);

            Assert.That(listItems.Count, Is.EqualTo(1));

            Assert.That(listItems[0], Is.EqualTo(ExpectedAllText));
        }

        [TestCase]
        public void Test_AddFilterOptionNone_String()
        {
            TBusinessProcess process = CreateBusinessProcess();

            List<String> listItems = new List<String>();
            process.AddFilterOptionNone(listItems);

            Assert.That(listItems.Count, Is.EqualTo(1));

            Assert.That(listItems[0], Is.EqualTo(ExpectedNoneText));
        }

        [TestCase]
        public void Test_AddFilterOptionsAdditional_String()
        {
            TBusinessProcess process = CreateBusinessProcess();

            List<String> listItems = new List<String>();
            process.AddFilterOptionsAdditional(listItems);

            Assert.That(listItems.Count, Is.EqualTo(2));

            Assert.That(listItems[0], Is.EqualTo(ExpectedAllText));
            Assert.That(listItems[1], Is.EqualTo(ExpectedNoneText));
        }

        [TestCase]
        public void Test_GetBlankEntry()
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity = process.GetBlankEntry();

            Assert.That(entity.Id, Is.EqualTo(ExpectedNullId));
            CheckBlankEntry(entity);
        }

        [TestCase]
        public virtual void Test_GetAllEntry()
        {
            TBusinessProcess process = CreateBusinessProcess();

            if (String.IsNullOrEmpty(process.ComboBoxDisplayMember))
            {
                String parameterName = nameof(process.ComboBoxDisplayMember);
                Object actualType = CoreInstance.Container.Get<TBusinessProcess>();
                String errorMessage = $"Empty {parameterName} passed to {actualType.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {parameterName}";

                ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
                Assert.That(actualException.ParamName, Is.EqualTo(parameterName));
            }
            else if (process.ComboBoxDisplayMember == "Made up property name")
            {
                Object actualType = CoreInstance.Container.Get<TEntity>();
                String errorMessage = $"Member '{actualType.GetType()}.{process.ComboBoxDisplayMember}' not found.";

                MissingMemberException actualException = Assert.Throws<MissingMemberException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            }
            else
            {
                TEntity entity = process.GetAllEntry();

                Assert.That(entity.Id, Is.EqualTo(ExpectedAllId));
                CheckAllEntry(entity);
            }
        }

        [TestCase]
        public virtual void Test_GetNoneEntry()
        {
            TBusinessProcess process = CreateBusinessProcess();

            if (String.IsNullOrEmpty(process.ComboBoxDisplayMember))
            {
                String parameterName = nameof(process.ComboBoxDisplayMember);
                Object actualType = CoreInstance.Container.Get<TBusinessProcess>();
                String errorMessage = $"Empty {parameterName} passed to {actualType.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {parameterName}";

                ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
                Assert.That(actualException.ParamName, Is.EqualTo(parameterName));
            }
            else if (process.ComboBoxDisplayMember == "Made up property name")
            {
                Object actualType = CoreInstance.Container.Get<TEntity>();
                String errorMessage = $"Member '{actualType.GetType()}.{process.ComboBoxDisplayMember}' not found.";

                MissingMemberException actualException = Assert.Throws<MissingMemberException>(() =>
                {
                    List<TEntity> listItems = new List<TEntity>();
                    process.AddFilterOptionsAdditional(listItems);
                });

                Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            }
            else
            {
                TEntity entity = process.GetNoneEntry();

                Assert.That(entity.Id, Is.EqualTo(ExpectedNoneId));
                CheckNoneEntry(entity);
            }
        }

        [TestCase]
        public void Test_Validate_Success()
        {
            TBusinessProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);

            process.ValidateEntity(entity1);
        }

        [TestCase]
        public void Test_Validate_Exception()
        {
            AggregateException actualException = null;

            try
            {
                TBusinessProcess process = CreateBusinessProcess();
                TEntity entity1 = CreateBlankEntity(process);
                process.ValidateEntity(entity1);
            }
            catch (AggregateException ex)
            {
                actualException = ex;
            }

            Assert.That(actualException, Is.Not.Null);

            Assert.That(actualException.InnerExceptions.Count > 0);

            foreach (Exception ex in actualException.InnerExceptions)
            {
                Assert.That(ex, Is.InstanceOf<ValidationException>());
                ValidationException vException = ex as ValidationException;

                Assert.That(vException, Is.Not.Null);
                Assert.That(vException.Message, Is.Not.Null);
            }
        }

        [TestCase]
        public void Test_IsValidEntity()
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity1 = CreateBlankEntity(process);
            List<ValidationException> validationExceptions = process.IsValidEntity(entity1);

            foreach (ValidationException validationException in validationExceptions)
            {
                Assert.That(validationException, Is.Not.Null);
                Assert.That(validationException.Message, Is.Not.Null);
            }
        }

        [TestCase]
        public void Test_CanRefreshData()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Boolean actual = process.CanRefreshData();

            Assert.That(actual, Is.EqualTo(ExpectedCanRefreshData));
        }

        [TestCase]
        public void Test_CanViewRecord()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Boolean actual = process.CanViewRecord();

            Assert.That(actual, Is.EqualTo(ExpectedCanViewData));
        }

        [TestCase(true, true, true, ApplicationRole.None)]
        [TestCase(true, false, true, ApplicationRole.None)]
        [TestCase(true, true, true, ApplicationRole.Reporter)]
        [TestCase(true, false, true, ApplicationRole.Reporter)]
        [TestCase(true, true, false, ApplicationRole.SystemAdministrator)]
        [TestCase(true, false, false, ApplicationRole.SystemAdministrator)]
        public void Test_CanViewRecord(Boolean expectedResult, Boolean createEntity, Boolean isSystemSupport, ApplicationRole applicationRoleToRemove)
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity = createEntity ? CreateEntity(process) : default;

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = isSystemSupport;
            if (!isSystemSupport)
            {
                RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);
            }

            if (applicationRoleToRemove != ApplicationRole.None)
            {
                RemoveRoleFromLoggedOnUser(applicationRoleToRemove);
            }

            Boolean actual = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_CanAddRecord()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Boolean actual = process.CanAddRecord();

            Assert.That(actual, Is.EqualTo(ExpectedCanAddData));
        }

        [TestCase(true, true, ApplicationRole.None)]
        [TestCase(true, false, ApplicationRole.None)]
        [TestCase(true, true, ApplicationRole.Creator)]
        [TestCase(false, false, ApplicationRole.Creator)]
        [TestCase(true, true, ApplicationRole.SystemAdministrator)]
        [TestCase(true, false, ApplicationRole.SystemAdministrator)]
        public void Test_CanAddRecord(Boolean expectedResult, Boolean isSystemSupport, ApplicationRole applicationRoleToRemove)
        {
            TBusinessProcess process = CreateBusinessProcess();

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = isSystemSupport;
            if (!isSystemSupport)
            {
                RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);
            }

            if (applicationRoleToRemove != ApplicationRole.None)
            {
                RemoveRoleFromLoggedOnUser(applicationRoleToRemove);
            }

            Boolean actual = process.CanAddRecord(CoreInstance.CurrentLoggedOnUser.UserProfile);

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_CanEditRecord()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Boolean actual = process.CanEditRecord();

            Assert.That(actual, Is.EqualTo(ExpectedCanEditData));
        }

        [TestCase(true, true, true, ApplicationRole.None)]
        [TestCase(true, false, true, ApplicationRole.None)]
        [TestCase(false, true, false, ApplicationRole.OwnEditor)]
        [TestCase(false, false, false, ApplicationRole.OwnEditor)]
        [TestCase(false, true, false, ApplicationRole.AllEditor)]
        [TestCase(false, false, false, ApplicationRole.AllEditor)]
        [TestCase(false, true, false, ApplicationRole.SystemAdministrator)]
        [TestCase(false, false, false, ApplicationRole.SystemAdministrator)]
        public void Test_CanEditRecord(Boolean expectedResult, Boolean createEntity, Boolean isSystemSupport, ApplicationRole applicationRoleToRemove)
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity = createEntity ? CreateEntity(process) : default;

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = isSystemSupport;
            if (!isSystemSupport)
            {
                RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);
            }

            if (applicationRoleToRemove != ApplicationRole.None)
            {
                RemoveRoleFromLoggedOnUser(applicationRoleToRemove);
            }

            Boolean actual = process.CanEditRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_CanDeleteRecord()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Boolean actual = process.CanDeleteRecord();

            Assert.That(actual, Is.EqualTo(ExpectedCanDeleteData));
        }

        [TestCase(true, true, true, ApplicationRole.None)]
        [TestCase(true, false, true, ApplicationRole.None)]
        [TestCase(false, true, false, ApplicationRole.OwnDelete)]
        [TestCase(false, false, false, ApplicationRole.OwnDelete)]
        [TestCase(false, true, false, ApplicationRole.AllDelete)]
        [TestCase(false, false, false, ApplicationRole.AllDelete)]
        [TestCase(false, true, false, ApplicationRole.SystemAdministrator)]
        [TestCase(false, false, false, ApplicationRole.SystemAdministrator)]
        public void Test_CanDeleteRecord(Boolean expectedResult, Boolean createEntity, Boolean isSystemSupport, ApplicationRole applicationRoleToRemove)
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity = createEntity ? CreateEntity(process) : default;

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = isSystemSupport;
            if (!isSystemSupport)
            {
                RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);
            }

            if (applicationRoleToRemove != ApplicationRole.None)
            {
                RemoveRoleFromLoggedOnUser(applicationRoleToRemove);
            }

            Boolean actual = process.CanDeleteRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_Save_Entity()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Repository.Save(Arg.Any<TEntity>()).Returns(args =>
            {
                TEntity retVal = (TEntity)args[0];
                retVal.Id = new EntityId(1);
                return retVal;
            });

            TEntity entity1 = CreateEntity(process);
            TEntity savedEntity1 = process.Save(entity1);

            Assert.That(savedEntity1, Is.Not.Null);
            Assert.That(savedEntity1.Id.ToInteger(), Is.GreaterThan(0));

            CompareEntityProperties(entity1, savedEntity1);
        }

        [TestCase]
        public virtual void Test_Update_Entity()
        {
            TBusinessProcess process = CreateBusinessProcess();
            Repository.Save(Arg.Any<TEntity>()).Returns(args =>
            {
                TEntity retVal = (TEntity)args[0];
                retVal.Id = new EntityId(1);

                return retVal;
            });

            TEntity entity1 = CreateEntity(process);
            TEntity savedEntity1 = process.Save(entity1);

            UpdateEntityProperties(entity1);

            TEntity savedEntity2 = process.Save(savedEntity1);

            Assert.That(savedEntity1, Is.Not.Null);
            Assert.That(savedEntity2.Id.ToInteger() > 0);

            CompareEntityProperties(savedEntity1, savedEntity2);
        }

        [TestCase]
        public void Test_Save_MultipleEntities()
        {
            TBusinessProcess process = CreateBusinessProcess();

            Repository.Save(Arg.Any<List<TEntity>>()).Returns(args =>
            {
                Int32 idCounter = 1;
                List<TEntity> retVal = (List<TEntity>)args[0];
                retVal.ForEach(fe => fe.Id = new EntityId(idCounter++));
                return retVal;
            });

            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);

            List<TEntity> entitiesToSave = new List<TEntity> { entity1, entity2, entity3 };
            List<TEntity> savedEntities = process.Save(entitiesToSave);

            Assert.That(savedEntities, Is.Not.Null);
            Assert.That(savedEntities.Count, Is.EqualTo(entitiesToSave.Count));
            Assert.That(savedEntities[0].Id.ToInteger() > 0);
            Assert.That(savedEntities[1].Id.ToInteger() > 0);
            Assert.That(savedEntities[2].Id.ToInteger() > 0);

            CompareEntityProperties(entitiesToSave[0], savedEntities[0]);
            CompareEntityProperties(entitiesToSave[1], savedEntities[1]);
            CompareEntityProperties(entitiesToSave[2], savedEntities[2]);
        }

        [TestCase]
        public virtual void Test_Delete_Entity_Id()
        {
            TBusinessProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);

            Repository.Save(Arg.Is(entity1)).Returns(args =>
            {
                TEntity retVal = (TEntity)args[0];
                retVal.Id = new EntityId(1);
                retVal.EntityState = EntityState.Saved;
                return retVal;
            });

            TEntity savedEntity = process.Save(entity1);

            Repository.Get(Arg.Is(savedEntity.Id)).Returns(args =>
            {
                TEntity retVal = (TEntity)savedEntity.Clone();
                retVal.Id = new EntityId(1);
                retVal.EntityLife = EntityLife.Deleted;
                retVal.EntityState = EntityState.Saved;
                retVal.EntityStatus = EntityStatus.Inactive;
                return retVal;
            });

            TEntity loadedEntity1 = process.Get(savedEntity.Id);
            process.Delete(loadedEntity1.Id);

            TEntity loadedEntity2 = process.Get(entity1.Id);

            Assert.That(loadedEntity2.EntityLife, Is.EqualTo(EntityLife.Deleted));
            Assert.That(loadedEntity2.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(loadedEntity2.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That((Object)savedEntity == (Object)loadedEntity2, Is.EqualTo(false));
        }

        [TestCase]
        public virtual void Test_Delete_Entity_Object()
        {
            TBusinessProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);

            Repository.Save(Arg.Is(entity1)).Returns(args =>
            {
                TEntity retVal = (TEntity)args[0];
                retVal.Id = new EntityId(1);
                retVal.EntityState = EntityState.Saved;
                return retVal;
            });

            TEntity savedEntity = process.Save(entity1);

            Repository.Get(Arg.Is(savedEntity.Id)).Returns(args =>
            {
                TEntity retVal = (TEntity)savedEntity.Clone();
                retVal.Id = new EntityId(1);
                retVal.EntityLife = EntityLife.Deleted;
                retVal.EntityState = EntityState.Saved;
                retVal.EntityStatus = EntityStatus.Inactive;
                return retVal;
            });

            TEntity loadedEntity1 = process.Get(savedEntity.Id);
            process.Delete(loadedEntity1);

            TEntity loadedEntity2 = process.Get(entity1.Id);

            Assert.That(loadedEntity2.EntityLife, Is.EqualTo(EntityLife.Deleted));
            Assert.That(loadedEntity2.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(loadedEntity2.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That((Object)savedEntity == (Object)loadedEntity2, Is.EqualTo(false));
        }

        [TestCase]
        public virtual void Test_Delete_MultipleEntities()
        {
            TBusinessProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);

            List<TEntity> entitiesToDelete = new List<TEntity> { entity1, entity2, entity3 };

            Repository.Delete(Arg.Any<List<TEntity>>()).Returns(args =>
            {
                List<TEntity> entities = (List<TEntity>)args[0];

                entities.ForEach(fe =>
                {
                    fe.EntityStatus = EntityStatus.Inactive;
                });

                return entities;
            });

            List<TEntity> deletedEntities = process.Delete(entitiesToDelete);

            Assert.That(deletedEntities[0].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(deletedEntities[1].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(deletedEntities[2].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(deletedEntities, Is.EquivalentTo(entitiesToDelete));
        }

        [TestCase]
        public void Test_Get()
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity newEntity = CreateEntity(process);

            TEntity savedEntity = process.Save(newEntity);
            EntityId savedEntityId = savedEntity.Id;

            Thread.Sleep(500);

            TEntity loadedEntity = process.Get(savedEntityId);

            CompareEntityBaseProperties(Repository, savedEntity, loadedEntity);
        }

        [TestCase]
        public void Test_Get_Multiple()
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);

            Int32 entityCounter = 0;
            Repository.Save(Arg.Any<TEntity>()).Returns(args =>
            {
                TEntity retVal = (TEntity)((TEntity)args[0]).Clone();

                retVal.Id = new EntityId(entityCounter++);

                return retVal;
            });

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id };

            Repository.Get(Arg.Any<List<EntityId>>()).Returns(args =>
            {
                List<TEntity> retVal = new List<TEntity>
                {
                    (TEntity)savedEntity1.Clone(),
                    (TEntity)savedEntity2.Clone(),
                    (TEntity)savedEntity3.Clone(),
                };

                return retVal;
            });

            List<TEntity> loadedEntities = process.Get(savedEntityIds).ToList();

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(Repository, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(Repository, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(Repository, savedEntity3, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll()
        {
            TBusinessProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);

            Int32 entityCounter = 0;
            Repository.Save(Arg.Any<TEntity>()).Returns(args =>
            {
                TEntity retVal = (TEntity)((TEntity)args[0]).Clone();

                retVal.Id = new EntityId(entityCounter++);

                return retVal;
            });

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id };

            Repository.GetAllActive().Returns(args =>
            {
                List<TEntity> retVal = new List<TEntity>
                {
                    (TEntity)savedEntity1.Clone(),
                    (TEntity)savedEntity2.Clone(),
                    (TEntity)savedEntity3.Clone(),
                };

                return retVal;
            });

            List<TEntity> loadedEntities = process.GetAll();

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(Repository, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(Repository, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(Repository, savedEntity3, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll_ExcludeDeleted_True()
        {
            const Boolean excludeDeleted = true;
            const Boolean activeOnly = false;

            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);
            TEntity entity4 = CreateEntity(process);

            entity1.EntityStatus = EntityStatus.Inactive;

            List<TEntity> entities = new List<TEntity> { entity2, entity3, entity4 };

            Repository.GetAll(Arg.Is(excludeDeleted), Arg.Is(activeOnly)).Returns(entities);

            List<TEntity> loadedEntities = process.GetAll(excludeDeleted);

            Assert.That(loadedEntities.Count, Is.EqualTo(entities.Count));
            CompareEntityBaseProperties(Repository, entity2, loadedEntities[0]);
            CompareEntityBaseProperties(Repository, entity3, loadedEntities[1]);
            CompareEntityBaseProperties(Repository, entity4, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll_ExcludeDeleted_False()
        {
            const Boolean excludeDeleted = false;
            const Boolean activeOnly = false;

            TBusinessProcess process = CreateBusinessProcess();
            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);
            TEntity entity4 = CreateEntity(process);

            entity1.EntityStatus = EntityStatus.Inactive;

            List<TEntity> entities = new List<TEntity> { entity1, entity2, entity3, entity4 };

            Repository.GetAll(Arg.Is(excludeDeleted), Arg.Is(activeOnly)).Returns(entities);

            List<TEntity> loadedEntities = process.GetAll(excludeDeleted);

            Assert.That(loadedEntities.Count, Is.EqualTo(entities.Count));
            CompareEntityBaseProperties(Repository, entity1, loadedEntities[0]);
            CompareEntityBaseProperties(Repository, entity2, loadedEntities[1]);
            CompareEntityBaseProperties(Repository, entity3, loadedEntities[2]);
            CompareEntityBaseProperties(Repository, entity4, loadedEntities[3]);
        }


        [TestCase]
        public void Test_ExportToCsv()
        {
            TBusinessProcess process = CreateBusinessProcess();

            String exportToCsvPath = Path.Combine(BaseTemporaryOutputsPath, "ExportToCsv");
            DirectoryInfo exportToCsvDirectoryInfo = new DirectoryInfo(exportToCsvPath);
            if (!exportToCsvDirectoryInfo.Exists) { exportToCsvDirectoryInfo.Create(); }

            List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitions();
            List<TEntity> sourceData = new List<TEntity>
            {
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
                CreateEntity(process),
            };

            String actualCsvData = process.ExportToCsv(gridColumnDefinitions, sourceData);

            Encoding encoding = Encoding.UTF8;
            String targetOutputFile = Path.Combine(exportToCsvPath, $"ExportToCsv.{GetType().Name}.csv");
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            fileApi.DeleteFile(targetOutputFile);
            using (TextWriter tw = fileApi.OpenFileForWriting(targetOutputFile, encoding))
            {
                tw.WriteLine(actualCsvData);
            }

            actualCsvData = FixUpStringWithReplacements(actualCsvData);
        }

        [TestCase]
        public void Test_ExportToCsv_NullProperty()
        {
            TBusinessProcess process = CreateBusinessProcess();
            String paramName = "MadeUpPropertyName";
            String entityType = CreateBlankEntity(process).GetType().ToString();
            String errorMessage = $"Cannot find property called '{paramName}' in type {entityType}. {process.GetType()}.ExportToExcel.\r\nParameter name: {paramName}";

            ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
            {
                List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitions();
                List<TEntity> sourceData = new List<TEntity>
                {
                    CreateEntity(process),
                };

                if (gridColumnDefinitions[0] is GridColumnDefinition gcd)
                {
                    gcd.SetDataMemberName(paramName);
                }
                else
                {
                    Assert.Fail($"GridColumnDefinition is not of expected type: {gridColumnDefinitions[0]}");
                }

                _ = process.ExportToCsv(gridColumnDefinitions, sourceData);
            });

            Assert.That(actualException, Is.Not.Null);

            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            Assert.That(actualException.ParamName, Is.EqualTo(paramName));
        }

        [TestCase]
        public void Test_ExportToCsv_Exception_NullGridColumnDefinitions()
        {
            TBusinessProcess process = CreateBusinessProcess();
            String paramName = "gridColumnDefinitions";
            String errorMessage = $"Empty {paramName} passed to {process.GetType()}.ExportToCsv\r\nParameter name: {paramName}";

            ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
            {
                const List<IGridColumnDefinition> gridColumnDefinitions = null;
                List<TEntity> sourceData = new List<TEntity>
                {
                    CreateEntity(process),
                };

                _ = process.ExportToCsv(gridColumnDefinitions, sourceData);
            });

            Assert.That(actualException, Is.Not.Null);

            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            Assert.That(actualException.ParamName, Is.EqualTo(paramName));
        }

        [TestCase]
        public void Test_ExportToCsv_Exception_NullSourceData()
        {
            TBusinessProcess process = CreateBusinessProcess();
            String paramName = "sourceData";
            String errorMessage = $"Empty {paramName} passed to {process.GetType()}.ExportToCsv\r\nParameter name: {paramName}";

            ArgumentNullException actualException = Assert.Throws<ArgumentNullException>(() =>
            {
                List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitions();
                const List<TEntity> sourceData = null;

                _ = process.ExportToCsv(gridColumnDefinitions, sourceData);
            });

            Assert.That(actualException, Is.Not.Null);

            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            Assert.That(actualException.ParamName, Is.EqualTo(paramName));
        }
    }
}
