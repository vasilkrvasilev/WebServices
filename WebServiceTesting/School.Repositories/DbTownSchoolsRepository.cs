using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class DbTownSchoolsRepository : IRepository<TownSchool>
    {
        private DbContext dbContext;
        private DbSet<TownSchool> entitySet;

        public DbTownSchoolsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<TownSchool>();
        }

        public TownSchool Add(TownSchool item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public TownSchool Update(int id, TownSchool item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Name = item.Name;
            itemToUpdate.Location = item.Location;
            itemToUpdate.Students = item.Students;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public void Delete(TownSchool item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public TownSchool Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<TownSchool> All()
        {
            return this.entitySet;
        }

        public IQueryable<TownSchool> Find(System.Linq.Expressions.Expression<Func<TownSchool, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
