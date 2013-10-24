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
    public class DbSongsRepository : IRepository<Song>
    {
        private DbContext dbContext;
        private DbSet<Song> entitySet;

        public DbSongsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Song>();
        }

        public Song Add(Song item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Song Update(int id, Song item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.SongTitle = item.SongTitle;
            itemToUpdate.SongYear = item.SongYear;
            itemToUpdate.SongGenre = item.SongGenre;
            itemToUpdate.Description = item.Description;
            itemToUpdate.ArtistId = item.ArtistId;
            itemToUpdate.SongArtist = item.SongArtist;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public void Delete(Song item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public Song Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Song> All()
        {
            return this.entitySet;
        }

        public IQueryable<Song> Find(System.Linq.Expressions.Expression<Func<Song, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
