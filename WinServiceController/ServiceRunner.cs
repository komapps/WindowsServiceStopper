using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace WinServiceController
{
    public class ServiceRunner
    {
        public List<string> ServicesToStop { get; set; }

        public Timer Timer { get; set; }

        public ServiceRunner()
        {
            ServicesToStop = File.ReadAllLines("ServicesToStop.txt").ToList();
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = 3000;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var allServices = ServiceController.GetServices().Where(a => ServicesToStop.Contains(a.ServiceName));
            foreach (var serviceController in allServices)
            {
                try
                {
                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        serviceController.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
