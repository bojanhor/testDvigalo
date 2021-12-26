using System;
using System.Collections.Generic;
using System.Text;

namespace testDvigalo
{
    //GENERAL
    partial class Prog
    {

        // SETTINGS
        public static int ForceredrawEvery_ms = 500;
        public static decimal timespan_m = 120;

        // END SETTINGS

        MyGraphics g;
        Form1 form;
        public TimeManager t;

        public Prog(Form1 thisform)
        {
            form = thisform;
            t = new TimeManager(form);
            g = new MyGraphics(form, t);

        }
    }
}
