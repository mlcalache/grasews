using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeStyleObject : IGraphStyleObject
    {
        #region Ctors

        //regular ctor
        public CytoscapeStyleObject()
        {
        }

        //ctor for json converter to work with interface properties
        [JsonConstructor]
        public CytoscapeStyleObject(CytoscapeStyle style)
        {
            Style = style;
        }

        #endregion Ctors

        #region Public props

        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public string Selector { get; set; }

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public IGraphStyle Style { get; set; }

        #endregion Public props
    }
}