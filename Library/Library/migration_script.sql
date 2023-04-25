IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    CREATE TABLE [Genres] (
        [GenreId] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    CREATE TABLE [Statuses] (
        [StatusId] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Statuses] PRIMARY KEY ([StatusId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    CREATE TABLE [Books] (
        [Id] int NOT NULL IDENTITY,
        [ISBN] nvarchar(13) NULL,
        [Title] nvarchar(max) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [Author] nvarchar(max) NOT NULL,
        [GenreId] nvarchar(450) NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        [StatusId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Books_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([GenreId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Books_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([StatusId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenreId', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] ON;
    INSERT INTO [Genres] ([GenreId], [Name])
    VALUES (N'1', N'Fiction'),
    (N'2', N'Non-Fiction'),
    (N'3', N'Children'),
    (N'4', N'Other');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenreId', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StatusId', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
        SET IDENTITY_INSERT [Statuses] ON;
    INSERT INTO [Statuses] ([StatusId], [Name])
    VALUES (N'available', N'Available'),
    (N'checked', N'Checked'),
    (N'returned', N'Returned');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StatusId', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
        SET IDENTITY_INSERT [Statuses] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    CREATE INDEX [IX_Books_GenreId] ON [Books] ([GenreId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    CREATE INDEX [IX_Books_StatusId] ON [Books] ([StatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425150749_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230425150749_InitialCreate', N'3.1.32');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230425234201_mssql_migration_535')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230425234201_mssql_migration_535', N'3.1.32');
END;

GO

