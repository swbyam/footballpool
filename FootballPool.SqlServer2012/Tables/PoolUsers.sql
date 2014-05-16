CREATE TABLE [dbo].[PoolUsers]
(
	[PoolUserID] INT NOT NULL PRIMARY KEY, 
    [SiteUserID] INT NOT NULL,
	[PoolID] INT NOT NULL, 
	[UserName] VARCHAR(30) NOT NULL, 
    [Version] ROWVERSION NOT NULL,
    CONSTRAINT [FK_PoolUsers_SiteUsers] FOREIGN KEY ([SiteUserID]) REFERENCES [SiteUsers]([SiteUserID]), 
    CONSTRAINT [FK_PoolUsers_Pool] FOREIGN KEY ([PoolID]) REFERENCES [Pools]([PoolID])
)
