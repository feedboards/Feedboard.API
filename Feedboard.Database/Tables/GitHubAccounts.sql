CREATE TABLE [dbo].[GitHubAccounts] (
    [UserId]      VARCHAR (450) NOT NULL,
    [AccessToken] VARCHAR (512) NOT NULL,
    [Scopes]      VARCHAR (128) NOT NULL,
    [Username]    VARCHAR (256) NOT NULL,
    [Email]       VARCHAR (450) NOT NULL,
    [CreatedAt]   DATETIME2 (7) CONSTRAINT [DF_GitHubAccounts_CreatedAt] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]   DATETIME2 (7) NULL,
    [IsActive]    BIT           NOT NULL,
    [PublicEmail] VARCHAR (450) NULL,
    CONSTRAINT [PK_GitHubAccounts_1] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_GitHubAccounts]
    ON [dbo].[GitHubAccounts]([CreatedAt] ASC);

