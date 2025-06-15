using System;
using System.Collections.Generic;

namespace Foundation.Core
{
    public class HeartbeatResult
    {
        public HeartbeatResult()
        {
            DateRun = DateTime.Now;
            DateRun = new DateTime(DateRun.Year, DateRun.Month, DateRun.Day, DateRun.Hour, DateRun.Minute, DateRun.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// The Date/Time the request was serviced
        /// </summary>
        public DateTime DateRun { get; set; }

        /// <summary>
        /// Version of the Api
        /// </summary>
        public String Version { get; set; } = String.Empty;

        /// <summary>
        /// Resulting status of the call
        /// </summary>
        public Boolean Success { get; set; }

        /// <summary>
        /// The content of any Logs created on the Api that are to be sent back to the caller in the result
        /// </summary>
        public List<String> Logs { get; set; } = new List<String>();

        public override String ToString()
        {
            String retVal = $"{DateRun:yyyy-MM-ddTHH:mm:ss.fff}. {Version}. {Success}. {String.Join(", ", Logs)}";

            return retVal;
        }
    }
}
