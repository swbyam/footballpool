Select b.*, p.UserName, t.TeamName
From Bets b Join Games g On b.GameID = g.ID
Join PoolUsers p On b.PlacedByID = p.PoolUserID
Join Teams t On b.TeamToCoverBetID = t.TeamID
Where g.WeekNumber = 1
Order By Amount
--Order By t.TeamName
--Order By p.UserName