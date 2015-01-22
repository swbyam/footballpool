//-----------------------------------------------------------------------
// <copyright file="GameControllerUnitTest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using Autofac;

using Lincoln.FootballPool.Domain.Entities;
using Lincoln.FootballPool.Domain.Persistence.Repositories;
using Lincoln.FootballPool.WebApi.AppStart;
using Lincoln.FootballPool.WebApi.Model.Dtos;
using Lincoln.FootballPool.WebApi.Model.RequestModels;
using Lincoln.TestUtilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lincoln.FootballPool.WebApi.Test
{
    /// <summary>
    /// Class contains integration tests written against the <see cref="Lincoln.FootballPool.WebApi.Controllers.GamesController"/> class.
    /// </summary>
    [TestClass]
    public class GameControllerIntegrationTest
    {
        #region Properties

        public TestContext TestContext { get; set; }

        #endregion

        [TestInitialize()]
        public void AttachTestDatabase()
        {
           // DatabaseUtility.AttachUnitTestDatabase();
        }

        [TestCleanup()]
        public void DropTestDatabase()
        {
            ////TODO: Add code to drop database.

        }

        /// <summary>
        /// Test verifies that a <see cref="Lincoln.FootballPool.WebApi.Model.Dtos.GameDto"/> instance can be retrieved by sending an HTTP request message to an Web API web service.
        /// </summary>
        /// <remarks>Since this is an integration test, the GameDto instance is returned from the actual persistence store.</remarks>
        [TestMethod]
        public void CanGetGameByIdFromDb()
        {
            ////Set up HTTP configuration for test.
            HttpConfiguration httpConfig = GameControllerIntegrationTest.SetUpIntegrationConfiguration();

            ////Create HTTP request.
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost/api/games/1"));
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ////Send HTTP request message, and block current thread until result is returned.
            HttpResponseMessage responseMessage = GameControllerIntegrationTest.SendHttpRequestMessage(httpRequestMessage, httpConfig).Result;
            GameDto gameDto = responseMessage.Content.ReadAsAsync<GameDto>().Result;

            Assert.IsNotNull(gameDto);
            Assert.AreEqual<int>(1, gameDto.Id);
            Assert.AreEqual<string>("New England", gameDto.HomeTeamCity);
            Assert.AreEqual<string>("Patriots", gameDto.HomeTeamName);
            Assert.AreEqual<int>(1, gameDto.HomeTeamId);
            Assert.AreEqual<string>("Miami", gameDto.VisitingTeamCity);
            Assert.AreEqual<string>("Dolphins", gameDto.VisitingTeamName);
            Assert.AreEqual<int>(2, gameDto.VisitingTeamId);
            Assert.AreEqual<string>("New England", gameDto.FavoriteTeamCity);
            Assert.AreEqual<string>("Patriots", gameDto.FavoriteTeamName);
            Assert.AreEqual<int>(1, gameDto.FavoriteTeamId);
            Assert.AreEqual<DateTime>(new DateTime(2014, 9, 12), gameDto.StartDateTime);
            Assert.AreEqual<int>(1, gameDto.WeekNumber);
            Assert.AreEqual<float>(2.5f, gameDto.Line);
            Assert.AreEqual<float>(46f, gameDto.OverUnder);
        }

        /// <summary>
        /// Test verifies that a new game is created and saved to the persistence store via a call to a Post action method on the GamesController class.  The test also checks that a <see cref="Lincoln.FootballPool.WebApi.Model.Dtos.GameDto"/> instance is returned from the method.
        /// </summary>
        [TestMethod]
        public void CanCreateNewGameInDb()
        {
            ////Set up HTTP configuration for test.
            HttpConfiguration httpConfig = GameControllerIntegrationTest.SetUpIntegrationConfiguration();

            ////Create HTTP request.
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("http://localhost:1166/api/games"));
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            ////Create game request model to send to Web API method.
            GameBaseRequestModel requestModel = new GameBaseRequestModel()
            {
                HomeTeamId = 2,
                VisitingTeamId = 1,
                FavoriteTeamId = 1,
                StartDateTime = new DateTime(2014, 12, 13),
                WeekNumber = 13,
                Line = 3.5f,
                OverUnder = 38.5f
            };

            ////Add game request model to body of HTTP request message.
            httpRequestMessage.Content = new ObjectContent<GameBaseRequestModel>(requestModel, new JsonMediaTypeFormatter());

            ////Send HTTP request message, and block current thread until result is returned.
            HttpResponseMessage responseMessage = GameControllerIntegrationTest.SendHttpRequestMessage(httpRequestMessage, httpConfig).Result;
            GameDto gameDto = responseMessage.Content.ReadAsAsync<GameDto>().Result;

            Assert.IsNotNull(gameDto);

            ////Verify game was created in the database.
            bool doesGameExistInPersistenceStore = DatabaseUtility.DoesGameExist(gameDto.Id, "FootballPoolDb");

            Assert.IsTrue(doesGameExistInPersistenceStore);
        }
        
        /// <summary>
        /// Test verifies that a game cannot be created in the persistence store because the team that is favored to win the game is not a part of the actual game (<see cref="Lincoln.FootballPool.WebApi.Model.RequestModels.GameBaseRequestModel"/> that is submitted to the web service.
        /// </summary>
        [TestMethod]
        public void CannotCreateNewGameInDbFavoredTeamNotInGame()
        {
            ////Set up HTTP configuration for test.
            HttpConfiguration httpConfig = GameControllerIntegrationTest.SetUpIntegrationConfiguration();

            ////Create HTTP request.
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("http://localhost:1166/api/games"));
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ////Create game request model to send to Web API method.
            GameBaseRequestModel requestModel = new GameBaseRequestModel()
            {
                HomeTeamId = 2,
                VisitingTeamId = 1,
                ////favorite team not part of game
                FavoriteTeamId = 3,
                StartDateTime = new DateTime(2014, 12, 13),
                WeekNumber = 13,
                Line = 3.5f,
                OverUnder = 38.5f
            };

            ////Add game request model to body of HTTP request message.
            httpRequestMessage.Content = new ObjectContent<GameBaseRequestModel>(requestModel, new JsonMediaTypeFormatter());

            ////Send HTTP request message, and block current thread until result is returned.
            HttpResponseMessage responseMessage = GameControllerIntegrationTest.SendHttpRequestMessage(httpRequestMessage, httpConfig).Result;

            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);

            ////Get http error from the web service.
            HttpError httpError = responseMessage.Content.ReadAsAsync<HttpError>().Result;

            this.TestContext.WriteLine(httpError.Message);
        }

        #region Helper Methods

        /// <summary>
        /// Sends the supplied HTTP request message <paramref name="httpRequestMessage"/> according to information contained in the HTTP configuration <paramref name="httpConfig"/> instance.
        /// </summary>
        /// <param name="httpRequestMessage">HTTP request message to send.</param>
        /// <param name="httpConfig">HTTP configuration instance containing information needed to send message.</param>
        /// <returns>Task representing execution of method with a HTTP response message return type.</returns>
        private static async Task<HttpResponseMessage> SendHttpRequestMessage(HttpRequestMessage httpRequestMessage, HttpConfiguration httpConfig)
        {
            HttpResponseMessage httpResponseMessage = null;

            using (HttpServer httpServer = new HttpServer(httpConfig))
            {
                using (HttpClient httpClient = new HttpClient(httpServer))
                {
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            return httpResponseMessage;
        }

        /// <summary>
        /// Sets up configuration for Web API methods that are to be integration tested.
        /// </summary>
        private static HttpConfiguration SetUpIntegrationConfiguration()
        {
            ////Create HttpConfiguration and register it with WebApiConfig.
            HttpConfiguration httpConfig = new HttpConfiguration();
            WebApiConfig.Register(httpConfig);
            AutofacIocConfig.Configure(httpConfig);

            return httpConfig;
        }

        #endregion
    }
}
