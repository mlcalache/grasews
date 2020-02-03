using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Containers
{
    public interface IDomainContainer
    {
        TService GetInstance<TService>();

        IEnumerable<TService> GetAllInstances<TService>();
    }
}