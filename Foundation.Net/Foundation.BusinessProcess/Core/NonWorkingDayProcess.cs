﻿//-----------------------------------------------------------------------
// <copyright file="NonWorkingDayProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Newtonsoft.Json;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.BusinessProcess
{
    /// <summary>
    /// The Non-Working Business Process 
    /// </summary>
    [DependencyInjectionTransient]
    public class NonWorkingDayProcess : CommonBusinessProcess<INonWorkingDay, INonWorkingDayRepository>, INonWorkingDayProcess
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NonWorkingDayProcess" /> class.
        /// </summary>
        /// <param name="core">The Foundation Core service</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="repository">The data access.</param>
        /// <param name="statusRepository">The status data access.</param>
        /// <param name="userProfileRepository">The user profile data access.</param>
        /// <param name="applicationConfigurationProcess">The application configuration process.</param>
        /// <param name="countryProcess">The country process.</param>
        /// <param name="httpWebApi">The http web API.</param>
        public NonWorkingDayProcess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            INonWorkingDayRepository repository,
            IStatusRepository statusRepository,
            IUserProfileRepository userProfileRepository,
            IApplicationConfigurationProcess applicationConfigurationProcess,
            ICountryProcess countryProcess,
            IHttpApi httpWebApi
        ) 
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                repository,
                statusRepository,
                userProfileRepository
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, repository, statusRepository, userProfileRepository);

            ApplicationConfigurationProcess = applicationConfigurationProcess;
            CountryProcess = countryProcess;
            HttpWebApi = httpWebApi;

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Gets the application configuration process
        /// </summary>
        /// <value>
        /// The application configuration process
        /// </value>
        private IApplicationConfigurationProcess ApplicationConfigurationProcess { get; }

        /// <summary>
        /// Gets the country process.
        /// </summary>
        /// <value>
        /// The country process.
        /// </value>
        private ICountryProcess CountryProcess { get; }

        /// <summary>
        /// Gets the Http web API process.
        /// </summary>
        /// <value>
        /// The Http web API process.
        /// </value>
        private IHttpApi HttpWebApi { get; }


        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalAction1"/>
        public override Boolean HasOptionalAction1 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Action1Name"/>
        public override String Action1Name => "Refresh from Government source";

        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalDropDownParameter1"/>
        public override Boolean HasOptionalDropDownParameter1 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Filter1Name"/>
        public override String Filter1Name => "Country:";

        /// <inheritdoc cref="ICommonBusinessProcess.Filter1DisplayMemberPath"/>
        public override String Filter1DisplayMemberPath => CountryProcess.ComboBoxDisplayMember;


        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalDropDownParameter2"/>
        public override Boolean HasOptionalDropDownParameter2 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Filter2Name"/>
        public override String Filter2Name => "Year:";

        /// <inheritdoc cref="ICommonBusinessProcess.Filter2DisplayMemberPath"/>
        public override String Filter2DisplayMemberPath => ".";

        /// <inheritdoc cref="ICommonBusinessProcess.Filter2SelectedValuePath"/>
        public override String Filter2SelectedValuePath => ".";


        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalDropDownParameter3"/>
        public override Boolean HasOptionalDropDownParameter3 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Filter3Name"/>
        public override String Filter3Name => "Description:";

        /// <inheritdoc cref="ICommonBusinessProcess.Filter3DisplayMemberPath"/>
        public override String Filter3DisplayMemberPath => ".";

        /// <inheritdoc cref="ICommonBusinessProcess.Filter3SelectedValuePath"/>
        public override String Filter3SelectedValuePath => ".";


        /// <inheritdoc cref="ICommonBusinessProcess.ScreenTitle"/>
        public override String ScreenTitle => "Non-Working Days";

        /// <inheritdoc cref="ICommonBusinessProcess.StatusBarText"/>
        public override String StatusBarText => "Number of Non-Working Days:";

        /// <inheritdoc cref="ICommonBusinessProcess.ComboBoxDisplayMember" />
        public override String ComboBoxDisplayMember => FDC.NonWorkingDay.Description;

        /// <inheritdoc cref="ICommonBusinessProcess.GetColumnDefinitions()" />
        public override List<IGridColumnDefinition> GetColumnDefinitions()
        {
            LoggingHelpers.TraceCallEnter();

            List<IGridColumnDefinition> retVal = GetStandardEntityColumnDefinitions();
            IGridColumnDefinition gridColumnDefinition;

            gridColumnDefinition = new GridColumnDefinition(68, FDC.NonWorkingDay.CountryId, "Country", typeof(Image))
            {
                ReadOnly = true,
                DataSource = CountryProcess.GetAll(excludeDeleted: false),
                ValueMember = CountryProcess.ComboBoxValueMember,
                DisplayMember = CountryProcess.ComboBoxDisplayMember,
            };
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(115, FDC.NonWorkingDay.Date, "Date", typeof(DateTime))
            {
                DotNetFormat = Formats.DotNet.DateOnlyWithDoW
            };

            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, FDC.NonWorkingDay.Description, "Description", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(400, FDC.NonWorkingDay.Notes, "Notes", typeof(String));
            retVal.Add(gridColumnDefinition);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="INonWorkingDayProcess.ApplyFilter(List{INonWorkingDay}, ICountry, String, String)" />
        public List<INonWorkingDay> ApplyFilter(List<INonWorkingDay> nonWorkingDays, ICountry country, String year, String description)
        {
            LoggingHelpers.TraceCallEnter(nonWorkingDays, country, year, description);

            List<INonWorkingDay> retVal = null;

            var workingList = nonWorkingDays.AsQueryable();

            if (country.IsNotNull())
            {
                workingList = workingList.Where(nwd => (nwd.CountryId == country.Id) ||                     // Country
                                                       (country.Id == AllId)                                // All records
                );
            }

            if (!String.IsNullOrEmpty(year))
            {
                workingList = workingList.Where(nwd => (nwd.Date.ToString("yyyy") == year) ||               // Year
                                                       (year == AllText)                                    // All records
                                                );
            }

            if (!String.IsNullOrEmpty(description))
            {
                workingList = workingList.Where(nwd => (nwd.Description == description) ||                  // Description
                                                       (description == NoneText &&
                                                        String.IsNullOrEmpty(nwd.Description)) ||           // None filter
                                                       (description == AllText)                             // All records
                                                );
            }

            retVal = workingList.OrderBy(nwd => nwd.Date).ToList();

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="INonWorkingDayProcess.GetListOfNonWorkingDayCountries(IEnumerable{INonWorkingDay})" />
        public List<ICountry> GetListOfNonWorkingDayCountries(IEnumerable<INonWorkingDay> nonWorkingDays)
        {
            LoggingHelpers.TraceCallEnter(nonWorkingDays);

            List<ICountry> retVal = null;

            IEnumerable<EntityId> countryIds = nonWorkingDays.Select(nwd => nwd.CountryId);
            IEnumerable<EntityId> uniqueCountryIds = countryIds.Distinct();

            retVal = CountryProcess.Get(uniqueCountryIds).ToList();

            CountryProcess.AddFilterOptionAll(retVal);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="INonWorkingDayProcess.GetListOfNonWorkingDayYears(List{INonWorkingDay})" />
        public List<String> GetListOfNonWorkingDayYears(List<INonWorkingDay> nonWorkingDays)
        {
            LoggingHelpers.TraceCallEnter(nonWorkingDays);

            List<String> retVal = null;

            IEnumerable<String> years = nonWorkingDays.Select(nwd => nwd.Date.ToString("yyyy"));
            retVal = years.Distinct().ToList();
            retVal = retVal.OrderByDescending(y => y).ToList();

            retVal.Insert(0, AllText);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="INonWorkingDayProcess.GetListOfNonWorkingDayDescriptions(List{INonWorkingDay})" />
        public List<String> GetListOfNonWorkingDayDescriptions(List<INonWorkingDay> nonWorkingDays)
        {
            LoggingHelpers.TraceCallEnter(nonWorkingDays);

            List<String> retVal = null;

            IEnumerable<String> descriptions = nonWorkingDays.Select(nwd => nwd.Description);
            retVal = descriptions.Distinct().ToList();
            retVal.Sort();

            base.AddFilterOptionsAdditional(retVal);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="INonWorkingDayProcess.UpdateBankHolidayCalendarFromGovernmentSource(ICountry)" />
        public void UpdateBankHolidayCalendarFromGovernmentSource(ICountry country)
        {
            LoggingHelpers.TraceCallEnter(country);

            const String keyName = "service.holidays.national.uk.url";

            String sourceUrl = ApplicationConfigurationProcess.Get<String>(ApplicationSettings.ApplicationId, Core.CurrentLoggedOnUser.UserProfile, keyName);
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.Http,
                Location = sourceUrl,
            };
            String jsonData = HttpWebApi.DownloadString(fileTransferSettings);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);

            UpdateBankHolidayCalendar(country, rootObject);

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Updates Bank Holiday calendar
        /// </summary>
        /// <param name="country"></param>
        /// <param name="bankHolidaysData"></param>
        private void UpdateBankHolidayCalendar(ICountry country, RootObject bankHolidaysData)
        {
            LoggingHelpers.TraceCallEnter(country, bankHolidaysData);

            if (bankHolidaysData.IsNotNull())
            {
                foreach (BankHolidayEvent holidayEvent in bankHolidaysData.EnglandAndWales.Events)
                {
                    INonWorkingDay nonWorkingDay = EntityRepository.Get(country.Id, holidayEvent.BankHolidayDate);

                    if (nonWorkingDay.IsNull())
                    {
                        nonWorkingDay = Core.Container.Get<INonWorkingDay>();
                        nonWorkingDay.CountryId = country.Id;
                        nonWorkingDay.Date = holidayEvent.BankHolidayDate;
                        nonWorkingDay.Description = holidayEvent.Title;
                        nonWorkingDay.Notes = holidayEvent.Notes;
                        nonWorkingDay.ValidFrom = DefaultValidFromDateTime;
                        nonWorkingDay.ValidTo = DefaultValidToDateTime;

                        EntityRepository.Save(nonWorkingDay);
                    }
                }
            }

            LoggingHelpers.TraceCallReturn();
        }
    }
}
