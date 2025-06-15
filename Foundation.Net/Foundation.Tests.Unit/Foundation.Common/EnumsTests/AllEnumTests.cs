
//-----------------------------------------------------------------------
// <copyright file="AllEnumTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

using FEnums = Foundation.Interfaces;

namespace Foundation.Tests.Unit.Foundation.Common.EnumsTests
{
    /// <summary>
    /// Unit Tests for all Enums
    /// </summary>
    [TestFixture]
    public class AllEnumTests : UnitTestBase
    {
        /// <summary>
        /// Checks to make sure each the Enum values have the correct attributes set.
        /// Each Enum must have the Id and Display attributes set to ensure they will work
        /// correctly with the rest of the Application Framework
        /// </summary>
        /// <param name="enumType"></param>
        private void CheckAttributesOfEnumType(Type enumType)
        {
            foreach (Enum enumValue in Enum.GetValues(enumType))
            {
                String errorMessage = $"{enumType}.{enumValue} does not have the correct attributes set";

                FieldInfo fieldInfo = enumType.GetField(enumValue.ToString());

                List<CustomAttributeData> attributes = fieldInfo.CustomAttributes.ToList();

                Boolean hasId = attributes.Any(a => a.AttributeType.Name == "IdAttribute");
                Boolean hasDisplay = attributes.Any(a => a.AttributeType.Name == "DisplayAttribute");

                Assert.That(hasId && hasDisplay, errorMessage);
            }
        }

        /// <summary>
        /// Checks to make sure each the Enum values have the correct Id and Value.
        /// </summary>
        /// <param name="enumType"></param>
        private void TestEnumIdsAndValues(Type enumType)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            foreach (Enum enumValue in Enum.GetValues(enumType))
            {
                Int32 id = enumValue.Id();
                Int32 value = ((IConvertible)enumValue).ToInt32(cultureInfo);

                String errorMessage = $"{enumType}.{enumValue} does not have the correct Id {id} and Value {value}";

                Assert.That(value, Is.EqualTo(id), errorMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_AllMembers()
        {
            IEnumerable<MethodInfo> testMethods = GetListOfTestMethods();
            Int32 testMethodCount = testMethods.Count();

            // Get a list of all types
            List<Type> allTypes = GetListOfValidTypes(t => t.IsEnum && 
                                                           t.IsPublic &&
                                                           !String.IsNullOrEmpty(t.Namespace) &&
                                                           t.Namespace.StartsWith("Foundation"));

            Assert.That(allTypes.Count, Is.EqualTo(testMethodCount - 1));

            Int32 index = 0;

            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ApplicationRole)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ApprovalStatus)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ConfigurationScope)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ControlToShow)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.DataStatus)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.DialogResult)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.DisplayTarget)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.EntityLife)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.EntityState)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.EntityStatus)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ErrorDialogButtons)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.FileTransferArchiveAction)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.FileTransferMethod)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.InputType)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.KnownFolder)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.LogSeverity)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.MessageBoxButton)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.MessageBoxImage)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.MessageType)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ScheduleInterval)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.ServiceStatus)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.TaskStatus)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.TextAlignment)));
            Assert.That(allTypes[index++].Name, Is.EqualTo(nameof(FEnums.WindowsMessages)));

            Assert.That(allTypes.Count, Is.EqualTo(index));

            foreach (Type enumType in allTypes)
            {
                CheckAttributesOfEnumType(enumType);

                TestEnumIdsAndValues(enumType);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ApplicationRole()
        {
            // This test exists to ensure all the Application Role are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ApplicationRole));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ApplicationRole.None, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ApplicationRole.ReadOnly, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ApplicationRole.Reporter, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ApplicationRole.Creator, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.ApplicationRole.OwnEditor, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.ApplicationRole.AllEditor, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.ApplicationRole.OwnDelete, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.ApplicationRole.AllDelete, Is.EqualTo(7));
            index++; Assert.That((Int32)FEnums.ApplicationRole.Approver, Is.EqualTo(8));
            index++; Assert.That((Int32)FEnums.ApplicationRole.TeamSupervisor, Is.EqualTo(9));
            index++; Assert.That((Int32)FEnums.ApplicationRole.DeputyTeamManager, Is.EqualTo(10));
            index++; Assert.That((Int32)FEnums.ApplicationRole.PrimaryTeamManager, Is.EqualTo(11));
            index++; Assert.That((Int32)FEnums.ApplicationRole.SystemSupervisor, Is.EqualTo(998));
            index++; Assert.That((Int32)FEnums.ApplicationRole.SystemDataAdministrator, Is.EqualTo(999));
            index++; Assert.That((Int32)FEnums.ApplicationRole.SystemAdministrator, Is.EqualTo(1000));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ApprovalStatus()
        {
            // This test exists to ensure all the Approval Status are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ApprovalStatus));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ApprovalStatus.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ApprovalStatus.Approved, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ApprovalStatus.Pending, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ApprovalStatus.Rejected, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ConfigurationScope()
        {
            // This test exists to ensure all the Control To Show are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ConfigurationScope));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ConfigurationScope.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ConfigurationScope.System, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ConfigurationScope.Application, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ConfigurationScope.User, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ControlToShow()
        {
            // This test exists to ensure all the Control To Show are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ControlToShow));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ControlToShow.ListControl, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ControlToShow.BarControl, Is.EqualTo(1));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DataStatus()
        {
            // This test exists to ensure all the Dialog Result are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.DataStatus));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.DataStatus.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.DataStatus.InProgress, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.DataStatus.Ready, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.DataStatus.Aborted, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DialogResult()
        {
            // This test exists to ensure all the Dialog Result are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.DialogResult));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.DialogResult.None, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.DialogResult.Ok, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.DialogResult.Cancel, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.DialogResult.Abort, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.DialogResult.Retry, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.DialogResult.Ignore, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.DialogResult.Yes, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.DialogResult.No, Is.EqualTo(7));
            index++; Assert.That((Int32)FEnums.DialogResult.TryAgain, Is.EqualTo(10));
            index++; Assert.That((Int32)FEnums.DialogResult.Continue, Is.EqualTo(11));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DisplayTarget()
        {
            // This test exists to ensure all the Display Target are tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.DisplayTarget));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.DisplayTarget.User, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.DisplayTarget.Admin, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.DisplayTarget.Both, Is.EqualTo(2));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EntityLife()
        {
            // This test exists to ensure all the Entity Life is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.EntityLife));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.EntityLife.Created, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.EntityLife.Deleted, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.EntityLife.Updated, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.EntityLife.Loaded, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EntityState()
        {
            // This test exists to ensure all the Entity State is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.EntityState));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.EntityState.Dirty, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.EntityState.Saved, Is.EqualTo(1));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_EntityStatus()
        {
            // This test exists to ensure all the Entity Status is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.EntityStatus));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.EntityStatus.Inactive, Is.EqualTo(-1));
            index++; Assert.That((Int32)FEnums.EntityStatus.Active, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.EntityStatus.Approved, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.EntityStatus.PendingApproval, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.EntityStatus.Incomplete, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ErrorDialogButtons()
        {
            // This test exists to ensure all the Error Dialog Buttons is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ErrorDialogButtons));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ErrorDialogButtons.All, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ErrorDialogButtons.Continue, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ErrorDialogButtons.ExitApplication, Is.EqualTo(4));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_FileTransferArchiveAction()
        {
            // This test exists to ensure all the Error Dialog Buttons is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.FileTransferArchiveAction));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.FileTransferArchiveAction.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.FileTransferArchiveAction.Copy, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.FileTransferArchiveAction.Move, Is.EqualTo(2));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_FileTransferMethod()
        {
            // This test exists to ensure all the Error Dialog Buttons is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.FileTransferMethod));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.FileTransferMethod.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.Email, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.FileSystem, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.Ftp, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.Http, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.Rest, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.FileTransferMethod.Mq, Is.EqualTo(6));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InputType()
        {
            // This test exists to ensure all the Input Type is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.InputType));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.InputType.AllCharacters, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.InputType.AlphaNumeric, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.InputType.Integer, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.InputType.AlphaOnly, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.InputType.Decimal2dp, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.InputType.Money, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.InputType.LocalDate, Is.EqualTo(6));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_KnownFolder()
        {
            // This test exists to ensure all the Known Folder is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.KnownFolder));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.KnownFolder.Contacts, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.KnownFolder.Desktop, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.KnownFolder.Documents, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.KnownFolder.Downloads, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.KnownFolder.Favourites, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.KnownFolder.Links, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.KnownFolder.Music, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.KnownFolder.Pictures, Is.EqualTo(7));
            index++; Assert.That((Int32)FEnums.KnownFolder.SavedGames, Is.EqualTo(8));
            index++; Assert.That((Int32)FEnums.KnownFolder.SavedSearches, Is.EqualTo(9));
            index++; Assert.That((Int32)FEnums.KnownFolder.Videos, Is.EqualTo(10));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_LogSeverity()
        {
            // This test exists to ensure all the Log Severity is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.LogSeverity));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.LogSeverity.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.LogSeverity.Trace, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.LogSeverity.Information, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.LogSeverity.Success, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.LogSeverity.Audit, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.LogSeverity.Warning, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.LogSeverity.Error, Is.EqualTo(6));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MessageBoxButton()
        {
            // This test exists to ensure all the Message Box Button is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.MessageBoxButton));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.MessageBoxButton.Ok, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.OkCancel, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.AbortRetryIgnore, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.YesNoCancel, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.YesNo, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.RetryCancel, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.MessageBoxButton.CancelTryContinue, Is.EqualTo(6));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MessageBoxImage()
        {
            // This test exists to ensure all the Message Box Image is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.MessageBoxImage));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.MessageBoxImage.None, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Hand, Is.EqualTo(16));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Question, Is.EqualTo(32));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Exclamation, Is.EqualTo(48));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Asterisk, Is.EqualTo(64));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Stop, Is.EqualTo(16));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Error, Is.EqualTo(16));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Warning, Is.EqualTo(48));
            index++; Assert.That((Int32)FEnums.MessageBoxImage.Information, Is.EqualTo(64));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_MessageType()
        {
            // This test exists to ensure all the Message Type is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.MessageType));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.MessageType.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.MessageType.Information, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.MessageType.Success, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.MessageType.Warning, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.MessageType.SeriousWarning, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.MessageType.Error, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.MessageType.FatalError, Is.EqualTo(6));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ServiceStatus()
        {
            // This test exists to ensure all the Schedule Type is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ServiceStatus));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ServiceStatus.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ServiceStatus.Stopped, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ServiceStatus.StartPending, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ServiceStatus.StopPending, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.ServiceStatus.Running, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.ServiceStatus.ContinuePending, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.ServiceStatus.PauseEnding, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.ServiceStatus.Paused, Is.EqualTo(7));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ScheduleInterval()
        {
            // This test exists to ensure all the Schedule Type is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.ScheduleInterval));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.ScheduleInterval.NotSet, Is.EqualTo(-1));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Milliseconds, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Seconds, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Minutes, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Hours, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Days, Is.EqualTo(4));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Weeks, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Months, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Years, Is.EqualTo(7));
            index++; Assert.That((Int32)FEnums.ScheduleInterval.Other, Is.EqualTo(8));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_TaskStatus()
        {
            // This test exists to ensure all the Task Status is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.TaskStatus));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.TaskStatus.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.TaskStatus.Success, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.TaskStatus.Warning, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.TaskStatus.Error, Is.EqualTo(4));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_TextAlignment()
        {
            // This test exists to ensure all the Text Alignment is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.TextAlignment));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.TextAlignment.NotSet, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.TextAlignment.Left, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.TextAlignment.Centre, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.TextAlignment.Right, Is.EqualTo(3));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_WindowsMessages()
        {
            // This test exists to ensure all the Task Status is tested/checked in the next test
            FieldInfo[] fieldInfos = GetFieldInfosForType(typeof(FEnums.WindowsMessages));

            Int32 index = 0;

            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNull, Is.EqualTo(0));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCreate, Is.EqualTo(1));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDestroy, Is.EqualTo(2));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMove, Is.EqualTo(3));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSize, Is.EqualTo(5));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmActivate, Is.EqualTo(6));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetFocus, Is.EqualTo(7));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmKillFocus, Is.EqualTo(8));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEnable, Is.EqualTo(10));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetRedraw, Is.EqualTo(11));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetText, Is.EqualTo(12));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetText, Is.EqualTo(13));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetTextLength, Is.EqualTo(14));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaint, Is.EqualTo(15));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmClose, Is.EqualTo(16));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueryEndSession, Is.EqualTo(17));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQuit, Is.EqualTo(18));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueryOpen, Is.EqualTo(19));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEraseBackground, Is.EqualTo(20));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysColorChange, Is.EqualTo(21));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEndSession, Is.EqualTo(22));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmShowWindow, Is.EqualTo(24));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmWinIniChange, Is.EqualTo(26));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSettingChange, Is.EqualTo(26));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDevModeChange, Is.EqualTo(27));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmActivateApp, Is.EqualTo(28));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmFontChange, Is.EqualTo(29));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmTimeChange, Is.EqualTo(30));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCancelMode, Is.EqualTo(31));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetCursor, Is.EqualTo(32));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseActivate, Is.EqualTo(33));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmChildActivate, Is.EqualTo(34));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueueSync, Is.EqualTo(35));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetMinMaxInfo, Is.EqualTo(36));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaintIcon, Is.EqualTo(38));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmIconEraseBackground, Is.EqualTo(39));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNextDialogControl, Is.EqualTo(40));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSpoolerStatus, Is.EqualTo(42));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDrawItem, Is.EqualTo(43));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMeasureItem, Is.EqualTo(44));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDeleteItem, Is.EqualTo(45));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmVKeyToItem, Is.EqualTo(46));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCharToItem, Is.EqualTo(47));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetFont, Is.EqualTo(48));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetFont, Is.EqualTo(49));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetHotKey, Is.EqualTo(50));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetHotKey, Is.EqualTo(51));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueryDragIcon, Is.EqualTo(55));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCompareItem, Is.EqualTo(57));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetObject, Is.EqualTo(61));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCompacting, Is.EqualTo(65));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCommandNotify, Is.EqualTo(68));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmWindowPosChanging, Is.EqualTo(70));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmWindowPosChanged, Is.EqualTo(71));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPower, Is.EqualTo(72));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCopyData, Is.EqualTo(74));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCancelJournal, Is.EqualTo(75));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNotify, Is.EqualTo(78));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInputLanguageChangeRequest, Is.EqualTo(80));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInputLanguageChange, Is.EqualTo(81));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmTrainingCard, Is.EqualTo(82));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHelp, Is.EqualTo(83));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUserChanged, Is.EqualTo(84));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNotifyFormat, Is.EqualTo(85));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmContextMenu, Is.EqualTo(123));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmStyleChanging, Is.EqualTo(124));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmStyleChanged, Is.EqualTo(125));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDisplayChange, Is.EqualTo(126));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetIcon, Is.EqualTo(127));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSetIcon, Is.EqualTo(128));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcCreate, Is.EqualTo(129));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcDestroy, Is.EqualTo(130));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcCalcSize, Is.EqualTo(131));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcHitTest, Is.EqualTo(132));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcPaint, Is.EqualTo(133));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcActivate, Is.EqualTo(134));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetDialogCode, Is.EqualTo(135));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSyncPaint, Is.EqualTo(136));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMouseMove, Is.EqualTo(160));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcLeftButtonDown, Is.EqualTo(161));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcLeftButtonUp, Is.EqualTo(162));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcLeftButtonDoubleClick, Is.EqualTo(163));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcRightButtonDown, Is.EqualTo(164));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcRightButtonUp, Is.EqualTo(165));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcRightButtonDoubleClick, Is.EqualTo(166));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMiddleButtonDown, Is.EqualTo(167));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMiddleButtonUp, Is.EqualTo(168));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMiddleButtonDoubleClick, Is.EqualTo(169));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcXButtonDown, Is.EqualTo(171));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcXButtonUp, Is.EqualTo(172));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcXButtonDoubleClick, Is.EqualTo(173));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInputDeviceChange, Is.EqualTo(254));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInput, Is.EqualTo(255));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmKeyFirst, Is.EqualTo(256));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmKeyDown, Is.EqualTo(256));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmKeyUp, Is.EqualTo(257));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmChar, Is.EqualTo(258));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDeadChar, Is.EqualTo(259));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysKeyDown, Is.EqualTo(260));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysKeyUp, Is.EqualTo(261));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysChar, Is.EqualTo(262));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysDeadChar, Is.EqualTo(263));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUniChar, Is.EqualTo(265));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmKeyLast, Is.EqualTo(265));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeStartComposition, Is.EqualTo(269));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeEndComposition, Is.EqualTo(270));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeComposition, Is.EqualTo(271));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeKeyLast, Is.EqualTo(271));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInitDialog, Is.EqualTo(272));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCommand, Is.EqualTo(273));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSysCommand, Is.EqualTo(274));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmTimer, Is.EqualTo(275));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHorizontalScroll, Is.EqualTo(276));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmVerticalScroll, Is.EqualTo(277));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInitMenu, Is.EqualTo(278));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmInitMenuPopup, Is.EqualTo(279));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuSelect, Is.EqualTo(287));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuChar, Is.EqualTo(288));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEnterIdle, Is.EqualTo(289));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuRightButtonUp, Is.EqualTo(290));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuDrag, Is.EqualTo(291));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuGetObject, Is.EqualTo(292));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUnInitMenuPopUp, Is.EqualTo(293));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMenuCommand, Is.EqualTo(294));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmChangeUiState, Is.EqualTo(295));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUpdateUiState, Is.EqualTo(296));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueryUiState, Is.EqualTo(297));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorMessageBox, Is.EqualTo(306));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorEdit, Is.EqualTo(307));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorListBox, Is.EqualTo(308));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorButton, Is.EqualTo(309));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorDialog, Is.EqualTo(310));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorScrollBar, Is.EqualTo(311));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCtlColorStatic, Is.EqualTo(312));
            index++; Assert.That((Int32)FEnums.WindowsMessages.MnGetHorizontalMenu, Is.EqualTo(481));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseFirst, Is.EqualTo(512));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseMove, Is.EqualTo(512));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmLButtonDown, Is.EqualTo(513));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmLeftButtonUp, Is.EqualTo(514));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmLeftButtonDoubleClick, Is.EqualTo(515));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmRightButtonDown, Is.EqualTo(516));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmRightButtonUp, Is.EqualTo(517));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmRightButtonDoubleClick, Is.EqualTo(518));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMiddleButtonDown, Is.EqualTo(519));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMiddleButtonUp, Is.EqualTo(520));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMiddleButtonDoubleClick, Is.EqualTo(521));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseWheel, Is.EqualTo(522));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmXButtonDown, Is.EqualTo(523));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmXButtonUp, Is.EqualTo(524));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmXButtonDoubleClick, Is.EqualTo(525));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseHorizontalWheel, Is.EqualTo(526));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmParentNotify, Is.EqualTo(528));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEnterMenuLoop, Is.EqualTo(529));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmExitMenuLoop, Is.EqualTo(530));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNextMenu, Is.EqualTo(531));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSizing, Is.EqualTo(532));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCaptureChanged, Is.EqualTo(533));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMoving, Is.EqualTo(534));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPowerBroadcast, Is.EqualTo(536));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDeviceChange, Is.EqualTo(537));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiCreate, Is.EqualTo(544));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiDestroy, Is.EqualTo(545));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiActivate, Is.EqualTo(546));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiRestore, Is.EqualTo(547));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiNext, Is.EqualTo(548));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiMaximize, Is.EqualTo(549));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiTile, Is.EqualTo(550));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiCascade, Is.EqualTo(551));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiIconArrange, Is.EqualTo(552));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiGetActive, Is.EqualTo(553));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiSetMenu, Is.EqualTo(560));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmEnterSizeMove, Is.EqualTo(561));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmExitSizeMove, Is.EqualTo(562));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDropFiles, Is.EqualTo(563));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMdiRefreshMenu, Is.EqualTo(564));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeSetContext, Is.EqualTo(641));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeNotify, Is.EqualTo(642));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeControl, Is.EqualTo(643));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeCompositionFull, Is.EqualTo(644));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeSelect, Is.EqualTo(645));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeChar, Is.EqualTo(646));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeRequest, Is.EqualTo(648));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeKeyDown, Is.EqualTo(656));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmImeKeyUp, Is.EqualTo(657));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMouseHover, Is.EqualTo(672));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseHover, Is.EqualTo(673));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmNcMouseLeave, Is.EqualTo(674));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmMouseLeave, Is.EqualTo(675));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmWtsSessionChange, Is.EqualTo(689));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmTabletFirst, Is.EqualTo(704));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmTabletLast, Is.EqualTo(735));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCut, Is.EqualTo(768));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmCopy, Is.EqualTo(769));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaste, Is.EqualTo(770));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmClear, Is.EqualTo(771));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUndo, Is.EqualTo(772));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmRenderFormat, Is.EqualTo(773));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmRenderAllFormats, Is.EqualTo(774));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDestroyClipboard, Is.EqualTo(775));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDrawClipboard, Is.EqualTo(776));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaintClipboard, Is.EqualTo(777));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmVerticalScrollClipboard, Is.EqualTo(778));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmSizeClipboard, Is.EqualTo(779));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmAskCbFormatName, Is.EqualTo(780));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmChangeCbChain, Is.EqualTo(781));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHorizontalScrollClipboard, Is.EqualTo(782));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmQueryNewPalette, Is.EqualTo(783));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaletteIsChanging, Is.EqualTo(784));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPaletteChanged, Is.EqualTo(785));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHotKey, Is.EqualTo(786));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPrint, Is.EqualTo(791));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPrintClient, Is.EqualTo(792));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmAppCommand, Is.EqualTo(793));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmThemeChanged, Is.EqualTo(794));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmClipboardUpdate, Is.EqualTo(797));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDwmCompositionChanged, Is.EqualTo(798));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDwmNcRenderingChanged, Is.EqualTo(799));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDwmColorizationColorChanged, Is.EqualTo(800));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmDwmWindowMaximizedChange, Is.EqualTo(801));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmGetTitlebarInfoEx, Is.EqualTo(831));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHandheldFirst, Is.EqualTo(856));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmHandheldLast, Is.EqualTo(863));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmAfxFirst, Is.EqualTo(864));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmAfxLast, Is.EqualTo(895));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPenWinFirst, Is.EqualTo(896));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmPenWinLast, Is.EqualTo(911));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmUser, Is.EqualTo(1024));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmReflect, Is.EqualTo(8192));
            index++; Assert.That((Int32)FEnums.WindowsMessages.WmApp, Is.EqualTo(32768));

            Assert.That(fieldInfos.Length, Is.EqualTo(index));
        }
    }
}
