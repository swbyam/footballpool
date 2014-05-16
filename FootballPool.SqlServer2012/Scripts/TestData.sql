	--Site Users data
	If Not Exists(Select 1 From SiteUsers Where SiteUserID = 1)
		Insert Into SiteUsers(SiteUserID,UserName)
		Values(1, 'CarterMouse')
	If Not Exists(Select 1 From SiteUsers Where SiteUserID = 2)
		Insert Into SiteUsers(SiteUserID,UserName)
		Values(2, 'LucyGoosey')

	--Pools data
	If Not Exists(Select 1 From Pools Where PoolID = 1)
		Insert Into Pools(PoolID,PoolName)
		Values(1, 'Malomutt')

	--Pool Users data
	If Not Exists(Select 1 From PoolUsers Where PoolUserID = 1)
		Insert Into PoolUsers(PoolUserID,SiteUserID,PoolID,UserName) 
		Values(1, 1, 1, 'Carter Mouse Cat')
	If Not Exists(Select 1 From PoolUsers Where PoolUserID = 2)
		Insert Into PoolUsers(PoolUserID,SiteUserID,PoolID,UserName) 
		Values(2, 2, 1, 'Lucy Goosey Katoosi')
		
	--Teams data
	If Not Exists(Select 1 From Teams Where TeamID = 1)
		Insert Into Teams(TeamID,TeamName,City) 
		Values(1, 'Patriots', 'New England')
	If Not Exists(Select 1 From Teams Where TeamID = 2)
		Insert Into Teams(TeamID,TeamName,City)
		Values(2, 'Dolphins', 'Miami')
	If Not Exists(Select 1 From Teams Where TeamID = 3)
		Insert Into Teams(TeamID,TeamName,City)
		Values(3, 'Jets', 'New York')
	If Not Exists(Select 1 From Teams Where TeamID = 4)
		Insert Into Teams(TeamID,TeamName,City)
		Values(4, 'Bills', 'Buffalo')
	If Not Exists(Select 1 From Teams Where TeamID = 5)
		Insert Into Teams(TeamID,TeamName,City)
		Values(5, 'Steelers', 'Pittsburgh')
	If Not Exists(Select 1 From Teams Where TeamID = 6)
		Insert Into Teams(TeamID,TeamName,City)
		Values(6, 'Ravens', 'Baltimore')
	If Not Exists(Select 1 From Teams Where TeamID = 7)
		Insert Into Teams(TeamID,TeamName,City)
		Values(7, 'Bengals', 'Cincinatti')
	If Not Exists(Select 1 From Teams Where TeamID = 8)
		Insert Into Teams(TeamID,TeamName,City)
		Values(8, 'Browns', 'Cleveland')

	--Games data
	--Miami at New England week 1.
	Set Identity_Insert Games On
	If Not Exists(Select 1 From Games Where ID = 1)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(1, 1, 2, 1, '9/12/2014', 1, 2.5, 46)
	--Buffalo at Jets week 1.
	If Not Exists(Select 1 From Games Where ID = 2)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(2, 3, 4, 3, '9/12/2014', 1, 5, 43)
	--Ravens at Steelers week 1.
	If Not Exists(Select 1 From Games Where ID = 3)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(3, 5, 6, 6, '9/12/2014', 1, 1.5, 40.5)
	--Patriots at Bills week 2
	If Not Exists(Select 1 From Games Where ID = 4)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(4, 4, 1, 1, '9/19/2014', 2, 7, 49)
	--Jets at Dolphins week 2
	If Not Exists(Select 1 From Games Where ID = 5)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(5, 2, 3, 2, '9/19/2014', 2, 4.5, 45)
	--Bengals at Browns week 4.
	If Not Exists(Select 1 From Games Where ID = 6)
		Insert Into Games(ID,HomeTeamID,VisitingTeamID,FavoriteTeamID,GameDate,WeekNumber,Line,OverUnder)
		Values(6, 8, 7, 7, '10/12/2014', 4, 7.5, 45)
		--Select * From Bets
	Set Identity_Insert Games Off
	 --Bets data.
	 --Bet placed on Pats/Dolphins by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 1)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(1,1,1,1,2.5,50000)
	 --Bet placed on game Pats/Dolphins by Lucy.
	 If Not Exists(Select 1 From Bets Where BetID = 2)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(2,1,2,2,2.5,25000)
	 --Bet placed on Jets/Bills by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 3)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(3,2,1,4,5,75000)
	 --Bet placed on Steelers/Ravens by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 4)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(4,3,1,6,1.5,75000)
	 --Bet placed on Pats/Bills by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 5)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(5,4,1,1,1.7,100000)
	 --Bet placed on Dolphins/Jets by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 6)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(6,5,1,2,4.5,100000)
	 --Bet placed on Browns/Bengals by Carter.
	 If Not Exists(Select 1 From Bets Where BetID = 7)
	 Insert Into Bets(BetID,GameID,PlacedByID,TeamToCoverBetID,Points,Amount)
	 Values(7,6,1,8,7.5,95000)
