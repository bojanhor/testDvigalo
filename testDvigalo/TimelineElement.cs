using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Diagnostics;

namespace testDvigalo
{

    class TimelineEment : Control
    {
        Form form;
        public readonly string ID;
        public Rectangle Outline;
        public Color color = Color.FromArgb(80, 135, 240);
        public Color hovercolor;
        public Pen OutlinePen;
        public Point OriginalLocation;


        public TimelineEment(Form form)
        {
            this.form = form;
            OutlinePen = new Pen(color);
            hovercolor = Misc.ColorToLighterConverter(color);

            ID = Guid.NewGuid().ToString("N");
            BackColor = color;
            Width = 100;
            Height = 40;


            MouseEnter += MyQline_MouseEnter;
            MouseLeave += MyQline_MouseLeave;
            MouseDown += MyQline_MouseDown;
            MouseUp += MyQline_MouseUp;
            MouseMove += MyQline_MouseMove;

        }


        public bool mouseHover = false;
        public bool MouseDown_ = false;
        public bool Moving = false;
        public bool Snapped = false;

        private void MyQline_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseHover)
            {
                if (MouseDown_)
                {
                    Location = offsetLocation(e.Location); // moving aroud with cursor
                    Moving = true;
                    Snapped = false;
                }

                if (Moving == false)
                {
                    if (!Location.Equals(OriginalLocation))
                    {
                        if (!Snapped)
                        {
                            Location = OriginalLocation; // If it doesnt reach destination snap back to wwhere it came from
                        }
                        
                    }
                }
            }
        }

        void searchSnapPosition()
        {
            Point p = new Point(Location.X + Width/2, Location.Y + Height/2); // center

            if (TimeLine.Rectangle.Contains(p))
            {
                snapToPosition();
            }
        }

        void snapToPosition()
        {
            if (Left < TimeLine.Rectangle.X)
            {
                Left = TimeLine.Rectangle.X;
            }
            else if (Left+Width > TimeLine.Rectangle.Left + TimeLine.Rectangle.Width)
            {
                Left = TimeLine.Rectangle.Left + TimeLine.Rectangle.Width - Width;
            }
           

            Location = new Point(Left, TimeLine.Rectangle.Y + ((TimeLine.Rectangle.Height- Height)/2));
            Outline = new Rectangle(Left, Top, Width, Height);
            Snapped = true;
        }

        private void MyQline_MouseUp(object sender, MouseEventArgs e)
        {
            Outline = new Rectangle(Left, Top, Width, Height);
            MouseDown_ = false;
            Moving = false;
            searchSnapPosition();
            form.Invalidate();
        }

        private void MyQline_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown_ = true;
        }

        private void MyQline_MouseLeave(object sender, EventArgs e)
        {
            Outline = new Rectangle(Left, Top, Width, Height);
            mouseHover = false;
            form.Invalidate();
        }

        private void MyQline_MouseEnter(object sender, EventArgs e)
        {
            Outline = new Rectangle(Left, Top, Width, Height);
            mouseHover = true;
            form.Invalidate();
        }


        public void AddToForm(Form form, int top, int left)
        {
            if (Parent == null)
            {
                var id = ID;
                OriginalLocation = new Point(left, top);
                form.Controls.Add(this);
                Misc.PositionControl(this, top, left);
            }

        }


        Point offsetLocation(Point p)
        {
            return new Point(Location.X + p.X - Width / 2, Location.Y + p.Y - Height / 2);

        }
    }

}
