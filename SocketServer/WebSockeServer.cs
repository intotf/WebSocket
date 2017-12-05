using NetworkSocket.Core;
using NetworkSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.Fast;
using NetworkSocket;

namespace SocketServer
{
    public class WebSockeServer : JsonWebSocketApiService
    {
        public void SendAllTcp(string msg)
        {
            //var fastServer = new FastMathService();
            //fastServer.SendMsgToAll("webSocket" + msg);

            var tcps = this.CurrentContext.AllSessions.FilterProtocol(Protocol.Fast).Select(item => (FastSession)item.Wrapper);
            //var tcps = this.CurrentContext.AllSessions.FilterWrappers<FastSession>();
            //发送给所有TCP 连接端
            foreach (var tcp in tcps)
            {
                tcp.InvokeApi("SendMsg", msg);
            }
        }


        /// <summary>
        /// 成员发表群聊天内容
        /// </summary>
        /// <param name="userid">内容</param>
        /// <param name="message">发送给指定人员</param>
        /// <returns></returns>        
        [Api]
        public RestResult SendMessage(string userid, string message)
        {
            if (string.IsNullOrEmpty(message) == true)
            {
                return RestResult.False("发送的内容不能为空");
            }

            Console.WriteLine("{0} 收到后回复信息：{1}", this.CurrentContext.Session.RemoteEndPoint.ToString(), message);
            var name = (string)this.CurrentContext.Session.Tag.Get("name").AsString(); // 发言人


            var session = this.CurrentContext.JsonWebSocketSessions.Where(item => item.Tag.ID == userid)
                                .FirstOrDefault();

            if (session == null)
            {
                return RestResult.False("发送的人员不在线");
            }

            if (session != null)
            {
                var title = string.Format("{0}对{1}说：", name, session.Tag.Get("name"));
                session.InvokeApi("onWebNotify", title, message, DateTime.Now.ToString("HH:mm:ss"));
            }
            return RestResult.True("发送成功");
        }

        /// <summary>
        /// 发送消息给所有人
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Api]
        public RestResult SendAllMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg) == true)
            {
                return RestResult.False("发信消息不能为空");
            }

            var sender = this.CurrentContext.Session.Tag.Get("name").AsString(); //发言人
            //发送给所有人
            foreach (var session in this.CurrentContext
                    .JsonWebSocketSessions)
            {
                session.InvokeApi("onWebNotify", msg, sender, DateTime.Now);
            }
            return RestResult.True("发送成功", null);
        }


        public void SendAllOnlien()
        {
            //发送给所有人
            foreach (var session in this.CurrentContext
                    .JsonWebSocketSessions)
            {
                session.InvokeApi("setOnlien", this.CurrentContext.JsonWebSocketSessions.Select(item => new { id = item.Tag.ID, name = (string)item.Tag.Get("name").AsString() }));
            }
        }


        /// <summary>
        /// 设置用户ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Api]
        public RestResult UserLogin(string id, string name)
        {
            this.CurrentContext.Session.Tag.ID = id;
            this.CurrentContext.Session.Tag.Set("name", name);
            //发送给所有人
            var msg = name + "上线" + DateTime.Now;
            this.SendAllMessage(msg);
            this.SendAllOnlien();
            this.SendAllTcp(msg);

            return RestResult.True("设置成功", this.CurrentContext.Session.Tag.ID);
        }
    }
}
