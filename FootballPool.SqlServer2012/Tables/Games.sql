CREATE TABLE [dbo].[Games]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [HomeTeamID] INT NOT NULL, 
    [VisitingTeamID] INT NOT NULL, 
    [FavoriteTeamID] INT NOT NULL, 
    [GameDate] SMALLDATETIME NOT NULL, 
    [WeekNumber] TINYINT NOT NULL, 
    [Line] FLOAT NOT NULL, 
    [OverUnder] FLOAT NOT NULL, 
    [Version] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_Games_Home_Teams] FOREIGN KEY ([HomeTeamID]) REFERENCES [Teams]([TeamID]), 
    CONSTRAINT [FK_Games_Visting_Teams] FOREIGN KEY ([VisitingTeamID]) REFERENCES [Teams]([TeamID]), 
    CONSTRAINT [FK_Games_Favorite_Teams] FOREIGN KEY ([FavoriteTeamID]) REFERENCES [Teams]([TeamID]), 
    CONSTRAINT [PK_Games] PRIMARY KEY ([ID])
)
