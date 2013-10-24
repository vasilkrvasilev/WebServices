using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using BlogSystem.Services.Models;
using Newtonsoft.Json;
using System.Net;

namespace BlogSystem.Tests
{
    [TestClass]
    public class UserRegisterIntegrationTests
    {
        [TestMethod]
        public void GetAll_WhenDataInDatabase_ShouldReturnData()
        {
            var httpServer = new InMemoryHttpServer("http://localhost/");

            var response = httpServer.CreateGetRequest("api/threads");

            //Assert
        }

        static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TearDown()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void Register_WhenUserModelValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "AnotherVALIDUSER",
                Nickname = "AnotherVALIDNICK",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<UserLoggedModel>(contentString);
            Assert.AreEqual(testUser.Nickname, model.Nickname);
            Assert.IsNotNull(model.SessionKey);
        }

        [TestMethod]
        public void Register_WhenUsernameNotValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "NoT",
                Nickname = "Ae1.- _",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenNicknameNotValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "Ae10._",
                Nickname = "not---?",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenAuthCodeNotValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "AeAeAe",
                Nickname = "valid--",
                AuthCode = new string('b', 39)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenUserNickNameAreNull_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = null,
                Nickname = null,
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenAuthCodeNotValidSymbols_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "AeAeAe",
                Nickname = "not",
                AuthCode = null
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/users/register", testUser);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
