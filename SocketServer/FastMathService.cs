using NetworkSocket;
using NetworkSocket.Core;
using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class FastMathService : FastApiService
    {
        /// <summary>
        /// 获取其它已登录的会话
        /// </summary>
        public IEnumerable<ISession> OtherSessions
        {
            get
            {
                return this.CurrentContext.AllSessions.FilterProtocol(Protocol.Fast);
            }
        }

        /// <summary>
        /// 登录操作
        /// </summary>       
        /// <param name="user">用户数据</param>
        /// <param name="ifAdmin"></param>
        /// <returns></returns>    
        [Api]
        public void SendMsgToAll(string msg)
        {
            var FastSessions = this.CurrentContext.FastSessions;
            foreach (var fast in FastSessions)
            {
                fast.InvokeApi("SendMsg", msg);
            }
        }
    }
}
