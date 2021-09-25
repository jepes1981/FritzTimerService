using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cassia;


namespace CassiaLogic
{
    static class CassiaLogicClass
    {
        public static int UserActiveCheck(string _userName)
        {
            int userIsActive = -1; // -1 = error, was not even able to check users // 0 = user is NOT active // 1 = user IS active 

            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {


                    //  Console.WriteLine("*************");
                    //  Console.WriteLine("Hi there, " + session.UserAccount + " on session " + session.SessionId);
                    //  Console.WriteLine("It looks like you logged on at " + session.LoginTime +
                    //                    " and are now " + session.ConnectionState);
                    if (session.UserName.Equals(_userName))
                    {
                        if (session.ConnectionState.ToString().Equals("Active"))
                        {
                            userIsActive = 1;
                        }
                        else
                        {
                            userIsActive = 0;
                        }
                    }
                    userIsActive = 0;
                    //Console.WriteLine("*************");
                }
            }
            return userIsActive;
            // Thread.Sleep(1000);
            // Console.Clear();
            //Console.ReadLine();
        }
    }
}
