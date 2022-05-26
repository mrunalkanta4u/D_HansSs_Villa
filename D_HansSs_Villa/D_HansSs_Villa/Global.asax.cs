using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.IO;

namespace D_HansSs_Villa
{
    public class Global : System.Web.HttpApplication
    {
        VillaAccountManager accntMgr = new VillaAccountManager();
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            // Add Administrator.
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
            if (Membership.GetUser("Admin") == null)
            {
                Membership.CreateUser("Admin", "Pa$$word", "villaaccntmanager@gmail.com");
                Roles.AddUserToRole("Admin", "Administrator");
            }

            string tempPath = Server.MapPath("Temp"); //with complete path
            if (!System.IO.Directory.Exists(tempPath))
            {
                System.IO.Directory.CreateDirectory(tempPath);
            }
            else
            {
                // else part
            }

            ThreadStart childthreat = new ThreadStart(Bill_Alert_At_Month_End);
            Thread child = new Thread(childthreat);
            child.Start();
            Console.WriteLine("parent");
        }
 
        public void Bill_Alert_At_Month_End() 
        {
            DateTime thisMonth, NextMonth;
            while (true)
            {
                thisMonth = DateTime.Now;
                int today = thisMonth.Month;
                NextMonth = thisMonth.AddDays(1);
                int tomorrow = NextMonth.Month;
                if (today != tomorrow)
                {
                    accntMgr.InformAll('a', Server.MapPath("."));
                    //TimeSpan interval = new TimeSpan(1, 0, 0);
                    //Thread.Sleep(interval); //Sleep till Next Day and check again (to avoid running the process contineously)
                }
            }
        }
        
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}