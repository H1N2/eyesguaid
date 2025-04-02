using System.Drawing;
using System.Drawing.Drawing2D;

namespace eyesGuard.Extensions
{
    public static class GraphicsPathExtensions
    {
        public static void AddRoundedRectangle(this GraphicsPath path, Rectangle bounds, int cornerRadius)
        {
            int diameter = cornerRadius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            // 左上角
            path.AddArc(arc, 180, 90);

            // 顶边
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // 右边
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // 底边
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
        }
    }
}