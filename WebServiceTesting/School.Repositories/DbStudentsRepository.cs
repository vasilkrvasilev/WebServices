using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class DbStudentsRepository : IRepository<Student>
    {
        private DbContext dbContext;
        private DbSet<Student> entitySet;

        public DbStudentsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Student>();
        }

        public Student Add(Student item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Student Update(int id, Student item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.FirstName = item.FirstName;
            itemToUpdate.LastName = item.LastName;
            itemToUpdate.Age = item.Age;
            itemToUpdate.Grade = item.Grade;
            itemToUpdate.Marks = item.Marks;
            itemToUpdate.TownSchoolId = item.TownSchoolId;
            itemToUpdate.TownSchool = item.TownSchool;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Student item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public Student Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Student> All()
        {
            return this.entitySet;
        }

        public IQueryable<Student> Find(System.Linq.Expressions.Expression<Func<Student, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
