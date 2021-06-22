using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets.Kcp;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KCPNet
{
    public enum SessionState
    {
        None,
        Connected,
        DisConnected
    }

    public abstract class KCPSession<T> where T : KCPMsg, new()
    {
        protected uint m_SessionId;
        Action<byte[], IPEndPoint> m_UdpSender;
        private IPEndPoint m_RemotePoint;
        protected SessionState m_SessionState = SessionState.None;
        public Action<uint> OnSessionClose;

        private KCPHandle m_Handle;
        private Kcp m_Kcp;
        private CancellationTokenSource cts;
        private CancellationToken ct;

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

            m_Handle.OnReceive = (byte[] buffer) =>
            {
                buffer = KCPTool.Decompress(buffer);
                T msg = KCPTool.Deserialize<T>(buffer);
                if (msg != null)
                {
                    OnReceiveMsg(msg);
                }
            };

            OnConnected();

            cts = new CancellationTokenSource();
            ct = cts.Token;
            Task.Run(BeginUpdateAsync, ct);
        }

        public void ReceiveData(byte[] buffer)
        {
            m_Kcp.Input(buffer.AsSpan());
        }

        private async void BeginUpdateAsync()
        {
            try
            {
                while (true)
                {
                    DateTime now = DateTime.UtcNow;
                    OnUpdate(now);
                    if (ct.IsCancellationRequested)
                    {
                        KCPTool.ColorLog(ConsoleColor.Cyan, "SessionUpdate Task is Cancelled.");
                    }
                    else
                    {
                        m_Kcp.Update(now);
                        int len;
                        while ((len = m_Kcp.PeekSize()) > 0)
                        {
                            byte[] buffer = new byte[len];
                            if (m_Kcp.Recv(buffer) >= 0)
                            {
                                m_Handle.Receive(buffer);
                            }
                        }
                        await Task.Delay(10);
                    }
                }
            }
            catch (Exception e)
            {
                KCPTool.Warning($"Session Update 异常:{e.ToString()}");
            }
        }

        public void SendMsg(T msg)
        {
            if (IsConnected())
            {
                byte[] bytes = KCPTool.Serialize(msg);
                if (bytes != null)
                {
                    SendMsg(bytes);
                }
            }
            else
            {
                KCPTool.Warning("Session没有连接，不能发消息");
            }
        }

        /// <summary>
        /// 如果是广播消息，则先序列化然后广播字节数组
        /// </summary>
        public void SendMsg(byte[] bytes)
        {
            if (IsConnected())
            {
                bytes = KCPTool.Compress(bytes);
                m_Kcp.Send(bytes.AsSpan());
            }
            else
            {
                KCPTool.Warning("Session没有连接，不能发消息");
            }
        }

        public void CloseSession()
        {
            cts.Cancel();
            OnDisConnected();

            OnSessionClose?.Invoke(m_SessionId);
            OnSessionClose = null;

            m_SessionState = SessionState.DisConnected;
            m_RemotePoint = null;
            m_UdpSender = null;
            m_SessionId = 0;

            m_Handle = null;
            m_Kcp = null;
            cts = null;
        }

        protected abstract void OnConnected();
        protected abstract void OnUpdate(DateTime now);
        protected abstract void OnReceiveMsg(T msg);
        protected abstract void OnDisConnected();

        public bool IsConnected()
        {
            return m_SessionState == SessionState.Connected;
        }
    }
}
