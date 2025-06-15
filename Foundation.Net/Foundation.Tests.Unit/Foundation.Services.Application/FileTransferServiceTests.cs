﻿//-----------------------------------------------------------------------
// <copyright file="FileTransferServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for FileTransferServiceTests
    /// </summary>
    [TestFixture]
    public class FileTransferServiceTests : UnitTestBase
    {
        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_FileSystem_GetStream(String sourceFile)
        {
            IFileTransferService fileTransferService = CoreInstance.Container.Get<IFileTransferService>();
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = sourceFile,
            };

            Stream fileContent = fileTransferService.TransferFile(fileTransferSettings);

            Stream actualFileContent = File.OpenRead(sourceFile);
            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_FileSystem_Move(String sourceFile)
        {
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            IFileTransferService fileTransferService = CoreInstance.Container.Get<IFileTransferService>();

            String workingSourceFolder = @".Support\SampleDocuments\FileTransferService\CopySource";
            Directory.CreateDirectory(workingSourceFolder);
            String workingSourceFile = Path.Combine(workingSourceFolder, Path.GetFileName(sourceFile));
            fileApi.CopyFile(sourceFile, workingSourceFile);

            IFileTransferSettings sourceFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = workingSourceFile,
            };
            MemoryStream sourceFileStream = new MemoryStream();
            using (Stream tempFileStream = fileApi.GetFileContentsAsStream(sourceFileTransferSettings.Location))
            {
                tempFileStream.CopyTo(sourceFileStream);
            }

            String destinationFolder = @".Support\SampleDocuments\FileTransferService\CopyDestination";
            Directory.CreateDirectory(destinationFolder);

            IFileTransferSettings destinationFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = Path.Combine(destinationFolder, Guid.NewGuid().ToString()),
            };

            String archiveFolder = @".Support\SampleDocuments\FileTransferService\Archive";
            Directory.CreateDirectory(archiveFolder);

            IArchiveTransferSettings archiveFileTransferSettings = new ArchiveTransferSettings
            {
                FileTransferArchiveAction = FileTransferArchiveAction.Move,
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = Path.Combine(archiveFolder, Guid.NewGuid().ToString()),
            };

            fileTransferService.TransferFile(sourceFileTransferSettings, destinationFileTransferSettings, archiveFileTransferSettings);

            Boolean sourceFileExists = fileApi.DoesFileExist(sourceFileTransferSettings.Location);
            Assert.That(sourceFileExists, Is.EqualTo(false));

            Boolean destinationFileExists = fileApi.DoesFileExist(destinationFileTransferSettings.Location);
            Assert.That(destinationFileExists, Is.EqualTo(true));

            Stream destinationFileStream = fileApi.GetFileContentsAsStream(destinationFileTransferSettings.Location);

            Assert.That(sourceFileStream, Is.EqualTo(destinationFileStream));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_FileSystem_Copy(String sourceFile)
        {
            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            IFileTransferService fileTransferService = CoreInstance.Container.Get<IFileTransferService>();

            IFileTransferSettings sourceFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = sourceFile,
            };
            Stream sourceFileStream = fileApi.GetFileContentsAsStream(sourceFileTransferSettings.Location);

            String destinationFolder = @".Support\SampleDocuments\FileTransferService\CopyDestination";
            Directory.CreateDirectory(destinationFolder);

            IFileTransferSettings destinationFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = Path.Combine(destinationFolder, Guid.NewGuid().ToString()),
            };

            String archiveFolder = @".Support\SampleDocuments\FileTransferService\Archive";
            Directory.CreateDirectory(archiveFolder);

            IArchiveTransferSettings archiveFileTransferSettings = new ArchiveTransferSettings
            {
                FileTransferArchiveAction = FileTransferArchiveAction.Copy,
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = Path.Combine(archiveFolder, Guid.NewGuid().ToString()),
            };

            fileTransferService.TransferFile(sourceFileTransferSettings, destinationFileTransferSettings, archiveFileTransferSettings);

            Boolean sourceFileExists = fileApi.DoesFileExist(sourceFileTransferSettings.Location);
            Assert.That(sourceFileExists, Is.EqualTo(true));

            Boolean destinationFileExists = fileApi.DoesFileExist(destinationFileTransferSettings.Location);
            Assert.That(destinationFileExists, Is.EqualTo(true));

            Stream destinationFileStream = fileApi.GetFileContentsAsStream(sourceFileTransferSettings.Location);

            Assert.That(sourceFileStream, Is.EqualTo(destinationFileStream));
        }
    }
}
