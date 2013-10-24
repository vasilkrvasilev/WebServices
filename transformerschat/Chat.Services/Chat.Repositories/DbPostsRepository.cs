using Chat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public class DbPostsRepository : IRepository<Post>
    {
        private DbContext dbContext;
        private DbSet<Post> entitySet;

        public DbPostsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<Post>();
        }

        public Post Add(Post item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public Post Update(int id, Post item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Date = item.Date;
            itemToUpdate.Content = item.Content;
            itemToUpdate.IsFile = item.IsFile;
            itemToUpdate.UserId = item.UserId;
            itemToUpdate.User = item.User;
            itemToUpdate.ChatRoomId = item.ChatRoomId;
            itemToUpdate.ChatRoom = item.ChatRoom;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Post item)
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

        public Post Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<Post> All()
        {
            return this.entitySet;
        }

        public IQueryable<Post> Find(System.Linq.Expressions.Expression<Func<Post, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
