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
using System.Security.Principal;
using SerializerN;
using CassiaLogic;

namespace FritzTimerService
{
    
    public partial class FritzService : ServiceBase
    {
        FritzTimerClass Fritz = new FritzTimerClass("Fritz");
        SerializerClass<FritzTimerClass> Se = new SerializerClass<FritzTimerClass>();
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public FritzService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;



            if(File.Exists("FrtzTime.dat"))
            {
                
                Fritz = Se.DeSerializeNow(Fritz);
            }
            else
            {
                // create initial file instance
                Se.SerializeNow(Fritz);
            }



        }

        protected override void OnStop()
        {
            if (!File.Exists("FrtzTime.dat"))
            {
                // create initial file instance
                Se.SerializeNow(Fritz);
            }
            WriteToFile("Service is stopped at " + DateTime.Now);

        }


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            if(!AdminUserCheck()) return; // AdminUserCheck already logged the problem

            // check if account is active
            switch(CassiaLogicClass.UserActiveCheck(Fritz.UserName))
            {
                case -1:
                    WriteToFile("Unable to load terminal blah blah" + DateTime.Now);
                    return;
                case 0:
                    //do nothing 
                    return;
                case 1:
                    ActiveUserLogic();
                    break;
            }
            
            WriteToFile("Service is recall at " + DateTime.Now);
        }

        private void ActiveUserLogic()
        {
            // Validate current status of user and update
            //if user is active, and max play time, disable user
            if (TimeSpan.Compare(Fritz.RunningAllowTime, Fritz.MaxAllowTime) == 1)    // if Fritz.MaxAllowTime > Fritz.MaxAllowTime is true
            {
                Fritz.DisableEnableUser(Fritz.UserName, false);
                Fritz.LogOffUser(Fritz.UserName);
            }
            

                
            
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public bool AdminUserCheck()
        {
            bool isAdmin;

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                // If is administrator, the variable updates from False to True
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            // Check with a simple condition whether you are admin or not
            if (isAdmin)
                return true;
            else
            {
                WriteToFile("You don't have administrator rights :C !"+DateTime.Now);
                return false;
            }
        }
    }
}
