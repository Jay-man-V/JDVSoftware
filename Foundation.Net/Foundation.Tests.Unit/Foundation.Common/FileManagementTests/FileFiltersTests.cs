//-----------------------------------------------------------------------
// <copyright file="FileFiltersTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ConstantsTests
{
    /// <summary>
    /// The File Filters Tests class
    /// </summary>
    [TestFixture]
    public class FileFiltersTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            Type thisType = this.GetType();
            MethodInfo[] testMethods = thisType.GetMethods();
            Int32 testMethodCount = testMethods.Count(m => m.Name.StartsWith("Test_"));

            // This test exists to ensure all the Properties are tested/checked in the next test
            Type theType = typeof(FileFilters);
            PropertyInfo[] propertyInfos = theType.GetProperties();

            Assert.That(propertyInfos.Length, Is.EqualTo(testMethodCount - 1));

            Int32 index = 0;
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FileFilters.AllFiles)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FileFilters.TextFiles)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FileFilters.CsvFiles)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FileFilters.ExcelFiles)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FileFilters.WordFiles)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        [TestCase]
        public void Test_AllFiles()
        {
            String expected = "All Files (*.*)|*.*";
            String actual = FileFilters.AllFiles;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_TextFiles()
        {
            String expected = "All Files (*.*)|*.*|Text Files (*.txt)|*.txt";
            String actual = FileFilters.TextFiles;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_CsvFiles()
        {
            String expected = "All Files (*.*)|*.*|Comma Separated Values Files (Csv) (*.csv)|*.csv";
            String actual = FileFilters.CsvFiles;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_ExcelFiles()
        {
            String expected = "All Files (*.*)|*.*|Excel Files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx|Excel Files (*.xlsm)|*.xlsm";
            String actual = FileFilters.ExcelFiles;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_WordFiles()
        {
            String expected = "All Files (*.*)|*.*|Word Files (*.doc)|*.doc|Word Files (*.docx)|*.docx|Word Files (*.docm)|*.docm";
            String actual = FileFilters.WordFiles;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
