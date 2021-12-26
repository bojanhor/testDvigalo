using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace testDvigalo
{
    class Job
    {
        public List<SubJob> SubJobList;
        Pen outlinePen;

        public int Width { get; private set; }
        public int Height { get; private set; }
        private DateTime snappedTo;
        public DateTime SnappedTo
        {
            get { return snappedTo; }
            set
            {
                snappedTo = value;
                SnappedToChanged();
            }
        }

        public Job()
        {
            SubJobList = new List<SubJob>();

            Height = 40;

            outlinePen = new Pen(new SolidBrush(Color.DarkGray));


        }

        public void AddSubJobToLastPosition(SubJob subJob)
        {
            SubJobList.Add(subJob);
            TotalWidthSet();

            if (SubJobList.Count > 1)
            {
                // add eachothers references to previous subjob and this subjob
                SubJobList[SubJobList.Count - 2].NextSubJob = subJob;
                SubJobList[SubJobList.Count - 1].PreviousSubJob = SubJobList[SubJobList.Count - 2];


            }
        }


        void SnappedToChanged()
        {
            SubJob buff;
            for (int i = 0; i < SubJobList.Count; i++)
            {
                buff = SubJobList[i];

                if (!buff.hasPrevious)
                {
                    buff.StartTime = snappedTo;
                }

                if (buff.hasNext)
                {
                    buff.NextSubJob.StartTime = buff.EndTimeWithGap;
                }

            }
        }

        public void PaintSelf(Form form, PaintEventArgs e, int x, int y)
        {


            int offset = 0;
            SubJob sub;
            for (int i = 0; i < SubJobList.Count; i++)
            {
                sub = SubJobList[i];
                sub.PaintSelf(form, e, x + offset, y, Height);
                offset = offset + Misc.CalculatePxFromTime(sub.DurationWithGap);

                if (sub.mouseover)
                {
                    drawOutline(e, sub, x, y);
                }


                if (sub.hasPrevious) // first one is dragged in subjob class others folow here
                {
                    sub.Left = Misc.CalculatePxFromTime(sub.PreviousSubJob.DurationWithGap) + sub.PreviousSubJob.Left;
                    sub.Top = sub.PreviousSubJob.Top;
                    sub.Top = SubJobList[0].Top;
                }


            }

        }


        void drawOutline(PaintEventArgs e, SubJob subjob, int x, int y)
        {
            Rectangle outline = new Rectangle(x, y, Width, Height);
            outline.Inflate(2, 2);
            e.Graphics.DrawRectangle(outlinePen, outline);
        }

        void TotalWidthSet()
        {
            int buff = 0;
            for (int i = 0; i < SubJobList.Count; i++)
            {
                buff = buff + Misc.CalculatePxFromTime(SubJobList[i].DurationWithGap);
            }

            Width = buff;
        }

    }

}
