//-----------------------------------------------------------------------
// <copyright file="SmtpConfiguration.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Common
{
    /// <summary>
    /// Defines the Config Properties Section used in application configuration
    /// </summary>
    [Serializable]
    public class SmtpConfiguration
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public String Server { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public Int32 Port { get; set; }

        /// <summary>
        /// Gets or sets from address.
        /// </summary>
        /// <value>
        /// From address.
        /// </value>
        public String FromAddress { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether [enable SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable SSL]; otherwise, <c>false</c>.
        /// </value>
        public Boolean EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public String Username { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public String Password { get; set; } = String.Empty;
    }
}
