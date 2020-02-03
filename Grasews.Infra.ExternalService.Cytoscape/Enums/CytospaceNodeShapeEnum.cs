using System.ComponentModel;

namespace Grasews.Infra.ExternalService.Cytoscape.Enums
{
    public enum CytospaceNodeShapeEnum
    {
        [Description("ellipse")]
        Ellipse,

        [Description("triangle")]
        Triangle,

        [Description("rectangle")]
        Rectangle,

        [Description("roundrectangle")]
        RoundRectangle,

        [Description("bottomroundrectangle")]
        BottomRoundRectangle,

        [Description("cutrectangle")]
        CutRectangle,

        [Description("barrel")]
        Barrel,

        [Description("rhomboid")]
        Rhomboid,

        [Description("diamond")]
        Diamond,

        [Description("pentagon")]
        Pentagon,

        [Description("hexagon")]
        Hexagon,

        [Description("concavehexagon")]
        ConcaveHexagon,

        [Description("heptagon")]
        Heptagon,

        [Description("octagon")]
        Octagon,

        [Description("star")]
        Star,

        [Description("tag")]
        Tag,

        [Description("vee")]
        Vee
    }
}