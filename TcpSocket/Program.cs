using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpSocket
{
    class Program
    {
        static Main(string[] args)
        {
            FastTcpClient client = new FastTcpClient();
            client.Connect("10.0.3.88", 1850);

        }
    }
}
