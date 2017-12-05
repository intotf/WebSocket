using NetworkSocket.Core;
using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TcpSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<ListenerControl>();
                c.RunAsLocalSystem();
                c.SetServiceName("FastMessageService");
                c.SetDisplayName("FastMessageService");
                c.SetDescription("FastMessageService");
            });
        }
    }
}
