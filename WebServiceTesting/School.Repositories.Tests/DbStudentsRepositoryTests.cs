using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using School.Models;
using System.Transactions;
using System.Linq;
using School.Data;

namespace School.Repositories.Tests
{
    [TestClass]
    public class DbStudentsRepositoryTests
    {
        public DbContext dbContext { get; set; }

        public IRepository<Student> studentsRepository { get; set; }

        private static TransactionScope tranScope;

        public DbStudentsRepositoryTests()
        {
            this.dbContext = new SchoolContext();
            this.studentsRepository = new DbStudentsRepository(this.dbContext);
        }

        [TestInitialize]
        public void TestInit()
        {
            tranScope = new TransactionScope();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            tranScope.Dispose();
        }

        [TestMethod]
        public void AddStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();
            Assert.IsTrue(student.StudentId > 0);
        }

        [TestMethod]
        public void AddSeveralStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var studentA = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            var studentB = new Student { FirstName = "Tom", LastName = "Tom", TownSchool = school };
            var studentC = new Student { FirstName = "Ben", LastName = "Ben", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(studentA);
            dbContext.Set<Student>().Add(studentB);
            dbContext.Set<Student>().Add(studentC);
            dbContext.SaveChanges();
            Assert.IsTrue(studentA.StudentId > 0);
            Assert.IsTrue(studentB.StudentId > 0);
            Assert.IsTrue(studentC.StudentId > 0);
        }

        [TestMethod]
        public void AddStudentInDatabaseTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.SaveChanges();

            var createdStudent = this.studentsRepository.Add(student);
            var foundStudent = this.dbContext.Set<Student>().Find(createdStudent.StudentId);
            Assert.IsNotNull(foundStudent);
            Assert.AreEqual("Sam", foundStudent.FirstName);
            Assert.AreEqual("Chen", foundStudent.LastName);
            Assert.AreEqual(school.TownSchoolId, foundStudent.TownSchoolId);
        }

        [TestMethod]
        public void UpdateStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();

            var studentId = student.StudentId;
            var schoolId = student.TownSchoolId;
            student.FirstName = "Tom";
            var updatedStudent = this.studentsRepository.Update(studentId, student);
            Assert.IsNotNull(updatedStudent);
            Assert.AreEqual(studentId, updatedStudent.StudentId);
            Assert.AreEqual("Tom", updatedStudent.FirstName);
            Assert.AreEqual("Chen", updatedStudent.LastName);
            Assert.AreEqual(schoolId, updatedStudent.TownSchoolId);

        }

        [TestMethod]
        public void UpdateFullStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", Age = 9, Grade = 4, TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();

            student.Age = 15;
            var updatedStudent = this.studentsRepository.Update(student.StudentId, student);
            Assert.IsNotNull(updatedStudent);
            Assert.AreEqual(15, updatedStudent.Age);
            Assert.AreEqual(4, updatedStudent.Grade);
        }

        [TestMethod]
        public void UpdeteStudentMarksTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", Age = 9, Grade = 4, TownSchool = school };
            var mark = new Mark { Subject = "Math", Value = 5, Student = student };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.Set<Mark>().Add(mark);
            dbContext.SaveChanges();

            var studentId = student.StudentId;
            var schoolId = student.TownSchoolId;
            student.Marks.First().Subject = "IT";
            var updatedStudent = this.studentsRepository.Update(studentId, student);
            Assert.IsNotNull(updatedStudent);
            Assert.AreEqual(1, updatedStudent.Marks.Count);
            Assert.AreEqual("IT", updatedStudent.Marks.First().Subject);
            Assert.AreEqual(5, updatedStudent.Marks.First().Value);
        }

        [TestMethod]
        public void DeleteStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();

            var count = dbContext.Set<Student>().Count();
            this.studentsRepository.Delete(student);
            var foundStudent = this.dbContext.Set<Student>().Find(student.StudentId);
            Assert.IsNull(foundStudent);
            Assert.IsTrue(dbContext.Set<Student>().Count() == count - 1);

        }

        [TestMethod]
        public void DeleteStudentIdTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();

            var count = dbContext.Set<Student>().Count();
            this.studentsRepository.Delete(student.StudentId);
            var foundStudent = this.dbContext.Set<Student>().Find(student.StudentId);
            Assert.IsNull(foundStudent);
            Assert.IsTrue(dbContext.Set<Student>().Count() == count - 1);

        }

        [TestMethod]
        public void GetStudentTest()
        {
            var school = new TownSchool { Name = "School" };
            var student = new Student { FirstName = "Sam", LastName = "Chen", Age = 9, Grade = 4, TownSchool = school };
            dbContext.Set<TownSchool>().Add(school);
            dbContext.Set<Student>().Add(student);
            dbContext.SaveChanges();

            var studentId = student.StudentId;
            var gettedStudent = this.studentsRepository.Get(studentId);
            Assert.IsNotNull(gettedStudent);
            Assert.AreEqual(student.StudentId, gettedStudent.StudentId);
            Assert.AreEqual(9, gettedStudent.Age);
            Assert.AreEqual(4, gettedStudent.Grade);
            Assert.AreEqual("Sam", gettedStudent.FirstName);
            Assert.AreEqual("Chen", gettedStudent.LastName);
            Assert.AreEqual(student.TownSchoolId, gettedStudent.TownSchoolId);
        }
    }
}
