using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeEdgeData : IGraphEdgeData
    {
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
    }
}