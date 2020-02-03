namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphEdge
    {
        string Classes { get; set; }
        IGraphEdgeData Data { get; set; }
    }
}