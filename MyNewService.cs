using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AutomationFPP;

namespace MyNewService
{ 
    public partial class MyNewService : ServiceBase
    {        
        private LibraryFPP l;


        
        private System.Diagnostics.EventLog eventLog1;
        Thread childThread;

        public MyNewService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("FPP"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "FPP", "FPP");
            }
            l = new LibraryFPP();
            eventLog1.Source = "Application";
            eventLog1.Log = "Application";
            eventLog1.WriteEntry("In OnStart of FPP");


            ThreadStart childref = new ThreadStart(MyThread);
            childThread = new Thread(childref);
            childThread.Start();
        }

        protected override void OnStart(string[] args)
        {
            if (childThread.IsAlive) return;

            eventLog1.WriteEntry("In OnStart of FPP");


            ThreadStart childref = new ThreadStart(MyThread);            
            childThread = new Thread(childref);
            childThread.Start();
            
        }


        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop of FPP(start)");
            childThread.Abort();
            eventLog1.WriteEntry("In OnStop of FPP (complete)");
                 }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
        }


        private void MyThread()
        {
            long i = 0;
            while (childThread.IsAlive)
            {
                i++;
                eventLog1.WriteEntry("In MyThread of FPP (Start) - " + i);
                l.SaveXMLtoDB2("http://weather.cyi.ac.cy/data/met/CyDoM.xml");
                l.DoForecast(new DateTime().Date);
                eventLog1.WriteEntry("In MyThread of FPP (Complete) - " + i);
                Thread.Sleep(60000*60); //1 hour
            }
        }

    }
}
