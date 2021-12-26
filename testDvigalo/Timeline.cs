using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Diagnostics;

namespace testDvigalo
{
    class TimeLine
    {
        public static decimal ratioPxPerMin = 0; //  px per minute
        Form form;
        Label lblTimeNow = new Label(), lblTimeMax = new Label();
        
        public int Top { get; private set; }
        public int Left { get; private set; }

        private int width;
        public int Width
        {
            get { return width; }
            private set { width = value; resize(value);  }
        }

        public int Height { get; private set; }

        public static Rectangle Rectangle { get; private set; }

        SolidBrush solidBrush = new SolidBrush(Color.DarkGray);
        static TimeManager TimeManager;

       
        public TimeSpan MyTimelineDuration
        {
            get 
            {
                return TimeSpan.FromMinutes((double)(Width / ratioPxPerMin));
            }
            set 
            {                
                Width = (int)Math.Round(((decimal)value.TotalMinutes * ratioPxPerMin),0);
            }
        }


        public TimeLine(Form form, TimeManager t)
        {
            this.form = form;
            TimeManager = t;

            Top = 50;
            Left = 40;

            Height = 50;
        }

        void resize(int width)
        {            
            Rectangle = new Rectangle(Left,Top, Width, Height);
        }


        public void Draw(PaintEventArgs e)
        {
            Width = form.Width - Left * 2 - 20; // has to be recalculated after resize            
            Misc.PositionControl(lblTimeMax, 10, Left + Width - 40);

            e.Graphics.FillRectangle(solidBrush, Rectangle);
            ratioPxPerMin = Width / Prog.timespan_m;

            lblTimeNow.Text = TimeManager.GetCurrentTimeString();
            lblTimeMax.Text = TimeManager.GetMaxTimeString();
        }
      

        public void AddTimelineLables()
        {

            Misc.PositionControl(lblTimeNow, 10, 10);
            form.Controls.Add(lblTimeNow);

            form.Controls.Add(lblTimeMax);
        }

        public static DateTime GetDateTimeFromPx(Point point)
        {
           return GetDateTimeFromPx(point.X);
        }

        public static DateTime GetDateTimeFromPx(int Xpoint)
        {
            var span = TimeSpan.FromMinutes((double) (Xpoint / ratioPxPerMin));
            return TimeManager.currentTime.Add(span);
        }
    }
        

}
