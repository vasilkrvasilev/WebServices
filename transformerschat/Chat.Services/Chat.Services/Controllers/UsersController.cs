using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Models;
using Chat.Repositories;
using Chat.Data;
using Chat.Services.Models;

namespace Chat.Services.Controllers
{
    public class UsersController : ApiController
    {
        private readonly string picture = 
            "https://www.dropbox.com/lightbox/home/Apps/ChatFileStorage/Sent_Files_635120994288012520";
        private IRepository<User> userRepository;

        public UsersController()
        {
            var dbContext = new ChatContext();
            this.userRepository = new DbUsersRepository(dbContext);
        }

        public UsersController(IRepository<User> repository)
        {
            this.userRepository = repository;
        }

        [HttpPut]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserModel model)
        {
            if (model.Username == null && model.Password == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "User Username and Password could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.userRepository.All().
                Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no such element"));
                throw new HttpResponseException(errResponse);
            }

            entity.IsOnline = true;
            entity = this.userRepository.Update(entity.Id, entity);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(User model)
        {
            model.IsOnline = false;
            var entity = this.userRepository.Update(model.Id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = model.Id }));

            return response;
        }

        [HttpPut]
        [ActionName("update")]
        public HttpResponseMessage UpdateUser(int id, User model)
        {
            if (model.Username == null && model.Password == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "User Username and Password could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.userRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.userRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.Id }));

            return response;
        }

        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserModel model)
        {
            if (model.Username == null && model.Password == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "User Username and Password could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.userRepository.All().
                Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (entity != null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is already such element"));
                throw new HttpResponseException(errResponse);
            }

            User user = new User { Username = model.Username, Password = model.Password, 
                Picture = picture, IsOnline = true };
            var addedUser = this.userRepository.Add(user);

            var response =
                Request.CreateResponse(HttpStatusCode.OK, addedUser);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = addedUser.Id }));

            return response;
        }

        [HttpGet]
        [ActionName("get")]
        public UserFullModel Get(int id)
        {
            var entity = this.userRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no such element"));
                throw new HttpResponseException(errResponse);
            }

            var model = UserFullModel.Convert(entity);
            return model;
        }

        [HttpGet]
        [ActionName("get-posts")]
        public ICollection<Post> GetPosts(User user)
        {
            var entity = this.userRepository.Get(user.Id).Posts;
            return entity;
        }
    }
}
