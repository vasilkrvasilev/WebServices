using School.Data;
using School.Repositories;
using School.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace School.Services.DependencyResolvers
{
    public class DbDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var context = new SchoolContext();
            if (serviceType == typeof(MarksController))
            {
                var repository = new DbMarksReposiotry(context);
                return new MarksController(repository);
            }
            else if (serviceType == typeof(StudentsController))
            {
                var repository = new DbStudentsRepository(context);
                return new StudentsController(repository);
            }
            else if (serviceType == typeof(TownSchoolsController))
            {
                var repository = new DbTownSchoolsRepository(context);
                return new TownSchoolsController(repository);
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