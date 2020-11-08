using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace SSE.UIL
{
    public class Util
    {
        static Bitmap shadowBmp = null;
        public static void PaintDropShadow(object sender, PaintEventArgs e, Form frm, Control control, int i, int l)
        {
            if (shadowBmp == null || shadowBmp.Size != frm.Size)
            {
                shadowBmp?.Dispose();
                shadowBmp = new Bitmap(frm.Width, frm.Height, PixelFormat.Format32bppArgb);
            }
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddRectangle(new Rectangle(control.Location.X, control.Location.Y, control.Size.Width, control.Size.Height));
                DrawShadowSmooth(gp, i, l, shadowBmp);
            }
            e.Graphics.DrawImage(shadowBmp, new Point(0, 0));

        }
        public static void DrawShadowSmooth(GraphicsPath gp, int intensity, int radius, Bitmap dest)
        {
            using (Graphics g = Graphics.FromImage(dest))
            {
                g.Clear(Color.Transparent);
                g.CompositingMode = CompositingMode.SourceCopy;
                double alpha = 0;
                double astep = 0;
                double astepstep = (double)intensity / radius / (radius / 2D);
                for (int thickness = radius; thickness > 0; thickness--)
                {
                    using (Pen p = new Pen(Color.FromArgb((int)alpha, 0, 0, 0), thickness))
                    {
                        p.LineJoin = LineJoin.Round;
                        g.DrawPath(p, gp);
                    }
                    alpha += astep;
                    astep += astepstep;
                }
            }
        }
    }
}
