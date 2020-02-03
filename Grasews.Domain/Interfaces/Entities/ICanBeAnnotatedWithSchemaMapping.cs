namespace Grasews.Domain.Interfaces.Entities
{
    public interface ICanBeAnnotatedWithSchemaMapping
    {
        string LiftingSchemaMapping { get; set; }

        string LoweringSchemaMapping { get; set; }
    }
}