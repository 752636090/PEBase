using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KCPNet
{
    public class KCPNet<T> where T : KCPSession, new()
    {
        private UdpClient udp;
        private IPEndPoint remotePoint;

        #region 客户端
        public T ClientSession;

        public void StartAsClient(string ip, int port)
        {
            udp = new UdpClient(0);
            remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
            KCPTool.ColorLog(ConsoleColor.Green, "Client Start...");
        }

        public void ConnectServer()
        {
            SendUdpMsg(new byte[4], remotePoint);
        }

        private async void ClientReceive()
        {
            UdpReceiveResult result;
            while (true)
            {
                try
                {
                    result = await udp.ReceiveAsync();

                    if (Equals(remotePoint, result.RemoteEndPoint))
                    {
                        uint sid = BitConverter.ToUInt32(result.Buffer, 0);
                        if (sid == 0) // sid数据
                        {
                            if (ClientSession != null && ClientSession.IsConnected())
                            {
                                KCPTool.Warning("已经建立连接，初始化完成了，直接丢弃多的sid");
                            }
                            else // 未初始化，收到服务器分配的sid数据，初始化一个客户端session
                            {
                                sid = BitConverter.ToUInt32(result.Buffer, 4);
                                KCPTool.ColorLog(ConsoleColor.DarkGreen, $"UDP Request Conv Sid:{sid}");

                                // 会话处理
                                ClientSession = new T();
                                ClientSession.InitSession(sid, SendUdpMsg, remotePoint);

                            }
                        }
                        else // 处理业务逻辑
                        {
                            
                        }
                    }
                    else
                    {
                        KCPTool.Warning("Client Udp 接收了非法目标的数据");
                    }
                }
                catch (Exception e)
                {
                    KCPTool.Warning($"Client Udp 接收异常:{e}");
                }
            }
        }
        #endregion

        private void SendUdpMsg(byte[] bytes, IPEndPoint remotePoint)
        {
            if (udp != null)
            {
                udp.SendAsync(bytes, bytes.Length, remotePoint);
            }
        }
    }
}
