SET IDENTITY_INSERT TestEntity ON

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (1, 0, 1, 2, '2017-01-01 21:26', '2018-05-27 05:00', '2018-01-01', '2018-01-31', 'Name: 1', 'Code: 1', 'Description: 1')

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (2, 0, 1, 2, '2017-02-01 21:26', '2018-02-27 05:00', '2018-01-01', '2100-01-31', 'Name: 2', 'Code: 2', 'Description: 2')

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (3, 0, 1, 2, '2017-01-01 21:26', '2018-05-27 05:00', NULL, NULL, 'Name: 3', 'Code: 3', 'Description: 3')

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (4, 0, 1, 2, '2017-02-01 21:26', '2018-02-27 05:00', NULL, NULL, 'Name: 4', 'Code: 4', 'Description: 4')

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (5, 0, 1, 2, '2017-01-01 21:26', '2018-05-27 05:00', '2018-01-01', NULL, 'Name: 5', 'Code: 5', 'Description: 5')

INSERT INTO TestEntity (Id, StatusId, CreatedByUserProfileId, LastUpdatedByUserProfileId, CreatedOn, LastUpdatedOn, ValidFrom, ValidTo, Name, Code, Description)
VALUES (6, 0, 1, 2, '2017-02-01 21:26', '2018-02-27 05:00', '2018-01-01', NULL, 'Name: 6', 'Code: 6', 'Description: 6')

SET IDENTITY_INSERT TestEntity OFF
