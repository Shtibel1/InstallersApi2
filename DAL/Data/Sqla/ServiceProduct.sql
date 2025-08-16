/* 1) ServiceProducts */
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceProducts]') AND type = 'U')
BEGIN
  CREATE TABLE [dbo].[ServiceProducts](
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(200)    NOT NULL,
    CONSTRAINT [PK_ServiceProducts] PRIMARY KEY CLUSTERED ([Id] ASC)
  );
  CREATE UNIQUE INDEX [UX_ServiceProducts_Name] ON [dbo].[ServiceProducts]([Name]);
END
GO

/* 2) Product ↔ ServiceProduct requirement (with required Quantity per product) */
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductRequiredServiceProducts]') AND type = 'U')
BEGIN
  CREATE TABLE [dbo].[ProductRequiredServiceProducts](
    [ProductId]        UNIQUEIDENTIFIER NOT NULL,
    [ServiceProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]         INT              NOT NULL CONSTRAINT [DF_PRSP_Quantity] DEFAULT (1),
    CONSTRAINT [CK_PRSP_Quantity_Positive] CHECK ([Quantity] > 0),

    CONSTRAINT [PK_ProductRequiredServiceProducts]
      PRIMARY KEY CLUSTERED ([ProductId],[ServiceProductId]),

    CONSTRAINT [FK_PRSP_Products_ProductId]
      FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]) ON DELETE CASCADE,

    CONSTRAINT [FK_PRSP_ServiceProducts_ServiceProductId]
      FOREIGN KEY ([ServiceProductId]) REFERENCES [dbo].[ServiceProducts]([Id]) ON DELETE CASCADE
  );

  CREATE INDEX [IX_PRSP_ServiceProductId] ON [dbo].[ProductRequiredServiceProducts]([ServiceProductId]);
END
GO

/* 3) Per-provider stock of each ServiceProduct
      (ServiceProviderIdExternal is a GUID from Central DB; no FK here) */
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceProviderStock]') AND type = 'U')
BEGIN
  CREATE TABLE [dbo].[ServiceProviderStock](
    [Id]                        UNIQUEIDENTIFIER NOT NULL,
    [ServiceProviderIdExternal] UNIQUEIDENTIFIER NOT NULL,
    [ServiceProductId]          UNIQUEIDENTIFIER NOT NULL,
    [Amount]                    INT              NOT NULL CONSTRAINT [DF_SPS_Amount] DEFAULT (0),
    [RowVersion]                ROWVERSION       NOT NULL,  -- optimistic concurrency
    CONSTRAINT [CK_SPS_Amount_NonNegative] CHECK ([Amount] >= 0),

    CONSTRAINT [PK_ServiceProviderStock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SPS_ServiceProducts_ServiceProductId]
      FOREIGN KEY ([ServiceProductId]) REFERENCES [dbo].[ServiceProducts]([Id]) ON DELETE NO ACTION
  );

  /* One row per provider + service product */
  CREATE UNIQUE INDEX [UX_SPS_Provider_ServiceProduct]
    ON [dbo].[ServiceProviderStock]([ServiceProviderIdExternal],[ServiceProductId]);

  CREATE INDEX [IX_SPS_ServiceProductId] ON [dbo].[ServiceProviderStock]([ServiceProductId]);
END
GO

/* 4) Append-only audit for stock changes */
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceProviderStockAudit]') AND type = 'U')
BEGIN
  CREATE TABLE [dbo].[ServiceProviderStockAudit](
    [Id]                        UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_SPSA_Id] DEFAULT (NEWID()),
    [ServiceProviderIdExternal] UNIQUEIDENTIFIER NOT NULL,
    [ServiceProductId]          UNIQUEIDENTIFIER NOT NULL,
    [Delta]                     INT              NOT NULL,                  -- +in / -out
    [BalanceAfter]              INT              NOT NULL,
    [Reason]                    NVARCHAR(200)    NULL,
    [PerformedByUserId]         UNIQUEIDENTIFIER NULL,
    [ReferenceId]               UNIQUEIDENTIFIER NULL,                      -- e.g. AssignmentId/OrderId
    [PerformedAt]               DATETIME2(3)     NOT NULL CONSTRAINT [DF_SPSA_PerformedAt] DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [CK_SPSA_Delta_NotZero] CHECK ([Delta] <> 0),

    CONSTRAINT [PK_ServiceProviderStockAudit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SPSA_ServiceProducts_ServiceProductId]
      FOREIGN KEY ([ServiceProductId]) REFERENCES [dbo].[ServiceProducts]([Id]) ON DELETE NO ACTION
  );

  CREATE INDEX [IX_SPSA_Provider_ServiceProduct_PerformedAt]
    ON [dbo].[ServiceProviderStockAudit]([ServiceProviderIdExternal],[ServiceProductId],[PerformedAt]);
END
GO
