using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace testDvigalo
{
    class SubJob : Control
    {
        Form form;
        public bool hasNext { get; private set; } = false;
        public bool hasPrevious { get; private set; } = false;

        private Color backColor;

        new public Color BackColor
        {
            get { return backColor; }
            set
            {
                if (value != backColor)
                {
                    base.BackColor = backColor;
                    backColor = value;
                    SubJob_BackColorChanged();
                }
            }
        }


        // ----------------------------

        private SubJob nextSubJob;

        public SubJob NextSubJob
        {
            get { return nextSubJob; }
            set
            {
                if (value != null)
                {
                    hasNext = true;
                    nextSubJob = value;
                }
                else
                {
                    hasNext = false;
                    nextSubJob = null;
                }

            }
        }
        // ----------------------------

        private SubJob previousSubJob;

        public SubJob PreviousSubJob
        {
            get { return previousSubJob; }
            set
            {
                if (value != null)
                {
                    hasPrevious = true;
                    previousSubJob = value;
                }
                else
                {
                    hasPrevious = false;
                    previousSubJob = null;
                }

            }
        }
        // ----------------------------

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                SetDurationFixedStartTime(value);
            }
        }
        // ----------------------------

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                if (notzero(startTime))
                {
                    if (notzero(duration))
                    {
                        endTime = startTime.AddSeconds(duration.Seconds);
                    }
                }
            }
        }
        // ----------------------------

        private DateTime endTime;

        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                if (notzero(endTime))
                {
                    if (notzero(duration))
                    {
                        startTime = endTime.AddSeconds(-duration.Seconds);
                    }
                }
            }
        }
        // ----------------------------

        private TimeSpan gapToNext;

        public TimeSpan GapToNext
        {
            get { return gapToNext; }
            set { gapToNext = value; }
        }
        // ----------------------------

        bool notzero(TimeSpan t)
        {
            if (t.Ticks == 0)
            {
                return false;
            }
            return true;
        }

        bool notzero(DateTime t)
        {
            if (t.Ticks == 0)
            {
                return false;
            }
            return true;
        }

       
        public TimeSpan DurationWithGap
        {
            get
            {
                return duration + gapToNext;
            }

        }


        public SubJob(Form form)
        {
            BackColor = Color.LightBlue;
            Paint += SubJob_Paint;
            MouseEnter += SubJob_MouseEnter;
            MouseLeave += SubJob_MouseLeave;
            MouseDown += SubJob_MouseDown;
            MouseUp += SubJob_MouseUp;
            MouseMove += SubJob_MouseMove;

            this.form = form;
            pen = new Pen(brush);
            pen.Width = 5;

        }

        private void SubJob_MouseMove(object sender, MouseEventArgs e)
        {
            if (DragStarted)
            {
                var currentX = e.Location.X + Left;
                var currentY = e.Location.Y + Top;

                var newX = currentX - Width / 2;
                var newY = currentY - Height / 2;

                Location = new Point(newX, newY);

                                

            }
        }

        private void SubJob_MouseUp(object sender, MouseEventArgs e)
        {
            DragStarted = false;

            if (!hasPrevious)
            {
                if (canBeSnapped())
                {

                    Snap();
                }
                else
                {
                    Location = OriginalPosition;
                }
            }
           
           
        }

       
        
        public bool DragStarted { get; private set; } = false;
        Point OriginalPosition;
        bool canSnap = false;        

        private void SubJob_MouseDown(object sender, MouseEventArgs e)
        {
            if (DisplayRectangle.Contains(e.Location))
            {
                if (!hasPrevious) // only first one can be dragged
                {
                    DragStarted = true;
                    OriginalPosition = Location;
                }
                
            }
        }

        private void SubJob_BackColorChanged()
        {
            NormalColor = BackColor;
            MouseOverColor = Misc.ColorToLighterConverter(NormalColor);
            brush = new SolidBrush(BackColor);
            pen = new Pen(brush);
            if (form != null)
            {
                form.Invalidate();
            }

        }


        // Graphics
        public bool mouseover { get; private set; } = false;
        Brush brush = new SolidBrush(Color.LightBlue);
        Pen pen;

        Color NormalColor;
        Color MouseOverColor;
        //
        private void SubJob_MouseLeave(object sender, EventArgs e)
        {
            mouseover = false;
            base.BackColor = NormalColor;

            form.Invalidate();
        }

        private void SubJob_MouseEnter(object sender, EventArgs e)
        {
            mouseover = true;
            base.BackColor = MouseOverColor;

            form.Invalidate();
        }

        private void SubJob_Paint(object sender, PaintEventArgs e)
        {
            // Must be
        }

        public void SetDurationFixedStartTime(TimeSpan duration)
        {
            this.duration = duration;

            if (notzero(duration))
            {
                endTime = startTime.AddSeconds(duration.Seconds);
                Width = Misc.CalculatePxFromTime(duration);
            }

        }

        public void SetDurationFixedEndTime(TimeSpan duration)
        {
            this.duration = duration;
            if (notzero(endTime))
            {
                if (notzero(duration))
                {
                    startTime = endTime.AddSeconds(-duration.Seconds);
                    Width = Misc.CalculatePxFromTime(duration);
                }
            }
        }

        public void PaintSelf(Form form, PaintEventArgs e, int x, int y, int height)
        {
            if (this.Parent == null)
            {
                Misc.PositionControl(this, y, x);
                form.Controls.Add(this);
                base.BackColor = NormalColor;
            }

            Height = height;
            

            if (mouseover)
            {
                if (!DragStarted)
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(Left, Top, Width, height));
                }
                
            }

        }

        bool canBeSnapped()
        {
            var Myline = Prog.MyGraphics.Myline;
            Rectangle M = new Rectangle(Myline.Left, Myline.Top, Myline.Width, Myline.Height);

            var tolerance = 15;

            if (Left > M.X - tolerance)
            {
                if (Right <M.Right + tolerance)
                {
                    if (Top > M.Top - tolerance)
                    {
                        if (Bottom < M.Bottom + tolerance) 
                        {
                            canSnap = true;
                            return true;
                        }
                    }
                }
            }
            canSnap = false;
            return false;

        }

        void Snap()
        {
            var Myline = Prog.MyGraphics.Myline;
            Rectangle M = new Rectangle(Myline.Left, Myline.Top, Myline.Width, Myline.Height);


        
            if (canSnap)
            {
                Location = new Point(Left, M.Y + (M.Height - Height) / 2);

                if (Left < M.Left)
                {
                    Left = M.Left;
                }

                if (Top < M.Top)
                {
                    Top = M.Top + (M.Height - Height / 2);
                }

                if (Right > M.Right)
                {
                    Left = M.Right - Width;
                }

                if (Bottom > M.Bottom)
                {
                    Top = M.Bottom - (M.Height - Height) / 2;
                }

                
                

            }

           
        }

       
    }
}
