//-----------------------------------------------------------------------
// <copyright file="CommonBusinessController.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Api
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommonBusinessController : CommonController, ICommonBusinessController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="core"></param>
        /// <param name="runTimeEnvironmentSettings"></param>
        /// <param name="dateTimeService"></param>
        protected CommonBusinessController
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService);

            LoggingHelpers.TraceCallReturn();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class CommonBusinessController<TModel> : CommonBusinessController, ICommonBusinessController<TModel>
        where TModel : IFoundationModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="core"></param>
        /// <param name="runTimeEnvironmentSettings"></param>
        /// <param name="dateTimeService"></param>
        /// <param name="commonBusinessProcess"></param>
        protected CommonBusinessController
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            ICommonBusinessProcess<TModel> commonBusinessProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, commonBusinessProcess);

            CommonBusinessProcess = commonBusinessProcess;

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// 
        /// </summary>
        protected ICommonBusinessProcess<TModel> CommonBusinessProcess { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAll()
        {
            List<TModel> retVal = CommonBusinessProcess.GetAll();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }
    }
}