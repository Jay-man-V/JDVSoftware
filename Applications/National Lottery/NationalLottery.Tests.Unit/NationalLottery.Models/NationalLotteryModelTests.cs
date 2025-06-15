//-----------------------------------------------------------------------
// <copyright file="NationalLotteryModelTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using NUnit.Framework;

using NationalLottery.UnitTests.Mocks;
using NationalLottery.UnitTests.Support;

using Foundation.Common;
using Foundation.Core;
using Foundation.Interfaces;

using NationalLottery.Interfaces;

namespace NationalLottery.Tests.Unit.NationalLottery.Models
{
    /// <summary>
    /// Summary description for NationalLotteryModelTests
    /// </summary>
    [TestFixture]
    public class NationalLotteryModelTests : NLUnitTestBase
    {
        private String InitialEntityNameValue => "Initial entity name";
        private String UpdatedEntityNameValue => "Updated entity name";

        [TestCase]
        public void Test_Constructor_1()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();

            Assert.That(nationalLotteryModel, Is.Not.EqualTo(null));

            Type[] allInterfaces = nationalLotteryModel.GetType().GetInterfaces();

            Int32 index = 0;
            Assert.That(nationalLotteryModel, Is.InstanceOf<ICloneable>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INotifyPropertyChanged>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INotifyPropertyChanging>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationModel>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationObjectId>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationModelTracking>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IChangeTracking>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IMockNationalLotteryModel>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INationalLotteryModel>()); index++;

            Assert.That(index, Is.EqualTo(allInterfaces.Length));
        }

        [TestCase]
        public void Test_Constructor_2()
        {
            EntityId expectedEntityId = new EntityId(123);
            Object nationalLotteryModel = new MockNationalLotteryModel(123);

            Assert.That(nationalLotteryModel, Is.Not.EqualTo(null));
            Assert.That(((INationalLotteryModel)nationalLotteryModel).Id, Is.EqualTo(expectedEntityId));

            Type[] allInterfaces = nationalLotteryModel.GetType().GetInterfaces();

            Int32 index = 0;
            Assert.That(nationalLotteryModel, Is.InstanceOf<ICloneable>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INotifyPropertyChanged>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INotifyPropertyChanging>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationModel>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationObjectId>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IFoundationModelTracking>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IChangeTracking>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<IMockNationalLotteryModel>()); index++;
            Assert.That(nationalLotteryModel, Is.InstanceOf<INationalLotteryModel>()); index++;

            Assert.That(index, Is.EqualTo(allInterfaces.Length));
        }

        [TestCase]
        public void Test_InitialState()
        {
            INationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<INationalLotteryModel>();

            EntityId statusId = nationalLotteryModel.StatusId;
            Assert.That(statusId.TheEntityId, Is.EqualTo(0));

            EntityStatus entityStatus = nationalLotteryModel.EntityStatus;
            Assert.That(entityStatus, Is.EqualTo(EntityStatus.Active));

            EntityLife entityLife = nationalLotteryModel.EntityLife;
            Assert.That(entityLife, Is.EqualTo(EntityLife.Created));

            EntityState entityState = nationalLotteryModel.EntityState;
            Assert.That(entityState, Is.EqualTo(EntityState.Dirty));
        }

        [TestCase]
        public void Test_SetProperties()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            MockNationalLotteryModel mockNationalLotteryModel = nationalLotteryModel as MockNationalLotteryModel;

            mockNationalLotteryModel.FoundationPropertyChanged += (o, e) => { Assert.Fail("Method should not be called"); };
            mockNationalLotteryModel.FoundationPropertyChanging += (o, e) => { Assert.Fail("Method should not be called"); };
            nationalLotteryModel.PropertyChanged += (o, e) => { Assert.Fail("Method should not be called"); };
            nationalLotteryModel.PropertyChanging += (o, e) => { Assert.Fail("Method should not be called"); };

            nationalLotteryModel.Initialising = true;
            Assert.That(nationalLotteryModel.Initialising, Is.EqualTo(true));

            nationalLotteryModel.Code = "New Code";
            nationalLotteryModel.Description = "New Description";
            nationalLotteryModel.Name = "New Name";
            nationalLotteryModel.Initialising = false;
            Assert.That(nationalLotteryModel.Initialising, Is.EqualTo(false));

            Assert.That(nationalLotteryModel.Code, Is.EqualTo("New Code"));
            Assert.That(nationalLotteryModel.Description, Is.EqualTo("New Description"));
            Assert.That(nationalLotteryModel.Name, Is.EqualTo("New Name"));
        }

        [TestCase]
        public void Test_Properties()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();

            nationalLotteryModel.Initialising = true;
            nationalLotteryModel.Code = "New Code";
            nationalLotteryModel.Description = "New Description";
            nationalLotteryModel.Name = "New Name";
            nationalLotteryModel.ImagePicture = new Bitmap(1, 1);
            nationalLotteryModel.Initialising = false;

            Assert.That(nationalLotteryModel.Code, Is.EqualTo("New Code"));
            Assert.That(nationalLotteryModel.Description, Is.EqualTo("New Description"));
            Assert.That(nationalLotteryModel.Name, Is.EqualTo("New Name"));
        }

        [TestCase]
        public void Test_Clone()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();

            nationalLotteryModel.Initialising = true;
            nationalLotteryModel.Code = "New Code";
            nationalLotteryModel.Description = "New Description";
            nationalLotteryModel.Name = "New Name";
            nationalLotteryModel.ImagePicture = new Bitmap(1, 1);
            nationalLotteryModel.Initialising = false;

            MockNationalLotteryModel clonedNationalLotteryModel = nationalLotteryModel.Clone() as MockNationalLotteryModel;

            Assert.That(clonedNationalLotteryModel, Is.Not.EqualTo(nationalLotteryModel));
            Assert.That(clonedNationalLotteryModel.Code, Is.EqualTo(nationalLotteryModel.Code));
            Assert.That(clonedNationalLotteryModel.Description, Is.EqualTo(nationalLotteryModel.Description));
            Assert.That(clonedNationalLotteryModel.Name, Is.EqualTo(nationalLotteryModel.Name));
            Assert.That(clonedNationalLotteryModel.ImagePicture, Is.Not.EqualTo(nationalLotteryModel.ImagePicture));
            //Assert.That(nationalLotteryModel.ImagePicture.CompareAsByteArray(clonedNationalLotteryModel.ImagePicture));
        }

        [TestCase]
        public void Test_ChangedPropertiesDictionary()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            MockNationalLotteryModel mockNationalLotteryModel = nationalLotteryModel as MockNationalLotteryModel;

            String[] changedProperties = { "Code", "Description", "Name" };
            String[] changedValues1 = { "Code1", "Description1", "Name1" };
            String[] changedValues2 = { "Code2", "Description1", "Name2" };

            nationalLotteryModel.Code = "Code1";
            nationalLotteryModel.Description = "Description1";
            nationalLotteryModel.Name = "Name1";

            IReadOnlyList<FoundationProperty> changedProperties1 = mockNationalLotteryModel.ChangedProperties;
            Assert.That(changedProperties1.Count, Is.EqualTo(3));

            for (Int32 counter = 0; counter < changedProperties.Length; counter++)
            {
                Assert.That(changedProperties1[counter].PropertyName, Is.EqualTo(changedProperties[counter]));
                Assert.That(changedProperties1[counter].NewValue, Is.EqualTo(changedValues1[counter]));
                Assert.That(changedProperties1[counter].OldValue, Is.EqualTo(null));
            }

            nationalLotteryModel.Code = "Code2";
            nationalLotteryModel.Name = "Name2";

            IReadOnlyList<FoundationProperty> changedProperties2 = mockNationalLotteryModel.ChangedProperties;
            Assert.That(changedProperties2.Count, Is.EqualTo(changedProperties.Length));

            for (Int32 counter = 0; counter < changedProperties.Length; counter++)
            {
                Assert.That(changedProperties2[counter].PropertyName, Is.EqualTo(changedProperties[counter]));
                Assert.That(changedProperties2[counter].NewValue, Is.EqualTo(changedValues2[counter]));
                Assert.That(changedProperties1[counter].OldValue, Is.EqualTo(null));
            }
        }

        [TestCase]
        public void Test_ChangedPropertiesDictionaryWithOldValues()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            MockNationalLotteryModel mockNationalLotteryModel = nationalLotteryModel as MockNationalLotteryModel;

            String[] changedProperties = { "Code", "Name" };
            String[] changedValues1 = { "Code2", "Name2" };

            nationalLotteryModel.Initialising = true;
            nationalLotteryModel.Code = "Code1";
            nationalLotteryModel.Description = "Description1";
            nationalLotteryModel.Name = "Name1";
            nationalLotteryModel.Initialising = false;

            nationalLotteryModel.Code = "Code2";
            nationalLotteryModel.Name = "Name2";

            IReadOnlyList<FoundationProperty> changedProperties1 = mockNationalLotteryModel.ChangedProperties;
            Assert.That(changedProperties1.Count, Is.EqualTo(changedProperties.Length));

            for (Int32 counter = 0; counter < changedProperties.Length; counter++)
            {
                Assert.That(changedProperties1[counter].PropertyName, Is.EqualTo(changedProperties[counter]));
                Assert.That(changedProperties1[counter].NewValue, Is.EqualTo(changedValues1[counter]));
            }
        }

        [TestCase]
        public void Test_PropertyUpdating_1()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();

            Assert.That(nationalLotteryModel.Name, Is.EqualTo(null));
            Assert.That(nationalLotteryModel.EntityState, Is.EqualTo(EntityState.Dirty));
            Assert.That(nationalLotteryModel.EntityLife, Is.EqualTo(EntityLife.Created));

            nationalLotteryModel.Name = InitialEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(InitialEntityNameValue));

            nationalLotteryModel.Name = UpdatedEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(UpdatedEntityNameValue));

            Assert.That(nationalLotteryModel.EntityState, Is.EqualTo(EntityState.Dirty));
            Assert.That(nationalLotteryModel.EntityLife, Is.EqualTo(EntityLife.Created));
        }

        [TestCase]
        public void Test_PropertyUpdating_2()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            nationalLotteryModel.EntityState = EntityState.Saved;
            nationalLotteryModel.EntityLife = EntityLife.Loaded;

            Assert.That(nationalLotteryModel.Name, Is.EqualTo(null));
            Assert.That(nationalLotteryModel.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(nationalLotteryModel.EntityLife, Is.EqualTo(EntityLife.Loaded));

            nationalLotteryModel.Name = InitialEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(InitialEntityNameValue));

            nationalLotteryModel.Name = UpdatedEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(UpdatedEntityNameValue));

            Assert.That(nationalLotteryModel.EntityState, Is.EqualTo(EntityState.Dirty));
            Assert.That(nationalLotteryModel.EntityLife, Is.EqualTo(EntityLife.Updated));
        }

        [TestCase]
        public void Test_PropertyEventHandling_Success()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            MockNationalLotteryModel mockNationalLotteryModel = nationalLotteryModel as MockNationalLotteryModel;

            Assert.That(nationalLotteryModel.Name, Is.EqualTo(null));

            nationalLotteryModel.Name = InitialEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(InitialEntityNameValue));

            mockNationalLotteryModel.FoundationPropertyChanging += (sender, args) =>
            {
                Assert.That(sender, Is.Not.EqualTo(null));
                Assert.That(args.PropertyName, Is.EqualTo("Name"));
                Assert.That(InitialEntityNameValue, Is.EqualTo(args.OldValue.ToString()));
                Assert.That(UpdatedEntityNameValue, Is.EqualTo(args.NewValue.ToString()));
            };

            mockNationalLotteryModel.FoundationPropertyChanged += (sender, args) =>
            {
                Assert.That(sender, Is.Not.EqualTo(null));
                Assert.That(args.PropertyName, Is.EqualTo("Name"));

                Assert.That(InitialEntityNameValue, Is.EqualTo(args.OldValue.ToString()));
                Assert.That(UpdatedEntityNameValue, Is.EqualTo(args.NewValue.ToString()));
            };

            nationalLotteryModel.PropertyChanged += (o, e) => { Assert.That(e.PropertyName, Is.EqualTo("Name")); };
            nationalLotteryModel.PropertyChanging += (o, e) => { Assert.That(e.PropertyName, Is.EqualTo("Name")); };

            nationalLotteryModel.Name = UpdatedEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(UpdatedEntityNameValue));
        }

        [TestCase]
        public void Test_PropertyEventHandling_Cancel()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();
            MockNationalLotteryModel mockNationalLotteryModel = nationalLotteryModel as MockNationalLotteryModel;

            Assert.That(nationalLotteryModel.Name, Is.EqualTo(null));

            nationalLotteryModel.Name = InitialEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(InitialEntityNameValue));

            mockNationalLotteryModel.FoundationPropertyChanging += (sender, args) =>
            {
                Assert.That(sender, Is.Not.EqualTo(null));
                Assert.That(InitialEntityNameValue, Is.EqualTo(args.OldValue.ToString()));
                Assert.That(UpdatedEntityNameValue, Is.EqualTo(args.NewValue.ToString()));

                args.Cancel = true;
            };

            mockNationalLotteryModel.FoundationPropertyChanged += (o, e) => { Assert.Fail("nationalLotteryModel2_FoundationPropertyChanged should not have been called"); };

            nationalLotteryModel.Name = UpdatedEntityNameValue;
            Assert.That(nationalLotteryModel.Name, Is.EqualTo(InitialEntityNameValue));
        }

        [TestCase]
        public void Test_Entity_AcceptChanges()
        {
            IMockNationalLotteryModel nationalLotteryModel = Core.Instance.Container.Get<IMockNationalLotteryModel>();

            Assert.That(nationalLotteryModel.IsChanged, Is.EqualTo(false));

            nationalLotteryModel.Name = "Test Name";
            Assert.That(nationalLotteryModel.IsChanged, Is.EqualTo(true));

            nationalLotteryModel.AcceptChanges();
            Assert.That(nationalLotteryModel.IsChanged, Is.EqualTo(false));
            Assert.That(nationalLotteryModel.EntityLife, Is.EqualTo(EntityLife.Loaded));
            Assert.That(nationalLotteryModel.EntityState, Is.EqualTo(EntityState.Saved));
        }
    }
}
