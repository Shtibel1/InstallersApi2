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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Worker] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [IdentityId] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Worker] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Position] int NULL,
    [CustomerInstallationPrice] float NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CategoryInstaller] (
    [CategoryId] int NOT NULL,
    [InstallerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CategoryInstaller] PRIMARY KEY ([CategoryId], [InstallerId]),
    CONSTRAINT [FK_CategoryInstaller_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryInstaller_Worker_InstallerId] FOREIGN KEY ([InstallerId]) REFERENCES [Worker] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Assignments] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [InstallationDate] datetime2 NULL,
    [CustomerNeedsToPay] float NULL,
    [CustomerAlreadyPaid] float NULL,
    [AssignmentCost] float NOT NULL,
    [InstallationPrice] float NULL,
    [InnerFloorPrice] float NULL,
    [OuterFloorPrice] float NULL,
    [CarryPrice] float NULL,
    [Status] nvarchar(max) NOT NULL,
    [ProductId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [ManagerId] uniqueidentifier NOT NULL,
    [InstallerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Assignments_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Assignments_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Assignments_Worker_InstallerId] FOREIGN KEY ([InstallerId]) REFERENCES [Worker] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Assignments_Worker_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [Worker] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [InstallerPricing] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [ProductId] int NOT NULL,
    [InstallerId] nvarchar(max) NOT NULL,
    [InstallerId1] uniqueidentifier NOT NULL,
    [InstallationPrice] float NULL,
    [OuterFloorPrice] float NULL,
    [InnerFloorPrice] float NULL,
    [CarryPrice] float NULL,
    CONSTRAINT [PK_InstallerPricing] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InstallerPricing_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_InstallerPricing_Worker_InstallerId1] FOREIGN KEY ([InstallerId1]) REFERENCES [Worker] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [WorkerId] uniqueidentifier NOT NULL,
    [AssignmentId] int NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Assignments_CustomerId] ON [Assignments] ([CustomerId]);
GO

CREATE INDEX [IX_Assignments_InstallerId] ON [Assignments] ([InstallerId]);
GO

CREATE INDEX [IX_Assignments_ManagerId] ON [Assignments] ([ManagerId]);
GO

CREATE INDEX [IX_Assignments_ProductId] ON [Assignments] ([ProductId]);
GO

CREATE INDEX [IX_CategoryInstaller_InstallerId] ON [CategoryInstaller] ([InstallerId]);
GO

CREATE INDEX [IX_Comments_AssignmentId] ON [Comments] ([AssignmentId]);
GO

CREATE INDEX [IX_InstallerPricing_InstallerId1] ON [InstallerPricing] ([InstallerId1]);
GO

CREATE INDEX [IX_InstallerPricing_ProductId] ON [InstallerPricing] ([ProductId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240504163505_init', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [InstallerPricing] DROP CONSTRAINT [FK_InstallerPricing_Worker_InstallerId1];
GO

DROP INDEX [IX_InstallerPricing_InstallerId1] ON [InstallerPricing];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InstallerPricing]') AND [c].[name] = N'InstallerId1');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [InstallerPricing] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [InstallerPricing] DROP COLUMN [InstallerId1];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InstallerPricing]') AND [c].[name] = N'InstallerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [InstallerPricing] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [InstallerPricing] ALTER COLUMN [InstallerId] uniqueidentifier NOT NULL;
GO

CREATE INDEX [IX_InstallerPricing_InstallerId] ON [InstallerPricing] ([InstallerId]);
GO

ALTER TABLE [InstallerPricing] ADD CONSTRAINT [FK_InstallerPricing_Worker_InstallerId] FOREIGN KEY ([InstallerId]) REFERENCES [Worker] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240504171741_second', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Assignments] ADD [DistancePrice] float NULL;
GO

ALTER TABLE [Assignments] ADD [PickupStatus] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240518231958_distancepickup', N'7.0.14');
GO

COMMIT;
GO

