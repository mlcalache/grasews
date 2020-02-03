namespace Grasews.Domain.Interfaces.DTOs
{
    public interface IParseWsdlRequestDTO
    {
        string ServiceName { get; set; }
        string Xml { get; set; }
    }
}