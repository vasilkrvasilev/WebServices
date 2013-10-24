using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogSystem.Services.Models;
using Newtonsoft.Json;
using System.Net;
using System.Transactions;

namespace BlogSystem.Tests
{
    [TestClass]
    public class CreatePostIntegrationTests
    {
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
        public void Create_WhenPostModelValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "NewVALIDUSER",
                Nickname = "NewVALIDNICK",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var userResponse = httpServer.CreatePostRequest("api/users/register", testUser);
            var userContentString = userResponse.Content.ReadAsStringAsync().Result;
            var userModel = JsonConvert.DeserializeObject<UserLoggedModel>(userContentString);

            var testPost = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = new string[] { "valid" }
            };

            var response = httpServer.CreatePostRequest("api/posts", testPost, 
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<PostCreatedModel>(contentString);
            Assert.AreEqual(testPost.Title, model.Title);
            Assert.IsTrue(model.Id != 0);
        }

        [TestMethod]
        public void Create_WhenSessionKeyNotValid_ShouldSaveToDatabase()
        {
            var testPost = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = { }
            };

            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/posts", testPost,
                "application/json", new string('a', 50));

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Create_WhenPostTitleNull_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "NewVALIDUSER",
                Nickname = "NewVALIDNICK",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var userResponse = httpServer.CreatePostRequest("api/users/register", testUser);
            var userContentString = userResponse.Content.ReadAsStringAsync().Result;
            var userModel = JsonConvert.DeserializeObject<UserLoggedModel>(userContentString);

            var testPost = new PostNewModel()
            {
                Title = null,
                Text = "ValidText",
                Tags = { }
            };

            var response = httpServer.CreatePostRequest("api/posts", testPost,
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Create_WhenPostTextNull_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "NewVALIDUSER",
                Nickname = "NewVALIDNICK",
                AuthCode = new string('b', 40)
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var userResponse = httpServer.CreatePostRequest("api/users/register", testUser);
            var userContentString = userResponse.Content.ReadAsStringAsync().Result;
            var userModel = JsonConvert.DeserializeObject<UserLoggedModel>(userContentString);

            var testPost = new PostNewModel()
            {
                Title = "Title",
                Text = null,
                Tags = { }
            };

            var response = httpServer.CreatePostRequest("api/posts", testPost,
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
