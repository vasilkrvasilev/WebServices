using MusicStore.Data;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories
{
    public class DbArtistsRepository : IRepository<Artist>
    {
        private DbContext dbContext;
        private DbSet<Artist> entitySet;

        public DbArtistsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Artist>();
        }

        public Artist Add(Artist item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Artist Update(int id, Artist item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Country = item.Country;
            itemToUpdate.Name = item.Name;
            itemToUpdate.DateOfBirth = item.DateOfBirth;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public void Delete(Artist item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public Artist Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Artist> All()
        {
            return this.entitySet;
        }

        public IQueryable<Artist> Find(Expression<Func<Artist, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
