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

    public abstract class KCPSession
    {
        protected uint m_SessionId;
        Action<byte[], IPEndPoint> m_UdpSender;
        private IPEndPoint m_RemotePoint;
        protected SessionState m_SessionState = SessionState.None;
        public Action<uint> OnSessionClose;

        private KCPHandle m_Handle;
        private Kcp m_Kcp;

        /// <param name="conv">conversation id</param>
        public void InitSession(uint sid, Action<byte[], IPEndPoint> udpSender, IPEndPoint remotePoint)
        {
            m_SessionId = sid;
            m_UdpSender = udpSender;
            m_RemotePoint = remotePoint;
            m_SessionState = SessionState.Connected;

            m_Handle = new KCPHandle();
            m_Kcp = new Kcp(sid, m_Handle);
            m_Kcp.NoDelay(1, 10, 2, 1);
            m_Kcp.WndSize(64, 64);
            m_Kcp.SetMtu(512);

            m_Handle.Out = (Memory<byte> buffer) =>
            {
                byte[] bytes = buffer.ToArray();
                m_UdpSender(bytes, m_RemotePoint);
            };
        }

        public void CloseSession()
        {
            OnDisConnected();

            OnSessionClose?.Invoke(m_SessionId);
            OnSessionClose = null;

            m_SessionState = SessionState.DisConnected;
            m_RemotePoint = null;
            m_UdpSender = null;
            m_SessionId = 0;

            m_Handle = null;
            m_Kcp = null;
        }

        protected abstract void OnDisConnected();

        public bool IsConnected()
        {
            return m_SessionState == SessionState.Connected;
        }
    }
}
