using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Entities
{
    public interface ICanBeAnnotatedWithModelReference
    {
        ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }
    }
}