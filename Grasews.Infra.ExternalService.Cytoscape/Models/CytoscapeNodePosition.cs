using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeNodePosition : IGraphNodePosition
    {
        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        public float X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public float Y { get; set; }
    }
}