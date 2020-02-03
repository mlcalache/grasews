namespace Grasews.Domain.Interfaces.DTOs
{
    public interface ISawsdlLiftingSchemaMappingRequestUpdateDTO
    {
        int IdServiceDescription { get; set; }
        int IdServiceDescriptionElement { get; set; }
        string LiftingSchemaMappingUrl { get; set; }
    }
}