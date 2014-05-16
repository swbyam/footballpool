CREATE TABLE [dbo].[SiteUsers]
(
	[SiteUserID] INT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(30) NOT NULL, 
    [Version] ROWVERSION NOT NULL
)
