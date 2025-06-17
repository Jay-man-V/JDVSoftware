//-----------------------------------------------------------------------
// <copyright file="ImageTypeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ImageTypeProcessTests
    /// </summary>
    [TestFixture]
    public class ImageTypeProcessTests : CommonBusinessProcessTestBaseClass<IImageType, IImageTypeProcess, IImageTypeRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Image Types";
        protected override String ExpectedStatusBarText => "Number of Image Types:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ImageType.Name;

        protected override IImageTypeRepository CreateDataAccess()
        {
            IImageTypeRepository dataAccess = Substitute.For<IImageTypeRepository>();

            return dataAccess;
        }

        protected override IImageTypeProcess CreateBusinessProcess()
        {
            IImageTypeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IImageTypeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IImageTypeProcess process = new ImageTypeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IImageType CreateBlankEntity(IImageTypeProcess process)
        {
            IImageType retVal = CoreInstance.Container.Get<IImageType>();

            return retVal;
        }

        protected override IImageType CreateEntity(IImageTypeProcess process)
        {
            IImageType retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.FileExtension = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IImageType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IImageType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IImageType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IImageType entity1, IImageType entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.FileExtension, Is.EqualTo(entity1.FileExtension));
        }

        protected override void UpdateEntityProperties(IImageType entity)
        {
            entity.Name += "Updated";
            entity.FileExtension += "Updated";
        }
    }
}
