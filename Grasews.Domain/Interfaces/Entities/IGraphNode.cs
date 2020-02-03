namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphNode
    {
        string Classes { get; set; }
        IGraphNodeData Data { get; set; }
        IGraphNodePosition Position { get; set; }
    }
}