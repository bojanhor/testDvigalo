using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace testDvigalo
{
    class TimeManager
    {
        Thread currentTimeThread;
        string currentTimeString = "";
        string maxTimeString = "";

        public DateTime currentTime { get; private set; }
        public DateTime maxTime { get; private set; }

        Form1 form;

        public TimeManager(Form1 form)
        {
            this.form = form;

            //
            currentTimeThread = new Thread(()=> currentTimeMethod());
            currentTimeThread.SetApartmentState(ApartmentState.MTA);
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
            //


        }

        public string GetCurrentTimeString()
        {
            return currentTimeString;
        }

        public string GetMaxTimeString()
        {
            return maxTimeString;
        }

        void currentTimeMethod()
        {
            while (true)
            {
                currentTime = DateTime.Now;
                maxTime = currentTime + TimeSpan.FromMinutes((double)Prog.timespan_m);

                currentTimeString = currentTime.ToString("HH:mm:ss");
                maxTimeString = maxTime.ToString("HH:mm:ss");

                form.Invalidate();
                               
                Thread.Sleep(Prog.ForceredrawEvery_ms);
            }
        }
    }
}
