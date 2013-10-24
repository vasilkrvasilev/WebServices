using Chat.Data;
using Chat.Repositories;
using Chat.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Chat.Services.DependencyResolvers
{
    public class DbDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var context = new ChatContext();
            if (serviceType == typeof(PostsController))
            {
                var repository = new DbPostsRepository(context);
                return new PostsController(repository);
            }
            else if (serviceType == typeof(UsersController))
            {
                var repository = new DbUsersRepository(context);
                return new UsersController(repository);
            }
            else if (serviceType == typeof(ChatRoomsController))
            {
                var repository = new DbChatRoomsRepository(context);
                return new ChatRoomsController(repository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
        }
    }
}