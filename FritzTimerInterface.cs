using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritzTimerService
{
    interface IFritzTimer
    {
  
        int DisableEnableUser(string _userName, bool _enableState);
        int LogOffUser(string _userName);
        bool AllowStatus { get; set; }
        string UserName { get; set; }
        DateTime CurrentTime { get; set; }

        TimeSpan RunningAllowTime { get; set; }
        TimeSpan RunningRestTime { get; set; }
    }
}
