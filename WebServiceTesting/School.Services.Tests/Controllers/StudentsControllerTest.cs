using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.Services.Controllers;
using System.Collections.Generic;
using School.Models;
using System.Linq;
using Newtonsoft.Json;

namespace School.Services.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTest
    {
        [TestMethod]
        public void GetAllStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };

            repository.entities.Add(student);
            var controller = new StudentsController(repository);
            var studentsModels = controller.GetAll();

            Assert.IsTrue(studentsModels.Count() == 1);
            Assert.AreEqual(student.FirstName, studentsModels.First().FirstName);
            Assert.AreEqual(student.LastName, studentsModels.First().LastName);
            Assert.AreEqual(student.Age, studentsModels.First().Age);
            Assert.AreEqual(student.Grade, studentsModels.First().Grade);
        }

        [TestMethod]
        public void GetAllSeveralStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            var thirdStudent = new Student { FirstName = "Todor", LastName = "Todorov", Age = 7, Grade = 1, TownSchool = school };

            repository.entities.Add(firstStudent);
            repository.entities.Add(secondStudent);
            repository.entities.Add(thirdStudent);
            var controller = new StudentsController(repository);
            var studentsModels = controller.GetAll();

            Assert.IsTrue(studentsModels.Count() == 3);
        }

        [TestMethod]
        public void GetByIdStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();

            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            var thirdStudent = new Student { FirstName = "Todor", LastName = "Todorov", Age = 7, Grade = 1, TownSchool = school };

            repository.entities.Add(firstStudent);
            repository.entities.Add(secondStudent);
            repository.entities.Add(thirdStudent);
            var controller = new StudentsController(repository);
            var studentModel = controller.Get(1);

            Assert.AreEqual(secondStudent.FirstName, studentModel.FirstName);
            Assert.AreEqual(secondStudent.LastName, studentModel.LastName);
            Assert.AreEqual(secondStudent.Age, studentModel.Age);
            Assert.AreEqual(secondStudent.Grade, studentModel.Grade);
        }

        [TestMethod]
        public void PostStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var student = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };

            var controller = new StudentsController(repository);
            var studentModel = controller.Post(student).Content.ReadAsStringAsync().Result;
            Student studentDeserialized = JsonConvert.DeserializeObject<Student>(studentModel);

            Assert.IsTrue(controller.GetAll().Count() == 1);
            Assert.IsNotNull(studentDeserialized);
            Assert.AreEqual(student.FirstName, studentDeserialized.FirstName);
            Assert.AreEqual(student.LastName, studentDeserialized.LastName);
            Assert.AreEqual(student.Age, studentDeserialized.Age);
            Assert.AreEqual(student.Grade, studentDeserialized.Grade);
        }

        [TestMethod]
        public void PutStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            var thirdStudent = new Student { FirstName = "Todor", LastName = "Todorov", Age = 7, Grade = 1, TownSchool = school };

            var controller = new StudentsController(repository);
            var firstStudentModel = controller.Post(firstStudent);
            var secondStudentModel = controller.Post(secondStudent);
            var updatedStudentModel = controller.Put(2, thirdStudent).Content.ReadAsStringAsync().Result;
            Student studentDeserialized = JsonConvert.DeserializeObject<Student>(updatedStudentModel);

            Assert.IsTrue(controller.GetAll().Count() == 2);
            Assert.IsNotNull(studentDeserialized);
            Assert.AreEqual(thirdStudent.FirstName, studentDeserialized.FirstName);
            Assert.AreEqual(thirdStudent.LastName, studentDeserialized.LastName);
            Assert.AreEqual(thirdStudent.Age, studentDeserialized.Age);
            Assert.AreEqual(thirdStudent.Grade, studentDeserialized.Grade);
        }

        [TestMethod]
        public void DeleteStudentsControllerTest()
        {
            var repository = new FakeStudentRepository();
            var school = new TownSchool { Name = "School", Location = "Town" };
            var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
            var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
            var thirdStudent = new Student { FirstName = "Todor", LastName = "Todorov", Age = 7, Grade = 1, TownSchool = school };

            var controller = new StudentsController(repository);
            var firstStudentModel = controller.Post(firstStudent);
            var secondStudentModel = controller.Post(secondStudent);
            var thirdStudentModel = controller.Post(thirdStudent);
            controller.Delete(3);

            Assert.IsTrue(controller.GetAll().Count() == 2);
            Assert.IsTrue(controller.GetAll().Count(x => x.FirstName == thirdStudent.FirstName) == 0);
        }
    }
}
