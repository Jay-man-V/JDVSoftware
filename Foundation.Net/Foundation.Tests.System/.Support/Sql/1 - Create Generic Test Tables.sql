IF (EXISTS (
            SELECT
                * 
            FROM
                INFORMATION_SCHEMA.TABLES 
            WHERE
                TABLE_SCHEMA = 'dbo' AND 
                TABLE_NAME = 'TestEntity'
            )
   )
BEGIN

    DROP TABLE [TestEntity]

END

CREATE TABLE [TestEntity]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Timestamp] [timestamp] NOT NULL,
    [StatusId] [int] NOT NULL,
    [CreatedByUserProfileId] [int] NOT NULL,
    [LastUpdatedByUserProfileId] [int] NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [LastUpdatedOn] [datetime] NOT NULL,
    [ValidFrom] [datetime] NULL,
    [ValidTo] [datetime] NULL,
    [IsOpen] [bit] NULL,
    [IsClosed] [bit] NULL,
    [UnitPrice] [decimal](18, 10) NULL,
    [Quantity] [decimal](18, 10) NULL,
    [Count] [int] NULL,
    [Name] [nvarchar](150) NULL,
    [Code] [nvarchar](25) NULL,
    [Description] [nvarchar](150) NULL,
    [ImagePicture] [varbinary](MAX) NULL,
    [Duration] [time](7) NULL,
    [ExecutionTime] [time](7) NULL,
    CONSTRAINT [PK_CORE_Status] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
