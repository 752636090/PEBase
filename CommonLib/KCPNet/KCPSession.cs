using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets.Kcp;
using System.Text;

namespace KCPNet
{
    public enum SessionState
    {
        None,
        Connected,
        DisConnected
    }

    public class KCPSession
    {
        protected uint m_SessionId;
        Action<byte[], IPEndPoint> m_UdpSender;
        private IPEndPoint m_RemotePoint;
        protected SessionState m_SessionState = SessionState.None;

        private KCPHandle m_Handle;
        public Kcp Kcp;

        /// <param name="conv">conversation id</param>
        public void InitSession(uint sid, Action<byte[], IPEndPoint> udpSender, IPEndPoint remotePoint)
        {
            m_SessionId = sid;
            m_UdpSender = udpSender;
            m_RemotePoint = remotePoint;
            m_SessionState = SessionState.Connected;

            m_Handle = new KCPHandle();
            Kcp = new Kcp(sid, m_Handle);
            Kcp.NoDelay(1, 10, 2, 1);
            Kcp.WndSize(64, 64);
            Kcp.SetMtu(512);

            m_Handle.Out = (Memory<byte> buffer) =>
            {
                byte[] bytes = buffer.ToArray();
                m_UdpSender(bytes, m_RemotePoint);
            };
        }
    }
}
