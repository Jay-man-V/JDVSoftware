﻿//-----------------------------------------------------------------------
// <copyright file="ImapApi.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Emailer
{
    /// <inheritdoc cref="IImapApi"/>
    [DependencyInjectionTransient]
    public class ImapApi : IImapApi
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ImapApi"/> class.
        /// </summary>
        public ImapApi()
        {
        }

        /// <inheritdoc cref="IRemoteServiceApi.DeleteFile(IFileTransferSettings)"/>
        public void DeleteFile(IFileTransferSettings fileTransferSettings)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings);

            Task t = DeleteFileAsync(fileTransferSettings);
            t.Wait();

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IRemoteServiceApi.DeleteFileAsync(IFileTransferSettings)"/>
        public Task DeleteFileAsync(IFileTransferSettings fileTransferSettings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IRemoteServiceApi.DownloadFile(IFileTransferSettings)"/>
        public Stream DownloadFile(IFileTransferSettings fileTransferSettings)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings);

            Task<Stream> t = DownloadFileAsync(fileTransferSettings);
            t.Wait();
            Stream retVal = t.Result;

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IRemoteServiceApi.DownloadFileAsync(IFileTransferSettings)"/>
        public async Task<Stream> DownloadFileAsync(IFileTransferSettings fileTransferSettings)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings);

            const Stream retVal = null;

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IRemoteServiceApi.UploadFile(IFileTransferSettings, String)"/>
        public String UploadFile(IFileTransferSettings fileTransferSettings, String filePath)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings, filePath);

            Task<String> t = UploadFileAsync(fileTransferSettings, filePath);
            t.Wait();
            String retVal = t.Result;

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IRemoteServiceApi.UploadFile(IFileTransferSettings, Stream)"/>
        public String UploadFile(IFileTransferSettings fileTransferSettings, Stream fileContent)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings, fileContent);

            Task<String> t = UploadFileAsync(fileTransferSettings, fileContent);
            t.Wait();
            String retVal = t.Result;

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IRemoteServiceApi.UploadFileAsync(IFileTransferSettings, String)"/>
        public async Task<String> UploadFileAsync(IFileTransferSettings fileTransferSettings, String filePath)
        {
            LoggingHelpers.TraceCallEnter(fileTransferSettings, filePath);

            String retVal;
            using (Stream stream = File.OpenRead(filePath))
            {
                Task<String> t = UploadFileAsync(fileTransferSettings, stream);
                await t;
                retVal = t.Result;
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="IRemoteServiceApi.UploadFileAsync(IFileTransferSettings, Stream)"/>
        public Task<String> UploadFileAsync(IFileTransferSettings fileTransferSettings, Stream fileContent)
        {
            //https://medium.com/@niteshsinghal85/file-upload-to-web-api-with-different-http-clients-in-c-ae123555ef49
            throw new NotImplementedException();
        }
    }
}
