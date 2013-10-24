using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class DbMarksReposiotry : IRepository<Mark>
    {
        private DbContext dbContext;
        private DbSet<Mark> entitySet;

        public DbMarksReposiotry(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Mark>();
        }

        public Mark Add(Mark item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Mark Update(int id, Mark item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Subject = item.Subject;
            itemToUpdate.Value = item.Value;
            itemToUpdate.StudentId = item.StudentId;
            itemToUpdate.Student = item.Student;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Mark item)
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

        public Mark Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Mark> All()
        {
            return this.entitySet;
        }

        public IQueryable<Mark> Find(System.Linq.Expressions.Expression<Func<Mark, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
