using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.SetOut(Debugger.Out);
            }

            HostFactory.Run(c =>
            {
                c.Service<ListenerControl>();
                c.RunAsLocalSystem();
                c.SetServiceName("WebMessageService");
                c.SetDisplayName("WebMessageService");
                c.SetDescription("WebMessageService");
            });
        }
    }
}
