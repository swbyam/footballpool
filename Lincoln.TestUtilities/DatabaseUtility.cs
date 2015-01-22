//-----------------------------------------------------------------------
// <copyright file="DatabaseUtility.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.TestUtilities
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Lincoln.FootballPool.Domain.Entities;

    public sealed class DatabaseUtility
    {
        #region Member Variables

        private static string unitTestDatabaseName = "UnitTestDb";

        #endregion

        #region Public Methods

        public static void AttachUnitTestDatabase()
        {
            ////TODO: Get connection string from app.config of unit test project?
            DatabaseDeploymentService dbDeploymentService = new DatabaseDeploymentService("Server=(localdb)\\Projects;initial catalog=FootballPool.SqlServer2012;Integrated Security=True;Application Name=Football Pool application");

            ////Export template database as bacpac.
            dbDeploymentService.ExportBacPac("FootballPool.SqlServer2012", "c:\\temp\\test.bacpac");

            ////Import exported bacpac to local db.
            dbDeploymentService.ImportBacPac(DatabaseUtility.unitTestDatabaseName, "c:\\temp\\test.bacpac");
        }

         public void DropUnitTestDatabase()
        {
            ////TODO: Implement method.  Need privileged account to perform operation.
        }

        /// <summary>
        /// Determines whether or not a game with the supplied <paramref name="gameId"/> exists in the database.
        /// </summary>
        /// <param name="gameId">ID of the game to check for in the database.</param>
        /// <returns>True if the game exists in the database; otherwise false.</returns>
         public static bool DoesGameExist(int gameId, string connectionStringName)
         {
             DbConnection dbConnection = SqlClientFactory.Instance.CreateConnection();
             dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
             DbCommand dbCommand = SqlClientFactory.Instance.CreateCommand();
             dbCommand.Connection = dbConnection;
             dbCommand.CommandText = "Select Count(*) From Games Where ID = @gameId";
             dbCommand.Parameters.Add(new SqlParameter("gameId", gameId));

             int numberRecords;

             try
             {
                 dbConnection.Open();
                 numberRecords = (int)dbCommand.ExecuteScalar();

                 return numberRecords == 1;
             }
             finally
             {
                 if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
                 {
                     dbConnection.Close();
                     dbConnection.Dispose();
                 }

                 if (dbCommand != null)
                 {
                     dbCommand.Dispose();
                 }
             }
         }

        #endregion
    }
}
