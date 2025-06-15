﻿//-----------------------------------------------------------------------
// <copyright file="MailAttachment.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Emailer
{
    /// <summary>
    /// The Mail Message class
    /// </summary>
    [DependencyInjectionTransient]
    public class MailAttachment : IMailAttachment
    {
        /// <inheritdoc cref="IMailAttachment.Filename"/>
        public String Filename { get; set; } = String.Empty;

        /// <inheritdoc cref="IMailAttachment.Content"/>
        public Byte[] Content { get; set; } = null;

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public Object Clone()
        {
            IMailAttachment retVal = (IMailAttachment)Activator.CreateInstance(this.GetType());

            retVal.Filename = this.Filename;

            if (Content.IsNotNull())
            {
                retVal.Content = Content.ToArray();
            }

            return retVal;
        }
    }
}