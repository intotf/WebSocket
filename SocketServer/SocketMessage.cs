using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    /// <summary>
    /// 消息体内容
    /// </summary>
    public class SocketMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 消息标题 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
