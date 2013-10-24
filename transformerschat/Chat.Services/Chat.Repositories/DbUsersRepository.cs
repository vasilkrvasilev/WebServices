using Chat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public class DbUsersRepository : IRepository<User>
    {
        private DbContext dbContext;
        private DbSet<User> entitySet;

        public DbUsersRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<User>();
        }

        public User Add(User item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public User Update(int id, User item)
        {
            var itemToUpdate = this.entitySet.Find(id);
            itemToUpdate.Username = item.Username;
            itemToUpdate.Password = item.Password;
            itemToUpdate.Picture = item.Picture;
            itemToUpdate.IsOnline = item.IsOnline;
            itemToUpdate.Posts = item.Posts;
            itemToUpdate.ChatRooms = item.ChatRooms;
            this.dbContext.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(User item)
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

        public User Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<User> All()
        {
            return this.entitySet;
        }

        public IQueryable<User> Find(System.Linq.Expressions.Expression<Func<User, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
