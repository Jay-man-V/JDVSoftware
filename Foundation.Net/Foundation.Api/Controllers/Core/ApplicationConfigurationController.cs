//-----------------------------------------------------------------------
// <copyright file="HeartbeatController.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Api.Controllers
{
    [DependencyInjectionTransient]
    public class ApplicationConfigurationController : CommonBusinessController<IApplicationConfiguration>, IApplicationConfigurationController
    {
        public ApplicationConfigurationController
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IApplicationConfigurationProcess applicationConfigurationProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                applicationConfigurationProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, applicationConfigurationProcess);

            ApplicationConfigurationProcess = applicationConfigurationProcess;

            LoggingHelpers.TraceCallReturn();
        }

        private IApplicationConfigurationProcess ApplicationConfigurationProcess { get; }
    }
}