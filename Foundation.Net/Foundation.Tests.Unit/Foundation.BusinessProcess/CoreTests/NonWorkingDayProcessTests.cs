//-----------------------------------------------------------------------
// <copyright file="NonWorkingDayProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for NonWorkingDayProcessTests
    /// </summary>
    [TestFixture]
    public class NonWorkingDayProcessTests : CommonBusinessProcessTestBaseClass<INonWorkingDay, INonWorkingDayProcess, INonWorkingDayRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Non-Working Days";
        protected override String ExpectedStatusBarText => "Number of Non-Working Days:";

        protected override Boolean ExpectedHasOptionalAction1 => true;
        protected override string ExpectedAction1Name => "Refresh from Government source";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Country:";
        protected override String ExpectedFilter1DisplayMemberPath => FDC.Country.AbbreviatedName;

        protected override Boolean ExpectedHasOptionalDropDownParameter2 => true;
        protected override String ExpectedFilter2Name => "Year:";
        protected override String ExpectedFilter2DisplayMemberPath => ".";
        protected override string ExpectedFilter2ValueMemberPath => ".";


        protected override Boolean ExpectedHasOptionalDropDownParameter3 => true;
        protected override String ExpectedFilter3Name => "Description:";
        protected override String ExpectedFilter3DisplayMemberPath => ".";
        protected override string ExpectedFilter3ValueMemberPath => ".";


        protected override String ExpectedComboBoxDisplayMember => FDC.NonWorkingDay.Description;

        protected override INonWorkingDayRepository CreateDataAccess()
        {
            INonWorkingDayRepository dataAccess = Substitute.For<INonWorkingDayRepository>();

            return dataAccess;
        }

        protected override INonWorkingDayProcess CreateBusinessProcess()
        {
            INonWorkingDayProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override INonWorkingDayProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationConfigurationProcess applicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            ICountryProcess countryProcess = Substitute.For<ICountryProcess>();
            IHttpApi httpWebApi = Substitute.For<IHttpApi>();

            CopyProperties(countryProcess, CoreInstance.Container.Get<ICountryProcess>());

            INonWorkingDayProcess process = new NonWorkingDayProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationConfigurationProcess, countryProcess, httpWebApi);

            return process;
        }

        private void CopyProperties(ICountryProcess substitute, ICountryProcess concrete)
        {
            substitute.ComboBoxDisplayMember.Returns(concrete.ComboBoxDisplayMember);
            substitute.ComboBoxValueMember.Returns(concrete.ComboBoxValueMember);
        }

        protected override INonWorkingDay CreateBlankEntity(INonWorkingDayProcess process)
        {
            INonWorkingDay retVal = CoreInstance.Container.Get<INonWorkingDay>();

            return retVal;
        }

        protected override INonWorkingDay CreateEntity(INonWorkingDayProcess process)
        {
            INonWorkingDay retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Date = new DateTime(2022, 12, 11);
            retVal.CountryId = new EntityId(1);
            retVal.Description = Guid.NewGuid().ToString();
            retVal.Notes = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(INonWorkingDay entity)
        {
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(INonWorkingDay entity)
        {
            Assert.That(entity.Description, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(INonWorkingDay entity)
        {
            Assert.That(entity.Description, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(INonWorkingDay entity1, INonWorkingDay entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Date, Is.EqualTo(entity1.Date));
            Assert.That(entity2.CountryId, Is.EqualTo(entity1.CountryId));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
            Assert.That(entity2.Notes, Is.EqualTo(entity1.Notes));
        }

        protected override void UpdateEntityProperties(INonWorkingDay entity)
        {
            entity.Date = DateTime.Now;
            entity.Description += "Updated";
            entity.Notes += "Updated";
        }

        [TestCase]
        public void Test_ApplyFilter_Country()
        {
            INonWorkingDayProcess process = CreateBusinessProcess();

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            ICountry country = CoreInstance.Container.Get<ICountry>();
            country.Id = new EntityId(1);
            String year = String.Empty;
            String description = String.Empty;

            List<INonWorkingDay> filteredNonWorkingDays = process.ApplyFilter(nonWorkingDays, country, year, description);

            Assert.That(filteredNonWorkingDays.Count, Is.EqualTo(8));

            Assert.That(filteredNonWorkingDays[0].Date, Is.EqualTo(new DateTime(2020, 1, 1)));
            Assert.That(filteredNonWorkingDays[1].Date, Is.EqualTo(new DateTime(2020, 1, 2)));
            Assert.That(filteredNonWorkingDays[2].Date, Is.EqualTo(new DateTime(2021, 1, 1)));
            Assert.That(filteredNonWorkingDays[3].Date, Is.EqualTo(new DateTime(2021, 1, 2)));
            Assert.That(filteredNonWorkingDays[4].Date, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(filteredNonWorkingDays[5].Date, Is.EqualTo(new DateTime(2022, 1, 2)));
            Assert.That(filteredNonWorkingDays[6].Date, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(filteredNonWorkingDays[7].Date, Is.EqualTo(new DateTime(2023, 1, 2)));

            Assert.That(filteredNonWorkingDays[0].Description, Is.EqualTo("C1. Y2020. New Years Day"));
            Assert.That(filteredNonWorkingDays[1].Description, Is.EqualTo("C1. Y2020. Second New Years Day1"));
            Assert.That(filteredNonWorkingDays[2].Description, Is.EqualTo("C1. Y2021. New Years Day"));
            Assert.That(filteredNonWorkingDays[3].Description, Is.EqualTo("C1. Y2021. Second New Years Day2"));
            Assert.That(filteredNonWorkingDays[4].Description, Is.EqualTo("C1. Y2022. New Years Day"));
            Assert.That(filteredNonWorkingDays[5].Description, Is.EqualTo("C1. Y2022. Second New Years Day3"));
            Assert.That(filteredNonWorkingDays[6].Description, Is.EqualTo("C1. Y2023. New Years Day"));
            Assert.That(filteredNonWorkingDays[7].Description, Is.EqualTo("C1. Y2023. Second New Years Day4"));
        }

        [TestCase]
        public void Test_ApplyFilter_Year()
        {
            INonWorkingDayProcess process = CreateBusinessProcess();

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            const ICountry country = null;
            String year = "2022";
            String description = String.Empty;

            List<INonWorkingDay> filteredNonWorkingDays = process.ApplyFilter(nonWorkingDays, country, year, description);

            Assert.That(filteredNonWorkingDays.Count, Is.EqualTo(6));

            Assert.That(filteredNonWorkingDays[0].Date, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(filteredNonWorkingDays[1].Date, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(filteredNonWorkingDays[2].Date, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(filteredNonWorkingDays[3].Date, Is.EqualTo(new DateTime(2022, 1, 2)));
            Assert.That(filteredNonWorkingDays[4].Date, Is.EqualTo(new DateTime(2022, 1, 2)));
            Assert.That(filteredNonWorkingDays[5].Date, Is.EqualTo(new DateTime(2022, 1, 2)));

            Assert.That(filteredNonWorkingDays[0].Description, Is.EqualTo("C1. Y2022. New Years Day"));
            Assert.That(filteredNonWorkingDays[1].Description, Is.EqualTo("C2. Y2022. New Years Day"));
            Assert.That(filteredNonWorkingDays[2].Description, Is.EqualTo("C3. Y2022. New Years Day"));
            Assert.That(filteredNonWorkingDays[3].Description, Is.EqualTo("C1. Y2022. Second New Years Day3"));
            Assert.That(filteredNonWorkingDays[4].Description, Is.EqualTo("C2. Y2022. Second New Years Day3"));
            Assert.That(filteredNonWorkingDays[5].Description, Is.EqualTo("C3. Y2022. Second New Years Day3"));
        }

        [TestCase]
        public void Test_ApplyFilter_Description()
        {
            INonWorkingDayProcess process = CreateBusinessProcess();

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            const ICountry country = null;
            String year = String.Empty;
            const String description = "C1. Y2022. New Years Day";

            List<INonWorkingDay> filteredNonWorkingDays = process.ApplyFilter(nonWorkingDays, country, year, description);

            Assert.That(filteredNonWorkingDays.Count, Is.EqualTo(1));

            Assert.That(filteredNonWorkingDays[0].Date, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(filteredNonWorkingDays[0].Description, Is.EqualTo("C1. Y2022. New Years Day"));
        }

        private INonWorkingDay CreateEntity(EntityId countryId, DateTime holidayDate, String description)
        {
            INonWorkingDay retVal = CoreInstance.Container.Get<INonWorkingDay>();

            retVal.CountryId = countryId;
            retVal.Date = holidayDate;
            retVal.Description = description;

            return retVal;
        }

        private List<INonWorkingDay> CreateListOfNonWorkingDays()
        {
            List<INonWorkingDay> retVal = new List<INonWorkingDay>
            {
                CreateEntity(new EntityId(1), new DateTime(2020, 1, 1), "C1. Y2020. New Years Day"),
                CreateEntity(new EntityId(1), new DateTime(2021, 1, 1), "C1. Y2021. New Years Day"),
                CreateEntity(new EntityId(1), new DateTime(2022, 1, 1), "C1. Y2022. New Years Day"),
                CreateEntity(new EntityId(1), new DateTime(2023, 1, 1), "C1. Y2023. New Years Day"),

                CreateEntity(new EntityId(1), new DateTime(2020, 1, 2), "C1. Y2020. Second New Years Day1"),
                CreateEntity(new EntityId(1), new DateTime(2021, 1, 2), "C1. Y2021. Second New Years Day2"),
                CreateEntity(new EntityId(1), new DateTime(2022, 1, 2), "C1. Y2022. Second New Years Day3"),
                CreateEntity(new EntityId(1), new DateTime(2023, 1, 2), "C1. Y2023. Second New Years Day4"),

                CreateEntity(new EntityId(2), new DateTime(2020, 1, 1), "C2. Y2020. New Years Day"),
                CreateEntity(new EntityId(2), new DateTime(2021, 1, 1), "C2. Y2021. New Years Day"),
                CreateEntity(new EntityId(2), new DateTime(2022, 1, 1), "C2. Y2022. New Years Day"),
                CreateEntity(new EntityId(2), new DateTime(2023, 1, 1), "C2. Y2023. New Years Day"),

                CreateEntity(new EntityId(2), new DateTime(2020, 1, 2), "C2. Y2020. Second New Years Day1"),
                CreateEntity(new EntityId(2), new DateTime(2021, 1, 2), "C2. Y2021. Second New Years Day2"),
                CreateEntity(new EntityId(2), new DateTime(2022, 1, 2), "C2. Y2022. Second New Years Day3"),
                CreateEntity(new EntityId(2), new DateTime(2023, 1, 2), "C2. Y2023. Second New Years Day4"),

                CreateEntity(new EntityId(3), new DateTime(2020, 1, 1), "C3. Y2020. New Years Day"),
                CreateEntity(new EntityId(3), new DateTime(2021, 1, 1), "C3. Y2021. New Years Day"),
                CreateEntity(new EntityId(3), new DateTime(2022, 1, 1), "C3. Y2022. New Years Day"),
                CreateEntity(new EntityId(3), new DateTime(2023, 1, 1), "C3. Y2022. New Years Day"),

                CreateEntity(new EntityId(3), new DateTime(2020, 1, 2), "C3. Y2020. Second New Years Day1"),
                CreateEntity(new EntityId(3), new DateTime(2021, 1, 2), "C3. Y2021. Second New Years Day2"),
                CreateEntity(new EntityId(3), new DateTime(2022, 1, 2), "C3. Y2022. Second New Years Day3"),
                CreateEntity(new EntityId(3), new DateTime(2023, 1, 2), "C3. Y2023. Second New Years Day4"),
            };

            return retVal;
        }

        [TestCase]
        public void Test_GetListOfNonWorkingDayCountries()
        {
            IApplicationConfigurationProcess applicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            ICountryProcess countryProcess = Substitute.For<ICountryProcess>();
            IHttpApi httpWebApi = Substitute.For<IHttpApi>();

            INonWorkingDayProcess process = new NonWorkingDayProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationConfigurationProcess, countryProcess, httpWebApi);

            List<ICountry> countriesToReturn = new List<ICountry>
            {
                CoreInstance.Container.Get<ICountry>(),
                CoreInstance.Container.Get<ICountry>(),
                CoreInstance.Container.Get<ICountry>(),
                CoreInstance.Container.Get<ICountry>(),
            };
            countriesToReturn[0].Id = new EntityId(1);
            countriesToReturn[1].Id = new EntityId(2);
            countriesToReturn[2].Id = new EntityId(3);
            countriesToReturn[0].Id = new EntityId(1);

            countryProcess.Get(Arg.Any<IEnumerable<EntityId>>()).Returns(countriesToReturn);

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            List<ICountry> countries = process.GetListOfNonWorkingDayCountries(nonWorkingDays);

            Assert.That(countries.Count, Is.EqualTo(countriesToReturn.Count));
            Assert.That(countries[0].Id, Is.EqualTo(new EntityId(1)));
            Assert.That(countries[1].Id, Is.EqualTo(new EntityId(2)));
            Assert.That(countries[2].Id, Is.EqualTo(new EntityId(3)));
        }

        [TestCase]
        public void Test_GetListOfNonWorkingDayYears()
        {
            INonWorkingDayProcess process = CreateBusinessProcess();

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            List<String> years = process.GetListOfNonWorkingDayYears(nonWorkingDays);

            Assert.That(years.Count, Is.EqualTo(5));
            Assert.That(years[0], Is.EqualTo(process.AllText));
            Assert.That(years[1], Is.EqualTo("2023"));
            Assert.That(years[2], Is.EqualTo("2022"));
            Assert.That(years[3], Is.EqualTo("2021"));
            Assert.That(years[4], Is.EqualTo("2020"));
        }

        [TestCase]
        public void Test_GetListOfNonWorkingDayDescriptions()
        {
            INonWorkingDayProcess process = CreateBusinessProcess();

            List<INonWorkingDay> nonWorkingDays = CreateListOfNonWorkingDays();
            List<String> years = process.GetListOfNonWorkingDayDescriptions(nonWorkingDays);

            Assert.That(years.Count, Is.EqualTo(25));
            Assert.That(years[0], Is.EqualTo(process.AllText));
            Assert.That(years[1], Is.EqualTo(process.NoneText));
            Assert.That(years[2], Is.EqualTo("C1. Y2020. New Years Day"));
            Assert.That(years[3], Is.EqualTo("C1. Y2020. Second New Years Day1"));
            Assert.That(years[4], Is.EqualTo("C1. Y2021. New Years Day"));
            Assert.That(years[5], Is.EqualTo("C1. Y2021. Second New Years Day2"));
            Assert.That(years[6], Is.EqualTo("C1. Y2022. New Years Day"));
            Assert.That(years[7], Is.EqualTo("C1. Y2022. Second New Years Day3"));
            Assert.That(years[8], Is.EqualTo("C1. Y2023. New Years Day"));
            Assert.That(years[9], Is.EqualTo("C1. Y2023. Second New Years Day4"));
            Assert.That(years[10], Is.EqualTo("C2. Y2020. New Years Day"));
            Assert.That(years[11], Is.EqualTo("C2. Y2020. Second New Years Day1"));
            Assert.That(years[12], Is.EqualTo("C2. Y2021. New Years Day"));
            Assert.That(years[13], Is.EqualTo("C2. Y2021. Second New Years Day2"));
            Assert.That(years[14], Is.EqualTo("C2. Y2022. New Years Day"));
            Assert.That(years[15], Is.EqualTo("C2. Y2022. Second New Years Day3"));
            Assert.That(years[16], Is.EqualTo("C2. Y2023. New Years Day"));
            Assert.That(years[17], Is.EqualTo("C2. Y2023. Second New Years Day4"));
            Assert.That(years[18], Is.EqualTo("C3. Y2020. New Years Day"));
            Assert.That(years[19], Is.EqualTo("C3. Y2020. Second New Years Day1"));
            Assert.That(years[20], Is.EqualTo("C3. Y2021. New Years Day"));
            Assert.That(years[21], Is.EqualTo("C3. Y2021. Second New Years Day2"));
            Assert.That(years[22], Is.EqualTo("C3. Y2022. New Years Day"));
            Assert.That(years[23], Is.EqualTo("C3. Y2022. Second New Years Day3"));
            Assert.That(years[24], Is.EqualTo("C3. Y2023. Second New Years Day4"));
        }

        [TestCase]
        public void Test_UpdateBankHolidayCalendarFromGovernmentSource()
        {
            IApplicationConfigurationProcess applicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            ICountryProcess countryProcess = Substitute.For<ICountryProcess>();
            IHttpApi httpWebApi = Substitute.For<IHttpApi>();

            String inputJsonData = @"{
    ""england-and-wales"": {
        ""division"": ""england-and-wales"",
        ""events"": [
            {
                ""title"": """",
                ""date"": ""2020-01-01"",
                ""notes"": ""Some notes"",
                ""bunting"": false
            }
        ]
    }
}";

            ICountry country = CoreInstance.Container.Get<ICountry>();

            const INonWorkingDay nonWorkingDay = null;
            DataAccess.Get(Arg.Any<EntityId>(), Arg.Any<DateTime>()).Returns(nonWorkingDay);

            applicationConfigurationProcess.GetValue<String>(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), Arg.Any<String>()).Returns("Configuration Value");
            httpWebApi.DownloadString(Arg.Any<IFileTransferSettings>()).Returns(inputJsonData);

            INonWorkingDayProcess process = new NonWorkingDayProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationConfigurationProcess, countryProcess, httpWebApi);

            process.UpdateBankHolidayCalendarFromGovernmentSource(country);

            DataAccess.Get(Arg.Any<EntityId>(), Arg.Any<DateTime>()).Received(1);
            DataAccess.Save(Arg.Any<INonWorkingDay>()).Received(1);
        }
    }
}
