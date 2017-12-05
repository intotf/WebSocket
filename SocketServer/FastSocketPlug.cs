using NetworkSocket.Plugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetworkSocket.WebSocket;

namespace SocketServer
{
    public class FastSocketPlug : PlugBase
    {
        protected override void OnConnected(object sender, NetworkSocket.IContenxt context)
        {
            var sessions = context.AllSessions.FilterWrappers<JsonWebSocketSession>();
            foreach (var session in sessions)
            {
                session.InvokeApi("onWebNotify", "我是 Tcp 连接" + context.Session.RemoteEndPoint.ToString());
            }
        }
    }
}
