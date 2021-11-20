using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simple_Launcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int argsCount = args.Length;
            string l_currentPath = Directory.GetCurrentDirectory();
            string l_programPath = l_currentPath + "\\Test_HelloWorld.exe";
            string l_startTime = DateTime.Now.AddSeconds(5).ToString("HH:mm:ss");
            string l_endTime = DateTime.Now.AddSeconds(10).ToString("HH:mm:ss");
            int l_checkingInterval = 1;
            string l_targetProcessName = Path.GetFileNameWithoutExtension(l_programPath);
            string l_thisProgName = System.AppDomain.CurrentDomain.FriendlyName;

            if ( argsCount < 3)
            {
                Console.WriteLine("You need to add the following 3 parameters.");
                Console.WriteLine("#1 path : string");
                Console.WriteLine("#2 startTime : hh:mm:ss");
                Console.WriteLine("#3 KillTime : hh:mm:ss");                
                Console.WriteLine(@"   ex) {0} {1} {2} {3}", l_thisProgName, l_programPath, l_startTime, l_endTime);

                Console.WriteLine("");
                Console.WriteLine("now running the ex) above.");
                Console.WriteLine("> Start at {0} everyday", l_startTime, l_checkingInterval);
                Console.WriteLine("> End at {0}  everyday ", l_endTime, l_checkingInterval);
            }
            else if(argsCount == 3)
            {
                l_programPath = args[0];
                l_startTime = args[1];
                l_endTime = args[2];
                l_targetProcessName = Path.GetFileNameWithoutExtension(args[0]);
                Console.WriteLine("now running.... {0}", DateTime.Now.ToString() );
                Console.WriteLine("> Start at {0} everyday", l_startTime, l_checkingInterval);
                Console.WriteLine("> End at {0}  everyday ", l_endTime, l_checkingInterval);
            }


            while (true)
            {
                
                if (IsTimeBetweenAnB(l_startTime, l_checkingInterval))
                {
                    startProgram(l_programPath);
                    Console.WriteLine(@"Start Program : {0} {1}", l_programPath, DateTime.Now.ToString());
                }
                if (IsTimeBetweenAnB(l_endTime, l_checkingInterval)
                    )
                {
                    EndProgram(l_targetProcessName);
                    Console.WriteLine(@"End Program : {0} {1}", l_programPath, DateTime.Now.ToString());
                    Console.WriteLine(@"Still running... next startTime is at the next {0}", l_startTime);
                }
                Thread.Sleep(l_checkingInterval * 1000);
            }
        }
        static public void startProgram(string pProgramPath)
        {
            try
            {
                //process.Exited += (sender, e) => { /* Code executed on process exit */ };
                Process.Start(pProgramPath);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private static void EndProgram(string pTargetProcessName)
        {
            var list = Process.GetProcesses();
            foreach (Process process in list)
            {
                if (process.ProcessName.StartsWith(pTargetProcessName)
                    && process.MainModule != null
                    )
                {
                    process.Kill();
                }
            }
        }

        // hh:mm:ss
        private static bool IsTimeBetweenAnB(string hhmmss, int checkingInterval_seconds)
        {
            string[] l_time = hhmmss.Split(':');
            int l_hour = Convert.ToInt32(l_time[0]);
            int l_minute = Convert.ToInt32(l_time[1]);
            int l_second = Convert.ToInt32(l_time[2]);
            if (DateTime.Today.AddHours(l_hour).AddMinutes(l_minute).AddSeconds(l_second) <= DateTime.Now
                &&  DateTime.Now < DateTime.Today.AddHours(l_hour).AddMinutes(l_minute).AddSeconds(l_second + checkingInterval_seconds) 
                ) return true;
            
            return false;
        }

    }



}
