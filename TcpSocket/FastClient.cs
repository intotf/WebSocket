using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.Fast;
using NetworkSocket.Core;

namespace TcpSocket
{
    public class FastClient : FastTcpClient
    {
        public FastClient(string ip, int port)
        {
            this.Connect(ip, port);
        }

        /// <summary>
        /// 发送给服务器
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void SendMsgToAll(string msg)
        {
            this.InvokeApi("SendMsgToAll", msg);
        }

        /// <summary>
        /// 服务器发送来的通知
        /// </summary>
        /// <param name="msg">消息内容</param>
        [Api]
        public bool SendMsg(string msg)
        {
            Console.WriteLine("服务器发过来的信息：" + msg);
            return true;
        }
    }
}
