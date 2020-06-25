using System;
using Topshelf;

namespace WinServiceController
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<ServiceRunner>(s =>
                {
                    s.ConstructUsing(service => new ServiceRunner());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });


                x.RunAsLocalSystem();
                x.SetServiceName("WindowsServiceStopper");
                x.SetDisplayName("Windows Service Stopper");
                x.SetDescription("Service for stopping other services of windows");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
