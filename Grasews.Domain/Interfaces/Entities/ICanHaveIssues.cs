using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Entities
{

    public interface ICanHaveIssues
    {
        ICollection<Issue> Issues { get; set; }
    }
}