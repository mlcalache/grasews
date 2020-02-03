namespace Grasews.Domain.Interfaces.DTOs
{
    public interface ISawsdlModelReferenceRequestCreateDTO
    {
        int IdOntologyTerm { get; set; }
        int IdOwnerUser { get; set; }
        int IdServiceDescription { get; set; }
        int IdServiceDescriptionElement { get; set; }
    }
}