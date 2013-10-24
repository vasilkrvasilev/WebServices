using School.Repositories;
using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Tests.Controllers
{
    public class FakeStudentRepository : IRepository<Student>
    {
      public  IList<Student> entities = new List<Student>();

        public Student Add(Student entity)
        {
            this.entities.Add(entity);
            return entity;
        }

        public Student Get(int id)
        {
            return this.entities[id];
        }

        public IQueryable<Student> All()
        {
            return this.entities.AsQueryable();
        }

        public Student Update(int id, Student item)
        {
            var itemToUpdate = this.entities[id];
            itemToUpdate.FirstName = item.FirstName;
            itemToUpdate.LastName = item.LastName;
            itemToUpdate.Age = item.Age;
            itemToUpdate.Grade = item.Grade;
            itemToUpdate.Marks = item.Marks;
            itemToUpdate.TownSchoolId = item.TownSchoolId;
            itemToUpdate.TownSchool = item.TownSchool;
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            this.entities.RemoveAt(id);
        }

        public void Delete(Student item)
        {
            this.entities.Remove(item);
        }

        public IQueryable<Student> Find(Expression<Func<Student, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
