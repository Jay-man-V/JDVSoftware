//-----------------------------------------------------------------------
// <copyright file="CurrencyProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for CurrencyProcessTests
    /// </summary>
    [TestFixture]
    public class CurrencyProcessTests : CommonBusinessProcessTestBaseClass<ICurrency, ICurrencyProcess, ICurrencyRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 13;
        protected override String ExpectedScreenTitle => "Currencies";
        protected override String ExpectedStatusBarText => "Number of Currencies:";

        protected override string ExpectedComboBoxDisplayMember => FDC.Currency.IsoCode;

        protected override ICurrencyRepository CreateRepository()
        {
            ICurrencyRepository dataAccess = Substitute.For<ICurrencyRepository>();

            return dataAccess;
        }

        protected override ICurrencyProcess CreateBusinessProcess()
        {
            ICurrencyProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ICurrencyProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ICurrencyProcess process = new CurrencyProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override ICurrency CreateBlankEntity(ICurrencyProcess process)
        {
            ICurrency retVal = CoreInstance.Container.Get<ICurrency>();

            return retVal;
        }

        protected override ICurrency CreateEntity(ICurrencyProcess process)
        {
            ICurrency retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.IsoCode = Guid.NewGuid().ToString();
            retVal.IsoNumber = Guid.NewGuid().ToString();
            retVal.Symbol = Guid.NewGuid().ToString();
            retVal.NumberToBasic = 10;
            retVal.PrefixSymbol = true;

            return retVal;
        }

        protected override void CheckBlankEntry(ICurrency entity)
        {
            Assert.That(entity.IsoCode, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ICurrency entity)
        {
            Assert.That(entity.IsoCode, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ICurrency entity)
        {
            Assert.That(entity.IsoCode, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ICurrency entity1, ICurrency entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.IsoCode, Is.EqualTo(entity1.IsoCode));
            Assert.That(entity2.IsoNumber, Is.EqualTo(entity1.IsoNumber));
            Assert.That(entity2.Symbol, Is.EqualTo(entity1.Symbol));
            Assert.That(entity2.NumberToBasic, Is.EqualTo(entity1.NumberToBasic));
            Assert.That(entity2.PrefixSymbol, Is.EqualTo(entity1.PrefixSymbol));
        }

        protected override void UpdateEntityProperties(ICurrency entity)
        {
            entity.Name += "Updated";
            entity.IsoCode += "Updated";
        }
    }
}
