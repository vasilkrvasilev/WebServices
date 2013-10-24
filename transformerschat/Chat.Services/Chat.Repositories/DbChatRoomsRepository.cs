using Chat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public class DbChatRoomsRepository : IRepository<ChatRoom>
    {
        private DbContext dbContext;
        private DbSet<ChatRoom> entitySet;

        public DbChatRoomsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<ChatRoom>();
        }

        public ChatRoom Add(ChatRoom item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public ChatRoom Update(int id, ChatRoom item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Name = item.Name;
            itemToUpdate.Posts = item.Posts;
            itemToUpdate.Users = item.Users;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var itemToDelete = this.entitySet.Find(id);
            this.entitySet.Remove(itemToDelete);
            this.dbContext.SaveChanges();
        }

        public void Delete(ChatRoom item)
        {
            this.entitySet.Remove(item);
            this.dbContext.SaveChanges();
        }

        public ChatRoom Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<ChatRoom> All()
        {
            return this.entitySet;
        }

        public IQueryable<ChatRoom> Find(System.Linq.Expressions.Expression<Func<ChatRoom, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
