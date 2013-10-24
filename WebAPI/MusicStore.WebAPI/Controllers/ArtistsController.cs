using MusicStore.Data;
using MusicStore.Models;
using MusicStore.Repositories;
using MusicStore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicStore.WebAPI.Controllers
{
    public class ArtistsController : ApiController
    {
        private IRepository<Artist> artistRepository;

        public ArtistsController()
        {
            var dbContext = new MusicStoreContext();
            this.artistRepository = new DbArtistsRepository(dbContext);
        }

        public ArtistsController(IRepository<Artist> repository)
        {
            this.artistRepository = repository;
        }

        // GET api/artists
        //[HttpGet]
        public IEnumerable<ArtistModel> GetAll()
        {
            var artistEntities = this.artistRepository.All().ToList();

            var artistModels =
                from artistEntity in artistEntities
                select ArtistModel.Convert(artistEntity);

            return artistModels.ToList();
        }

        // GET api/artists/5
        //[HttpGet]
        public ArtistModel Get(int id)
        {
            var entity = this.artistRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = ArtistModel.Convert(entity);
            return model;
        }

        //// GET api/artists?$filter=Name eq 'Name'
        //public IQueryable<ArtistModel> GetByName(string name)
        //{
        //    var artists = this.artistRepository.All().Where(x => x.Name == name);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$filter=Country eq 'Country'
        //public IQueryable<ArtistModel> GetByCountry(string country)
        //{
        //    var artists = this.artistRepository.All().Where(x => x.Country == country);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$filter=DateOfBirth.Year eq 2000
        //public IQueryable<ArtistModel> GetByYear(int year)
        //{
        //    var artists = this.artistRepository.All().Where(x => x.DateOfBirth.Year == year);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$orderby=Name
        //public IQueryable<ArtistModel> GetOrderedByName()
        //{
        //    var artists = this.artistRepository.All().OrderBy(x => x.Name);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$orderby=Country
        //public IQueryable<ArtistModel> GetOrderedByCountry()
        //{
        //    var artists = this.artistRepository.All().OrderBy(x => x.Country);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$orderby=DateOfBirth
        //public IQueryable<ArtistModel> GetOrderedByDateOfBirth()
        //{
        //    var artists = this.artistRepository.All().OrderBy(x => x.DateOfBirth);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        //// GET api/artists?$skip=2&$top=2
        //public IQueryable<ArtistModel> GetRange(int skip, int top)
        //{
        //    var artists = this.artistRepository.All().Skip(skip).Take(top);
        //    var artistModels =
        //        from artistEntity in artists
        //        select ArtistModel.Convert(artistEntity);
        //    return artistModels;
        //}

        // POST api/artists
        //[HttpPost]
        public HttpResponseMessage Post(Artist model)
        {
            if (model.Name == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Artist Name could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.artistRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.ArtistId }));

            return response;
        }

        // PUT api/artists/5
        //[HttpPut]
        public HttpResponseMessage Put(int id, Artist model)
        {
            if (model.Name == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Artist Name could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.artistRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.artistRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.ArtistId }));

            return response;
        }

        // DELETE api/artists/5
        //[HttpDelete]
        public void Delete(int id)
        {
            var entity = this.artistRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.artistRepository.Delete(entity);
        }
    }
}
