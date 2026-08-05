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
CREATE TABLE [Activities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [ActivityType] nvarchar(50) NOT NULL,
    [ActivityDate] date NOT NULL,
    [StartTime] time NOT NULL,
    [DurationSeconds] int NOT NULL,
    [DistanceMeters] float NOT NULL,
    [ElevationGainMeters] float NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260805194721_InitialCreate', N'10.0.10');

COMMIT;
GO

