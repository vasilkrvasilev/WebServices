using Chat.Data;
using Chat.Dropbox;
using Chat.Models;
using Chat.PubNub;
using Chat.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class PostsController : ApiController
    {
        private IRepository<Post> postRepository;

        public PostsController()
        {
            var dbContext = new ChatContext();
            this.postRepository = new DbPostsRepository(dbContext);
        }

        public PostsController(IRepository<Post> repository)
        {
            this.postRepository = repository;
        }

        // POST api/artists
        [HttpPost]
        [ActionName("send")]
        public HttpResponseMessage Post(Post model)
        {
            if (model.Date == null || model.UserId == 0 || model.ChatRoomId == 0 || model.Content == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Post shoul have Date, UserId, ChatRoomId, Content");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.postRepository.Add(model);
            PubNubConsole.Publish(string.Format("New message from {0} in {1} chat room",
                entity.UserId, entity.ChatRoomId));
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        // POST api/artists
        [HttpPost]
        [ActionName("send-file")]
        public HttpResponseMessage PostFile([FromBody]Post model)
        {
            if (model.Date == null || model.UserId == 0 || model.ChatRoomId == 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Post shoul have Date, UserId, ChatRoomId");
                throw new HttpResponseException(errResponse);
            }

            if (model.Content == null || !File.Exists(model.Content))
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "File path is not valid");
                throw new HttpResponseException(errResponse);
            }

            string url = DropboxAPI.GetUrl(model.Content);
            model.Content = url;
            var entity = this.postRepository.Add(model);
            PubNubConsole.Publish(string.Format("New message from {0} in {1} chat room",
                entity.UserId, entity.ChatRoomId));
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        // PUT api/artists/5
        [HttpPut]
        [ActionName("correct")]
        public HttpResponseMessage Put(int id, Post model)
        {
            if (model.Content == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Post Content could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.postRepository.Get(id);

            entity = this.postRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        // DELETE api/artists/5
        [HttpDelete]
        [ActionName("delete")]
        public void Delete(Post model)
        {
            this.postRepository.Delete(model);
        }
    }
}
