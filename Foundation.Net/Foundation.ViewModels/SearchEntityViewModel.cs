//-----------------------------------------------------------------------
// <copyright file="SearchEntityViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for generic Entity Search screen
    /// </summary>
    public class SearchEntityViewModel : ViewModelBase
    {
        private List<String> _selectedListItems;
        private List<String> _availableListItems;

        /// <summary>Initialises a new instance of the <see cref="SearchEntityViewModel" /> class.</summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="targetWindow">The target window.</param>
        /// <param name="parentViewModel">The parent view model.</param>
        public SearchEntityViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IWindow targetWindow,
            IViewModel parentViewModel
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                "Search..."
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, targetWindow, parentViewModel);

            List<String> t1 = new List<String> { "1", "2", "3", "4", "5", "6" };
            List<String> t2 = new List<String> { "A", "B", "C", "D", "E", "F" };

            _selectedListItems = t1;
            _availableListItems = t2;

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="Initialise()"/>
        public override void Initialise()
        {
            LoggingHelpers.TraceCallEnter();

            // Nothing to do

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>Gets or sets the selected list items.</summary>
        /// <value>The selected list items.</value>
        public List<String> SelectedListItems
        {
            get => _selectedListItems;
            set => SetPropertyValue(ref _selectedListItems, value);
        }

        /// <summary>Gets or sets the available list items.</summary>
        /// <value>The available list items.</value>
        public List<String> AvailableListItems
        {
            get => _availableListItems;
            set => SetPropertyValue(ref _availableListItems, value);
        }
    }
}
