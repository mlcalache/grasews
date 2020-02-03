using Grasews.Domain.Enums;

namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphNodeData
    {
        string Id { get; set; }
        int IdOntology { get; set; }
        int IdOntologyTerm { get; set; }
        int IdWsdlElement { get; set; }
        string Label { get; set; }
        string Name { get; set; }
        string NodeType { get; set; }
        GraphNodeTypeEnum? NodeTypeEnum { get; set; }
        string Parent { get; set; }
        string TermUri { get; set; }
    }
}