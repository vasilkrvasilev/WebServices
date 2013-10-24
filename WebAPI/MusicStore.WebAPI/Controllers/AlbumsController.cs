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
    public class AlbumsController : ApiController
    {
        private IRepository<Album> albumRepository;

        public AlbumsController()
        {
            var dbContext = new MusicStoreContext();
            this.albumRepository = new DbAlbumsRepository(dbContext);
        }

        public AlbumsController(IRepository<Album> repository)
        {
            this.albumRepository = repository;
        }

        // GET api/albums
        public IEnumerable<AlbumModel> Get()
        {
            var albumEntities = this.albumRepository.All().ToList();

            var albumModels =
                from albumEntity in albumEntities
                select AlbumModel.Convert(albumEntity);

            return albumModels.ToList();
        }

        // GET api/albums/5
        public AlbumFullModel Get(int id)
        {
            var entity = this.albumRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = AlbumFullModel.Convert(entity);
            return model;
        }

        //// GET api/albums?$filter=AlbumTitle eq 'Title'
        //public IQueryable<AlbumModel> GetByTitle(string title)
        //{
        //    var albums = this.albumRepository.All().Where(x => x.AlbumTitle == title);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$filter=Producer eq 'Producer'
        //public IQueryable<AlbumModel> GetByProducer(string producer)
        //{
        //    var albums = this.albumRepository.All().Where(x => x.Producer == producer);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$filter=AlbumYear eq 2000
        //public IQueryable<AlbumModel> GetByYear(int year)
        //{
        //    var albums = this.albumRepository.All().Where(x => x.AlbumYear == year);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$orderby=AlbumTitle
        //public IQueryable<AlbumModel> GetOrderedByTitle()
        //{
        //    var albums = this.albumRepository.All().OrderBy(x => x.AlbumTitle);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$orderby=Producer
        //public IQueryable<AlbumModel> GetOrderedByProducer()
        //{
        //    var albums = this.albumRepository.All().OrderBy(x => x.Producer);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$orderby=AlbumYear
        //public IQueryable<AlbumModel> GetOrderedByYear()
        //{
        //    var albums = this.albumRepository.All().OrderBy(x => x.AlbumYear);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        //// GET api/albums?$skip=2&$top=2
        //public IQueryable<AlbumModel> GetRange(int skip, int top)
        //{
        //    var albums = this.albumRepository.All().Skip(skip).Take(top);
        //    var albumModels =
        //        from albumEntity in albums
        //        select AlbumModel.Convert(albumEntity);
        //    return albumModels;
        //}

        // POST api/albums
        //[HttpPost]
        public HttpResponseMessage Post(Album model)
        {
            if (model.AlbumTitle == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Album Title could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.albumRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.AlbumId }));

            return response;
        }

        // PUT api/albums/5
        //[HttpPut]
        public HttpResponseMessage Put(int id, Album model)
        {
            if (model.AlbumTitle == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Album Title could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.albumRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.albumRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.AlbumId }));

            return response;
        }

        // DELETE api/albums/5
        //[HttpDelete]
        public void Delete(int id)
        {
            var entity = this.albumRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.albumRepository.Delete(entity);
        }
    }
}
