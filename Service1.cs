using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace WindowsServiceDemo
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        EventLog eventlog = new EventLog();
        UpdateRecord obj = new UpdateRecord();
        public Service1()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WriteToFile("Service started at " + DateTime.Now);
                //timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                //timer.Interval = 5000;
                //timer.Enabled = true;
                UpdateRecord.IsRunning = true;
                obj.checkStatus();
            }
            catch (Exception ex)
            {
                WriteToFile("OnStart error: " + ex.Message);
            }
            
        }

        protected override void OnStop()
        {
            WriteToFile("Service stopped at " + DateTime.Now);
        }

        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service recalled at " + DateTime.Now);
        }

        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }

        }
    }
}
