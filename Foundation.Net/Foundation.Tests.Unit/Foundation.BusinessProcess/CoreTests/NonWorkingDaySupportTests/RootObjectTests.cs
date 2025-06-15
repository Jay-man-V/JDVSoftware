//-----------------------------------------------------------------------
// <copyright file="RootObjectTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;

using NUnit.Framework;

using Newtonsoft.Json;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.NonWorkingDaySupportTests
{
    /// <summary>
    /// Summary description for RootObjectTests
    /// </summary>
    [TestFixture]
    public class RootObjectTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor()
        {
            RootObject rootObject = new RootObject();

            Assert.That(rootObject.EnglandAndWales, Is.EqualTo(null));
            Assert.That(rootObject.Scotland, Is.EqualTo(null));
            Assert.That(rootObject.NorthernIreland, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_Properties()
        {
            UkNation englandAndWales = new UkNation();
            UkNation scotland = new UkNation();
            UkNation northernIreland = new UkNation();

            RootObject rootObject = new RootObject
            {
                EnglandAndWales = englandAndWales,
                Scotland = scotland,
                NorthernIreland = northernIreland,
            };

            Assert.That(rootObject.EnglandAndWales, Is.EqualTo(englandAndWales));
            Assert.That(rootObject.Scotland, Is.EqualTo(scotland));
            Assert.That(rootObject.NorthernIreland, Is.EqualTo(northernIreland));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\bank-holidays.json", @".Support\SampleDocuments\")]
        public void Test_ParseBankHolidayData()
        {
            String sourceFile = @".Support\SampleDocuments\bank-holidays.json";
            String sourceData = File.ReadAllText(sourceFile);

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(sourceData);

            if (rootObject.IsNotNull())
            {
                Debug.WriteLine("England and Wales");
                foreach (BankHolidayEvent holidayEvent in rootObject.EnglandAndWales.Events)
                {
                    Debug.WriteLine($"{holidayEvent.BankHolidayDate.ToString(Formats.DotNet.DateOnly)} - {holidayEvent.DateAsString} - {holidayEvent.Title} - {holidayEvent.Notes}");
                }

                Debug.WriteLine("Scotland");
                foreach (BankHolidayEvent holidayEvent in rootObject.Scotland.Events)
                {
                    Debug.WriteLine($"{holidayEvent.BankHolidayDate.ToString(Formats.DotNet.DateOnly)} - {holidayEvent.DateAsString} - {holidayEvent.Title} - {holidayEvent.Notes}");
                }

                Debug.WriteLine("Northern Ireland");
                foreach (BankHolidayEvent holidayEvent in rootObject.NorthernIreland.Events)
                {
                    Debug.WriteLine($"{holidayEvent.BankHolidayDate.ToString(Formats.DotNet.DateOnly)} - {holidayEvent.DateAsString} - {holidayEvent.Title} - {holidayEvent.Notes}");
                }
            }
        }
    }
}
