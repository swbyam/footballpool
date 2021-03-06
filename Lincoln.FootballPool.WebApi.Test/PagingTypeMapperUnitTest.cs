﻿//-----------------------------------------------------------------------
// <copyright file="PagingTypeMapperUnitTest.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//<auto-generated/>
//-----------------------------------------------------------------------
using System;

using AutoMapper;

using Lincoln.FootballPool.Domain.Entities;
using Lincoln.FootballPool.Domain.Persistence;
using Lincoln.FootballPool.WebApi.AppStart;
using Lincoln.FootballPool.WebApi.Model;
using Lincoln.FootballPool.WebApi.Model.Dtos;
using Lincoln.FootballPool.WebApi.Model.RequestModels;
using Lincoln.FootballPool.WebApi.TypeMappers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lincoln.FootballPool.WebApi.Test
{
    /// <summary>
    /// Summary description for PagingTypeMapperUnitTest
    /// </summary>
    [TestClass]
    public class PagingTypeMapperUnitTest
    {
        public PagingTypeMapperUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        #region Unit Tests

        /// <summary>
        /// Test verifies whether a PagingInfo instance can be obtained or mapped from a PaginatedRequest instance.
        /// </summary>
        [TestMethod]
        public void CanGetPagingInfoFromPaginatedRequestInstance()
        {
            PaginatedRequest paginatedRequest = PagingTypeMapperUnitTest.CreatePaginatedRequestInstance();
            IPagingTypeMapper<BetDto, Bet, int> pagingTypeMapper = new PagingTypeMapper<BetDto, Bet, int>();

            ////Set up AutoMapper mappings.
            AutoMapperConfig.Configure();

            PagingInfo pagingInfo = pagingTypeMapper.GetPagingInfo(paginatedRequest);

            Assert.IsNotNull(pagingInfo);
            Assert.AreEqual<int>(1, pagingInfo.PageNumber);
            Assert.AreEqual<int>(10, pagingInfo.PageSize);
            Assert.AreEqual<string>("UserName", pagingInfo.SortField);
            Assert.AreEqual(SortDirection.Asc, pagingInfo.SortDirection);
        }

        #endregion

        #region Test Helper Methods

        private static PaginatedRequest CreatePaginatedRequestInstance()
        {
            return new PaginatedRequest() { PageNumber = 1, PageSize = 10, SortField = "UserName", SortDirection = "asc" };
        }

        #endregion
    }
}
