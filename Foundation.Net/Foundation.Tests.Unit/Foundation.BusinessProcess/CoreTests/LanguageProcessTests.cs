//-----------------------------------------------------------------------
// <copyright file="LanguageProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for LanguageProcessTests
    /// </summary>
    [TestFixture]
    public class LanguageProcessTests : CommonBusinessProcessTestBaseClass<ILanguage, ILanguageProcess, ILanguageRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Languages";
        protected override String ExpectedStatusBarText => "Number of Languages:";

        protected override String ExpectedComboBoxDisplayMember => FDC.Language.EnglishName;

        protected override ILanguageRepository CreateDataAccess()
        {
            ILanguageRepository dataAccess = Substitute.For<ILanguageRepository>();

            return dataAccess;
        }

        protected override ILanguageProcess CreateBusinessProcess()
        {
            ILanguageProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ILanguageProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ILanguageProcess process = new LanguageProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override ILanguage CreateBlankEntity(ILanguageProcess process)
        {
            ILanguage retVal = CoreInstance.Container.Get<ILanguage>();

            return retVal;
        }

        protected override ILanguage CreateEntity(ILanguageProcess process)
        {
            ILanguage retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.EnglishName = Guid.NewGuid().ToString();
            retVal.NativeName = Guid.NewGuid().ToString();
            retVal.CultureCode = Guid.NewGuid().ToString();
            retVal.UiCultureCode = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(ILanguage entity)
        {
            Assert.That(entity.EnglishName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ILanguage entity)
        {
            Assert.That(entity.EnglishName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ILanguage entity)
        {
            Assert.That(entity.EnglishName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ILanguage entity1, ILanguage entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.EnglishName, Is.EqualTo(entity1.EnglishName));
            Assert.That(entity2.NativeName, Is.EqualTo(entity1.NativeName));
            Assert.That(entity2.CultureCode, Is.EqualTo(entity1.CultureCode));
            Assert.That(entity2.UiCultureCode, Is.EqualTo(entity1.UiCultureCode));
        }

        protected override void UpdateEntityProperties(ILanguage entity)
        {
            entity.EnglishName += "Updated";
            entity.NativeName += "Updated";
            entity.CultureCode = "Updated";
            entity.UiCultureCode = "Updated";
        }
    }
}
