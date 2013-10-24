using MusicStore.Data;
using MusicStore.Repositories;
using MusicStore.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace MusicStore.WebAPI.DependencyResolvers
{
    public class DbDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(ArtistsController))
            {
                var repository = new DbArtistsRepository(new MusicStoreContext());
                return new ArtistsController(repository);
            }
            else if (serviceType == typeof(SongsController))
            {
                var repository = new DbSongsRepository(new MusicStoreContext());
                return new SongsController(repository);
            }
            else if (serviceType == typeof(AlbumsController))
            {
                var repository = new DbAlbumsRepository(new MusicStoreContext());
                return new AlbumsController(repository);
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