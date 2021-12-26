using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace testDvigalo
{
    
    public partial class Form1 : Form
    {
        Prog tl;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            tl = new Prog(this);

            
            Thread t = new Thread(new ThreadStart(() => NewJob.AddJobProgramaticaly(this)));
            t.Start();

            t = new Thread(new ThreadStart(() => NewJob.AddJobProgramaticaly2(this)));
            t.Start();


        }



    }
}
