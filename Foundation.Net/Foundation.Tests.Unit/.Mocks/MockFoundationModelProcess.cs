﻿//-----------------------------------------------------------------------
// <copyright file="MockFoundationModelProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;

namespace Foundation.Tests.Unit.Mocks
{
    public interface IMockFoundationModelProcess : ICommonBusinessProcess<IMockFoundationModel>
    {

    }

    [DependencyInjectionTransient]
    public class MockFoundationModelProcess : CommonBusinessProcess<IMockFoundationModel, IMockFoundationModelRepository>, IMockFoundationModelProcess
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MockFoundationModelProcess" /> class.
        /// </summary>
        /// <param name="core">The Foundation Core service</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="repository">The data access.</param>
        /// <param name="statusRepository">The status data access.</param>
        /// <param name="userProfileRepository">The user profile data access.</param>
        public MockFoundationModelProcess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IMockFoundationModelRepository repository,
            IStatusRepository statusRepository,
            IUserProfileRepository userProfileRepository
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
        }

        public override string ComboBoxDisplayMember => "Made up property name";
        //public override String ScreenTitle => "Mock Foundations";
        //public override String StatusBarText => "Number of Mock Foundation rows:";

        /// <inheritdoc cref="GetColumnDefinitions" />
        public override List<IGridColumnDefinition> GetColumnDefinitions()
        {
            const Boolean includeStatusColumn = true;
            List<IGridColumnDefinition> retVal = base.GetStandardEntityColumnDefinitions(includeStatusColumn);
            IGridColumnDefinition gridColumnDefinition;

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.ImagePicture), "Profile Picture", typeof(Image));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.Name), "Name", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(300, nameof(IMockFoundationModel.Code), "Code", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.Description), "Description", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.Duration), "Duration", typeof(TimeSpan));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.Count), "Count", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.IsClosed), "Is Closed", typeof(Boolean));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.IsOpen), "Is Open", typeof(Boolean));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.Quantity), "Quantity", typeof(Decimal));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.UnitPrice), "Unit Price", typeof(Decimal));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, nameof(IMockFoundationModel.ExecutionTime), "Execution Time", typeof(DateTime))
            {
                DotNetFormat = Formats.DotNet.DateTimeMilliseconds,
                ExcelFormat = Formats.Excel.DateTimeMilliseconds,
            };
            retVal.Add(gridColumnDefinition);

            return retVal;
        }
    }
}
