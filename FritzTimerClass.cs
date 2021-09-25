using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritzTimerService
{
    [Serializable]
    class FritzTimerClass : IFritzTimer
    {
        private bool _allowStatus;
        private string _userName;
        private DateTime _currentTime;
        public TimeSpan MaxAllowTime;
        public TimeSpan MaxRestTime;
        private TimeSpan _runningAllowTime;
        private TimeSpan _runningRestTime;

        public FritzTimerClass(string __userName)
        {
            _allowStatus = true;
            _userName = __userName;
            _currentTime = DateTime.Now;
            MaxAllowTime = new TimeSpan(3, 0, 0);
            MaxRestTime = new TimeSpan(2, 0, 0);
            _runningAllowTime = new TimeSpan(0, 0, 0);
            _runningRestTime = new TimeSpan(0, 0, 0);
        }
       //  bool IFritzTimer.AllowStatus { get { return _allowStatus; } set => _allowStatus = value; }
        //string IFritzTimer.UserName { get { return _userName; } set => _userName = value; }
     //   DateTime IFritzTimer.CurrentTime { get { _currentTime = DateTime.Now; return _currentTime; } set => _currentTime = value; }

        public string UserName { get { return _userName; } set => _userName = value; }
        public bool AllowStatus { get { return _allowStatus; } set => _allowStatus = value; }
        public DateTime CurrentTime { get { _currentTime = DateTime.Now; return _currentTime; } set => _currentTime = value; }

        public TimeSpan RunningAllowTime { get { return _runningAllowTime; } set => _runningAllowTime = value; }
        public TimeSpan RunningRestTime { get { return _runningRestTime; } set => _runningRestTime = value; }

        public int DisableEnableUser(string _userName, bool _enableState)
        {
            Console.WriteLine("Function to Disable User Called");
            return 0;
            //throw new NotImplementedException();
        }

        public int LogOffUser(string _userName)
        {
            Console.WriteLine("Function to LogOff User Called");
            return 0;
            //throw new NotImplementedException();
        }
    }
}
