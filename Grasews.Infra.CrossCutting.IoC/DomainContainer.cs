using Grasews.Domain.Interfaces.Containers;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Infra.CrossCutting.IoC
{
    public class DomainContainer : IDomainContainer
    {
        private readonly Container _container;

        public DomainContainer(Container container)
        {
            _container = container;
        }

        public TService GetInstance<TService>()
        {
            return (TService)_container.GetInstance(typeof(TService));
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances(typeof(TService)).Cast<TService>();
        }
    }
}