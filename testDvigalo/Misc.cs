using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace testDvigalo
{
    class Misc
    {
        public static void PositionControl(Control c, int top, int left)
        {
            c.Top = top;
            c.Left = left;            
        }

        public class Mouse
        {
            public Point Location = new Point();   
        }

        public static Color ColorToLighterConverter(Color c)
        {
            Color color = ChangeColorBrightness(c, 0.2f);
            return color;
            
        }

        public static Point LooseReference(Point p)
        {
            return new Point(p.X, p.Y);
        }

        static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static int CalculatePxFromTime(TimeSpan ts)
        {
            int buff = 0;
            if (ts.Ticks > 0)
            {
                buff = (int)Math.Round(((decimal)ts.TotalSeconds * TimeLine.ratioPxPerMin / 60), 0);
                if (buff == 0)
                {
                    buff = 1; // minimum width
                }
                return buff;
            }
            return 0;
            
        }
    }

    
}
