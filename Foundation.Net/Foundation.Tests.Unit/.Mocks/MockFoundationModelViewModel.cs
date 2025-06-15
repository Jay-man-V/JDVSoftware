﻿//-----------------------------------------------------------------------
// <copyright file="MockFoundationViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Interfaces;
using Foundation.ViewModels;

namespace Foundation.Tests.Unit.Mocks
{
    public interface IMockFoundationModelViewModel : IGenericDataGridViewModelBase<IMockFoundationModel>
    {
    }

    public class MockFoundationModelViewModel : GenericDataGridViewModelBase<IMockFoundationModel>, IMockFoundationModelViewModel
    {
        public MockFoundationModelViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IMockFoundationModelProcess mockFoundationModelProcess
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                mockFoundationModelProcess
            )
        {
        }
    }
}
