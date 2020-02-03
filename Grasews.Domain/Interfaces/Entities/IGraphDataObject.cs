using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphDataObject
    {
        ICollection<IGraphEdge> Edges { get; set; }
        ICollection<IGraphNode> Nodes { get; set; }
    }
}