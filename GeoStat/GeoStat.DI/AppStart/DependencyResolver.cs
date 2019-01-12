using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Autofac.Integration.WebApi;

namespace GeoStat.DI.AppStart
{
    public class DependencyResolver : IDependencyResolver
    {
        private AutofacWebApiDependencyResolver _resolver;

        public DependencyResolver(AutofacWebApiDependencyResolver dependencyResolver)
        {
            _resolver = dependencyResolver;
        }

        public IDependencyScope BeginScope()
        {
            return _resolver.BeginScope();
        }

        public void Dispose()
        {
            _resolver.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var a = _resolver.GetService(serviceType);
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {

            try
            {
                var a = _resolver.GetServices(serviceType);
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
