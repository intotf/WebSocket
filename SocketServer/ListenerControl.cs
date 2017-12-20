using NetworkSocket;
using NetworkSocket.Fast;
using NetworkSocket.Flex;
using NetworkSocket.Http;
using NetworkSocket.Util;
using NetworkSocket.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace SocketServer
{
    public class ListenerControl : ServiceControl
    {
        /// <summary>
        /// 监听器
        /// </summary>
        private TcpListener listener = new TcpListener();


        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            var ower = TcpSnapshot.Snapshot().FirstOrDefault(item => item.Port == port);
            if (ower != null)
            {
                ower.Kill();
            }

            listener.Use<FlexPolicyMiddleware>();
            listener.Use<JsonWebSocketMiddleware>().GlobalFilters.Add(new WebSockeGlobalFilter());
            listener.Use<HttpMiddleware>();
            listener.Use<FastMiddleware>();

            listener.UsePlug<WebSocketPlug>();
            listener.Start(port);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            listener.Dispose();
            return true;
        }
    }
}
