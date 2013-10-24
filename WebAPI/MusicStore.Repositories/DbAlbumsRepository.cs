using MusicStore.Data;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories
{
    public class DbAlbumsRepository : IRepository<Album>
    {
        private DbContext dbContext;
        private DbSet<Album> entitySet;

        public DbAlbumsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Album>();
        }

        public Album Add(Album item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Album Update(int id, Album item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.AlbumTitle = item.AlbumTitle;
            itemToUpdate.AlbumYear = item.AlbumYear;
            itemToUpdate.Producer = item.Producer;
            itemToUpdate.Artists = item.Artists;
            itemToUpdate.Songs = item.Songs;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public void Delete(Album item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public Album Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Album> All()
        {
            return this.entitySet;
        }

        public IQueryable<Album> Find(System.Linq.Expressions.Expression<Func<Album, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
