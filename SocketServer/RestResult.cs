using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    /// <summary>
    /// 返回数据
    /// </summary>
    public class RestResult
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 实体内容
        /// </summary>
        public object Data { get; set; }

        public RestResult()
        {

        }

        public RestResult(bool State, string Message, object Data)
        {
            this.Message = Message;
            this.Data = Data;
            this.State = State;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public static RestResult True(string msg, object data)
        {
            return new RestResult() { Data = data, State = true };
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public static RestResult True(string msg)
        {
            return new RestResult() { State = true, Message = msg };
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <returns></returns>
        public static RestResult False(string msg)
        {
            return new RestResult() { State = false, Message = msg };
        }
    }
}
