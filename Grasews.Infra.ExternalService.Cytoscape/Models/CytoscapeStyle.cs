using Grasews.Domain.Interfaces.Entities;
using Newtonsoft.Json;

namespace Grasews.Infra.ExternalService.Cytoscape.Models
{
    public class CytoscapeStyle : IGraphStyle
    {
        [JsonProperty("background-color", NullValueHandling = NullValueHandling.Ignore)]
        public string Background_Color { get; set; }

        [JsonProperty("background-opacity", NullValueHandling = NullValueHandling.Ignore)]
        public float Background_Opacity { get; set; }

        [JsonProperty("border-color", NullValueHandling = NullValueHandling.Ignore)]
        public string Border_Color { get; set; }

        [JsonProperty("border-opacity", NullValueHandling = NullValueHandling.Ignore)]
        public float Border_Opacity { get; set; }

        [JsonProperty("border-style", NullValueHandling = NullValueHandling.Ignore)]
        public string Border_Style { get; set; }

        [JsonProperty("border-width", NullValueHandling = NullValueHandling.Ignore)]
        public float Border_Width { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("control-point-distances", NullValueHandling = NullValueHandling.Ignore)]
        public string Control_Point_Distances { get; set; }

        [JsonProperty("control-point-weights", NullValueHandling = NullValueHandling.Ignore)]
        public string Control_Point_Weights { get; set; }

        [JsonProperty("curve-style", NullValueHandling = NullValueHandling.Ignore)]
        public string Curve_Style { get; set; }

        [JsonProperty("font-size", NullValueHandling = NullValueHandling.Ignore)]
        public int Font_Size { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public int Height { get; set; }

        [JsonProperty("line-color", NullValueHandling = NullValueHandling.Ignore)]
        public string Line_Color { get; set; }

        [JsonProperty("line-style", NullValueHandling = NullValueHandling.Ignore)]
        public string Line_Style { get; set; }

        [JsonProperty("shape", NullValueHandling = NullValueHandling.Ignore)]
        public string Shape { get; set; }

        [JsonProperty("target-arrow-color", NullValueHandling = NullValueHandling.Ignore)]
        public string Target_Arrow_Color { get; set; }

        [JsonProperty("target-arrow-shape", NullValueHandling = NullValueHandling.Ignore)]
        public string Target_Arrow_Shape { get; set; }

        [JsonProperty("text-halign", NullValueHandling = NullValueHandling.Ignore)]
        public string Text_H_Align { get; set; }

        [JsonProperty("text-margin-y", NullValueHandling = NullValueHandling.Ignore)]
        public int Text_Margin_Y { get; set; }

        [JsonProperty("text-outline-color", NullValueHandling = NullValueHandling.Ignore)]
        public string Text_Outline_Color { get; set; }

        [JsonProperty("text-outline-width", NullValueHandling = NullValueHandling.Ignore)]
        public float Text_Outline_Width { get; set; }

        [JsonProperty("text-valign", NullValueHandling = NullValueHandling.Ignore)]
        public string Text_V_Align { get; set; }

        [JsonProperty("text-wrap", NullValueHandling = NullValueHandling.Ignore)]
        public string Text_Wrap { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public int Width { get; set; }
    }
}