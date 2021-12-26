using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace testDvigalo
{
    class JobColector
    {
        public static List<Job> NewJobsList = new List<Job>();
    }


    class JobPainter
    {
        // Settings
        
        Job job;
        private Brush brush = Brushes.Blue;
        private Color color;


        public Color MyProperty
        {
            get
            {
                if (color != null)
                {
                    return color;
                }
                else
                {
                    return Color.Blue;
                }
            }
            set
            {
                color = value;
                brush = new SolidBrush(color);
            }
        }

        public JobPainter()
        {

        }

        public void Paint(Form form, PaintEventArgs e, int x, int y, int spacing)
        {            
            for (int i = 0; i < JobColector.NewJobsList.Count; i++)
            {
                JobColector.NewJobsList[i].PaintSelf(form,e,x,y);
                y = y + spacing;
            }

        }

        
    }

    class NewJob
    {

        Form1 form;
        FormAddJob formAddJob;

        

        public NewJob(Form1 form)
        {
            this.form = form;            
            formAddJob = new FormAddJob();
            formAddJob.Show();
            formAddJob.btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            AddJob();
            formAddJob.Dispose();
        }

        void AddJob()
        {
            Job newJob = new Job();
            SubJob newSubJob = new SubJob(form);

            newSubJob.Duration = parse(formAddJob.tbDur1);
            newSubJob.GapToNext = parse(formAddJob.tbGap1);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = parse(formAddJob.tbDur2);
            newSubJob.GapToNext = parse(formAddJob.tbGap2);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = parse(formAddJob.tbDur3);
            newSubJob.GapToNext = parse(formAddJob.tbGap3);
            newJob.AddSubJobToLastPosition(newSubJob);

            JobColector.NewJobsList.Add(newJob);

        }

        public static void AddJobProgramaticaly(Form form)
        {
            Thread.Sleep(900); // form load bug
            Job newJob = new Job();
            SubJob newSubJob = new SubJob(form);

            newSubJob.Duration = TimeSpan.FromSeconds(1000);
            newSubJob.GapToNext = TimeSpan.FromSeconds(100);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = TimeSpan.FromSeconds(500);
            newSubJob.GapToNext = TimeSpan.FromSeconds(1500);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = TimeSpan.FromSeconds(10);
            newSubJob.GapToNext = TimeSpan.FromSeconds(500);
            newJob.AddSubJobToLastPosition(newSubJob);

            JobColector.NewJobsList.Add(newJob);
        }

        public static void AddJobProgramaticaly2(Form form)
        {
            Thread.Sleep(1000);
            Job newJob = new Job();
            SubJob newSubJob = new SubJob(form);

            newSubJob.Duration = TimeSpan.FromSeconds(500);
            newSubJob.GapToNext = TimeSpan.FromSeconds(200);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = TimeSpan.FromSeconds(500);
            newSubJob.GapToNext = TimeSpan.FromSeconds(100);
            newJob.AddSubJobToLastPosition(newSubJob);

            newSubJob = new SubJob(form); newSubJob.Duration = TimeSpan.FromSeconds(10);
            newSubJob.GapToNext = TimeSpan.FromSeconds(1500);
            newJob.AddSubJobToLastPosition(newSubJob);

            JobColector.NewJobsList.Add(newJob);
        }



        TimeSpan parse(TextBox tb)
        {
            try
            {
                return TimeSpan.FromSeconds( Convert.ToInt32(tb.Text));
            }
            catch
            {
                return new TimeSpan();
            }
        }
               
    }

    class JobCoordinator
    {
        Thread coordinate;
        public JobCoordinator()
        {
            coordinate = new Thread(new ThreadStart(() => CoordinateMethod()));
            coordinate.Start();
        }

        void CoordinateMethod()
        {
            var jl = JobColector.NewJobsList;
            Job job;
            List<SubJob> sjl = new List<SubJob>();

            while (true)
            {
               
                for (int i = 0; i < jl.Count; i++)
                {
                    job = jl[i];

                    for (int j = 0; j < job.SubJobList.Count; j++)
                    {
                        sjl.Add(job.SubJobList[j]); // add all subjobs to list
                    }
                }

                for (int i = 0; i < sjl.Count; i++)
                {
                    for (int j = 0; j < sjl.Count; j++)
                    {
                        // i je glavni subjob ki ga čekiramo z j
                    }
                }

            }
        }

        

    }

   
   
}
