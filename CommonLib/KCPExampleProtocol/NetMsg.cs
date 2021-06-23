using KCPNet;
using System;

namespace KCPExampleProtocol
{
    [Serializable]
    public class NetMsg : KCPMsg
    {
        public CMD CMD;
        public NetPing NetPing;
        public string Info;
    }

    [Serializable]
    public class NetPing
    {
        /// <summary>
        /// 是否结束连接
        /// </summary>
        public bool IsOver;
    }

    public enum CMD
    {
        None,
        ReqLogin,
        NetPing
    }
}
