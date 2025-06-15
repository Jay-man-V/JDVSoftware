//-----------------------------------------------------------------------
// <copyright file="FileServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;
using Foundation.Services.Application;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for FileServiceTests
    /// </summary>
    [TestFixture]
    public class FileServiceTests : UnitTestBase
    {
        private IApplicationConfigurationProcess ApplicationConfigurationProcess { get; set; }
        private IFileApi CreateBusinessProcess()
        {
            ApplicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            IFileApi retVal = new FileService(CoreInstance, ApplicationConfigurationProcess);

            return retVal;
        }

        /// <summary>
        /// baseFolder and targetFolder do not have a trailing slash
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_Folder_1()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String baseFolder = "baseFolder";
            String targetFolder = "targetFolder";
            String expected = @"baseFolder\targetFolder\";
            String actual = fileApi.MakeDataPath(baseFolder, targetFolder);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// baseFolder and targetFolder do have a trailing slash
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_Folder_2()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String baseFolder = @"baseFolder\";
            String targetFolder = @"targetFolder\";
            String expected = @"baseFolder\targetFolder\";
            String actual = fileApi.MakeDataPath(baseFolder, targetFolder);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_File()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String baseFolder = "baseFolder";
            String targetFolder = "targetFolder";
            String targetFileName = "NewFile.txt";
            String expected = @"baseFolder\targetFolder\NewFile.txt";
            String actual = fileApi.MakeDataPath(baseFolder, targetFolder, targetFileName);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_UserDataPath()
        {
            String userDataPath = @".\UserData\";

            IFileApi fileApi = CreateBusinessProcess();
            ApplicationConfigurationProcess.GetValue<String>(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), ApplicationConfigurationKeys.UserDataPath).Returns(userDataPath);

            String expected = @".\UserData\";
            String actual = fileApi.UserDataPath;

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_SystemDataPath()
        {
            String systemDataPath = @"\SystemData\";

            IFileApi fileApi = CreateBusinessProcess();
            ApplicationConfigurationProcess.GetValue<String>(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), ApplicationConfigurationKeys.SystemDataPath).Returns(systemDataPath);

            String expected = @"\SystemData\";
            String actual = fileApi.SystemDataPath;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetNewTempFilePath()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String baseFolder = ".ExpectedResults";
            String filePrefix = "UnitTest";
            String actual = fileApi.GetNewTempFilePath(baseFolder, filePrefix);

            Assert.That(String.IsNullOrEmpty(actual), Is.EqualTo(false));
            Assert.That(String.IsNullOrWhiteSpace(actual), Is.EqualTo(false));

            FileInfo fi = new FileInfo(actual);

            String tempFolderPath = fi.DirectoryName;
            String tempFileName = fi.Name;

            Assert.That(Path.Combine(systemTempFolderPath, baseFolder), Is.EqualTo(tempFolderPath));
            Assert.That(tempFileName.StartsWith(filePrefix));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_EnsureFileExists_True()
        {
            String filePath = @".Support\SampleDocuments\Blank_10x10.bmp";

            IFileApi fileApi = CreateBusinessProcess();
            fileApi.EnsureFileExists(filePath);
        }

        [TestCase]
        public void Test_EnsureFileExists_False()
        {
            String filePath = "MadeUp.File.Name";
            String errorMessage = $"The file '{filePath}' does not exist or access to it is denied";
            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.EnsureFileExists(filePath);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;
            String actualErrorFileName = actualException.FileName;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualErrorFileName, Is.EqualTo(filePath));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_EnsureDirectoryExists_True()
        {
            String filePath = @".Support\SampleDocuments";

            IFileApi fileApi = CreateBusinessProcess();
            fileApi.EnsureDirectoryExists(filePath);
        }

        [TestCase]
        public void Test_EnsureDirectoryExists_False()
        {
            String directoryPath = "MadeUp.File.Name";
            String errorMessage = $"The directory '{directoryPath}' does not exist or access to it is denied";
            DirectoryNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.EnsureDirectoryExists(directoryPath);
            }
            catch (DirectoryNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_DoesFileExist_True()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\SampleDocuments\Blank_10x10.bmp";
            const Boolean expected = true;
            Boolean actual = fileApi.DoesFileExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DoesFileExist_False()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = "Fake filename" + Guid.NewGuid().ToString();
            const Boolean expected = false;
            Boolean actual = fileApi.DoesFileExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_DoesFileExist_SameAsDirectoryName()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\SampleDocuments";
            const Boolean expected = true;
            Boolean actual = fileApi.DoesFileExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_DoesDirectoryExist_True()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\SampleDocuments\";
            const Boolean expected = true;
            Boolean actual = fileApi.DoesDirectoryExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Blank_10x10.bmp", @".Support\SampleDocuments\")]
        public void Test_DoesDirectoryExist_SameAsFileName()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\SampleDocuments\Blank_10x10.bmp";
            const Boolean expected = true;
            Boolean actual = fileApi.DoesDirectoryExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DoesDirectoryExist_False()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\MadeUp folder\";
            const Boolean expected = false;
            Boolean actual = fileApi.DoesDirectoryExist(filePath);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetFileContentsAsStream_Fail()
        {
            String filePath = @"C:\FakeFile.txt";

            String message = $"The file '{filePath}' does not exist or access to it is denied";

            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.GetFileContentsAsStream(filePath);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(message));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\FileManagement\SmallFile.txt", @".ExpectedResults\FileManagement\")]
        public void Test_GetFileContentsAsStream_Success()
        {
            String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Stream fileContent = File.OpenRead(filePath);

            IFileApi fileApi = CreateBusinessProcess();
            Stream actualFileContent = fileApi.GetFileContentsAsStream(filePath);

            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase]
        public void Test_GetFileContentsAsText_Fail()
        {
            String filePath = @"C:\FakeFile.txt";
            Encoding encoding = Encoding.UTF8;

            String message = $"The file '{filePath}' does not exist or access to it is denied";

            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.GetFileContentsAsText(filePath, encoding);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(message));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\FileManagement\SmallFile.txt", @".ExpectedResults\FileManagement\")]
        public void Test_GetFileContentsAsText_Success()
        {
            String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Encoding encoding = Encoding.UTF8;
            String fileContent = "Just a small text file";

            IFileApi fileApi = CreateBusinessProcess();
            String actualFileContent = fileApi.GetFileContentsAsText(filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase]
        public void Test_GetFileContentsAsByteArray_Fail_NoFile()
        {
            String filePath = @"C:\FakeFile.txt";

            String message = $"The file '{filePath}' does not exist or access to it is denied";

            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.GetFileContentsAsByteArray(filePath);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(message));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\FileManagement\SmallFile.txt", @".ExpectedResults\FileManagement\")]
        public void Test_GetFileContentsAsByteArray_Success()
        {
            String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Byte[] fileContent = File.ReadAllBytes(filePath);

            IFileApi fileApi = CreateBusinessProcess();
            Byte[] actualFileContent = fileApi.GetFileContentsAsByteArray(filePath);

            Assert.That(actualFileContent, Is.EquivalentTo(fileContent));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_GetAssemblyResource_Fail()
        {
            String filePath = "FakeFile.txt";

            String message = $"Resource File '{filePath}' does not exist in the Assembly 'Foundation.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'";

            MissingManifestResourceException actualException = null;

            try
            {
                Assembly thisAssembly = Assembly.GetExecutingAssembly();

                IFileApi fileApi = CreateBusinessProcess();
                fileApi.GetAssemblyResource(thisAssembly, filePath);
            }
            catch (MissingManifestResourceException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(message));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_GetAssemblyResource_Success()
        {
            String filePath = "Foundation.Tests.Unit..Support.SampleDocuments.Embedded Sample Text Document.txt";
            String fileContent = "A sample text document";

            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            String actualFileContent;
            IFileApi fileApi = CreateBusinessProcess();
            using (Stream resourceStream = fileApi.GetAssemblyResource(thisAssembly, filePath))
            {
                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    actualFileContent = reader.ReadToEnd();
                }
            }

            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase]
        public void Test_GetFileContentsAsTextFromAssembly()
        {
            String filePath = "Foundation.Tests.Unit..Support.SampleDocuments.Embedded Sample Text Document.txt";
            Encoding encoding = Encoding.UTF8;
            String fileContent = "A sample text document";

            IFileApi fileApi = CreateBusinessProcess();
            String actualFileContent = fileApi.GetFileContentsAsTextFromAssembly(Assembly.GetAssembly(this.GetType()), filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Sample Word Document.docx", @".Support\SampleDocuments\")]
        public void Test_GetFileContentsAsByteArrayFromAssembly()
        {
            String filePath = "Foundation.Tests.Unit..Support.SampleDocuments.Embedded Sample Word Document.docx";
            Byte[] fileContent = File.ReadAllBytes(@".Support\\SampleDocuments\\Sample Word Document.docx");

            IFileApi fileApi = CreateBusinessProcess();
            Byte[] actualFileContent = fileApi.GetFileContentsAsByteArrayFromAssembly(Assembly.GetAssembly(this.GetType()), filePath);

            Assert.That(actualFileContent, Is.EquivalentTo(fileContent));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        public void Test_OpenFileForReading_FileExists()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String filePath = @".Support\SampleDocuments\Sample Text Document.txt";
            Encoding encoding = Encoding.UTF8;
            String expected = "A sample text document";
            String actual;
            using (TextReader reader = fileApi.OpenFileForReading(filePath, encoding))
            {
                actual = reader.ReadToEnd();
            }

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_OpenFileForReading_FileNotExists()
        {
            String filePath = @".Support\SampleDocuments\Random file that does not exist " + Guid.NewGuid();
            Encoding encoding = Encoding.UTF8;
            String errorMessage = $"The file '{filePath}' does not exist or access to it is denied";
            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                fileApi.OpenFileForReading(filePath, encoding);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;
            String actualErrorFileName = actualException.FileName;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualErrorFileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Fail_FileDoesNotExist()
        {
            String filePath = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String errorMessage = $"The file '{filePath}' does not exist or access to it is denied";

            FileNotFoundException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                const Boolean appendToFile = true;
                fileApi.OpenFileForWriting(filePath, encoding, appendToFile);
            }
            catch (FileNotFoundException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;
            String actualErrorFileName = actualException.FileName;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualErrorFileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Fail_FileAlreadyExists()
        {
            String filePath = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String errorMessage = $"The file '{filePath}' already exists and cannot be created";

            UnauthorizedAccessException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();
                const Boolean appendToFile1 = false;
                fileApi.OpenFileForWriting(filePath, encoding, appendToFile1);

                const Boolean appendToFile2 = false;
                fileApi.OpenFileForWriting(filePath, encoding, appendToFile2);
            }
            catch (UnauthorizedAccessException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Create_Success()
        {
            String filePath = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();
            using (TextWriter writer = fileApi.OpenFileForWriting(filePath, encoding))
            {
                writer.Write(fileContent);
            }

            String actualFileContent;
            fileApi.EnsureFileExists(filePath);
            using (TextReader reader = fileApi.OpenFileForReading(filePath, encoding))
            {
                actualFileContent = reader.ReadToEnd();
            }

            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Append_Success()
        {
            String filePath = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before appending to it
            using (TextWriter writer = fileApi.OpenFileForWriting(filePath, encoding))
            {
                writer.Write(fileContent);
            }

            // Now append to the file
            const Boolean appendToFile = true;
            using (TextWriter writer = fileApi.OpenFileForWriting(filePath, encoding, appendToFile))
            {
                writer.Write(fileContent);
            }

            String actualFileContent;
            fileApi.EnsureFileExists(filePath);
            using (TextReader reader = fileApi.OpenFileForReading(filePath, encoding))
            {
                actualFileContent = reader.ReadToEnd();
            }

            Assert.That(actualFileContent, Is.EqualTo(fileContent + fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_CopyFile_Success(String sourceFile)
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String outputFile = Guid.NewGuid().ToString();
            String destinationFilePath = Path.Combine(systemTempFolderPath, outputFile);
            fileApi.CopyFile(sourceFile, destinationFilePath);

            Boolean fileExists = fileApi.DoesFileExist(destinationFilePath);
            Assert.That(fileExists, Is.EqualTo(true));

            Stream sourceFileContent = File.OpenRead(sourceFile);
            Stream destinationFileContent = File.OpenRead(destinationFilePath);
            Assert.That(sourceFileContent, Is.EqualTo(destinationFileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_CopyFile_AlreadyExists(String sourceFile)
        {
            IFileApi fileApi = CreateBusinessProcess();

            String errorMessage = $"The destination file path '{sourceFile}' already exists.";

            Exception actualException = null;
            try
            {
                fileApi.CopyFile(sourceFile, sourceFile);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<IOException>());

            if (actualException is IOException e)
            {
                String actualErrorMessage = actualException.Message;
                Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            }
            else
            {
                Assert.Fail($"Unexpected exception: {actualException}");
            }
        }

        [TestCase]
        public void Test_MoveFile_Success()
        {
            String sourceFile = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before moving it
            using (TextWriter writer = fileApi.OpenFileForWriting(sourceFile, encoding))
            {
                writer.Write(fileContent);
            }

            String systemTempFolderPath = Path.GetTempPath();
            String outputFile = Guid.NewGuid().ToString();
            String destinationFilePath = Path.Combine(systemTempFolderPath, outputFile);

            MemoryStream sourceFileContent = new MemoryStream();
            using (Stream tempFileStream = File.OpenRead(sourceFile))
            {
                tempFileStream.CopyTo(sourceFileContent);
            }

            fileApi.MoveFile(sourceFile, destinationFilePath);

            Boolean sourceFileExists = fileApi.DoesFileExist(sourceFile);
            Assert.That(sourceFileExists, Is.EqualTo(false));

            Boolean destinationFileExists = fileApi.DoesFileExist(destinationFilePath);
            Assert.That(destinationFileExists, Is.EqualTo(true));
            Stream destinationFileContent = File.OpenRead(destinationFilePath);

            Assert.That(sourceFileContent, Is.EqualTo(destinationFileContent));
        }

        [TestCase]
        public void Test_MoveFile_AlreadyExists()
        {
            String sourceFile = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before moving it
            using (TextWriter writer = fileApi.OpenFileForWriting(sourceFile, encoding))
            {
                writer.Write(fileContent);
            }

            String errorMessage = $"The destination file path '{sourceFile}' already exists.";

            Exception actualException = null;
            try
            {
                fileApi.MoveFile(sourceFile, sourceFile);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<IOException>());

            if (actualException is IOException e)
            {
                String actualErrorMessage = actualException.Message;
                Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            }
            else
            {
                Assert.Fail($"Unexpected exception: {actualException}");
            }
        }

        [TestCase]
        public void Test_DeleteFile_Exists()
        {
            String filePath = Guid.NewGuid().ToString();
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before deleting it
            using (TextWriter writer = fileApi.OpenFileForWriting(filePath, encoding))
            {
                writer.Write(fileContent);
            }

            fileApi.EnsureFileExists(filePath);

            fileApi.DeleteFile(filePath);

            Boolean fileExists = fileApi.DoesFileExist(filePath);

            Assert.That(fileExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_DeleteFile_DoesNotExists()
        {
            String filePath = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.DeleteFile(filePath);

            Boolean fileExists = fileApi.DoesFileExist(filePath);

            Assert.That(fileExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_CreateDirectory_DoesNotExist()
        {
            String basePath = ".";
            String newDirectoryName = Guid.NewGuid().ToString();
            const Boolean throwExceptionIfExists = false;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean checkDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName);

            Assert.That(checkDirectoryExists, Is.EqualTo(false));

            fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);

            Boolean actualDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName);

            Assert.That(actualDirectoryExists, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CreateDirectory_AlreadyExist()
        {
            String basePath = ".";
            String newDirectoryName = Guid.NewGuid().ToString();
            Boolean throwExceptionIfExists = false;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean checkDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName);

            Assert.That(checkDirectoryExists, Is.EqualTo(false));

            fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);

            // Create a second time to ensure no exception is raised
            fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);

            checkDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName);

            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            String errorMessage = $@"The directory path '{basePath}\{newDirectoryName}' already exists, the directory '{newDirectoryName}' cannot be created again";

            Exception actualException = null;
            try
            {
                throwExceptionIfExists = true;

                // Call the Create method a second time
                fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_DeleteFolder()
        {
            String basePath1 = ".";
            String newDirectoryName1 = Guid.NewGuid().ToString();

            String basePath2 = Path.Combine(basePath1, newDirectoryName1);
            String newDirectoryName2 = Guid.NewGuid().ToString();

            const Boolean throwExceptionIfExists = false;
            const Boolean recursive = false;
            Boolean checkDirectoryExists;

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.CreateDirectory(basePath1, newDirectoryName1, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName1);
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            fileApi.CreateDirectory(basePath2, newDirectoryName2, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(Path.Combine(basePath2, newDirectoryName2));
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            fileApi.DeleteDirectory(Path.Combine(basePath2, newDirectoryName2), recursive);

            checkDirectoryExists = fileApi.DoesDirectoryExist(Path.Combine(basePath2, newDirectoryName2));

            Assert.That(checkDirectoryExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_DeleteFolderRecursive_Success()
        {
            String basePath1 = ".";
            String newDirectoryName1 = Guid.NewGuid().ToString();

            String basePath2 = Path.Combine(basePath1, newDirectoryName1);
            String newDirectoryName2 = Guid.NewGuid().ToString();

            const Boolean throwExceptionIfExists = false;
            const Boolean recursive = true;
            Boolean checkDirectoryExists;

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.CreateDirectory(basePath1, newDirectoryName1, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(newDirectoryName1);
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            fileApi.CreateDirectory(basePath2, newDirectoryName2, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(Path.Combine(basePath2, newDirectoryName2));
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            fileApi.DeleteDirectory(basePath2, recursive);

            checkDirectoryExists = fileApi.DoesDirectoryExist(Path.Combine(basePath2, newDirectoryName2));
            Assert.That(checkDirectoryExists, Is.EqualTo(false));

            checkDirectoryExists = fileApi.DoesDirectoryExist(basePath2);
            Assert.That(checkDirectoryExists, Is.EqualTo(false));
        }


        [TestCase]
        public void Test_DeleteFolderRecursive_Fail()
        {
            String basePath1 = ".";
            String newDirectoryName1 = Guid.NewGuid().ToString();

            String basePath2 = Path.Combine(basePath1, newDirectoryName1);
            String newDirectoryName2 = Guid.NewGuid().ToString();

            const Boolean throwExceptionIfExists = false;
            const Boolean recursive = false;
            Boolean checkDirectoryExists;

            IFileApi fileApi = CreateBusinessProcess();

            DirectoryInfo createdFolder = fileApi.CreateDirectory(basePath1, newDirectoryName1, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(createdFolder.FullName);
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            fileApi.CreateDirectory(basePath2, newDirectoryName2, throwExceptionIfExists);
            checkDirectoryExists = fileApi.DoesDirectoryExist(Path.Combine(basePath2, newDirectoryName2));
            Assert.That(checkDirectoryExists, Is.EqualTo(true));

            //String errorMessage = $@"The directory is not empty. : '{createdFolder.FullName}'";
            String errorMessage = $"The directory is not empty.{Environment.NewLine}";

            IOException actualException = null;
            try
            {
                fileApi.DeleteDirectory(basePath2, recursive);
            }
            catch (IOException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetNewTempFolderPath_False()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String baseFolder = ".ExpectedResults";
            const Boolean createFolder = false;
            String actual = fileApi.GetNewTempFolderPath(baseFolder, createFolder);

            Assert.That(String.IsNullOrEmpty(actual), Is.EqualTo(false));
            Assert.That(String.IsNullOrWhiteSpace(actual), Is.EqualTo(false));

            String tempFolderPath = Path.GetDirectoryName(actual);

            Assert.That(Path.Combine(systemTempFolderPath, baseFolder), Is.EqualTo(tempFolderPath));
        }

        [TestCase]
        public void Test_GetNewTempFolderPath_True()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String baseFolder = ".ExpectedResults";
            const Boolean createFolder = true;
            String actual = fileApi.GetNewTempFolderPath(baseFolder, createFolder);

            Assert.That(String.IsNullOrEmpty(actual), Is.EqualTo(false));
            Assert.That(String.IsNullOrWhiteSpace(actual), Is.EqualTo(false));

            Boolean folderExists = fileApi.DoesDirectoryExist(actual);
            Assert.That(folderExists, Is.EqualTo(true));

            String tempFolderPath = Path.GetDirectoryName(actual);
            Assert.That(Path.Combine(systemTempFolderPath, baseFolder), Is.EqualTo(tempFolderPath));
            fileApi.DeleteDirectory(tempFolderPath, recursive: true);
        }

        [TestCase]
        public void Test_WriteFileContent()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String outputFile = Guid.NewGuid().ToString();
            String outputPath = Path.Combine(systemTempFolderPath, outputFile);
            String fileOutput1 = "Sample text";
            String fileOutput2 = "New data";

            MemoryStream memoryStream1 = new MemoryStream();
            StreamWriter sw1 = new StreamWriter(memoryStream1);
            sw1.Write(fileOutput1);
            sw1.Flush();

            fileApi.WriteFileContent(outputPath, memoryStream1);
            Boolean fileWrite1FileExists = fileApi.DoesFileExist(outputPath);
            Assert.That(fileWrite1FileExists, Is.EqualTo(true));

            MemoryStream memoryStream2 = new MemoryStream();
            StreamWriter sw2 = new StreamWriter(memoryStream2);
            sw2.Write(fileOutput2);
            sw2.Flush();

            fileApi.WriteFileContent(outputPath, memoryStream2, overwriteIfFileExists: true);
            Boolean fileWrite2FileExists = fileApi.DoesFileExist(outputPath);
            Assert.That(fileWrite2FileExists, Is.EqualTo(true));

            String fileContent = fileApi.GetFileContentsAsText(outputPath, Encoding.Default);
            Assert.That(fileContent, Is.EqualTo(fileOutput2));
        }

        [TestCase]
        public void Test_WriteFileContent_Overwrite()
        {
            IFileApi fileApi = CreateBusinessProcess();
            String systemTempFolderPath = Path.GetTempPath();
            String outputFile = Guid.NewGuid().ToString();
            String outputPath = Path.Combine(systemTempFolderPath, outputFile);
            String fileOutput1 = "Sample text";
            String fileOutput2 = "New data";

            MemoryStream memoryStream1 = new MemoryStream();
            StreamWriter sw1 = new StreamWriter(memoryStream1);
            sw1.Write(fileOutput1);
            sw1.Flush();

            fileApi.WriteFileContent(outputPath, memoryStream1);
            Boolean fileWrite1FileExists = fileApi.DoesFileExist(outputPath);
            Assert.That(fileWrite1FileExists, Is.EqualTo(true));

            MemoryStream memoryStream2 = new MemoryStream();
            StreamWriter sw2 = new StreamWriter(memoryStream2);
            sw2.Write(fileOutput2);
            sw2.Flush();

            fileApi.WriteFileContent(outputPath, memoryStream2, overwriteIfFileExists: true);
            Boolean fileWrite2FileExists = fileApi.DoesFileExist(outputPath);
            Assert.That(fileWrite2FileExists, Is.EqualTo(true));

            String fileContent = fileApi.GetFileContentsAsText(outputPath, Encoding.Default);
            Assert.That(fileContent, Is.EqualTo(fileOutput2));
        }

        [TestCase]
        public void Test_RemoteServiceAPI_DeleteFile_FileExists()
        {
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = Guid.NewGuid().ToString()
            };
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before deleting to it
            using (TextWriter writer = fileApi.OpenFileForWriting(fileTransferSettings.Location, encoding))
            {
                writer.Write(fileContent);
            }

            fileApi.EnsureFileExists(fileTransferSettings.Location);

            fileApi.DeleteFile(fileTransferSettings);

            Boolean fileExists = fileApi.DoesFileExist(fileTransferSettings.Location);

            Assert.That(fileExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_RemoteServerAPI_DeleteFile_DoesNotExists()
        {
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = Guid.NewGuid().ToString()
            };

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.DeleteFile(fileTransferSettings);

            Boolean fileExists = fileApi.DoesFileExist(fileTransferSettings.Location);

            Assert.That(fileExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_RemoteServiceAPI_DeleteFileAsync_FileExists()
        {
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = Guid.NewGuid().ToString()
            };
            Encoding encoding = Encoding.UTF8;
            String fileContent = Guid.NewGuid().ToString();

            IFileApi fileApi = CreateBusinessProcess();

            // Create the file first, before deleting to it
            using (TextWriter writer = fileApi.OpenFileForWriting(fileTransferSettings.Location, encoding))
            {
                writer.Write(fileContent);
            }

            fileApi.EnsureFileExists(fileTransferSettings.Location);

            Task t = fileApi.DeleteFileAsync(fileTransferSettings);
            t.Wait();

            Boolean fileExists = fileApi.DoesFileExist(fileTransferSettings.Location);

            Assert.That(fileExists, Is.EqualTo(false));
        }


        [TestCase]
        public void Test_RemoteServerAPI_DeleteFileAsync_DoesNotExists()
        {
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                Location = Guid.NewGuid().ToString()
            };

            IFileApi fileApi = CreateBusinessProcess();

            Task t = fileApi.DeleteFileAsync(fileTransferSettings);
            t.Wait();

            Boolean fileExists = fileApi.DoesFileExist(fileTransferSettings.Location);

            Assert.That(fileExists, Is.EqualTo(false));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_RemoteServiceAPI_DownloadFileAsStream(String sourceFile)
        {
            IFileApi fileApi = CreateBusinessProcess();
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = sourceFile,
            };

            Stream fileContent = fileApi.DownloadFile(fileTransferSettings);

            Stream actualFileContent = File.OpenRead(sourceFile);
            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_RemoteServiceAPI_DownloadFileAsStream_Async(String sourceFile)
        {
            IFileApi fileApi = CreateBusinessProcess();
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = sourceFile,
            };

            Task<Stream> t = fileApi.DownloadFileAsync(fileTransferSettings);

            t.Wait();
            Stream fileContent = t.Result;

            Stream actualFileContent = File.OpenRead(sourceFile);
            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_RemoteServiceAPI_UploadFileAsStream(String sourceFile)
        {
            String targetLocation = Guid.NewGuid().ToString();
            IFileApi fileApi = CreateBusinessProcess();
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = targetLocation,
            };

            Stream fileContent = File.OpenRead(sourceFile);

            String newLocation = fileApi.UploadFile(fileTransferSettings, fileContent);

            Stream actualFileContent = File.OpenRead(newLocation);
            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_RemoteServiceAPI_UploadFileAsStream_Async(String sourceFile)
        {
            String targetLocation = Guid.NewGuid().ToString();
            IFileApi fileApi = CreateBusinessProcess();
            IFileTransferSettings fileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = targetLocation,
            };

            Stream fileContent = File.OpenRead(sourceFile);

            Task<String> t = fileApi.UploadFileAsync(fileTransferSettings, fileContent);

            t.Wait();
            String newLocation = t.Result;

            Stream actualFileContent = File.OpenRead(newLocation);
            Assert.That(actualFileContent, Is.EqualTo(fileContent));
        }

        [TestCase(@".Support\SampleDocuments\Sample Text Document.txt")]
        [TestCase(@".Support\SampleDocuments\32BitColour_16x16.bmp")]
        [TestCase(@".Support\SampleDocuments\Sample PDF Document.pdf")]
        [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
        [DeploymentItem(@".Support\SampleDocuments\Sample PDF Document.pdf", @".Support\SampleDocuments\")]
        public void Test_RemoteServiceAPI_DownloadFile_Then_Upload(String sourceFile)
        {
            IFileApi fileApi = CreateBusinessProcess();

            IFileTransferSettings sourceFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = sourceFile,
            };
            Stream fileContent = fileApi.DownloadFile(sourceFileTransferSettings);

            IFileTransferSettings destinationFileTransferSettings = new FileTransferSettings
            {
                FileTransferMethod = FileTransferMethod.FileSystem,
                Location = Path.Combine(@".Support\SampleDocuments\", Guid.NewGuid().ToString()),
            };
            fileApi.UploadFile(destinationFileTransferSettings, fileContent);

            Boolean fileExists = fileApi.DoesFileExist(destinationFileTransferSettings.Location);
            Assert.That(fileExists, Is.EqualTo(true));

            Stream sourceFileContent = File.OpenRead(sourceFileTransferSettings.Location);
            Stream destinationFileContent = File.OpenRead(destinationFileTransferSettings.Location);
            Assert.That(sourceFileContent, Is.EqualTo(destinationFileContent));
        }
    }
}
