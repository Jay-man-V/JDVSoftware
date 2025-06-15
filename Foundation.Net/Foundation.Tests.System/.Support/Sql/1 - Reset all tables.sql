-- disable all constraints
EXEC sp_MsForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

EXEC sp_MsForEachTable 'DELETE FROM ?'


-- core.ConfigurationScope

SET IDENTITY_INSERT [core].[ConfigurationScope] ON

-- INSERT INTO [core].[ConfigurationScope] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) values (0, 0, 1, 1, GETDATE(), GETDATE(), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 'NotSet', 'Not set');
INSERT INTO [core].[ConfigurationScope] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (1, 0, 1, 1, GETDATE(), GETDATE(), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 'System', 'System');
INSERT INTO [core].[ConfigurationScope] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (2, 0, 1, 1, GETDATE(), GETDATE(), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 'Application', 'Application');
INSERT INTO [core].[ConfigurationScope] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (3, 0, 1, 1, GETDATE(), GETDATE(), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 'User', 'User');

SET IDENTITY_INSERT [core].[ConfigurationScope] OFF


 -- core.Status

SET IDENTITY_INSERT [core].[Status] ON 

INSERT [core].[Status] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (-1, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Inactive', N'Inactive')
INSERT [core].[Status] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES ( 0, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Active', N'Active')
INSERT [core].[Status] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES ( 1, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'InComplete', N'In complete')
INSERT [core].[Status] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES ( 2, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Approved', N'Approved')
INSERT [core].[Status] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES ( 3, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'PendingApproval', N'Pending Approval')

SET IDENTITY_INSERT [core].[Status] OFF


-- sec.role

SET IDENTITY_INSERT [sec].[Role] ON 

INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   0, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'None', N'No Access', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   1, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'ReadOnly', N'Read only', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   2, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Reporter', N'Reporting only', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   3, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Creator', N'Can create records', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   4, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'OwnEditor', N'Edit their own data only', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   5, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'AllEditor', N'Can edit all records', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   6, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'OwnDelete', N'Delete their own data only', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   7, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'AllDelete', N'Can delete all records', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   8, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Approver', N'Approver', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (   9, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'TeamSupervisor', N'Team Supervisor', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (  10, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'DeputyTeamManager', N'Deputy Team Manager', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (  11, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'PrimaryTeamManager', N'Primary Team Manager', 0)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES ( 998, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'SystemSupervisor', N'System Supervisor', 1)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES ( 999, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'SystemDataAdministrator', N'System Data Administrator', 1)
INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly]) VALUES (1000, 0, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'SystemAdministrator', N'System Administrator', 1)

SET IDENTITY_INSERT [sec].[Role] OFF


-- sec.Application

SET IDENTITY_INSERT [sec].[Application] ON 

INSERT [sec].[Application] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (1, 0, 1, 1, CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'UnitTest', N'Unit Testing of Foundation Software')

SET IDENTITY_INSERT [sec].[Application] OFF


-- sec.ApplicationUserRole

SET IDENTITY_INSERT [sec].[ApplicationUserRole] ON 

INSERT [sec].[ApplicationUserRole] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ApplicationId], [UserProfileId], [RoleId]) VALUES (0, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', 1, 0, 1000)
INSERT [sec].[ApplicationUserRole] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ApplicationId], [UserProfileId], [RoleId]) VALUES (1, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', 1, 1, 1000)
INSERT [sec].[ApplicationUserRole] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ApplicationId], [UserProfileId], [RoleId]) VALUES (2, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', 1, 2, 1000)

SET IDENTITY_INSERT [sec].[ApplicationUserRole] OFF


-- sec.UserProfile

SET IDENTITY_INSERT sec.UserProfile ON 

INSERT sec.UserProfile (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, ExternalKeyId, Username, DisplayName, IsSystemSupport, ContactDetailId) VALUES (0, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2100-12-31 23:59:59', NULL, 'System', 'System', 1, NULL)
INSERT sec.UserProfile (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, ExternalKeyId, Username, DisplayName, IsSystemSupport, ContactDetailId) VALUES (1, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2100-12-31 23:59:59', NULL, 'System', 'System', 1, NULL)
INSERT sec.UserProfile (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, ExternalKeyId, Username, DisplayName, IsSystemSupport, ContactDetailId) VALUES (2, 0, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2100-12-31 23:59:59', NULL, 'Ganymede\Jayes', 'Jayesh Varsani', 1, NULL)

SET IDENTITY_INSERT sec.UserProfile OFF


-- core.TaskStatus

SET IDENTITY_INSERT [core].[TaskStatus] ON 

INSERT [core].[TaskStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (0, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T00:00:00.000' AS DateTime), N'Not Set', N'Not Set')
INSERT [core].[TaskStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (1, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T00:00:00.000' AS DateTime), N'Success', N'Success')
INSERT [core].[TaskStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (2, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T00:00:00.000' AS DateTime), N'Warning', N'Warning')
INSERT [core].[TaskStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description]) VALUES (3, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-21T00:00:00.000' AS DateTime), N'Error', N'Error')

SET IDENTITY_INSERT [core].[TaskStatus] OFF


-- log.LogSeverity

SET IDENTITY_INSERT [log].[LogSeverity] ON 

INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (0, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'N', N'Not Set')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (1, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'T', N'Trace')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (2, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'I', N'Information')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (3, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'S', N'Success')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (4, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'A', N'Audit')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (5, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'W', N'Warning')
INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description]) VALUES (6, 0, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'E', N'Error')

SET IDENTITY_INSERT [log].[LogSeverity] OFF

EXEC sp_MsForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'
