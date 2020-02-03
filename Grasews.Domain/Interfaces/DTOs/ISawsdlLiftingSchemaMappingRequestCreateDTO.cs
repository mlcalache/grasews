namespace Grasews.Domain.Interfaces.DTOs
{
    public interface ISawsdlLiftingSchemaMappingRequestCreateDTO
    {
        int IdServiceDescription { get; set; }
        int IdServiceDescriptionElement { get; set; }
        string LiftingSchemaMappingUrl { get; set; }
    }
}