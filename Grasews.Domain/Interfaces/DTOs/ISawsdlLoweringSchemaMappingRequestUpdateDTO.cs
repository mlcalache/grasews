namespace Grasews.Domain.Interfaces.DTOs
{

    public interface ISawsdlLoweringSchemaMappingRequestUpdateDTO
    {
        int IdServiceDescription { get; set; }
        int IdServiceDescriptionElement { get; set; }
        string LoweringSchemaMappingUrl { get; set; }
    }
}