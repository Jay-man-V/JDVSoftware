//-----------------------------------------------------------------------
// <copyright file="CommonBusinessProcessSystemTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.System.Support;

namespace Foundation.Tests.System.Foundation.BusinessProcess
{
    /// <summary>
    /// Summary description for CommonBusinessProcessSystemTests
    /// </summary>
    [TestFixture]
    public abstract class CommonBusinessProcessSystemTests<TEntity, TProcess, TDataAccess> : DataAccessSystemTestBase
        where TEntity : IFoundationModel
        where TProcess : ICommonBusinessProcess<TEntity>
        where TDataAccess : IFoundationModelRepository<TEntity>
    {
        protected abstract TProcess CreateBusinessProcess();
        protected abstract TProcess CreateBusinessProcess(IDateTimeService dateTimeService);
        protected abstract TEntity CreateEntity(TProcess process);
        protected abstract void UpdateEntityProperties(TEntity entity);
        protected abstract void CheckBaseClassProperties(TProcess process);
        protected abstract void CheckBlankEntry(TEntity entity);
        protected abstract void CheckAllEntry(TProcess process, TEntity entity);
        protected abstract void CheckNoneEntry(TProcess process, TEntity entity);
        protected abstract void CompareEntityProperties(TEntity entity1, TEntity entity2);

        protected TDataAccess CreateDataAccess()
        {
            TDataAccess retVal = Core.Core.Instance.Container.Get<TDataAccess>();

            return retVal;
        }

        protected void CompareEntityBaseProperties(TDataAccess dataAccess, TEntity entity1, TEntity entity2)
        {
            Assert.That(entity2.Id, Is.EqualTo(entity1.Id));
            Assert.That(entity2.CreatedByUserProfileId, Is.EqualTo(entity1.CreatedByUserProfileId));
            Assert.That(entity2.LastUpdatedByUserProfileId, Is.EqualTo(entity1.LastUpdatedByUserProfileId));
            Assert.That(entity2.CreatedOn, Is.EqualTo(entity1.CreatedOn));
            Assert.That(entity2.LastUpdatedOn, Is.EqualTo(entity1.LastUpdatedOn));
            Assert.That(entity2.EntityState, Is.EqualTo(entity1.EntityState));
            Assert.That(entity2.EntityLife, Is.EqualTo(entity1.EntityLife));
            Assert.That(entity2.EntityStatus, Is.EqualTo(entity1.EntityStatus));
            Assert.That(entity2.StatusId, Is.EqualTo(entity1.StatusId));
            Assert.That(entity2.Timestamp, Is.EquivalentTo(entity1.Timestamp));

            if (dataAccess.HasValidityPeriodColumns)
            {
                Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
                Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));
            }
        }

        protected override void StartTest()
        {
            DateTimeService = Substitute.For<IDateTimeService>();

            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(SystemDateTime);
            DateTimeService.SystemDateTimeNow.Returns(SystemDateTimeMs);
        }

        [TestCase]
        public void Test_BaseClassProperties()
        {
            TProcess process = CreateBusinessProcess(DateTimeService);

            Assert.That(process.NullId, Is.EqualTo(new EntityId(-1)));
            Assert.That(process.AllId, Is.EqualTo(new EntityId(-1)));
            Assert.That(process.AllText, Is.EqualTo("<All>"));
            Assert.That(process.NoneId, Is.EqualTo(new EntityId(-2)));
            Assert.That(process.NoneText, Is.EqualTo("<None>"));
            Assert.That(process.DefaultValidFromDateTime, Is.EqualTo(SystemDateTimeMs));
            Assert.That(process.DefaultValidToDateTime, Is.EqualTo(new DateTime(2199, 12, 31, 23, 59, 59, DateTimeKind.Utc)));

            CheckBaseClassProperties(process);
        }

        [TestCase]
        public void Test_GetBlankEntry()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = process.GetBlankEntry();

            Assert.That(entity.Id, Is.EqualTo(process.NullId));
            CheckBlankEntry(entity);
        }

        [TestCase]
        public void Test_GetAllEntry()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = process.GetAllEntry();

            Assert.That(entity.Id, Is.EqualTo(process.AllId));
            CheckAllEntry(process, entity);
        }

        [TestCase]
        public void Test_GetNoneEntry()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = process.GetNoneEntry();

            Assert.That(entity.Id, Is.EqualTo(process.NoneId));
            CheckNoneEntry(process, entity);
        }

        [TestCase]
        public void Test_CanViewRecord_True()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = CreateEntity(process);

            Boolean canViewRecord = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanViewRecord_True_EntityNull()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            Boolean canViewRecord = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanViewRecord_SystemAdministrator_NotReporter()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            RemoveRoleFromLoggedOnUser(ApplicationRole.Reporter);

            Boolean canViewRecord = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanViewRecord_NotSystemAdministrator_Reporter()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = false;
            RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);

            Boolean canViewRecord = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanViewRecord_NotSystemAdministrator_NotReporter()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = false;
            RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);
            RemoveRoleFromLoggedOnUser(ApplicationRole.Creator);

            Boolean canViewRecord = process.CanViewRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_CanAddRecord_True()
        {
            TProcess process = CreateBusinessProcess();

            Boolean canAddRecord = process.CanAddRecord(CoreInstance.CurrentLoggedOnUser.UserProfile);

            Assert.That(canAddRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanAddRecord_SystemAdministrator_NotCreator()
        {
            TProcess process = CreateBusinessProcess();

            RemoveRoleFromLoggedOnUser(ApplicationRole.Creator);

            Boolean canAddRecord = process.CanAddRecord(CoreInstance.CurrentLoggedOnUser.UserProfile);

            Assert.That(canAddRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanAddRecord_NotSystemAdministrator_Creator()
        {
            TProcess process = CreateBusinessProcess();

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = false;
            RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);

            Boolean canAddRecord = process.CanAddRecord(CoreInstance.CurrentLoggedOnUser.UserProfile);

            Assert.That(canAddRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanAddRecord_NotSystemAdministrator_NotCreator()
        {
            TProcess process = CreateBusinessProcess();

            CoreInstance.CurrentLoggedOnUser.UserProfile.IsSystemSupport = false;
            RemoveRoleFromLoggedOnUser(ApplicationRole.SystemAdministrator);

            RemoveRoleFromLoggedOnUser(ApplicationRole.Creator);

            Boolean canAddRecord = process.CanAddRecord(CoreInstance.CurrentLoggedOnUser.UserProfile);

            Assert.That(canAddRecord, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_CanEditRecord_True()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = CreateEntity(process);

            Boolean canEditRecord = process.CanEditRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canEditRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanEditRecord_True_EntityNull()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            Boolean canViewRecord = process.CanEditRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanDeleteRecord_True()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = CreateEntity(process);

            Boolean canDeleteRecord = process.CanDeleteRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canDeleteRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CanDeleteRecord_True_EntityNull()
        {
            TProcess process = CreateBusinessProcess();
            TEntity entity = default;

            Boolean canViewRecord = process.CanDeleteRecord(CoreInstance.CurrentLoggedOnUser.UserProfile, entity);

            Assert.That(canViewRecord, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Save_Entity()
        {
            TProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity savedEntity1 = process.Save(entity1);

            Assert.That(savedEntity1, Is.Not.EqualTo(null));
            Assert.That(savedEntity1.Id.ToInteger() > 0);

            CompareEntityProperties(entity1, savedEntity1);
        }

        [TestCase]
        public virtual void Test_Update_Entity()
        {
            TProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity savedEntity1 = process.Save(entity1);

            UpdateEntityProperties(entity1);

            TEntity savedEntity2 = process.Save(savedEntity1);

            Assert.That(savedEntity1, Is.Not.EqualTo(null));
            Assert.That(savedEntity2.Id.ToInteger() > 0);

            CompareEntityProperties(savedEntity1, savedEntity2);
        }

        [TestCase]
        public void Test_Save_MultipleEntities()
        {
            TProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);

            List<TEntity> entitiesToSave = new List<TEntity> { entity1, entity2, entity3 };
            List<TEntity> savedEntities = process.Save(entitiesToSave);

            Assert.That(savedEntities, Is.Not.EqualTo(null));
            Assert.That(savedEntities.Count, Is.EqualTo(entitiesToSave.Count));
            Assert.That(savedEntities[0].Id.ToInteger() > 0);
            Assert.That(savedEntities[1].Id.ToInteger() > 0);
            Assert.That(savedEntities[2].Id.ToInteger() > 0);

            CompareEntityProperties(entitiesToSave[0], savedEntities[0]);
            CompareEntityProperties(entitiesToSave[1], savedEntities[1]);
            CompareEntityProperties(entitiesToSave[2], savedEntities[2]);
        }

        [TestCase]
        public virtual void Test_Delete_Entity()
        {
            TProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity savedEntity = process.Save(entity1);
            TEntity loadedEntity1 = process.Get(savedEntity.Id);
            process.Delete(loadedEntity1);

            TEntity loadedEntity2 = process.Get(entity1.Id);

            Assert.That(loadedEntity2.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
        }

        [TestCase]
        public virtual void Test_Delete_MultipleEntities()
        {
            TProcess process = CreateBusinessProcess();

            TEntity entity1 = CreateEntity(process);
            TEntity entity2 = CreateEntity(process);
            TEntity entity3 = CreateEntity(process);

            List<TEntity> entitiesToSave = new List<TEntity> { entity1, entity2, entity3 };
            List<TEntity> savedEntities = process.Save(entitiesToSave);
            process.Delete(savedEntities);

            List<TEntity> loadedEntities = new List<TEntity>();
            savedEntities.ForEach(e =>
            {
                TEntity loadedEntity = process.Get(e.Id);
                loadedEntities.Add(loadedEntity);
            });

            Assert.That(loadedEntities[0].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(loadedEntities[1].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(loadedEntities[2].EntityStatus, Is.EqualTo(EntityStatus.Inactive));
        }

        [TestCase]
        public void Test_Get()
        {
            TDataAccess dataAccess = CreateDataAccess();
            TProcess process = CreateBusinessProcess();
            TEntity newEntity = CreateEntity(process);

            TEntity savedEntity = process.Save(newEntity);
            EntityId savedEntityId = savedEntity.Id;

            Thread.Sleep(500);

            TEntity loadedEntity = process.Get(savedEntityId);

            CompareEntityBaseProperties(dataAccess, savedEntity, loadedEntity);
        }

        [TestCase]
        public void Test_Get_Multiple()
        {
            TDataAccess dataAccess = CreateDataAccess();
            TProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id };

            Thread.Sleep(500);

            List<TEntity> loadedEntities = process.Get(savedEntityIds).ToList();

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(dataAccess, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(dataAccess, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(dataAccess, savedEntity3, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll()
        {
            TDataAccess dataAccess = CreateDataAccess();
            dataAccess.DeleteAll();

            TProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id };

            Thread.Sleep(500);

            List<TEntity> loadedEntities = process.GetAll();

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(dataAccess, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(dataAccess, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(dataAccess, savedEntity3, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll_ExcludeDeleted_True()
        {
            const Boolean excludeDeleted = true;
            TDataAccess dataAccess = CreateDataAccess();
            dataAccess.DeleteAll();

            TProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);
            TEntity newEntity4 = CreateEntity(process);

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);
            TEntity savedEntity4 = process.Save(newEntity4);

            process.Delete(savedEntity4);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id };

            Thread.Sleep(500);

            List<TEntity> loadedEntities = process.GetAll(excludeDeleted);

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(dataAccess, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(dataAccess, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(dataAccess, savedEntity3, loadedEntities[2]);
        }

        [TestCase]
        public virtual void Test_GetAll_ExcludeDeleted_False()
        {
            const Boolean excludeDeleted = false;
            TDataAccess dataAccess = CreateDataAccess();
            dataAccess.DeleteAll();

            TProcess process = CreateBusinessProcess();
            TEntity newEntity1 = CreateEntity(process);
            TEntity newEntity2 = CreateEntity(process);
            TEntity newEntity3 = CreateEntity(process);
            TEntity newEntity4 = CreateEntity(process);

            TEntity savedEntity1 = process.Save(newEntity1);
            TEntity savedEntity2 = process.Save(newEntity2);
            TEntity savedEntity3 = process.Save(newEntity3);
            TEntity savedEntity4 = process.Save(newEntity4);

            TEntity deletedEntity = process.Delete(savedEntity4);

            List<EntityId> savedEntityIds = new List<EntityId> { savedEntity1.Id, savedEntity2.Id, savedEntity3.Id, savedEntity4.Id };

            Thread.Sleep(500);

            List<TEntity> loadedEntities = process.GetAll(excludeDeleted);

            Assert.That(loadedEntities.Count, Is.EqualTo(savedEntityIds.Count));
            CompareEntityBaseProperties(dataAccess, savedEntity1, loadedEntities[0]);
            CompareEntityBaseProperties(dataAccess, savedEntity2, loadedEntities[1]);
            CompareEntityBaseProperties(dataAccess, savedEntity3, loadedEntities[2]);
            CompareEntityBaseProperties(dataAccess, deletedEntity, loadedEntities[3]);
        }
    }
}
