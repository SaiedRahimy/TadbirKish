IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Data') IS NULL EXEC(N'CREATE SCHEMA [Data];');
GO

CREATE TABLE [Data].[Coverages] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Min] bigint NOT NULL,
    [Max] bigint NOT NULL,
    [Coefficient] float NOT NULL,
    [Created] datetime2 NOT NULL DEFAULT (getdate()),
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL DEFAULT (getdate()),
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Coverages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Data].[RequestCoverages] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [TotalNetPremium] float NOT NULL,
    [Created] datetime2 NOT NULL DEFAULT (getdate()),
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL DEFAULT (getdate()),
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RequestCoverages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Data].[RequestCoverageDetails] (
    [Id] bigint NOT NULL IDENTITY,
    [RequestCoverageId] int NOT NULL,
    [CoverageId] int NOT NULL,
    [GrossPremium] bigint NOT NULL,
    [NetPremium] float NOT NULL,
    [Created] datetime2 NOT NULL DEFAULT (getdate()),
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL DEFAULT (getdate()),
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RequestCoverageDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestCoverageDetails_Coverages_CoverageId] FOREIGN KEY ([CoverageId]) REFERENCES [Data].[Coverages] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RequestCoverageDetails_RequestCoverages_RequestCoverageId] FOREIGN KEY ([RequestCoverageId]) REFERENCES [Data].[RequestCoverages] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Coefficient', N'CreatedBy', N'LastModifiedBy', N'Max', N'Min', N'Name') AND [object_id] = OBJECT_ID(N'[Data].[Coverages]'))
    SET IDENTITY_INSERT [Data].[Coverages] ON;
INSERT INTO [Data].[Coverages] ([Id], [Coefficient], [CreatedBy], [LastModifiedBy], [Max], [Min], [Name])
VALUES (1, 0.0051999999999999998E0, NULL, NULL, CAST(500000000 AS bigint), CAST(5000 AS bigint), N'جراحی'),
(2, 0.0041999999999999997E0, NULL, NULL, CAST(400000000 AS bigint), CAST(4000 AS bigint), N'دندانپزشکی'),
(3, 0.0050000000000000001E0, NULL, NULL, CAST(200000000 AS bigint), CAST(2000 AS bigint), N'بستری');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Coefficient', N'CreatedBy', N'LastModifiedBy', N'Max', N'Min', N'Name') AND [object_id] = OBJECT_ID(N'[Data].[Coverages]'))
    SET IDENTITY_INSERT [Data].[Coverages] OFF;
GO

CREATE UNIQUE INDEX [IX_Coverages_Name] ON [Data].[Coverages] ([Name]);
GO

CREATE INDEX [IX_RequestCoverageDetails_CoverageId] ON [Data].[RequestCoverageDetails] ([CoverageId]);
GO

CREATE UNIQUE INDEX [IX_RequestCoverageDetails_RequestCoverageId_CoverageId] ON [Data].[RequestCoverageDetails] ([RequestCoverageId], [CoverageId]);
GO

CREATE UNIQUE INDEX [IX_RequestCoverages_Name] ON [Data].[RequestCoverages] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251016083649_CreateDatabaseAndEnities', N'8.0.7');
GO

COMMIT;
GO

