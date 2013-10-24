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
    public class StudentsController : ApiController
    {
        private IRepository<Student> studentRepository;

        public StudentsController()
        {
            var dbContext = new SchoolContext();
            this.studentRepository = new DbStudentsRepository(dbContext);
        }

        public StudentsController(IRepository<Student> repository)
        {
            this.studentRepository = repository;
        }

        // GET api/artists
        [HttpGet]
        public IEnumerable<StudentModel> GetAll()
        {
            var studentEntities = this.studentRepository.All().ToList();

            var studentModels =
                from studentEntity in studentEntities
                select StudentModel.Convert(studentEntity);

            return studentModels.ToList();
        }

        // GET api/artists/5
        [HttpGet]
        public StudentFullModel Get(int id)
        {
            var entity = this.studentRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            var model = StudentFullModel.Convert(entity);
            return model;
        }

        // GET api/albums?$filter=AlbumTitle eq 'Title'
        [HttpGet]
        public ICollection<StudentModel> GetBySubjectAndMark(string subject, int value)
        {
            var studentEntities = this.studentRepository.All().SelectMany(s => s.Marks).
                Where(m => m.Subject == subject && m.Value >= value).Select(x => x.Student).ToList();
            var studentModels = new HashSet<StudentModel>();
            foreach (var item in studentEntities)
            {
                studentModels.Add(StudentModel.Convert(item));
            }

            return studentModels;
        }

        // POST api/artists
        [HttpPost]
        public HttpResponseMessage Post(Student model)
        {
            if (model.FirstName == null || model.LastName == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Student FirstName and LastName could not be null");
                throw new HttpResponseException(errResponse);
            }

            if (model.TownSchoolId < 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Student TownSchoolId should be positive numbers");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.studentRepository.Add(model);
            var response =
                this.Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.StudentId }));

            return response;
        }

        // PUT api/artists/5
        [HttpPut]
        public HttpResponseMessage Put(int id, Student model)
        {
            if (model.FirstName == null || model.LastName == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Student FirstName and LastName could not be null");
                throw new HttpResponseException(errResponse);
            }

            if (model.TownSchoolId <= 0)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Student TownSchoolId should be positive numbers");
                throw new HttpResponseException(errResponse);
            }

            var entity = this.studentRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            entity = this.studentRepository.Update(id, model);
            var response =
                this.Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.StudentId }));

            return response;
        }

        // DELETE api/artists/5
        [HttpDelete]
        public void Delete(int id)
        {
            var entity = this.studentRepository.Get(id);

            if (entity == null)
            {
                var errResponse = this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Format("There is no element with id {0}", id));
                throw new HttpResponseException(errResponse);
            }

            this.studentRepository.Delete(entity);
        }
    }
}
