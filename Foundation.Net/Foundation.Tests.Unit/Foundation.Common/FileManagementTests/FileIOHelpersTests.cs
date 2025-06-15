//-----------------------------------------------------------------------
// <copyright file="FileIOHelperTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;
using Foundation.Services.Application;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.FileManagementTests
{
    /// <summary>
    /// The File IO Helper tests
    /// </summary>
    [TestFixture]
    [DeploymentItem(@".ExpectedResults\FileManagement\ExistingFile.txt", @".ExpectedResults\FileManagement\")]
    [DeploymentItem(@".ExpectedResults\FileManagement\FileToDelete.txt", @".ExpectedResults\FileManagement\")]
    [DeploymentItem(@".ExpectedResults\FileManagement\SmallFile.txt", @".ExpectedResults\FileManagement\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample Word Document.docx", @".Support\SampleDocuments\")]
    [DeploymentItem(@".Support\SampleDocuments\Sample Text Document.txt", @".Support\SampleDocuments\")]
    public class FileIOHelperTests : UnitTestBase
    {
        private IApplicationConfigurationProcess ApplicationConfigurationProcess { get; set; }

        private IFileApi CreateBusinessProcess()
        {
            ApplicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            IFileApi retVal = new FileService(CoreInstance, ApplicationConfigurationProcess);

            return retVal;
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_UserDataPath()
        {
            String expectedFilePath = @".\UserData\";

            IFileApi fileApi = CreateBusinessProcess();
            ApplicationConfigurationProcess.GetValue<String>(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), ApplicationConfigurationKeys.UserDataPath).Returns(expectedFilePath);

            String actualFilePath = fileApi.UserDataPath;

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_SystemDataPath()
        {
            String expectedFilePath = @"\SystemData\";

            IFileApi fileApi = CreateBusinessProcess();
            ApplicationConfigurationProcess.GetValue<String>(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), ApplicationConfigurationKeys.SystemDataPath).Returns(expectedFilePath);

            String actualFilePath = fileApi.SystemDataPath;

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_Folder_NoTrailingSlash()
        {
            String baseFolder = @"C:\Root";
            String targetFolder = "targetFolderPath";
            String expectedFilePath = @"C:\Root\targetFolderPath\";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_Folder_WithTrailingSlash_1()
        {
            String baseFolder = @"C:\Root\";
            String targetFolder = @"targetFolderPath\";
            String expectedFilePath = @"C:\Root\targetFolderPath\";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_Folder_WithTrailingSlash_2()
        {
            String baseFolder = @"Root\";
            String targetFolder = @"targetFolderPath\";
            String expectedFilePath = @"Root\targetFolderPath\";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_File_NoTrailingSlash()
        {
            String baseFolder = @"C:\Root";
            String targetFolder = "targetFolderPath";
            String fileName = "MyFileName.txt";
            String expectedFilePath = @"C:\Root\targetFolderPath\MyFileName.txt";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder, fileName);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_File_WithTrailingSlash_1()
        {
            String baseFolder = @"C:\Root\";
            String targetFolder = @"targetFolderPath\";
            String fileName = "MyFileName.txt";
            String expectedFilePath = @"C:\Root\targetFolderPath\MyFileName.txt";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder, fileName);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MakeDataPath_File_WithTrailingSlash_2()
        {
            String baseFolder = @"Root\";
            String targetFolder = @"targetFolderPath\";
            String fileName = "MyFileName.txt";
            String expectedFilePath = @"Root\targetFolderPath\MyFileName.txt";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFilePath = fileApi.MakeDataPath(baseFolder, targetFolder, fileName);

            Assert.That(actualFilePath, Is.EqualTo(expectedFilePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureFileExists_Fail()
        {
            String filePath = @"C:\FakeFile.txt";

            String expectedMessage = $"The file '{filePath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureFileExists_Success()
        {
            String filePath = @"C:\Windows\Explorer.exe";

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.EnsureFileExists(filePath);
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureDirectoryExists_NoTrailingSlash_Fail()
        {
            String directoryPath = @"C:\FakeDirectory";

            String expectedMessage = $"The directory '{directoryPath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureDirectoryExists_WithTrailingSlash_Fail()
        {
            String directoryPath = @"C:\FakeDirectory\";

            String expectedMessage = $"The directory '{directoryPath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureDirectoryExists_NoTrailingSlash_Success()
        {
            String directoryPath = @"C:\Windows";

            Exception actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();

                fileApi.EnsureDirectoryExists(directoryPath);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.EqualTo(null));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EnsureDirectoryExists_WithTrailingSlash_Success()
        {
            String directoryPath = @"C:\Windows\";

            Exception actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();

                fileApi.EnsureDirectoryExists(directoryPath);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_DoesFileExist_Fail()
        {
            const String filePath = @"C:\FakeFile.txt";
            const Boolean expectedResult = false;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesFileExist(filePath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_DoesFileExist_Success()
        {
            const String filePath = @"C:\Windows\Explorer.exe";
            const Boolean expectedResult = true;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesFileExist(filePath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_DoesDirectoryExist_NoTrailingSlash_Fail()
        {
            const String directoryPath = @"C:\FakeDirectory";
            const Boolean expectedResult = false;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesDirectoryExist(directoryPath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_DoesDirectoryExist_WithTrailingSlash_Fail()
        {
            const String directoryPath = @"C:\FakeDirectory\";
            const Boolean expectedResult = false;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesDirectoryExist(directoryPath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_DoesDirectoryExist_NoTrailingSlash_Success()
        {
            const String directoryPath = @"C:\Windows";
            const Boolean expectedResult = true;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesDirectoryExist(directoryPath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_DoesDirectoryExist_WithTrailingSlash_Success()
        {
            const String directoryPath = @"C:\Windows\";
            const Boolean expectedResult = true;

            IFileApi fileApi = CreateBusinessProcess();

            Boolean actualResult = fileApi.DoesDirectoryExist(directoryPath);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase]
        public void Test_GetFileContentsAsText_Fail()
        {
            const String filePath = @"C:\FakeFile.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedMessage = $"The file '{filePath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_GetFileContentsAsText_Success()
        {
            const String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Encoding encoding = Encoding.UTF8;
            const String expectedFileContent = "Just a small text file";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFileContent = fileApi.GetFileContentsAsText(filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(expectedFileContent));
        }

        [TestCase]
        public void Test_GetFileContentsAsByteArray_Fail_NoFile()
        {
            const String filePath = @"C:\FakeFile.txt";

            String expectedMessage = $"The file '{filePath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_GetFileContentsAsByteArray_Success()
        {
            const String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Byte[] expectedFileContent = { 239, 187, 191, 74, 117, 115, 116, 32, 97, 32, 115, 109, 97, 108, 108, 32, 116, 101, 120, 116, 32, 102, 105, 108, 101 };

            IFileApi fileApi = CreateBusinessProcess();

            Byte[] actualFileContent = fileApi.GetFileContentsAsByteArray(filePath);

            Assert.That(actualFileContent, Is.EquivalentTo(expectedFileContent));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_GetAssemblyResource_Fail()
        {
            const String filePath = "FakeFile.txt";

            String expectedMessage = $"Resource File '{filePath}' does not exist in the Assembly 'Foundation.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase]
        public void Test_GetFileContentsAsTextFromAssembly()
        {
            const String filePath = "Foundation.Tests.Unit..Support.SampleDocuments.Embedded Sample Text Document.txt";
            Encoding encoding = Encoding.UTF8;
            const String expectedFileContent = "A sample text document";

            IFileApi fileApi = CreateBusinessProcess();

            String actualFileContent = fileApi.GetFileContentsAsTextFromAssembly(Assembly.GetAssembly(this.GetType()), filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(expectedFileContent));
        }

        [TestCase]
        public void Test_GetFileContentsAsByteArrayFromAssembly()
        {
            const String filePath = "Foundation.Tests.Unit..Support.SampleDocuments.Embedded Sample Word Document.docx";
            Byte[] expectedFileContent = { 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 223, 164, 210, 108, 90, 1, 0, 0, 32, 5, 0, 0, 19, 0, 8, 2, 91, 67, 111, 110, 116, 101, 110, 116, 95, 84, 121, 112, 101, 115, 93, 46, 120, 109, 108, 32, 162, 4, 2, 40, 160, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 180, 148, 203, 110, 194, 48, 16, 69, 247, 149, 250, 15, 145, 183, 85, 98, 232, 162, 170, 42, 2, 139, 62, 150, 45, 82, 233, 7, 24, 123, 2, 86, 253, 146, 199, 188, 254, 190, 19, 2, 81, 85, 1, 145, 10, 108, 34, 37, 51, 247, 222, 51, 86, 198, 131, 209, 218, 154, 108, 9, 17, 181, 119, 37, 235, 23, 61, 150, 129, 147, 94, 105, 55, 43, 217, 215, 228, 45, 127, 100, 25, 38, 225, 148, 48, 222, 65, 201, 54, 128, 108, 52, 188, 189, 25, 76, 54, 1, 48, 35, 181, 195, 146, 205, 83, 10, 79, 156, 163, 156, 131, 21, 88, 248, 0, 142, 42, 149, 143, 86, 36, 122, 141, 51, 30, 132, 252, 22, 51, 224, 247, 189, 222, 3, 151, 222, 37, 112, 41, 79, 181, 7, 27, 14, 94, 160, 18, 11, 147, 178, 215, 53, 125, 110, 72, 34, 24, 100, 217, 115, 211, 88, 103, 149, 76, 132, 96, 180, 20, 137, 234, 124, 233, 212, 159, 148, 124, 151, 80, 144, 114, 219, 131, 115, 29, 240, 142, 26, 24, 63, 152, 80, 87, 142, 7, 236, 116, 31, 116, 52, 81, 43, 200, 198, 34, 166, 119, 97, 169, 139, 175, 124, 84, 92, 121, 185, 176, 164, 44, 78, 219, 28, 224, 244, 85, 165, 37, 180, 250, 218, 45, 68, 47, 1, 145, 206, 220, 154, 162, 173, 88, 161, 221, 158, 255, 40, 7, 166, 141, 1, 188, 60, 69, 227, 219, 29, 15, 41, 145, 224, 26, 0, 59, 231, 78, 132, 21, 76, 63, 175, 70, 241, 203, 188, 19, 164, 162, 220, 137, 152, 26, 184, 60, 70, 107, 221, 9, 145, 104, 3, 161, 121, 246, 207, 230, 216, 218, 156, 138, 164, 206, 113, 244, 1, 105, 163, 227, 63, 198, 222, 175, 108, 173, 206, 105, 224, 0, 49, 233, 211, 127, 93, 155, 72, 214, 103, 207, 7, 245, 109, 160, 64, 29, 200, 230, 219, 251, 109, 248, 3, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 30, 145, 26, 183, 239, 0, 0, 0, 78, 2, 0, 0, 11, 0, 8, 2, 95, 114, 101, 108, 115, 47, 46, 114, 101, 108, 115, 32, 162, 4, 2, 40, 160, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 146, 193, 106, 195, 48, 12, 64, 239, 131, 253, 131, 209, 189, 81, 218, 193, 24, 163, 78, 47, 99, 208, 219, 24, 217, 7, 8, 91, 73, 76, 19, 219, 216, 106, 215, 254, 253, 60, 216, 216, 2, 93, 233, 97, 71, 203, 210, 211, 147, 208, 122, 115, 156, 70, 117, 224, 148, 93, 240, 26, 150, 85, 13, 138, 189, 9, 214, 249, 94, 195, 91, 251, 188, 120, 0, 149, 133, 188, 165, 49, 120, 214, 112, 226, 12, 155, 230, 246, 102, 253, 202, 35, 73, 41, 202, 131, 139, 89, 21, 138, 207, 26, 6, 145, 248, 136, 152, 205, 192, 19, 229, 42, 68, 246, 229, 167, 11, 105, 34, 41, 207, 212, 99, 36, 179, 163, 158, 113, 85, 215, 247, 152, 126, 51, 160, 153, 49, 213, 214, 106, 72, 91, 123, 7, 170, 61, 69, 190, 134, 29, 186, 206, 25, 126, 10, 102, 63, 177, 151, 51, 45, 144, 143, 194, 222, 178, 93, 196, 84, 234, 147, 184, 50, 141, 106, 41, 245, 44, 26, 108, 48, 47, 37, 156, 145, 98, 172, 10, 26, 240, 188, 209, 234, 122, 163, 191, 167, 197, 137, 133, 44, 9, 161, 9, 137, 47, 251, 124, 102, 92, 18, 90, 254, 231, 138, 230, 25, 63, 54, 239, 33, 89, 180, 95, 225, 111, 27, 156, 93, 65, 243, 1, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 64, 225, 183, 114, 214, 2, 0, 0, 69, 10, 0, 0, 17, 0, 0, 0, 119, 111, 114, 100, 47, 100, 111, 99, 117, 109, 101, 110, 116, 46, 120, 109, 108, 164, 150, 221, 111, 155, 48, 16, 192, 223, 39, 237, 127, 64, 188, 183, 134, 64, 190, 80, 147, 106, 91, 154, 170, 15, 149, 170, 101, 211, 30, 39, 199, 152, 15, 5, 127, 200, 118, 66, 179, 191, 126, 103, 8, 33, 19, 93, 69, 232, 11, 96, 159, 239, 231, 187, 243, 221, 225, 187, 251, 87, 86, 56, 7, 170, 116, 46, 248, 194, 245, 111, 61, 215, 161, 156, 136, 56, 231, 233, 194, 253, 249, 99, 125, 51, 115, 29, 109, 48, 143, 113, 33, 56, 93, 184, 71, 170, 221, 251, 229, 231, 79, 119, 101, 20, 11, 178, 103, 148, 27, 7, 16, 92, 71, 165, 36, 11, 55, 51, 70, 70, 8, 105, 146, 81, 134, 245, 45, 203, 137, 18, 90, 36, 230, 150, 8, 134, 68, 146, 228, 132, 162, 82, 168, 24, 141, 60, 223, 171, 190, 164, 18, 132, 106, 13, 251, 125, 195, 252, 128, 181, 123, 194, 145, 215, 126, 180, 88, 225, 18, 148, 45, 48, 68, 36, 195, 202, 208, 215, 150, 225, 95, 13, 25, 163, 57, 154, 117, 65, 163, 1, 32, 240, 112, 228, 119, 81, 193, 213, 168, 9, 178, 86, 117, 64, 225, 32, 16, 88, 213, 33, 141, 135, 145, 222, 112, 110, 50, 140, 52, 234, 146, 166, 195, 72, 65, 151, 52, 27, 70, 234, 164, 19, 235, 38, 184, 144, 148, 131, 48, 17, 138, 97, 3, 67, 149, 34, 134, 213, 110, 47, 111, 0, 44, 177, 201, 183, 121, 145, 155, 35, 48, 189, 73, 131, 193, 57, 223, 13, 176, 8, 180, 206, 4, 22, 196, 87, 19, 166, 136, 137, 152, 22, 65, 220, 80, 196, 194, 221, 43, 30, 157, 244, 111, 206, 250, 214, 244, 168, 214, 63, 189, 26, 13, 213, 199, 255, 90, 101, 117, 106, 14, 149, 231, 72, 209, 2, 98, 33, 184, 206, 114, 121, 174, 112, 54, 148, 6, 194, 172, 129, 28, 222, 115, 226, 192, 138, 102, 93, 41, 253, 158, 229, 242, 191, 246, 180, 170, 67, 217, 2, 251, 152, 127, 138, 63, 43, 106, 203, 223, 39, 250, 94, 143, 19, 177, 136, 179, 70, 31, 19, 254, 221, 179, 177, 132, 65, 22, 182, 27, 15, 10, 205, 69, 112, 253, 158, 13, 164, 1, 140, 58, 128, 9, 201, 123, 166, 116, 195, 168, 163, 9, 254, 128, 230, 5, 71, 211, 235, 48, 227, 6, 163, 143, 172, 45, 245, 82, 166, 31, 203, 150, 71, 37, 246, 178, 165, 229, 31, 163, 61, 181, 181, 95, 218, 191, 240, 21, 172, 83, 214, 93, 86, 130, 254, 152, 49, 155, 12, 75, 104, 9, 140, 68, 79, 41, 23, 10, 111, 11, 176, 8, 114, 200, 129, 52, 112, 170, 19, 176, 79, 56, 21, 199, 22, 157, 187, 132, 171, 194, 86, 196, 71, 251, 150, 32, 9, 35, 137, 21, 126, 130, 211, 14, 39, 171, 245, 195, 124, 254, 197, 173, 102, 161, 209, 154, 106, 214, 159, 205, 215, 193, 124, 10, 179, 17, 92, 75, 226, 239, 11, 215, 243, 166, 225, 116, 62, 245, 206, 83, 43, 154, 224, 125, 97, 172, 36, 240, 131, 209, 120, 93, 237, 162, 236, 195, 44, 55, 152, 201, 130, 58, 207, 27, 231, 23, 216, 237, 52, 215, 148, 59, 100, 133, 246, 89, 173, 219, 10, 177, 179, 173, 122, 99, 160, 199, 3, 215, 166, 95, 181, 1, 199, 12, 252, 249, 253, 40, 190, 98, 178, 115, 209, 229, 218, 7, 30, 159, 87, 162, 10, 37, 173, 88, 83, 98, 94, 212, 27, 230, 86, 46, 167, 155, 63, 32, 130, 82, 245, 253, 185, 253, 9, 148, 81, 6, 223, 147, 89, 48, 171, 225, 50, 125, 198, 86, 217, 8, 232, 40, 126, 24, 214, 94, 230, 105, 102, 218, 225, 86, 24, 35, 88, 59, 46, 104, 114, 33, 205, 40, 142, 41, 244, 230, 169, 55, 179, 195, 68, 8, 115, 49, 76, 247, 166, 26, 86, 38, 151, 17, 17, 133, 134, 89, 45, 49, 161, 245, 154, 106, 26, 162, 244, 168, 236, 145, 69, 69, 206, 233, 75, 110, 8, 88, 25, 76, 26, 63, 107, 23, 171, 207, 250, 44, 81, 123, 255, 91, 254, 5, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 214, 100, 179, 81, 244, 0, 0, 0, 49, 3, 0, 0, 28, 0, 8, 1, 119, 111, 114, 100, 47, 95, 114, 101, 108, 115, 47, 100, 111, 99, 117, 109, 101, 110, 116, 46, 120, 109, 108, 46, 114, 101, 108, 115, 32, 162, 4, 1, 40, 160, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 146, 203, 106, 195, 48, 16, 69, 247, 133, 254, 131, 152, 125, 45, 59, 125, 80, 66, 228, 108, 74, 33, 219, 214, 253, 0, 69, 30, 63, 168, 44, 9, 205, 244, 225, 191, 175, 72, 73, 235, 208, 96, 186, 240, 114, 174, 152, 115, 207, 128, 54, 219, 207, 193, 138, 119, 140, 212, 123, 167, 160, 200, 114, 16, 232, 140, 175, 123, 215, 42, 120, 169, 30, 175, 238, 65, 16, 107, 87, 107, 235, 29, 42, 24, 145, 96, 91, 94, 94, 108, 158, 208, 106, 78, 75, 212, 245, 129, 68, 162, 56, 82, 208, 49, 135, 181, 148, 100, 58, 28, 52, 101, 62, 160, 75, 47, 141, 143, 131, 230, 52, 198, 86, 6, 109, 94, 117, 139, 114, 149, 231, 119, 50, 78, 25, 80, 158, 48, 197, 174, 86, 16, 119, 245, 53, 136, 106, 12, 248, 31, 182, 111, 154, 222, 224, 131, 55, 111, 3, 58, 62, 83, 33, 63, 112, 255, 140, 204, 233, 56, 74, 88, 29, 91, 100, 5, 147, 48, 75, 68, 144, 231, 69, 86, 75, 138, 208, 31, 139, 99, 50, 167, 80, 44, 170, 192, 163, 197, 169, 192, 97, 158, 171, 191, 93, 178, 158, 211, 46, 254, 182, 31, 198, 239, 176, 152, 115, 184, 89, 210, 161, 241, 142, 43, 189, 183, 19, 143, 159, 232, 40, 33, 79, 62, 122, 249, 5, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 211, 19, 8, 67, 34, 6, 0, 0, 139, 26, 0, 0, 21, 0, 0, 0, 119, 111, 114, 100, 47, 116, 104, 101, 109, 101, 47, 116, 104, 101, 109, 101, 49, 46, 120, 109, 108, 236, 89, 77, 139, 27, 55, 24, 190, 23, 250, 31, 196, 220, 29, 127, 205, 248, 99, 137, 55, 216, 99, 59, 105, 179, 155, 132, 236, 38, 37, 71, 121, 70, 158, 81, 172, 25, 25, 73, 222, 93, 19, 2, 37, 57, 22, 10, 165, 105, 233, 161, 129, 222, 122, 40, 109, 3, 9, 244, 146, 254, 154, 109, 83, 218, 20, 242, 23, 42, 105, 60, 246, 200, 150, 89, 218, 108, 96, 41, 89, 195, 90, 31, 207, 251, 234, 209, 251, 74, 143, 52, 158, 203, 87, 78, 18, 2, 142, 16, 227, 152, 166, 29, 167, 122, 169, 226, 0, 148, 6, 52, 196, 105, 212, 113, 238, 28, 14, 75, 45, 7, 112, 1, 211, 16, 18, 154, 162, 142, 51, 71, 220, 185, 178, 251, 225, 7, 151, 225, 142, 136, 81, 130, 128, 180, 79, 249, 14, 236, 56, 177, 16, 211, 157, 114, 153, 7, 178, 25, 242, 75, 116, 138, 82, 217, 55, 166, 44, 129, 66, 86, 89, 84, 14, 25, 60, 150, 126, 19, 82, 174, 85, 42, 141, 114, 2, 113, 234, 128, 20, 38, 210, 237, 205, 241, 24, 7, 8, 28, 42, 151, 206, 110, 238, 124, 64, 228, 191, 84, 112, 213, 16, 16, 118, 160, 92, 35, 195, 66, 99, 195, 73, 85, 125, 241, 57, 247, 9, 3, 71, 144, 116, 28, 57, 78, 72, 143, 15, 209, 137, 112, 0, 129, 92, 200, 142, 142, 83, 209, 127, 78, 121, 247, 114, 121, 105, 68, 196, 22, 219, 130, 221, 80, 255, 45, 236, 22, 6, 225, 164, 166, 237, 88, 52, 90, 26, 186, 174, 231, 54, 186, 75, 255, 26, 64, 196, 38, 110, 208, 28, 52, 6, 141, 165, 63, 13, 128, 65, 32, 103, 154, 113, 49, 125, 54, 107, 190, 187, 192, 22, 64, 89, 209, 226, 187, 223, 236, 215, 171, 6, 190, 224, 191, 190, 129, 239, 122, 234, 99, 224, 53, 40, 43, 186, 27, 248, 225, 208, 95, 197, 176, 0, 202, 138, 222, 6, 222, 235, 181, 123, 125, 211, 191, 6, 101, 197, 198, 6, 190, 89, 233, 246, 221, 166, 129, 215, 160, 152, 224, 116, 178, 129, 174, 120, 141, 186, 159, 207, 118, 9, 25, 83, 114, 205, 10, 111, 123, 238, 176, 89, 91, 192, 87, 168, 114, 97, 117, 101, 246, 169, 216, 182, 214, 18, 120, 159, 178, 161, 4, 232, 228, 66, 129, 83, 32, 230, 83, 52, 134, 129, 196, 249, 144, 224, 17, 195, 96, 15, 71, 177, 92, 120, 83, 152, 82, 46, 155, 43, 181, 202, 176, 82, 151, 255, 213, 199, 213, 37, 157, 81, 184, 131, 96, 193, 58, 107, 10, 248, 70, 147, 226, 3, 120, 192, 240, 84, 116, 156, 143, 165, 87, 167, 0, 121, 243, 242, 199, 55, 47, 159, 131, 211, 71, 47, 78, 31, 253, 114, 250, 248, 241, 233, 163, 159, 45, 86, 215, 96, 26, 21, 173, 94, 127, 255, 197, 223, 79, 63, 5, 127, 61, 255, 238, 245, 147, 175, 236, 120, 94, 196, 255, 254, 211, 103, 191, 253, 250, 165, 29, 40, 138, 192, 87, 95, 63, 251, 227, 197, 179, 87, 223, 124, 254, 231, 15, 79, 44, 240, 46, 131, 163, 34, 252, 16, 39, 136, 131, 27, 232, 24, 220, 166, 137, 156, 152, 101, 0, 52, 98, 255, 206, 226, 48, 134, 184, 104, 209, 77, 35, 14, 83, 168, 108, 44, 232, 129, 136, 13, 244, 141, 57, 36, 208, 130, 235, 33, 51, 130, 119, 153, 148, 9, 27, 240, 234, 236, 190, 65, 248, 32, 102, 51, 129, 45, 192, 235, 113, 98, 0, 247, 41, 37, 61, 202, 172, 115, 186, 174, 198, 42, 70, 97, 150, 70, 246, 193, 217, 172, 136, 187, 13, 225, 145, 109, 108, 127, 45, 191, 131, 217, 84, 174, 119, 108, 115, 233, 199, 200, 160, 121, 139, 200, 148, 195, 8, 165, 72, 0, 213, 71, 39, 8, 89, 204, 238, 97, 108, 196, 117, 31, 7, 140, 114, 58, 22, 224, 30, 6, 61, 136, 173, 33, 57, 196, 35, 99, 53, 173, 140, 174, 225, 68, 230, 101, 110, 35, 40, 243, 109, 196, 102, 255, 46, 232, 81, 98, 115, 223, 71, 71, 38, 82, 238, 10, 72, 108, 46, 17, 49, 194, 120, 21, 206, 4, 76, 172, 140, 97, 66, 138, 200, 61, 40, 98, 27, 201, 131, 57, 11, 140, 128, 115, 33, 51, 29, 33, 66, 193, 32, 68, 156, 219, 108, 110, 178, 185, 65, 247, 186, 148, 23, 123, 218, 247, 201, 60, 49, 145, 76, 224, 137, 13, 185, 7, 41, 45, 34, 251, 116, 226, 199, 48, 153, 90, 57, 227, 52, 46, 98, 63, 226, 19, 185, 68, 33, 184, 69, 133, 149, 4, 53, 119, 136, 170, 203, 60, 192, 116, 107, 186, 239, 98, 100, 164, 251, 236, 189, 125, 71, 42, 171, 125, 129, 168, 158, 25, 179, 109, 9, 68, 205, 253, 56, 39, 99, 136, 180, 243, 242, 154, 158, 39, 56, 61, 83, 220, 215, 100, 221, 123, 183, 178, 46, 133, 244, 213, 183, 79, 237, 186, 123, 33, 5, 189, 203, 176, 117, 71, 173, 203, 248, 54, 220, 186, 120, 251, 148, 133, 248, 226, 107, 119, 31, 206, 210, 91, 72, 110, 23, 11, 244, 189, 116, 191, 151, 238, 255, 189, 116, 111, 219, 207, 231, 47, 216, 43, 141, 214, 151, 248, 252, 170, 174, 221, 36, 91, 239, 237, 99, 76, 200, 129, 152, 19, 180, 199, 181, 186, 115, 57, 189, 112, 40, 27, 117, 69, 27, 45, 31, 19, 166, 177, 44, 46, 134, 51, 112, 17, 131, 186, 12, 24, 21, 159, 96, 17, 31, 196, 112, 42, 135, 169, 234, 17, 34, 190, 112, 29, 113, 48, 165, 92, 158, 15, 186, 217, 234, 91, 117, 144, 89, 178, 79, 195, 172, 181, 90, 205, 159, 76, 165, 1, 20, 171, 118, 121, 190, 228, 237, 242, 52, 18, 89, 107, 163, 185, 122, 4, 91, 186, 215, 181, 72, 63, 42, 231, 4, 148, 237, 191, 33, 81, 24, 204, 36, 81, 183, 144, 104, 230, 141, 103, 144, 208, 51, 59, 23, 22, 109, 11, 139, 150, 114, 191, 149, 133, 254, 90, 100, 69, 238, 63, 0, 213, 143, 26, 158, 155, 49, 146, 235, 13, 18, 20, 170, 60, 101, 246, 121, 118, 207, 61, 211, 219, 130, 105, 78, 187, 102, 153, 94, 91, 113, 61, 159, 76, 27, 36, 10, 203, 205, 36, 81, 88, 134, 49, 12, 209, 122, 243, 57, 231, 186, 189, 74, 169, 65, 79, 133, 98, 147, 70, 179, 245, 46, 114, 173, 68, 100, 77, 27, 72, 106, 214, 192, 177, 220, 115, 117, 79, 186, 9, 224, 180, 227, 140, 229, 205, 80, 22, 147, 169, 244, 199, 149, 110, 66, 18, 165, 29, 39, 16, 139, 64, 255, 23, 101, 153, 50, 46, 250, 144, 199, 25, 76, 119, 101, 243, 79, 176, 64, 12, 16, 156, 200, 181, 94, 76, 3, 73, 87, 220, 170, 181, 166, 154, 227, 5, 37, 215, 174, 92, 188, 200, 233, 175, 98, 146, 209, 120, 140, 2, 177, 165, 101, 85, 149, 125, 153, 19, 107, 239, 91, 130, 85, 133, 206, 36, 233, 131, 56, 60, 6, 35, 50, 99, 183, 161, 12, 148, 215, 172, 170, 0, 134, 152, 139, 101, 52, 67, 204, 10, 139, 123, 21, 197, 53, 185, 90, 108, 69, 227, 23, 179, 213, 22, 133, 100, 26, 195, 197, 137, 82, 20, 243, 12, 174, 203, 75, 58, 133, 121, 104, 166, 235, 179, 50, 235, 139, 201, 140, 34, 149, 164, 183, 62, 117, 207, 54, 82, 29, 5, 209, 220, 114, 128, 168, 83, 211, 174, 31, 239, 238, 144, 47, 176, 90, 233, 190, 193, 42, 147, 238, 117, 173, 107, 231, 90, 183, 237, 148, 120, 251, 3, 161, 64, 109, 53, 152, 65, 77, 49, 182, 80, 91, 181, 154, 212, 206, 241, 66, 80, 24, 110, 185, 52, 183, 157, 17, 231, 125, 26, 172, 175, 90, 117, 64, 228, 247, 74, 93, 219, 120, 53, 65, 71, 247, 229, 202, 239, 203, 235, 234, 140, 8, 174, 169, 162, 19, 249, 140, 224, 231, 63, 42, 103, 74, 160, 91, 115, 117, 57, 17, 96, 198, 112, 199, 121, 80, 241, 186, 174, 95, 243, 252, 82, 165, 229, 13, 74, 110, 221, 173, 148, 90, 94, 183, 94, 234, 122, 94, 189, 58, 240, 170, 149, 126, 175, 246, 80, 6, 69, 196, 73, 213, 203, 198, 30, 202, 231, 25, 50, 95, 188, 121, 209, 237, 27, 111, 95, 146, 252, 154, 125, 41, 160, 73, 153, 234, 123, 112, 89, 27, 235, 183, 47, 213, 218, 246, 183, 47, 0, 203, 200, 60, 104, 212, 134, 237, 122, 187, 215, 40, 181, 235, 221, 97, 201, 237, 247, 90, 165, 182, 223, 232, 149, 250, 13, 191, 217, 31, 246, 125, 175, 213, 30, 62, 116, 192, 145, 6, 187, 221, 186, 239, 54, 6, 173, 82, 163, 234, 251, 37, 183, 81, 81, 244, 91, 237, 82, 211, 173, 213, 186, 110, 179, 219, 26, 184, 221, 135, 139, 88, 203, 153, 231, 223, 121, 120, 53, 175, 221, 127, 0, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 30, 226, 58, 20, 246, 3, 0, 0, 252, 10, 0, 0, 17, 0, 0, 0, 119, 111, 114, 100, 47, 115, 101, 116, 116, 105, 110, 103, 115, 46, 120, 109, 108, 180, 86, 219, 110, 219, 56, 16, 125, 95, 96, 255, 193, 208, 243, 42, 178, 36, 95, 133, 58, 133, 147, 212, 77, 138, 120, 187, 168, 189, 216, 103, 74, 164, 108, 34, 188, 129, 164, 236, 184, 197, 254, 251, 14, 41, 49, 118, 154, 162, 112, 182, 200, 75, 66, 205, 153, 57, 51, 28, 158, 33, 253, 238, 253, 35, 103, 189, 29, 209, 134, 74, 49, 139, 210, 139, 126, 212, 35, 162, 146, 152, 138, 205, 44, 250, 123, 189, 136, 39, 81, 207, 88, 36, 48, 98, 82, 144, 89, 116, 32, 38, 122, 127, 249, 251, 111, 239, 246, 133, 33, 214, 130, 155, 233, 1, 133, 48, 5, 175, 102, 209, 214, 90, 85, 36, 137, 169, 182, 132, 35, 115, 33, 21, 17, 0, 214, 82, 115, 100, 225, 83, 111, 18, 142, 244, 67, 163, 226, 74, 114, 133, 44, 45, 41, 163, 246, 144, 100, 253, 254, 40, 234, 104, 228, 44, 106, 180, 40, 58, 138, 152, 211, 74, 75, 35, 107, 235, 66, 10, 89, 215, 180, 34, 221, 191, 16, 161, 207, 201, 219, 134, 220, 200, 170, 225, 68, 88, 159, 49, 209, 132, 65, 13, 82, 152, 45, 85, 38, 176, 241, 255, 203, 6, 224, 54, 144, 236, 126, 182, 137, 29, 103, 193, 111, 159, 246, 207, 216, 238, 94, 106, 252, 20, 113, 78, 121, 46, 64, 105, 89, 17, 99, 224, 128, 56, 11, 5, 82, 113, 76, 60, 120, 65, 244, 148, 251, 2, 114, 119, 91, 244, 84, 16, 158, 246, 253, 234, 180, 242, 225, 235, 8, 178, 23, 4, 163, 138, 226, 215, 113, 140, 58, 142, 4, 34, 79, 120, 12, 121, 29, 205, 48, 208, 152, 3, 39, 143, 129, 200, 176, 115, 90, 219, 66, 247, 180, 212, 72, 183, 194, 237, 250, 202, 171, 226, 110, 35, 164, 70, 37, 131, 114, 160, 191, 61, 104, 81, 207, 87, 231, 254, 186, 138, 47, 97, 104, 190, 74, 201, 123, 251, 66, 17, 93, 129, 114, 96, 226, 250, 253, 40, 113, 0, 156, 151, 172, 87, 22, 89, 112, 47, 140, 34, 140, 249, 17, 172, 24, 65, 192, 190, 47, 54, 26, 113, 24, 158, 96, 241, 49, 152, 212, 168, 97, 118, 141, 202, 149, 149, 10, 156, 118, 8, 54, 49, 206, 58, 202, 106, 139, 52, 170, 44, 209, 43, 133, 42, 96, 187, 150, 194, 106, 201, 130, 31, 150, 127, 74, 123, 13, 131, 168, 65, 39, 93, 132, 31, 203, 227, 106, 213, 142, 56, 68, 8, 196, 97, 91, 207, 198, 118, 41, 49, 113, 149, 53, 154, 158, 223, 127, 23, 224, 179, 167, 195, 211, 148, 223, 39, 146, 112, 37, 105, 138, 201, 218, 181, 115, 101, 15, 140, 44, 160, 248, 21, 253, 74, 230, 2, 127, 106, 140, 165, 192, 232, 135, 247, 23, 42, 248, 89, 1, 68, 184, 204, 159, 65, 0, 235, 131, 34, 11, 130, 108, 3, 109, 122, 163, 100, 254, 36, 22, 140, 170, 37, 213, 90, 234, 59, 129, 65, 27, 111, 150, 140, 214, 53, 209, 144, 128, 130, 214, 150, 32, 31, 170, 229, 222, 247, 249, 150, 32, 12, 47, 193, 27, 229, 109, 12, 249, 7, 156, 97, 254, 242, 53, 200, 242, 225, 74, 90, 43, 249, 237, 65, 109, 161, 215, 191, 118, 146, 94, 239, 201, 169, 124, 225, 61, 195, 38, 44, 190, 72, 105, 159, 92, 251, 105, 154, 78, 135, 157, 248, 28, 122, 14, 146, 167, 121, 54, 92, 252, 8, 25, 15, 198, 211, 113, 200, 223, 101, 229, 133, 123, 11, 254, 210, 97, 229, 164, 219, 227, 109, 196, 53, 226, 165, 166, 168, 183, 116, 175, 69, 226, 60, 74, 253, 112, 69, 69, 192, 75, 2, 183, 13, 57, 69, 86, 77, 25, 192, 56, 110, 1, 195, 17, 99, 11, 104, 98, 0, 124, 1, 188, 192, 212, 168, 27, 82, 251, 53, 91, 34, 189, 57, 242, 118, 30, 250, 135, 86, 184, 71, 62, 61, 113, 185, 123, 137, 232, 143, 90, 54, 170, 69, 247, 26, 169, 86, 146, 193, 37, 29, 12, 186, 72, 42, 236, 61, 229, 193, 110, 154, 114, 21, 162, 4, 220, 143, 39, 80, 35, 240, 231, 157, 246, 125, 58, 182, 103, 95, 88, 56, 98, 63, 218, 247, 200, 75, 197, 251, 18, 17, 127, 188, 234, 164, 196, 244, 202, 201, 128, 44, 145, 82, 173, 154, 202, 77, 58, 139, 24, 221, 108, 109, 234, 4, 96, 225, 11, 195, 143, 10, 255, 81, 110, 178, 14, 203, 60, 150, 181, 152, 255, 64, 149, 219, 25, 120, 119, 139, 163, 45, 11, 182, 19, 191, 60, 216, 242, 163, 109, 16, 108, 131, 163, 109, 24, 108, 195, 163, 109, 20, 108, 35, 103, 219, 194, 253, 161, 225, 50, 127, 0, 97, 135, 165, 179, 215, 146, 49, 185, 39, 248, 246, 136, 191, 48, 181, 77, 48, 91, 164, 200, 77, 123, 215, 131, 188, 100, 107, 232, 46, 127, 211, 219, 21, 228, 17, 94, 18, 130, 169, 133, 223, 106, 138, 98, 142, 30, 221, 195, 146, 141, 92, 120, 231, 205, 208, 65, 54, 246, 153, 175, 195, 156, 179, 122, 206, 128, 145, 69, 221, 40, 39, 207, 130, 189, 196, 191, 171, 197, 189, 65, 21, 5, 57, 174, 14, 188, 60, 62, 45, 23, 109, 225, 140, 26, 184, 6, 20, 188, 66, 86, 234, 128, 253, 225, 177, 116, 80, 96, 89, 221, 193, 36, 193, 170, 213, 98, 62, 24, 77, 39, 139, 172, 133, 135, 254, 245, 178, 254, 166, 128, 115, 255, 66, 234, 43, 100, 8, 238, 176, 16, 58, 108, 67, 191, 101, 163, 252, 250, 195, 120, 49, 143, 39, 211, 155, 44, 30, 44, 242, 121, 60, 239, 15, 243, 248, 42, 203, 63, 76, 39, 243, 73, 58, 30, 13, 254, 237, 134, 52, 252, 108, 189, 252, 15, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 248, 124, 31, 128, 95, 11, 0, 0, 32, 114, 0, 0, 15, 0, 0, 0, 119, 111, 114, 100, 47, 115, 116, 121, 108, 101, 115, 46, 120, 109, 108, 188, 157, 219, 114, 219, 56, 18, 134, 239, 183, 106, 223, 129, 165, 171, 221, 139, 68, 150, 15, 114, 226, 26, 103, 202, 118, 226, 177, 107, 236, 140, 39, 178, 39, 215, 16, 9, 73, 88, 131, 132, 150, 7, 31, 246, 233, 23, 0, 41, 137, 114, 19, 20, 27, 236, 241, 77, 98, 29, 250, 3, 136, 31, 127, 19, 205, 147, 126, 249, 245, 37, 150, 193, 19, 79, 51, 161, 146, 211, 193, 232, 227, 222, 32, 224, 73, 168, 34, 145, 204, 79, 7, 15, 247, 151, 31, 62, 13, 130, 44, 103, 73, 196, 164, 74, 248, 233, 224, 149, 103, 131, 95, 191, 252, 243, 31, 191, 60, 159, 100, 249, 171, 228, 89, 160, 1, 73, 118, 18, 135, 167, 131, 69, 158, 47, 79, 134, 195, 44, 92, 240, 152, 101, 31, 213, 146, 39, 250, 195, 153, 74, 99, 150, 235, 151, 233, 124, 24, 179, 244, 177, 88, 126, 8, 85, 188, 100, 185, 152, 10, 41, 242, 215, 225, 254, 222, 222, 120, 80, 97, 210, 46, 20, 53, 155, 137, 144, 127, 85, 97, 17, 243, 36, 183, 241, 195, 148, 75, 77, 84, 73, 182, 16, 203, 108, 69, 123, 238, 66, 123, 86, 105, 180, 76, 85, 200, 179, 76, 111, 116, 44, 75, 94, 204, 68, 178, 198, 140, 14, 1, 40, 22, 97, 170, 50, 53, 203, 63, 234, 141, 169, 122, 100, 81, 58, 124, 180, 103, 255, 138, 229, 6, 112, 132, 3, 236, 3, 192, 56, 20, 17, 142, 49, 174, 24, 67, 29, 89, 227, 100, 28, 135, 57, 90, 97, 178, 215, 152, 191, 12, 130, 56, 60, 185, 158, 39, 42, 101, 83, 169, 73, 122, 104, 2, 189, 117, 129, 5, 155, 127, 77, 99, 95, 244, 228, 136, 84, 248, 149, 207, 88, 33, 243, 204, 188, 76, 239, 210, 234, 101, 245, 202, 254, 119, 169, 146, 60, 11, 158, 79, 88, 22, 10, 113, 175, 59, 163, 137, 177, 208, 240, 171, 179, 36, 19, 3, 253, 9, 103, 89, 126, 150, 9, 214, 248, 225, 194, 252, 209, 248, 73, 152, 229, 181, 183, 207, 69, 36, 6, 67, 211, 98, 246, 63, 253, 225, 19, 147, 167, 131, 253, 253, 213, 59, 23, 166, 7, 91, 239, 73, 150, 204, 87, 239, 241, 228, 195, 111, 231, 245, 158, 216, 183, 30, 38, 230, 173, 169, 230, 158, 14, 88, 250, 97, 114, 102, 2, 135, 213, 134, 149, 255, 215, 54, 119, 249, 246, 149, 109, 120, 201, 66, 97, 219, 97, 179, 156, 235, 121, 63, 26, 239, 25, 168, 20, 198, 102, 251, 71, 159, 87, 47, 126, 20, 102, 160, 89, 145, 171, 170, 17, 11, 40, 255, 95, 99, 135, 96, 196, 181, 29, 180, 57, 38, 165, 71, 245, 167, 124, 118, 163, 194, 71, 30, 77, 114, 253, 193, 233, 192, 182, 165, 223, 124, 184, 190, 75, 133, 74, 181, 15, 79, 7, 159, 109, 155, 250, 205, 9, 143, 197, 149, 136, 34, 158, 212, 190, 152, 44, 68, 196, 127, 46, 120, 242, 144, 241, 104, 243, 254, 159, 151, 214, 75, 213, 27, 161, 42, 18, 253, 247, 193, 241, 216, 206, 2, 153, 69, 223, 94, 66, 190, 52, 206, 212, 159, 38, 204, 104, 242, 221, 4, 72, 243, 237, 66, 108, 26, 183, 225, 255, 93, 193, 70, 149, 18, 77, 241, 11, 206, 76, 122, 10, 70, 111, 17, 182, 251, 40, 196, 190, 137, 200, 106, 91, 219, 204, 44, 222, 108, 187, 253, 22, 170, 161, 131, 247, 106, 232, 240, 189, 26, 58, 122, 175, 134, 198, 239, 213, 208, 241, 123, 53, 244, 233, 189, 26, 178, 152, 191, 179, 33, 145, 68, 252, 165, 52, 34, 108, 6, 80, 119, 113, 28, 110, 68, 115, 28, 102, 67, 115, 28, 94, 66, 115, 28, 86, 65, 115, 28, 78, 64, 115, 28, 19, 29, 205, 113, 204, 99, 52, 199, 49, 77, 17, 156, 92, 133, 174, 89, 88, 155, 236, 7, 142, 217, 222, 206, 221, 189, 143, 240, 227, 238, 222, 37, 248, 113, 119, 239, 1, 252, 184, 187, 19, 190, 31, 119, 119, 126, 247, 227, 238, 78, 231, 126, 220, 221, 217, 219, 143, 187, 59, 89, 227, 185, 229, 82, 43, 184, 214, 54, 75, 242, 222, 46, 155, 41, 149, 39, 42, 231, 65, 206, 95, 250, 211, 88, 162, 89, 182, 102, 163, 225, 153, 157, 30, 79, 73, 54, 146, 0, 83, 102, 182, 106, 71, 220, 155, 22, 50, 251, 122, 247, 12, 177, 38, 245, 223, 159, 231, 166, 170, 11, 212, 44, 152, 137, 121, 145, 234, 82, 191, 111, 199, 121, 242, 196, 165, 46, 186, 3, 22, 69, 154, 71, 8, 76, 121, 94, 164, 142, 17, 241, 153, 211, 41, 159, 241, 148, 39, 33, 167, 156, 216, 116, 80, 83, 9, 6, 73, 17, 79, 9, 230, 230, 146, 205, 201, 88, 60, 137, 136, 135, 111, 69, 36, 73, 10, 235, 9, 173, 235, 231, 133, 49, 137, 32, 152, 212, 49, 11, 83, 213, 191, 107, 138, 145, 229, 135, 27, 145, 245, 31, 43, 3, 9, 206, 11, 41, 57, 17, 235, 59, 205, 20, 179, 172, 254, 181, 129, 197, 244, 47, 13, 44, 166, 127, 101, 96, 49, 253, 11, 131, 154, 102, 84, 67, 84, 209, 136, 70, 170, 162, 17, 13, 88, 69, 35, 26, 183, 114, 126, 82, 141, 91, 69, 35, 26, 183, 138, 70, 52, 110, 21, 173, 255, 184, 221, 139, 92, 218, 20, 95, 95, 117, 140, 186, 31, 187, 187, 144, 202, 28, 101, 239, 221, 143, 137, 152, 39, 76, 47, 0, 250, 239, 110, 170, 99, 166, 193, 29, 75, 217, 60, 101, 203, 69, 96, 142, 74, 55, 99, 235, 219, 140, 109, 231, 92, 69, 175, 193, 61, 197, 62, 109, 77, 162, 90, 215, 219, 41, 114, 161, 183, 90, 36, 69, 255, 1, 221, 162, 81, 153, 107, 205, 35, 178, 215, 154, 71, 100, 176, 53, 175, 191, 197, 110, 245, 50, 217, 44, 208, 174, 104, 234, 153, 73, 49, 205, 27, 77, 107, 73, 157, 76, 59, 97, 178, 40, 23, 180, 253, 221, 198, 242, 254, 51, 108, 99, 128, 75, 145, 102, 100, 54, 104, 198, 18, 204, 224, 239, 102, 57, 107, 228, 164, 200, 124, 155, 94, 246, 239, 216, 134, 213, 223, 86, 111, 179, 18, 105, 247, 42, 36, 65, 47, 165, 10, 31, 105, 210, 240, 213, 235, 146, 167, 186, 44, 123, 236, 77, 186, 84, 82, 170, 103, 30, 209, 17, 39, 121, 170, 202, 185, 86, 183, 252, 190, 149, 164, 147, 229, 191, 197, 203, 5, 203, 132, 173, 149, 182, 16, 221, 119, 245, 171, 243, 243, 193, 45, 91, 246, 222, 160, 59, 201, 68, 66, 163, 219, 183, 15, 49, 19, 50, 160, 91, 65, 92, 221, 223, 222, 4, 247, 106, 105, 202, 76, 51, 48, 52, 192, 115, 149, 231, 42, 38, 99, 86, 71, 2, 255, 245, 147, 79, 255, 77, 211, 193, 51, 93, 4, 39, 175, 68, 91, 123, 70, 116, 120, 200, 194, 46, 4, 193, 78, 166, 36, 169, 136, 136, 164, 151, 153, 34, 17, 36, 251, 80, 203, 251, 157, 191, 78, 21, 75, 35, 26, 218, 93, 202, 203, 75, 98, 114, 78, 68, 156, 176, 120, 89, 46, 58, 8, 188, 165, 243, 226, 179, 206, 63, 4, 171, 33, 203, 251, 139, 165, 194, 28, 23, 162, 50, 213, 61, 9, 172, 118, 216, 48, 43, 166, 255, 225, 97, 255, 84, 247, 93, 5, 36, 71, 134, 254, 40, 114, 123, 252, 209, 46, 117, 109, 52, 29, 174, 255, 50, 97, 11, 215, 127, 137, 96, 213, 212, 187, 7, 51, 127, 9, 54, 118, 11, 215, 127, 99, 183, 112, 84, 27, 123, 33, 89, 150, 9, 231, 41, 84, 111, 30, 213, 230, 174, 120, 212, 219, 219, 191, 248, 171, 120, 74, 170, 116, 86, 72, 186, 1, 92, 1, 201, 70, 112, 5, 36, 27, 66, 37, 139, 56, 201, 40, 183, 216, 242, 8, 55, 216, 242, 168, 183, 151, 112, 202, 88, 30, 193, 33, 57, 203, 251, 45, 21, 17, 153, 24, 22, 70, 165, 132, 133, 81, 201, 96, 97, 84, 26, 88, 24, 169, 0, 253, 175, 208, 169, 193, 250, 95, 166, 83, 131, 245, 191, 86, 167, 132, 17, 45, 1, 106, 48, 170, 121, 70, 186, 251, 39, 58, 203, 83, 131, 81, 205, 51, 11, 163, 154, 103, 22, 70, 53, 207, 44, 140, 106, 158, 29, 124, 13, 248, 108, 166, 23, 193, 116, 187, 152, 26, 146, 106, 206, 213, 144, 116, 59, 154, 36, 231, 241, 82, 165, 44, 125, 37, 66, 126, 147, 124, 206, 8, 14, 144, 150, 180, 187, 84, 205, 204, 189, 18, 42, 41, 47, 226, 38, 64, 154, 99, 212, 146, 112, 177, 93, 226, 168, 68, 254, 201, 167, 100, 93, 51, 44, 202, 126, 17, 28, 17, 101, 82, 42, 69, 116, 108, 109, 179, 195, 177, 145, 219, 215, 174, 237, 10, 179, 119, 114, 244, 238, 194, 157, 100, 33, 95, 40, 25, 241, 212, 177, 77, 238, 88, 93, 47, 79, 202, 219, 50, 222, 118, 223, 118, 163, 211, 97, 207, 27, 49, 95, 228, 193, 100, 177, 62, 218, 95, 199, 140, 247, 118, 70, 174, 10, 246, 173, 176, 221, 13, 54, 141, 249, 120, 117, 63, 75, 83, 216, 45, 143, 68, 17, 175, 58, 10, 111, 166, 24, 31, 116, 15, 182, 51, 122, 43, 248, 112, 119, 240, 102, 37, 177, 21, 121, 212, 49, 18, 182, 57, 222, 29, 185, 89, 37, 111, 69, 30, 119, 140, 132, 109, 126, 234, 24, 105, 125, 186, 21, 217, 230, 135, 175, 44, 125, 108, 156, 8, 199, 109, 243, 103, 93, 227, 57, 38, 223, 113, 219, 44, 90, 7, 55, 54, 219, 54, 145, 214, 145, 77, 83, 240, 184, 109, 22, 109, 89, 37, 56, 11, 67, 115, 182, 0, 170, 211, 205, 51, 238, 248, 110, 230, 113, 199, 99, 92, 228, 166, 96, 236, 228, 166, 116, 246, 149, 27, 209, 102, 176, 31, 252, 73, 152, 61, 59, 38, 105, 218, 246, 214, 87, 79, 128, 188, 111, 23, 209, 157, 50, 231, 159, 133, 42, 143, 219, 111, 157, 112, 234, 126, 83, 215, 181, 94, 56, 37, 25, 15, 26, 57, 7, 221, 79, 92, 109, 101, 25, 247, 56, 118, 78, 55, 110, 68, 231, 188, 227, 70, 116, 78, 64, 110, 68, 167, 76, 228, 12, 71, 165, 36, 55, 165, 115, 110, 114, 35, 58, 39, 41, 55, 2, 157, 173, 224, 30, 1, 151, 173, 96, 60, 46, 91, 193, 120, 159, 108, 5, 41, 62, 217, 170, 199, 42, 192, 141, 232, 188, 28, 112, 35, 208, 70, 133, 8, 180, 81, 123, 172, 20, 220, 8, 148, 81, 65, 184, 151, 81, 33, 5, 109, 84, 136, 64, 27, 21, 34, 208, 70, 133, 11, 48, 156, 81, 97, 60, 206, 168, 48, 222, 199, 168, 144, 226, 99, 84, 72, 65, 27, 21, 34, 208, 70, 133, 8, 180, 81, 33, 2, 109, 84, 136, 64, 27, 213, 115, 109, 239, 12, 247, 50, 42, 164, 160, 141, 10, 17, 104, 163, 66, 4, 218, 168, 118, 189, 216, 195, 168, 48, 30, 103, 84, 24, 239, 99, 84, 72, 241, 49, 42, 164, 160, 141, 10, 17, 104, 163, 66, 4, 218, 168, 16, 129, 54, 42, 68, 160, 141, 10, 17, 40, 163, 130, 112, 47, 163, 66, 10, 218, 168, 16, 129, 54, 42, 68, 160, 141, 90, 222, 106, 232, 111, 84, 24, 143, 51, 42, 140, 247, 49, 42, 164, 248, 24, 21, 82, 208, 70, 133, 8, 180, 81, 33, 2, 109, 84, 136, 64, 27, 21, 34, 208, 70, 133, 8, 148, 81, 65, 184, 151, 81, 33, 5, 109, 84, 136, 64, 27, 21, 34, 208, 70, 181, 39, 11, 123, 24, 21, 198, 227, 140, 10, 227, 125, 140, 10, 41, 62, 70, 133, 20, 180, 81, 33, 2, 109, 84, 136, 64, 27, 21, 34, 208, 70, 133, 8, 180, 81, 33, 2, 101, 84, 16, 238, 101, 84, 72, 65, 27, 21, 34, 208, 70, 133, 136, 182, 249, 89, 157, 162, 116, 93, 102, 63, 194, 31, 245, 116, 94, 177, 223, 253, 212, 85, 213, 169, 31, 245, 91, 185, 235, 168, 131, 238, 168, 85, 175, 220, 172, 238, 247, 34, 156, 43, 245, 24, 52, 222, 120, 120, 96, 235, 141, 110, 16, 49, 149, 66, 217, 67, 212, 142, 211, 234, 117, 174, 189, 36, 2, 117, 226, 243, 143, 139, 246, 59, 124, 234, 244, 158, 15, 93, 170, 238, 133, 176, 231, 76, 1, 252, 176, 107, 36, 56, 166, 114, 216, 54, 229, 235, 145, 160, 200, 59, 108, 155, 233, 245, 72, 176, 234, 60, 108, 203, 190, 245, 72, 176, 27, 60, 108, 75, 186, 214, 151, 171, 139, 82, 244, 238, 8, 4, 183, 165, 153, 90, 240, 200, 17, 222, 150, 173, 107, 225, 112, 136, 219, 114, 116, 45, 16, 142, 112, 91, 102, 174, 5, 194, 1, 110, 203, 199, 181, 192, 163, 192, 36, 231, 183, 209, 71, 29, 199, 105, 188, 190, 190, 20, 16, 218, 166, 99, 141, 112, 236, 38, 180, 77, 75, 168, 213, 42, 29, 67, 99, 116, 21, 205, 77, 232, 170, 158, 155, 208, 85, 70, 55, 1, 165, 167, 19, 131, 23, 214, 141, 66, 43, 236, 70, 249, 73, 13, 109, 134, 149, 218, 223, 168, 110, 2, 86, 106, 72, 240, 146, 26, 96, 252, 165, 134, 40, 111, 169, 33, 202, 79, 106, 152, 24, 177, 82, 67, 2, 86, 106, 255, 228, 236, 38, 120, 73, 13, 48, 254, 82, 67, 148, 183, 212, 16, 229, 39, 53, 220, 149, 97, 165, 134, 4, 172, 212, 144, 128, 149, 186, 231, 14, 217, 137, 241, 151, 26, 162, 188, 165, 134, 40, 63, 169, 225, 226, 14, 43, 53, 36, 96, 165, 134, 4, 172, 212, 144, 224, 37, 53, 192, 248, 75, 13, 81, 222, 82, 67, 148, 159, 212, 160, 74, 70, 75, 13, 9, 88, 169, 33, 1, 43, 53, 36, 120, 73, 13, 48, 254, 82, 67, 148, 183, 212, 16, 213, 38, 181, 61, 138, 178, 37, 53, 74, 225, 90, 56, 110, 17, 86, 11, 196, 237, 144, 107, 129, 184, 228, 92, 11, 244, 168, 150, 106, 209, 158, 213, 82, 141, 224, 89, 45, 65, 173, 86, 154, 227, 170, 165, 186, 104, 110, 66, 87, 245, 220, 132, 174, 50, 186, 9, 40, 61, 157, 24, 188, 176, 110, 20, 90, 97, 55, 202, 79, 106, 92, 181, 212, 36, 181, 191, 81, 221, 4, 172, 212, 184, 106, 201, 41, 53, 174, 90, 106, 149, 26, 87, 45, 181, 74, 141, 171, 150, 220, 82, 227, 170, 165, 38, 169, 113, 213, 82, 147, 212, 254, 201, 217, 77, 240, 146, 26, 87, 45, 181, 74, 141, 171, 150, 90, 165, 198, 85, 75, 110, 169, 113, 213, 82, 147, 212, 184, 106, 169, 73, 106, 92, 181, 212, 36, 117, 207, 29, 178, 19, 227, 47, 53, 174, 90, 106, 149, 26, 87, 45, 185, 165, 198, 85, 75, 77, 82, 227, 170, 165, 38, 169, 113, 213, 82, 147, 212, 184, 106, 201, 41, 53, 174, 90, 106, 149, 26, 87, 45, 181, 74, 141, 171, 150, 220, 82, 227, 170, 165, 38, 169, 113, 213, 82, 147, 212, 184, 106, 169, 73, 106, 92, 181, 228, 148, 26, 87, 45, 181, 74, 141, 171, 150, 90, 165, 198, 85, 75, 183, 58, 68, 16, 60, 2, 106, 18, 179, 52, 15, 232, 158, 23, 119, 197, 178, 69, 206, 250, 63, 156, 240, 33, 73, 121, 166, 228, 19, 143, 2, 218, 77, 189, 65, 109, 229, 240, 121, 235, 231, 175, 12, 219, 254, 88, 157, 254, 126, 174, 199, 204, 60, 1, 189, 118, 187, 82, 84, 62, 1, 182, 2, 218, 47, 94, 71, 235, 159, 169, 50, 193, 166, 39, 65, 245, 131, 96, 213, 219, 182, 195, 213, 233, 218, 178, 69, 27, 8, 155, 10, 23, 186, 173, 176, 122, 118, 149, 163, 169, 234, 25, 180, 235, 155, 168, 236, 19, 104, 223, 54, 236, 120, 80, 173, 237, 200, 102, 2, 174, 190, 93, 13, 233, 102, 188, 202, 239, 109, 141, 86, 107, 191, 115, 51, 225, 91, 250, 108, 13, 209, 58, 70, 165, 103, 92, 29, 252, 92, 37, 129, 93, 61, 212, 253, 153, 202, 242, 39, 211, 244, 31, 215, 73, 164, 1, 207, 213, 207, 133, 149, 61, 141, 94, 88, 137, 210, 159, 95, 112, 41, 111, 89, 249, 109, 181, 116, 127, 85, 242, 89, 94, 126, 58, 218, 179, 143, 44, 120, 243, 249, 180, 124, 250, 158, 51, 62, 181, 105, 218, 9, 24, 110, 119, 166, 124, 89, 253, 108, 155, 99, 188, 203, 231, 241, 87, 215, 15, 56, 167, 164, 201, 69, 13, 195, 109, 47, 102, 233, 59, 210, 155, 190, 173, 254, 202, 190, 252, 31, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 189, 212, 141, 191, 39, 1, 0, 0, 143, 2, 0, 0, 20, 0, 0, 0, 119, 111, 114, 100, 47, 119, 101, 98, 83, 101, 116, 116, 105, 110, 103, 115, 46, 120, 109, 108, 148, 210, 205, 106, 2, 49, 16, 0, 224, 123, 161, 239, 16, 114, 215, 172, 82, 165, 44, 174, 66, 41, 150, 94, 74, 161, 237, 3, 196, 236, 172, 134, 102, 50, 33, 19, 187, 218, 167, 111, 220, 106, 127, 240, 226, 94, 66, 38, 201, 124, 201, 132, 153, 45, 118, 232, 196, 7, 68, 182, 228, 43, 57, 26, 22, 82, 128, 55, 84, 91, 191, 174, 228, 219, 235, 114, 112, 43, 5, 39, 237, 107, 237, 200, 67, 37, 247, 192, 114, 49, 191, 190, 154, 181, 101, 11, 171, 23, 72, 41, 159, 100, 145, 21, 207, 37, 154, 74, 110, 82, 10, 165, 82, 108, 54, 128, 154, 135, 20, 192, 231, 205, 134, 34, 234, 148, 195, 184, 86, 168, 227, 251, 54, 12, 12, 97, 208, 201, 174, 172, 179, 105, 175, 198, 69, 49, 149, 71, 38, 94, 162, 80, 211, 88, 3, 247, 100, 182, 8, 62, 117, 249, 42, 130, 203, 34, 121, 222, 216, 192, 39, 173, 189, 68, 107, 41, 214, 33, 146, 1, 230, 92, 15, 186, 111, 15, 181, 245, 63, 204, 232, 230, 12, 66, 107, 34, 49, 53, 105, 152, 139, 57, 190, 168, 163, 114, 250, 168, 232, 102, 232, 126, 129, 73, 63, 96, 124, 6, 76, 141, 173, 251, 25, 211, 163, 161, 114, 230, 31, 135, 161, 31, 51, 57, 49, 188, 71, 216, 73, 129, 166, 124, 92, 123, 138, 122, 229, 178, 148, 191, 70, 228, 234, 68, 7, 31, 198, 195, 101, 243, 220, 33, 20, 146, 69, 251, 9, 75, 138, 119, 145, 90, 134, 168, 14, 203, 218, 57, 106, 159, 159, 30, 114, 160, 254, 181, 209, 252, 11, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 175, 86, 61, 164, 198, 1, 0, 0, 139, 5, 0, 0, 18, 0, 0, 0, 119, 111, 114, 100, 47, 102, 111, 110, 116, 84, 97, 98, 108, 101, 46, 120, 109, 108, 220, 146, 223, 106, 219, 48, 20, 198, 239, 7, 125, 7, 161, 251, 198, 178, 19, 167, 157, 169, 83, 232, 214, 192, 96, 236, 98, 116, 15, 160, 40, 178, 45, 166, 63, 70, 71, 137, 155, 183, 223, 145, 236, 164, 131, 80, 168, 111, 118, 49, 27, 132, 244, 157, 115, 126, 210, 249, 56, 15, 143, 175, 70, 147, 163, 244, 160, 156, 173, 105, 190, 96, 148, 72, 43, 220, 94, 217, 182, 166, 191, 94, 182, 183, 247, 148, 64, 224, 118, 207, 181, 179, 178, 166, 39, 9, 244, 113, 115, 243, 233, 97, 168, 26, 103, 3, 16, 172, 183, 80, 25, 81, 211, 46, 132, 190, 202, 50, 16, 157, 52, 28, 22, 174, 151, 22, 131, 141, 243, 134, 7, 60, 250, 54, 51, 220, 255, 62, 244, 183, 194, 153, 158, 7, 181, 83, 90, 133, 83, 86, 48, 182, 166, 19, 198, 127, 132, 226, 154, 70, 9, 249, 213, 137, 131, 145, 54, 164, 250, 204, 75, 141, 68, 103, 161, 83, 61, 156, 105, 195, 71, 104, 131, 243, 251, 222, 59, 33, 1, 176, 103, 163, 71, 158, 225, 202, 94, 48, 249, 234, 10, 100, 148, 240, 14, 92, 19, 22, 216, 204, 244, 162, 132, 194, 242, 156, 165, 157, 209, 111, 128, 114, 30, 160, 184, 2, 172, 133, 218, 207, 99, 172, 39, 70, 134, 149, 127, 113, 64, 206, 195, 148, 103, 12, 156, 140, 124, 165, 196, 136, 234, 91, 107, 157, 231, 59, 141, 36, 180, 134, 96, 119, 36, 129, 227, 26, 47, 219, 76, 179, 65, 134, 202, 114, 131, 89, 95, 184, 86, 59, 175, 82, 160, 231, 214, 129, 204, 49, 118, 228, 186, 166, 172, 96, 91, 86, 226, 26, 255, 21, 91, 198, 149, 102, 49, 81, 116, 220, 131, 140, 144, 49, 145, 141, 114, 195, 141, 210, 167, 179, 10, 131, 2, 24, 3, 189, 10, 162, 59, 235, 71, 238, 85, 124, 225, 24, 2, 213, 98, 224, 0, 59, 86, 211, 231, 21, 99, 197, 243, 118, 75, 71, 37, 199, 215, 49, 84, 86, 119, 79, 147, 82, 196, 187, 210, 247, 121, 82, 150, 23, 133, 69, 69, 36, 78, 58, 230, 35, 71, 36, 206, 37, 7, 239, 204, 70, 7, 174, 156, 120, 81, 70, 2, 249, 33, 7, 242, 211, 25, 110, 223, 113, 164, 96, 107, 116, 162, 68, 63, 162, 51, 203, 89, 142, 248, 196, 157, 229, 72, 236, 255, 202, 145, 187, 251, 242, 159, 56, 50, 205, 6, 249, 174, 218, 46, 188, 59, 33, 113, 46, 254, 211, 9, 153, 54, 176, 249, 3, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 151, 195, 215, 46, 110, 1, 0, 0, 243, 2, 0, 0, 17, 0, 8, 1, 100, 111, 99, 80, 114, 111, 112, 115, 47, 99, 111, 114, 101, 46, 120, 109, 108, 32, 162, 4, 1, 40, 160, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 156, 146, 203, 106, 195, 48, 16, 69, 247, 133, 254, 131, 209, 222, 150, 157, 64, 9, 198, 118, 160, 45, 89, 148, 6, 10, 77, 31, 116, 167, 74, 147, 68, 141, 245, 64, 82, 226, 248, 239, 43, 219, 177, 83, 147, 172, 10, 90, 204, 104, 206, 92, 141, 174, 148, 205, 143, 162, 12, 14, 96, 44, 87, 50, 71, 73, 20, 163, 0, 36, 85, 140, 203, 77, 142, 222, 86, 139, 112, 134, 2, 235, 136, 100, 164, 84, 18, 114, 84, 131, 69, 243, 226, 246, 38, 163, 58, 165, 202, 192, 139, 81, 26, 140, 227, 96, 3, 175, 36, 109, 74, 117, 142, 182, 206, 233, 20, 99, 75, 183, 32, 136, 141, 60, 33, 125, 113, 173, 140, 32, 206, 167, 102, 131, 53, 161, 59, 178, 1, 60, 137, 227, 59, 44, 192, 17, 70, 28, 193, 141, 96, 168, 7, 69, 116, 146, 100, 116, 144, 212, 123, 83, 182, 2, 140, 98, 40, 65, 128, 116, 22, 39, 81, 130, 207, 172, 3, 35, 236, 213, 134, 182, 242, 135, 20, 220, 213, 26, 174, 162, 125, 113, 160, 143, 150, 15, 96, 85, 85, 81, 53, 109, 81, 63, 127, 130, 63, 151, 207, 175, 237, 85, 67, 46, 27, 175, 40, 160, 34, 99, 52, 117, 220, 149, 80, 100, 248, 28, 250, 200, 238, 191, 127, 128, 186, 110, 123, 72, 124, 76, 13, 16, 167, 76, 241, 68, 188, 199, 219, 224, 157, 24, 75, 36, 111, 177, 190, 212, 152, 190, 131, 186, 82, 134, 89, 47, 48, 202, 60, 198, 192, 82, 195, 181, 243, 79, 217, 201, 143, 54, 60, 93, 18, 235, 150, 254, 109, 215, 28, 216, 125, 125, 113, 210, 37, 209, 52, 25, 56, 240, 230, 119, 20, 211, 150, 24, 210, 236, 100, 117, 55, 29, 176, 192, 91, 148, 118, 134, 246, 149, 143, 233, 195, 227, 106, 129, 138, 73, 60, 137, 195, 56, 241, 107, 149, 204, 210, 56, 246, 235, 171, 25, 112, 212, 127, 22, 20, 167, 1, 254, 173, 216, 11, 116, 30, 141, 191, 105, 241, 11, 0, 0, 255, 255, 3, 0, 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 195, 172, 71, 253, 113, 1, 0, 0, 199, 2, 0, 0, 16, 0, 8, 1, 100, 111, 99, 80, 114, 111, 112, 115, 47, 97, 112, 112, 46, 120, 109, 108, 32, 162, 4, 1, 40, 160, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 156, 82, 203, 106, 195, 48, 16, 188, 23, 250, 15, 198, 247, 68, 78, 2, 161, 148, 141, 74, 73, 40, 61, 244, 5, 113, 210, 179, 144, 214, 182, 168, 44, 9, 73, 9, 201, 223, 119, 93, 55, 174, 75, 111, 245, 105, 103, 118, 119, 152, 89, 11, 238, 78, 173, 201, 142, 24, 162, 118, 118, 149, 207, 166, 69, 158, 161, 149, 78, 105, 91, 175, 242, 93, 249, 48, 185, 201, 179, 152, 132, 85, 194, 56, 139, 171, 252, 140, 49, 191, 227, 215, 87, 240, 22, 156, 199, 144, 52, 198, 140, 36, 108, 92, 229, 77, 74, 254, 150, 177, 40, 27, 108, 69, 156, 82, 219, 82, 167, 114, 161, 21, 137, 96, 168, 153, 171, 42, 45, 113, 227, 228, 161, 69, 155, 216, 188, 40, 150, 12, 79, 9, 173, 66, 53, 241, 131, 96, 222, 43, 222, 30, 211, 127, 69, 149, 147, 157, 191, 184, 47, 207, 158, 244, 56, 148, 216, 122, 35, 18, 242, 151, 110, 211, 76, 149, 75, 45, 176, 129, 133, 210, 37, 97, 74, 221, 34, 47, 136, 30, 0, 188, 137, 26, 35, 159, 1, 235, 11, 120, 119, 65, 69, 190, 0, 214, 23, 176, 110, 68, 16, 50, 209, 253, 248, 156, 166, 70, 16, 238, 189, 55, 90, 138, 68, 135, 229, 207, 90, 6, 23, 93, 149, 178, 215, 47, 183, 89, 183, 14, 108, 60, 2, 148, 96, 139, 242, 16, 116, 58, 119, 38, 198, 16, 158, 180, 237, 109, 244, 5, 217, 10, 162, 14, 194, 55, 223, 222, 6, 4, 91, 41, 12, 174, 41, 59, 175, 132, 137, 8, 236, 135, 128, 181, 107, 189, 176, 36, 199, 134, 138, 244, 62, 226, 206, 151, 110, 211, 157, 225, 123, 229, 55, 57, 202, 248, 174, 83, 179, 245, 66, 146, 133, 249, 98, 156, 118, 212, 128, 45, 177, 168, 200, 254, 224, 96, 32, 224, 145, 126, 71, 48, 157, 60, 237, 218, 26, 213, 101, 230, 111, 163, 187, 223, 190, 127, 151, 124, 182, 156, 22, 244, 125, 29, 236, 194, 81, 236, 225, 193, 240, 79, 0, 0, 0, 255, 255, 3, 0, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 223, 164, 210, 108, 90, 1, 0, 0, 32, 5, 0, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 91, 67, 111, 110, 116, 101, 110, 116, 95, 84, 121, 112, 101, 115, 93, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 30, 145, 26, 183, 239, 0, 0, 0, 78, 2, 0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 147, 3, 0, 0, 95, 114, 101, 108, 115, 47, 46, 114, 101, 108, 115, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 64, 225, 183, 114, 214, 2, 0, 0, 69, 10, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 179, 6, 0, 0, 119, 111, 114, 100, 47, 100, 111, 99, 117, 109, 101, 110, 116, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 214, 100, 179, 81, 244, 0, 0, 0, 49, 3, 0, 0, 28, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 184, 9, 0, 0, 119, 111, 114, 100, 47, 95, 114, 101, 108, 115, 47, 100, 111, 99, 117, 109, 101, 110, 116, 46, 120, 109, 108, 46, 114, 101, 108, 115, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 211, 19, 8, 67, 34, 6, 0, 0, 139, 26, 0, 0, 21, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 238, 11, 0, 0, 119, 111, 114, 100, 47, 116, 104, 101, 109, 101, 47, 116, 104, 101, 109, 101, 49, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 30, 226, 58, 20, 246, 3, 0, 0, 252, 10, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 67, 18, 0, 0, 119, 111, 114, 100, 47, 115, 101, 116, 116, 105, 110, 103, 115, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 248, 124, 31, 128, 95, 11, 0, 0, 32, 114, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 104, 22, 0, 0, 119, 111, 114, 100, 47, 115, 116, 121, 108, 101, 115, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 189, 212, 141, 191, 39, 1, 0, 0, 143, 2, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 244, 33, 0, 0, 119, 111, 114, 100, 47, 119, 101, 98, 83, 101, 116, 116, 105, 110, 103, 115, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 175, 86, 61, 164, 198, 1, 0, 0, 139, 5, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 77, 35, 0, 0, 119, 111, 114, 100, 47, 102, 111, 110, 116, 84, 97, 98, 108, 101, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 151, 195, 215, 46, 110, 1, 0, 0, 243, 2, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 67, 37, 0, 0, 100, 111, 99, 80, 114, 111, 112, 115, 47, 99, 111, 114, 101, 46, 120, 109, 108, 80, 75, 1, 2, 45, 0, 20, 0, 6, 0, 8, 0, 0, 0, 33, 0, 195, 172, 71, 253, 113, 1, 0, 0, 199, 2, 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 232, 39, 0, 0, 100, 111, 99, 80, 114, 111, 112, 115, 47, 97, 112, 112, 46, 120, 109, 108, 80, 75, 5, 6, 0, 0, 0, 0, 11, 0, 11, 0, 193, 2, 0, 0, 143, 42, 0, 0, 0, 0 };

            IFileApi fileApi = CreateBusinessProcess();

            Byte[] actualFileContent = fileApi.GetFileContentsAsByteArrayFromAssembly(Assembly.GetAssembly(this.GetType()), filePath);

            Assert.That(actualFileContent, Is.EquivalentTo(expectedFileContent));
        }

        [TestCase]
        public void Test_OpenFileForReading_Fail_NoFile()
        {
            const String filePath = @"C:\FakeFile.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedMessage = $"The file '{filePath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_OpenFileForReading_FileAlreadyOpen()
        {
            const String filePath = @".ExpectedResults\FileManagement\SmallFile.txt";
            Encoding encoding = Encoding.UTF8;
            const String expectedFileContent = "Just a small text file";

            IFileApi fileApi = CreateBusinessProcess();

            TextReader tr1 = fileApi.OpenFileForReading(filePath, encoding);
            TextReader tr2 = fileApi.OpenFileForReading(filePath, encoding);

            String actualFileContent1 = tr1.ReadToEnd();
            String actualFileContent2 = tr2.ReadToEnd();

            Assert.That(actualFileContent1, Is.EqualTo(expectedFileContent));
            Assert.That(actualFileContent2, Is.EqualTo(expectedFileContent));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Append_NoFile_Fail()
        {
            const String filePath = @"C:\FakeFile.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedMessage = $"The file '{filePath}' does not exist or access to it is denied";

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

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
            Assert.That(actualException.FileName, Is.EqualTo(filePath));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Append_FileExists_Success()
        {
            const String filePath = @".ExpectedResults\FileManagement\ExistingFile.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedFileContent = $"Existing file content{Environment.NewLine}New file content{Environment.NewLine}";

            IFileApi fileApi = CreateBusinessProcess();

            const Boolean appendToFile = true;
            using (TextWriter tw = fileApi.OpenFileForWriting(filePath, encoding, appendToFile))
            {
                tw.WriteLine("New file content");
            }

            String actualFileContent = fileApi.GetFileContentsAsText(filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(expectedFileContent));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Create_FileExists_Fail()
        {
            const String filePath = @".ExpectedResults\FileManagement\ExistingFile.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedMessage = $"The file '{filePath}' already exists and cannot be created";

            UnauthorizedAccessException actualException = null;

            try
            {
                IFileApi fileApi = CreateBusinessProcess();

                fileApi.OpenFileForWriting(filePath, encoding);
            }
            catch (UnauthorizedAccessException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase]
        public void Test_OpenFileForWriting_Create_NoFile_Success()
        {
            String filePath = $@".ExpectedResults\FileManagement\{Guid.NewGuid()}.txt";
            Encoding encoding = Encoding.UTF8;

            String expectedFileContent = $"New file content{Environment.NewLine}";

            IFileApi fileApi = CreateBusinessProcess();

            using (TextWriter tw = fileApi.OpenFileForWriting(filePath, encoding))
            {
                tw.WriteLine("New file content");
            }

            String actualFileContent = fileApi.GetFileContentsAsText(filePath, encoding);

            Assert.That(actualFileContent, Is.EqualTo(expectedFileContent));
        }

        [TestCase]
        public void Test_DeleteFile_NoFile_Success()
        {
            const String filePath = "FakeFile.txt";

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.DeleteFile(filePath);
        }

        [TestCase]
        public void Test_DeleteFile_WithFile_Success()
        {
            const String filePath = @".ExpectedResults\FileManagement\FileToDelete.txt";

            IFileApi fileApi = CreateBusinessProcess();

            Boolean beforeFileExists = fileApi.DoesFileExist(filePath);
            Assert.That(beforeFileExists, Is.EqualTo(true));

            fileApi.DeleteFile(filePath);

            Boolean afterFileExists = fileApi.DoesFileExist(filePath);
            Assert.That(afterFileExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_CreateDirectory_DoesNotExist_Exception()
        {
            const String basePath = ".";
            String newDirectoryName = Guid.NewGuid().ToString();
            const Boolean throwExceptionIfExists = true;

            IFileApi fileApi = CreateBusinessProcess();

            String initialDirectory = Path.Combine(basePath, newDirectoryName);
            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(beforeDirectoryExists, Is.EqualTo(false));

            DirectoryInfo createdDirectory = fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);

            Boolean afterDirectoryExists = fileApi.DoesDirectoryExist(createdDirectory.FullName);

            Assert.That(afterDirectoryExists, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CreateDirectory_DoesNotExist_NoException()
        {
            const String basePath = ".";
            String newDirectoryName = Guid.NewGuid().ToString();
            const Boolean throwExceptionIfExists = false;

            IFileApi fileApi = CreateBusinessProcess();

            String initialDirectory = Path.Combine(basePath, newDirectoryName);
            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(beforeDirectoryExists, Is.EqualTo(false));

            DirectoryInfo createdDirectory = fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);
            Boolean afterDirectoryExists = fileApi.DoesDirectoryExist(createdDirectory.FullName);
            Assert.That(afterDirectoryExists, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_CreateDirectory_Exists_Exception()
        {
            const String basePath = ".ExpectedResults";
            const String newDirectoryName = "FileManagement";
            const Boolean throwExceptionIfExists = true;

            IFileApi fileApi = CreateBusinessProcess();

            String initialDirectory = Path.Combine(basePath, newDirectoryName);
            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(beforeDirectoryExists, initialDirectory);

            String expectedMessage = $"The directory path '{initialDirectory}' already exists, the directory '{newDirectoryName}' cannot be created again";

            IOException actualException = null;

            try
            {
                fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);
            }
            catch (IOException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase]
        public void Test_CreateDirectory_Exists_NoException()
        {
            const String basePath = ".ExpectedResults";
            const String newDirectoryName = "FileManagement";
            const Boolean throwExceptionIfExists = true;

            IFileApi fileApi = CreateBusinessProcess();

            String initialDirectory = Path.Combine(basePath, newDirectoryName);
            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(beforeDirectoryExists, Is.EqualTo(true));

            String expectedMessage = $"The directory path '{initialDirectory}' already exists, the directory '{newDirectoryName}' cannot be created again";

            IOException actualException = null;

            try
            {
                fileApi.CreateDirectory(basePath, newDirectoryName, throwExceptionIfExists);
            }
            catch (IOException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase]
        public void Test_DeleteDirectory_NoDirectory_Success()
        {
            const String directoryPath = @"D:\FakeDirectory";
            const Boolean recursive = false;

            IFileApi fileApi = CreateBusinessProcess();

            fileApi.DeleteDirectory(directoryPath, recursive);
        }

        [TestCase]
        public void Test_DeleteDirectory_WithDirectory_Recursive_Success()
        {
            String directoryPath = $@".ExpectedResults\{LocationUtils.GetFunctionName()}";
            const Boolean recursive = true;
            String initialDirectory = Path.Combine(Environment.CurrentDirectory, directoryPath);

            IFileApi fileApi = CreateBusinessProcess();

            Directory.CreateDirectory(initialDirectory);
            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(directoryPath);
            Assert.That(beforeDirectoryExists, Is.EqualTo(true));

            fileApi.DeleteDirectory(directoryPath, recursive);

            Boolean afterDirectoryExists = fileApi.DoesDirectoryExist(directoryPath);
            Assert.That(afterDirectoryExists, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_DeleteDirectory_WithFiles_NotRecursive_Fail()
        {
            String directoryPath = $@".ExpectedResults\{LocationUtils.GetFunctionName()}";
            const Boolean recursive = false;
            String initialDirectory = Path.Combine(Environment.CurrentDirectory, directoryPath);

            Directory.CreateDirectory(initialDirectory);
            using (File.CreateText(Path.Combine(initialDirectory, "ExistingFile.txt"))) { }
            using (File.CreateText(Path.Combine(initialDirectory, "FileToDelete.txt"))) { }
            using (File.CreateText(Path.Combine(initialDirectory, "SmallFile.txt"))) { }

            IFileApi fileApi = CreateBusinessProcess();

            Boolean beforeDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(beforeDirectoryExists, Is.EqualTo(true));

            String expectedMessage = $"The directory is not empty.{Environment.NewLine}";

            IOException actualException = null;

            try
            {
                fileApi.DeleteDirectory(initialDirectory, recursive);
            }
            catch (IOException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));

            Boolean afterDirectoryExists = fileApi.DoesDirectoryExist(initialDirectory);
            Assert.That(afterDirectoryExists, Is.EqualTo(true));
        }
    }
}
