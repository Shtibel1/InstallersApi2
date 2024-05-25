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

