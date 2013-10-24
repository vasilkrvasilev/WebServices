using Chat.Data;
using Chat.Models;
using Chat.Repositories;
using Chat.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class ChatRoomsController : ApiController
    {
        private IRepository<ChatRoom> chatRoomRepository;

        public ChatRoomsController()
        {
            var dbContext = new ChatContext();
            this.chatRoomRepository = new DbChatRoomsRepository(dbContext);
        }

        public ChatRoomsController(IRepository<ChatRoom> repository)
        {
            this.chatRoomRepository = repository;
        }

        [HttpGet]
        [ActionName("get-posts")]
        public ICollection<Post> GetPosts(ChatRoom chatRoom)
        {
            var entity = this.chatRoomRepository.Get(chatRoom.Id).Posts;
            return entity;
        }

        [HttpGet]
        [ActionName("get-users")]
        public ICollection<User> GetUsers(ChatRoom chatRoom)
        {
            var entity = this.chatRoomRepository.Get(chatRoom.Id).Users;
            return entity;
        }

        // GET api/artists
        [HttpGet]
        [ActionName("get-chatrooms")]
        public IEnumerable<ChatRoomModel> GetAll()
        {
            var chatRoomEntities = this.chatRoomRepository.All().ToList();

            var chatRoomModels =
                from chatRoomEntity in chatRoomEntities
                select ChatRoomModel.Convert(chatRoomEntity);

            return chatRoomModels.ToList();
        }

        // GET api/artists/5
        [HttpGet]
        [ActionName("get")]
        public ChatRoomFullModel Get(int id)
        {
            var entity = this.chatRoomRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no such element"));
                throw new HttpResponseException(errResponse);
            }

            var model = ChatRoomFullModel.Convert(entity);
            return model;
        }

        // POST api/artists
        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage Post(ChatRoom model)
        {
            if (model.Name == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "ChatRoom Name could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.chatRoomRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        [HttpPut]
        [ActionName("subscribe")]
        public HttpResponseMessage SubscribeUser(int id, User model)
        {
            var entity = this.chatRoomRepository.Get(id);

            entity.Users.Add(model);
            entity = this.chatRoomRepository.Update(id, entity);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        [HttpPut]
        [ActionName("unsubscribe")]
        public HttpResponseMessage UnsubscribeUser(int id, User model)
        {
            var entity = this.chatRoomRepository.Get(id);

            entity.Users.Remove(model);
            entity = this.chatRoomRepository.Update(id, entity);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        // DELETE api/artists/5
        [HttpDelete]
        [ActionName("delete")]
        public void Delete(int id)
        {
            var entity = this.chatRoomRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no such element"));
                throw new HttpResponseException(errResponse);
            }

            this.chatRoomRepository.Delete(entity);
        }
    }
}
