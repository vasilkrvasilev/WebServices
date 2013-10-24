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
    public class TownSchoolsController : ApiController
    {
        private IRepository<TownSchool> townSchoolRepository;

        public TownSchoolsController()
        {
            var dbContext = new SchoolContext();
            this.townSchoolRepository = new DbTownSchoolsRepository(dbContext);
        }

        public TownSchoolsController(IRepository<TownSchool> repository)
        {
            this.townSchoolRepository = repository;
        }

        // GET api/artists
        //[HttpGet]
        public IEnumerable<TownSchoolModel> GetAll()
        {
            var townSchoolEntities = this.townSchoolRepository.All().ToList();

            var townSchoolModels =
                from townSchoolEntity in townSchoolEntities
                select TownSchoolModel.Convert(townSchoolEntity);

            return townSchoolModels.ToList();
        }

        // GET api/artists/5
        //[HttpGet]
        public TownSchoolFullModel Get(int id)
        {
            var entity = this.townSchoolRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = TownSchoolFullModel.Convert(entity);
            return model;
        }

        // POST api/artists
        //[HttpPost]
        public HttpResponseMessage Post(TownSchool model)
        {
            if (model.Name == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "TownSchool Name could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.townSchoolRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.TownSchoolId }));

            return response;
        }

        // PUT api/artists/5
        //[HttpPut]
        public HttpResponseMessage Put(int id, TownSchool model)
        {
            if (model.Name == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "TownSchool Name could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.townSchoolRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.townSchoolRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.TownSchoolId }));

            return response;
        }

        // DELETE api/artists/5
        //[HttpDelete]
        public void Delete(int id)
        {
            var entity = this.townSchoolRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.townSchoolRepository.Delete(entity);
        }
    }
}
