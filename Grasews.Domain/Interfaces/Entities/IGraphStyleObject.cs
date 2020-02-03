namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphStyleObject
    {
        string Selector { get; set; }
        IGraphStyle Style { get; set; }
    }
}