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
    public class SongsController : ApiController
    {
        private IRepository<Song> songRepository;

        public SongsController()
        {
            var dbContext = new MusicStoreContext();
            this.songRepository = new DbSongsRepository(dbContext);
        }

        public SongsController(IRepository<Song> repository)
        {
            this.songRepository = repository;
        }

        // GET api/songs
        public IEnumerable<SongModel> GetAll()
        {
            var songEntities = this.songRepository.All().ToList();

            var songModels =
                from songEntity in songEntities
                select SongModel.Convert(songEntity);

            return songModels.ToList();
        }

        // GET api/songs/5
        public SongModel Get(int id)
        {
            var entity = this.songRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = SongModel.Convert(entity);
            return model;
        }

        //// GET api/songs?$filter=SongGenre eq 1
        //public IQueryable<SongModel> GetByGenre(int genre)
        //{
        //    var songs = this.songRepository.All().Where(x => x.SongGenre == (Genre)genre);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$filter=SongTitle eq 'Title'
        //public IQueryable<SongModel> GetByTitle(string title)
        //{
        //    var songs = this.songRepository.All().Where(x => x.SongTitle == title);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$filter=SongYear eq 2000
        //public IQueryable<SongModel> GetByYaer(int year)
        //{
        //    var songs = this.songRepository.All().Where(x => x.SongYear == year);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$orderby=SongGenre
        //public IQueryable<SongModel> GetOrderedByGenre()
        //{
        //    var songs = this.songRepository.All().OrderBy(x => x.SongGenre);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$orderby=SongTitle
        //public IQueryable<SongModel> GetOrderedByTitle()
        //{
        //    var songs = this.songRepository.All().OrderBy(x => x.SongTitle);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$orderby=SongYear
        //public IQueryable<SongModel> GetOrederedByYear()
        //{
        //    var songs = this.songRepository.All().OrderBy(x => x.SongYear);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        //// GET api/songs?$skip=2&$top=2
        //public IQueryable<SongModel> GetRange(int skip, int top)
        //{
        //    var songs = this.songRepository.All().Skip(skip).Take(top);
        //    var songModels =
        //        from songEntity in songs
        //        select SongModel.Convert(songEntity);
        //    return songModels;
        //}

        // POST api/songs
        //[HttpPost]
        public HttpResponseMessage Post(Song model)
        {
            if (model.SongTitle == null || model.ArtistId == 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Song Title and Song Artist (ArtistId could not be 0) could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.songRepository.Add(model);
            var response =
                Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.SongId }));

            return response;
        }

        // PUT api/songs/5
        //[HttpPut]
        public HttpResponseMessage Put(int id, Song model)
        {
            if (model.SongTitle == null || model.ArtistId == 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Song Title and Song Artist (ArtistId could not be 0) could not be null");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.songRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.songRepository.Update(id, model);
            var response =
                Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.SongId }));

            return response;
        }

        // DELETE api/songs/5
        //[HttpDelete]
        public void Delete(int id)
        {
            var entity = this.songRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.songRepository.Delete(entity);
        }
    }
}
