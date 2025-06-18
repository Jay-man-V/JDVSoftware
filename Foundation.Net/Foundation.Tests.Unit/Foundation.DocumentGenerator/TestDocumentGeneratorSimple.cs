//-----------------------------------------------------------------------
// <copyright file="TestDocumentGenerator_Simple.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

using NSubstitute;

using Foundation.DocumentGenerator;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.DocumentGenerator
{
    /// <summary>
    /// The unit test template class
    /// </summary>
    [TestFixture]
    public class TestDocumentGeneratorSimple : UnitTestBase
    {
        private IMockFoundationModelProcess CreateBusinessProcess()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
            IMockFoundationModelRepository repository = Substitute.For<IMockFoundationModelRepository>();

            IMockFoundationModelProcess process = new MockFoundationModelProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, repository, StatusRepository, UserProfileRepository);

            return process;
        }

        private List<IFoundationModel> CreateTestData(Int32 numberToCreate)
        {
            List<IFoundationModel> retVal = new List<IFoundationModel>();

            for (Int32 counter = 0; counter < numberToCreate; counter++)
            {
                IMockFoundationModel mockFoundationModel = new MockFoundationModel(counter)
                {
                    Count = counter,
                    IsClosed = false,
                    IsOpen = true,
                    Quantity = counter * 3,
                    UnitPrice = (Decimal)Math.PI * counter,
                    Name = $"Name {counter}.",
                    Code = $"Code {counter}.",
                    Description = $"Description {counter}.",
                    Duration = new TimeSpan(counter, 0, counter, 12, 456),
                    ExecutionTime = SystemDateTimeMs,
                };

                retVal.Add(mockFoundationModel);
            }
            return retVal;
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_GenerateSpreadsheet()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Debug.WriteLine(DateTime.Now);
            IList<IFoundationModel> sourceData = CreateTestData(100);
            IMockFoundationModelProcess process = CreateBusinessProcess();
            List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitions();

            String outputFile = @"D:\Sample.xlsx";

            SpreadsheetGenerator g = new SpreadsheetGenerator();
            g.ExportData(sourceData, gridColumnDefinitions, outputFile);

            sw.Stop();
            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
