//-----------------------------------------------------------------------
// <copyright file="HeartbeatController.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Interfaces.WebApi;

namespace Foundation.Api.Controllers
{
    [DependencyInjectionTransient]
    public class HeartbeatController : CommonController, IHeartbeatController
    {
        public HeartbeatController
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

        [HttpGet]
        public HttpResponseMessage IsAlive()
        {
            String username = Core.CurrentLoggedOnUser.Username;
            String displayName = Core.CurrentLoggedOnUser.DisplayName;
            DateTime dateTime = DateTimeService.SystemDateTimeNow;

            String retVal = $"{dateTime:yyyy-MMM-dd HH:mm:ss.fff} - {GetType()} - {displayName} - {username}";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetRunTimeEnvironmentSettings()
        {
            Object retVal = RunTimeEnvironmentSettings;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetLoggedOnUser()
        {
            Object retVal = Core.CurrentLoggedOnUser;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage ExceptionDemo()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public HttpResponseMessage BasicExceptionDemo()
        {
            ForceException();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage ReturnInput(Int32 id, [FromUri]String input)
        {
            String username = Core.CurrentLoggedOnUser.Username;
            String displayName = Core.CurrentLoggedOnUser.DisplayName;
            DateTime dateTime = DateTimeService.SystemDateTimeNow;
            String message = $"{dateTime:yyyy-MMM-dd HH:mm:ss.fff} - {displayName} - {username} - {id} - {input}";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, message);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetHeartbeat()
        {
            Boolean success = false;
            List<String> status = new List<String> { "WebService is reachable" };

            Assembly assembly = Assembly.GetExecutingAssembly();
            String version = assembly.GetName().Version.ToString();

            // Perform any specific checks you need here, setting the result boolean
            // Do some checks here
            status.Add("Some checks run");

            String message = String.Empty;
            try
            {
                IApprovalStatusProcess approvalStatusProcess = Core.Container.Get<IApprovalStatusProcess>();

                if (approvalStatusProcess != null)
                {
                    message = "Approval Status Process object created with IoC";
                }
                else
                {
                    message = "Approval Status Process was not created";
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }

            status.Add(message);

            success = status.Count > 0;
            IHeartbeatResult heartbeatResult = Core.Container.Get<IHeartbeatResult>();
            status.ForEach(s => heartbeatResult.Logs.Add(s));
            heartbeatResult.Success = success;
            heartbeatResult.Version = version;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, heartbeatResult);

            return response;
        }

        private void ForceException()
        {
            throw new Exception($"Something bad happened at: {DateTime.Now:yyyy-MMM-dd HH:MM:ss.fff}");
        }
    }
}