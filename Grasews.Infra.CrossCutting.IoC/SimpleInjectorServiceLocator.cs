using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Infra.CrossCutting.IoC
{
    public class SimpleInjectorServiceLocator : IServiceLocator
    {
        private readonly Container container;

        public SimpleInjectorServiceLocator(Container container)
        {
            this.container = container;
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return container.GetAllInstances(typeof(TService)).Cast<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            IServiceProvider serviceProvider = container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var collection = (IEnumerable<object>)serviceProvider.GetService(collectionType);
            return collection ?? Enumerable.Empty<object>();
        }

        public TService GetInstance<TService>(string key)
        {
            return (TService)GetInstance(typeof(TService));
        }

        public TService GetInstance<TService>()
        {
            return (TService)container.GetInstance(typeof(TService));
        }

        public object GetInstance(Type serviceType, string key)
        {
            return GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        public object GetService(Type serviceType)
        {
            IServiceProvider serviceProvider = container;
            return serviceProvider.GetService(serviceType);
        }
    }
}