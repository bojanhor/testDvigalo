using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;


namespace testDvigalo
{
    partial class Prog
    {

        public class MyGraphics
        {

            Form1 form;
            public static TimeLine Myline;
            Button btnNewJob = new Button();
            TimeManager t;
            NewJob nj;
            JobPainter jp = new JobPainter();
            JobCoordinator jc = new JobCoordinator();

            public MyGraphics(Form1 form, TimeManager t)
            {
                this.form = form;
                this.t = t;
                Myline = new TimeLine(form, t);
                addNewJobBtn();
                Myline.AddTimelineLables();    
                form.Paint += Form_Paint;

                                


            }

            private void Form_Click(object sender, EventArgs e)
            {
                Console.Beep(1000, 10);
            }




            // PAINT
            private void Form_Paint(object sender, PaintEventArgs e)
            {                
                drawEmptyTimeline(e);
                drawQLines(e);
            }

            void drawEmptyTimeline(PaintEventArgs e)
            {
                Myline.Draw(e);   
            }
            

            void addNewJobBtn()
            {
                var top = 110;
                var left = 20;
                Misc.PositionControl(btnNewJob, top, left);
                btnNewJob.Text = "Add Job";
                btnNewJob.Click += BtnNewJob_Click;
                form.Controls.Add(btnNewJob);
            }            

            private void BtnNewJob_Click(object sender, EventArgs e)
            {
                nj = new NewJob(form);
            }


            void drawQLines(PaintEventArgs e)
            {
                int startpos = 200;
                int spacing = 50;

                jp.Paint(form,e,Myline.Left, startpos, spacing);


            }

            

           
        }
        
    }
}
