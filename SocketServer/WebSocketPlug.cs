using NetworkSocket;
using NetworkSocket.Plugs;
using NetworkSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class WebSocketPlug : PlugBase
    {

        /// <summary>
        /// 会话断开后通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="context"></param>
        protected override void OnDisconnected(object sender, IContenxt context)
        {
            if (context.Session.Protocol != Protocol.WebSocket)
            {
                return;
            }
            Console.WriteLine("{0} 离线通知：{0}", DateTime.Now, context.Session.RemoteEndPoint.ToString());
        }

        /// <summary>
        /// 会话连接成功后触发    
        /// 如果关闭了会话，将停止传递给下个插件的OnConnected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="context"></param>
        protected override void OnConnected(object sender, IContenxt context)
        {
            Console.WriteLine("{0} 上线通知：{1}", DateTime.Now, context.Session.RemoteEndPoint.ToString());
        }

        ///// <summary>
        ///// 客户端发过来的数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="context"></param>
        //protected override void OnRequested(object sender, IContenxt context)
        //{
        //    Console.WriteLine("{0} 发回数据：{1}", context.Session.RemoteEndPoint.ToString(), context.StreamReader.ReadString(Encoding.UTF8));
        //}
    }
}
