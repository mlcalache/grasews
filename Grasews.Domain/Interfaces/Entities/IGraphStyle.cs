namespace Grasews.Domain.Interfaces.Entities
{
    public interface IGraphStyle
    {
        string Background_Color { get; set; }

        float Background_Opacity { get; set; }

        string Border_Color { get; set; }

        float Border_Opacity { get; set; }

        string Border_Style { get; set; }

        float Border_Width { get; set; }

        string Color { get; set; }

        string Content { get; set; }

        string Control_Point_Distances { get; set; }

        string Control_Point_Weights { get; set; }

        string Curve_Style { get; set; }

        int Font_Size { get; set; }

        int Height { get; set; }

        string Line_Color { get; set; }

        string Line_Style { get; set; }

        string Shape { get; set; }

        string Target_Arrow_Color { get; set; }

        string Target_Arrow_Shape { get; set; }

        string Text_H_Align { get; set; }

        int Text_Margin_Y { get; set; }

        string Text_Outline_Color { get; set; }

        float Text_Outline_Width { get; set; }

        string Text_V_Align { get; set; }

        string Text_Wrap { get; set; }

        int Width { get; set; }
    }
}