﻿CREATE TABLE [dbo].[Teams]
(
	[TeamID] INT NOT NULL PRIMARY KEY, 
    [TeamName] VARCHAR(30) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [Version] TIMESTAMP NOT NULL
)
