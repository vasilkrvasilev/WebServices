using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using BlogSystem.Services.Models;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace BlogSystem.Tests
{
    [TestClass]
    public class GetPostsByTagsIntegrationTests
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
        public void GetByTags_WhenPostModelValid_ShouldSaveToDatabase()
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

            var testPostOne = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = new string[] { "valid" }
            };

            var testPostTwo = new PostNewModel()
            {
                Title = "Next Post",
                Text = "NextText",
                Tags = new string[] { "other" }
            };

            var testPostThree = new PostNewModel()
            {
                Title = "Third Post",
                Text = "Some",
                Tags = new string[] { "some" }
            };

            httpServer.CreatePostRequest("api/posts", testPostOne,
                "application/json", userModel.SessionKey);
            httpServer.CreatePostRequest("api/posts", testPostTwo,
                "application/json", userModel.SessionKey);
            httpServer.CreatePostRequest("api/posts", testPostThree,
                "application/json", userModel.SessionKey);

            var response = httpServer.CreateGetRequest("api/posts?tags=some,post",
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<List<PostModel>>(contentString);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(testPostThree.Title, model[0].Title);
        }

        [TestMethod]
        public void GetByTags_WhenPostModelValidMany_ShouldSaveToDatabase()
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

            var testPostOne = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = new string[] { "other" }
            };

            var testPostTwo = new PostNewModel()
            {
                Title = "Next Post",
                Text = "NextText",
                Tags = new string[] { "other" }
            };

            var testPostThree = new PostNewModel()
            {
                Title = "Third Post",
                Text = "Some",
                Tags = new string[] { "other" }
            };

            httpServer.CreatePostRequest("api/posts", testPostOne,
                "application/json", userModel.SessionKey);
            httpServer.CreatePostRequest("api/posts", testPostTwo,
                "application/json", userModel.SessionKey);
            httpServer.CreatePostRequest("api/posts", testPostThree,
                "application/json", userModel.SessionKey);

            var response = httpServer.CreateGetRequest("api/posts?tags=other",
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<List<PostModel>>(contentString);
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public void GetByTags_WhenPostModelValidZero_ShouldSaveToDatabase()
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

            var testPostOne = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = new string[] { "post" }
            };

            httpServer.CreatePostRequest("api/posts", testPostOne,
                "application/json", userModel.SessionKey);

            var response = httpServer.CreateGetRequest("api/posts?tags=other",
                "application/json", userModel.SessionKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<List<PostModel>>(contentString);
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void GetByTags_WhenSessionKeyIsInvalid_ShouldSaveToDatabase()
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

            var testPostOne = new PostNewModel()
            {
                Title = "Post",
                Text = "ValidText",
                Tags = new string[] { "post" }
            };

            httpServer.CreatePostRequest("api/posts", testPostOne,
                "application/json", userModel.SessionKey);

            var response = httpServer.CreateGetRequest("api/posts?tags=post",
                "application/json", new string('-', 50));

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
