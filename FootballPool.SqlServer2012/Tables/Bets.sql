CREATE TABLE [dbo].[Bets]
(
	[BetID] INT NOT NULL PRIMARY KEY, 
    [GameID] INT NOT NULL, 
    [PlacedByID] INT NOT NULL, 
    [TeamToCoverBetID] INT NOT NULL, 
    [Points] FLOAT NOT NULL, 
    [Amount] INT NOT NULL, 
    [Version] ROWVERSION NOT NULL, 
    CONSTRAINT [FK_Bets_Games] FOREIGN KEY ([GameID]) REFERENCES [Games]([ID]), 
    CONSTRAINT [FK_Bets_PoolUsers] FOREIGN KEY ([PlacedByID]) REFERENCES [PoolUsers]([PoolUserID]), 
    CONSTRAINT [FK_Bets_Teams] FOREIGN KEY ([TeamToCoverBetID]) REFERENCES [Teams]([TeamID])
)
