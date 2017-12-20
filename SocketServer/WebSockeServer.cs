using NetworkSocket.Core;
using NetworkSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.Fast;
using NetworkSocket;
using Newtonsoft.Json;

namespace SocketServer
{
    public class WebSockeServer : JsonWebSocketApiService
    {
        /// <summary>
        /// 设置时间格式和忽略循环引用
        /// </summary>
        private static JsonSerializerSettings Setting = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

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
        /// 发送消息给所有人
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="sender">发送者</param>
        /// <returns></returns>
        public void SendAllMessage(SenMessge model)
        {
            //发送给所有人
            foreach (var session in this.CurrentContext
                    .JsonWebSocketSessions)
            {
                //消息不发给自己
                if (session.Tag.ID != this.CurrentContext.Session.Tag.ID)
                {
                    session.InvokeApi("onWebNotify", JsonConvert.SerializeObject(model, Setting));
                }
            }
        }


        [Api]
        public RestResult SendMessageToUsers(string[] users, string message)
        {
            if (string.IsNullOrEmpty(message) == true)
            {
                return RestResult.False("发送的内容不能为空");
            }
            Console.WriteLine("{0} 收到后回复信息：{1}", this.CurrentContext.Session.RemoteEndPoint.ToString(), message);
            var name = (string)this.CurrentContext.Session.Tag.Get("name").AsString(); // 发言人
            var model = new SenMessge(name, message);
            if (users.Length <= 0 || users[0] == "0")
            {
                SendAllMessage(model);
            }

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Trim()) && user != "0")
                {
                    var session = this.CurrentContext.JsonWebSocketSessions.Where(item => item.Tag.ID == user)
                                    .FirstOrDefault();
                    if (session == null)
                    {
                        model.sender = "系统";
                        model.Content = user + " 已经离线.";
                        this.CurrentContext.Session.InvokeApi("onWebNotify", model);
                        continue;
                    }
                    //消息不发给自己
                    if (session.Tag.ID != this.CurrentContext.Session.Tag.ID)
                    {
                        session.InvokeApi("onWebNotify", model);
                    }
                }
            }
            return RestResult.True("发送成功");
        }

        /// <summary>
        /// 设置所有人在线列表
        /// </summary>
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
        /// 用户登录
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
            this.SendMessageToUsers(new string[] { }, msg);
            this.SendAllOnlien();
            this.SendAllTcp(msg);
            return RestResult.True("设置成功", this.CurrentContext.Session.Tag.ID);
        }
    }
}
