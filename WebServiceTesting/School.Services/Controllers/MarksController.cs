using School.Data;
using School.Models;
using School.Repositories;
using School.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace School.Services.Controllers
{
    public class MarksController : ApiController
    {
        private IRepository<Mark> markRepository;

        public MarksController()
        {
            var dbContext = new SchoolContext();
            this.markRepository = new DbMarksReposiotry(dbContext);
        }

        public MarksController(IRepository<Mark> repository)
        {
            this.markRepository = repository;
        }

        // GET api/artists
        //[HttpGet]
        public IEnumerable<MarkModel> GetAll()
        {
            var markEntities = this.markRepository.All().ToList();

            var markModels =
                from markEntity in markEntities
                select MarkModel.Convert(markEntity);

            return markModels.ToList();
        }

        // GET api/artists/5
        //[HttpGet]
        public MarkFullModel Get(int id)
        {
            var entity = this.markRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = MarkFullModel.Convert(entity);
            return model;
        }

        // POST api/artists
        //[HttpPost]
        public HttpResponseMessage Post(Mark model)
        {
            if (model.Subject == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Mark Subject could not be null");
                throw new HttpResponseException(errResponse);
            }

            if (model.Value <= 0 || model.StudentId <= 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Mark Value and StudentId should be positive numbers");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.markRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.MarkId }));

            return response;
        }

        // PUT api/artists/5
        //[HttpPut]
        public HttpResponseMessage Put(int id, Mark model)
        {
            if (model.Subject == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Mark Subject could not be null");
                throw new HttpResponseException(errResponse);
            }

            if (model.Value <= 0 || model.StudentId <= 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Mark Value and StudentId should be positive numbers");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.markRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.markRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.MarkId }));

            return response;
        }

        // DELETE api/artists/5
        //[HttpDelete]
        public void Delete(int id)
        {
            var entity = this.markRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.markRepository.Delete(entity);
        }
    }
}
