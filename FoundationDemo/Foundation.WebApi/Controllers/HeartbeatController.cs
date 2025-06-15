using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Web.Http;

using Foundation.Core;
using System.Net.Http.Headers;

namespace Foundation.WebApi.Controllers
{
    public class HeartbeatController : ApiController, IHeartbeatController
    {
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
            HeartbeatResult heartbeatResult = new HeartbeatResult { Success = success, Logs = status, Version = version };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, heartbeatResult);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetFileAsString()
        {
            String basePath = AppDomain.CurrentDomain.BaseDirectory;
            String filePath = @"Data\SampleDocuments\Sample Text Document.txt";
            String fullPath = Path.Combine(basePath, filePath);

            String retVal = File.ReadAllText(fullPath);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetFile(String fileName)
        {
            String basePath = AppDomain.CurrentDomain.BaseDirectory;
            String filePath = @"Data\SampleDocuments";
            String fullPath = Path.Combine(basePath, filePath, fileName);

            Byte[] fileBytes = File.ReadAllBytes(fullPath);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetFileAsStream()
        {
            String retVal = DateTime.UtcNow.ToString("O");

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, retVal);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage BasicExceptionDemo()
        {
            ForceException();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage HttpResponseExceptionDemo()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Something bad happened at: {DateTime.Now:yyyy-MMM-dd HH:MM:ss.fff}"),
                ReasonPhrase = "Gen Error"
            };
            throw new HttpResponseException(resp);
        }

        [HttpGet]
        public HttpResponseMessage ExceptionDemo()
        {
            HttpResponseMessage responseMessage;
            try
            {
                ForceException();
                responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (Exception exception)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception);
            }

            return responseMessage;
        }

        private void ForceException()
        {
            throw new Exception($"Something bad happened at: {DateTime.Now:yyyy-MMM-dd HH:MM:ss.fff}");
        }
    }
}
