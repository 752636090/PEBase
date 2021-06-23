using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KCPNet
{
    public class KCPNet<T, K> 
        where T : KCPSession<K>, new()
        where K : KCPMsg, new()
    {
        private UdpClient udp;
        private IPEndPoint remotePoint;

        private CancellationTokenSource cts;
        private CancellationToken ct;

        public KCPNet()
        {
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        #region 客户端
        public T ClientSession;
        public void StartAsClient(string ip, int port)
        {
            udp = new UdpClient(0);
            remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
            KCPTool.ColorLog(ConsoleColor.Green, "Client Start...");

            Task.Run(ClientReceive, ct);
        }
        public Task<bool> ConnectServer(int interval = 200, int maxIntervalSum = 5000)
        {
            SendUdpMsg(new byte[4], remotePoint);
            int checkTimes = 0;
            Task<bool> task = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(interval);
                    checkTimes += interval;
                    if (ClientSession != null && ClientSession.IsConnected)
                    {
                        return true;
                    }
                    else
                    {
                        if (checkTimes > maxIntervalSum)
                        {
                            return false;
                        }
                    } 
                }
            });

            return task;
        }
        private async void ClientReceive()
        {
            UdpReceiveResult result;
            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                    {
                        KCPTool.ColorLog(ConsoleColor.Cyan, "ClientReceive Task is Cancelled");
                        break;
                    }
                    result = await udp.ReceiveAsync();

                    if (Equals(remotePoint, result.RemoteEndPoint))
                    {
                        uint sid = BitConverter.ToUInt32(result.Buffer, 0);
                        if (sid == 0) // sid数据
                        {
                            if (ClientSession != null && ClientSession.IsConnected)
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
                                ClientSession.OnSessionClose = OnClientSessionClose;
                            }
                        }
                        else // 处理业务逻辑
                        {
                            if (ClientSession != null && ClientSession.IsConnected)
                            {
                                ClientSession.ReceiveData(result.Buffer);
                            }
                            else // 没初始化且sid!=0，数据消息提前到了，直接丢弃消息，知道初始化完成，kcp重传再开始处理
                            {
                                KCPTool.Warning("Client is Initing...");
                            }
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
        private void OnClientSessionClose(uint sid)
        {
            cts.Cancel();
            if (udp != null)
            {
                udp.Close();
                udp = null;
            }
            KCPTool.Warning($"Client Session Close, sid:{sid}");
        }
        public void CloseClient()
        {
            if (ClientSession != null)
            {
                ClientSession.CloseSession();
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
