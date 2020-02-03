namespace Grasews.Domain.Interfaces.DTOs
{
    public interface IParseOwlRequestDTO
    {
        string OntologyName { get; set; }
        string Xml { get; set; }
    }
}