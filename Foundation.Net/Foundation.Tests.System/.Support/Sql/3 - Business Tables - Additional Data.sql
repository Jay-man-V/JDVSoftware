-- disable all constraints
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

-- core.ApprovalStatus

SET IDENTITY_INSERT [core].[ApprovalStatus] ON 

--INSERT [core].[ApprovalStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2021-03-29T00:00:00.000' AS DateTime), CAST(N'2021-03-29T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'D', N'Deleted')

SET IDENTITY_INSERT [core].[ApprovalStatus] OFF


-- core.ContractType

SET IDENTITY_INSERT [core].[ContractType] ON 

--INSERT [core].[ContractType] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2019-03-07T00:00:00.000' AS DateTime), CAST(N'2019-03-07T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'D', N'Deleted')

SET IDENTITY_INSERT [core].[ContractType] OFF


-- core.ContactType

SET IDENTITY_INSERT [core].[ContactType] ON 

--INSERT [core].[ContactType] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2017-09-30T00:00:00.000' AS DateTime), CAST(N'2017-09-30T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[ContactType] OFF


-- sec.UserProfile

SET IDENTITY_INSERT sec.UserProfile ON 

--INSERT sec.UserProfile (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, ExternalKeyId, Username, DisplayName, IsSystemSupport, ContactDetailId)
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2100-12-31 23:59:59', NULL, 'Ganymede\Deleted', 'Deleted', 1, NULL)

SET IDENTITY_INSERT sec.UserProfile OFF


 -- core.Status

 SET IDENTITY_INSERT core.Status ON 

--INSERT core.Status (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Description)
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2100-12-31 23:59:59', 'Deleted', 'Deleted')

SET IDENTITY_INSERT core.Status OFF


-- core.Country

SET IDENTITY_INSERT [core].[Country] ON 

--INSERT [core].[Country] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ISOCode], [AbbreviatedName], [FullName], [NativeName], [DialingCode], [PostCodeFormat], [CurrencyId], [LanguageId], [TimeZoneId], [WorldRegionId], [ImageTypeId], [CountryFlag])
--VALUES (9001, -1, 1, 1, CAST(N'2021-11-29T00:00:00.000' AS DateTime), CAST(N'2021-11-29T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), NULL, N'Deleted', N'Deleted', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

SET IDENTITY_INSERT [core].[Country] OFF


-- core.NationalRegion

SET IDENTITY_INSERT [core].[NationalRegion] ON 

--INSERT [core].[NationalRegion] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [CountryId], [Abbreviation], [ShortName], [FullName])
--VALUES (1, 0, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 67, N'National Region', N'National Region', N'National Region')
--INSERT [core].[NationalRegion] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [CountryId], [Abbreviation], [ShortName], [FullName])
--VALUES (9001, -1, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), 67, N'Deleted', N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[NationalRegion] OFF


-- core.NonWorkingDay

SET IDENTITY_INSERT [core].[NonWorkingDay] ON 

--INSERT [core].[NonWorkingDay] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Date], [CountryId], [Description], [Notes])
--VALUES (9001, -1, 1, 1, CAST(N'2022-09-18T00:00:00.000' AS DateTime), CAST(N'2022-09-18T00:00:00.000' AS DateTime), CAST(N'2000-09-18T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), CAST(N'2022-09-19T00:00:00.000' AS DateTime), 229, N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[NonWorkingDay] OFF

-- core.WorldRegion

SET IDENTITY_INSERT [core].[WorldRegion] ON 

--INSERT [core].[WorldRegion] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name])
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', N'Deleted')

SET IDENTITY_INSERT [core].[WorldRegion] OFF


-- core.TaskStatus

SET IDENTITY_INSERT [core].[TaskStatus] ON 

--INSERT [core].[TaskStatus] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-21T00:00:00.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[TaskStatus] OFF


-- log.LogSeverity

SET IDENTITY_INSERT [log].[LogSeverity] ON 

--INSERT [log].[LogSeverity] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Code], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2019-03-06T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'D', N'Deleted')

SET IDENTITY_INSERT [log].[LogSeverity] OFF


-- core.ImageType

SET IDENTITY_INSERT [core].[ImageType] ON 

--INSERT [core].[ImageType] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [FileExtension])
--VALUES (9001, -1, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[ImageType] OFF


-- core.ScheduleInterval

SET IDENTITY_INSERT [core].[ScheduleInterval] ON 

--INSERT [core].[ScheduleInterval] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [core].[ScheduleInterval] OFF


-- sec.Application

SET IDENTITY_INSERT [sec].[Application] ON 

--INSERT [sec].[Application] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [sec].[Application] OFF


-- sec.ApplicationType

SET IDENTITY_INSERT [sec].[ApplicationType] ON 

--INSERT [sec].[ApplicationType] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description])
--VALUES (9001, -1, 1, 1, CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted')

SET IDENTITY_INSERT [sec].[ApplicationType] OFF


-- sec.role

SET IDENTITY_INSERT [sec].[Role] ON 

--INSERT [sec].[Role] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [Name], [Description], [SystemSupportOnly])
--VALUES (9001, -1, 1, 1, CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2016-09-28T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2199-12-31T23:59:59.000' AS DateTime), N'Deleted', N'Deleted', 1)

SET IDENTITY_INSERT [sec].[Role] OFF


-- sec.ApplicationUserRole

SET IDENTITY_INSERT [sec].[ApplicationUserRole] ON 

--INSERT [sec].[ApplicationUserRole] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ApplicationId], [UserProfileId], [RoleId])
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', 9001, 9001, 9001)

SET IDENTITY_INSERT [sec].[ApplicationUserRole] OFF


-- sec.ApplicationRole

SET IDENTITY_INSERT [sec].[ApplicationRole] ON 

--INSERT [sec].[ApplicationRole] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ValidFrom], [ValidTo], [ApplicationId], [RoleId])
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), GETDATE(), '2199-12-31T23:59:59.000', 9001, 9001)

SET IDENTITY_INSERT [sec].[ApplicationRole] OFF


-- stg.ActiveDirectoryUser

SET IDENTITY_INSERT [stg].[ActiveDirectoryUser] ON 

--INSERT [stg].[ActiveDirectoryUser] ([Id], [StatusId], [CreatedByUserProfileId], [LastUpdatedByUserProfileId], [CreatedOn], [LastUpdatedOn], [ObjectSId], [Name], [FullName])
--VALUES (9001, -1, 1, 1, GETDATE(), GETDATE(), 'ABC123', 'DEF456', 'GHI789')

SET IDENTITY_INSERT [stg].[ActiveDirectoryUser] OFF



EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'
