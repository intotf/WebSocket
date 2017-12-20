using NetworkSocket.Core;
using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TcpSocket
{
    public class ListenerControl : ServiceControl
    {
        public static FastClient client = new FastClient("10.0.3.88", 1850);

        public bool Start(HostControl hostControl)
        {
            Task.Run(() => SendToAllMsg());
            return true;
        }

        public void SendToAllMsg()
        {
            for (int i = 0; i < 10; i++)
            {
                client.SendMsgToAll(client.RemoteEndPoint.ToString() + "发出消息测试上线" + i);
            }

        }


        public bool Stop(HostControl hostControl)
        {
            throw new NotImplementedException();
        }
    }
}
