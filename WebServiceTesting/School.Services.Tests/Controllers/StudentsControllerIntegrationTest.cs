using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.Models;
using System.Net;
using System.Transactions;

namespace School.Services.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerIntegrationTest
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
        public void GetAllStudentsTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            repository.entities.Add(student);
            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreateGetRequest("api/students");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetStudentTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            repository.entities.Add(firstStudent);
            repository.entities.Add(secondStudent);
            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreateGetRequest("api/students/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetStudentInvalidTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            repository.entities.Add(firstStudent);
            repository.entities.Add(secondStudent);
            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreateGetRequest("api/students/3");

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetStudentBySubjectMarkTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            firstStudent.Marks.Add(new Mark { Subject = "Math", Value = 5, Student = firstStudent });
            secondStudent.Marks.Add(new Mark { Subject = "History", Value = 4, Student = secondStudent });
            secondStudent.Marks.Add(new Mark { Subject = "IT", Value = 6, Student = secondStudent });
            repository.entities.Add(firstStudent);
            repository.entities.Add(secondStudent);
            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreateGetRequest("api/students?subject=Math&value=5");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void PostStudentTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };

            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreatePostRequest("api/students", student);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void PostStudentInvalidNameTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = null, LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };

            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreatePostRequest("api/students", student);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostStudentInvalidTownSchoolIdTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = null };

            var server = new InMemoryHttpServer("http://localhost/");

            var response = server.CreatePostRequest("api/students", student);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
