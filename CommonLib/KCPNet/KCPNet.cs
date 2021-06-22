using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KCPNet
{
    public class KCPNet
    {
        private UdpClient udp;
        private IPEndPoint remotePoint;

        #region 客户端
        public void StartAsClient(string ip, int port)
        {
            udp = new UdpClient(0);
            remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
            KCPTool.ColorLog(ConsoleColor.Green, "Client Start...");
        }
        #endregion
    }
}
