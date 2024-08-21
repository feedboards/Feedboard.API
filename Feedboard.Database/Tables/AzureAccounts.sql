CREATE TABLE [dbo].[AzureAccounts] (
    [Email]                NVARCHAR (450) NOT NULL,
    [IdToken]              VARCHAR (4500) NOT NULL,
    [AccessToken]          VARCHAR (4500) NOT NULL,
    [RefreshToken]         VARCHAR (4500) NOT NULL,
    [AccessTokenExpiredAt] DATETIME2 (7)  NOT NULL,
    [CreatedAt]            DATETIME2 (7)  CONSTRAINT [DF__AzureAcco__Creat__22AA2996] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]            DATETIME2 (7)  NULL,
    [IsActive]             BIT            NOT NULL,
    CONSTRAINT [PK_AzureAccounts] PRIMARY KEY CLUSTERED ([Email] ASC)
);


GO
CREATE NONCLUSTERED INDEX [AzureAccounts_CreatedAt_index]
    ON [dbo].[AzureAccounts]([CreatedAt] ASC);

