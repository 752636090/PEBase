using KCPExampleProtocol;
using KCPNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ServerSession : KCPSession<NetMsg>
{
    protected override void OnConnected()
    {
        KCPTool.ColorLog(ConsoleColor.DarkGreen, $"客户端上线，Sid:{SessionId}");
    }

    protected override void OnDisConnected()
    {
        KCPTool.Warning($"客户端下线，Sid:{SessionId}");
    }

    protected override void OnReceiveMsg(NetMsg msg)
    {
        KCPTool.ColorLog(ConsoleColor.Magenta, $"Sid:{SessionId},收到客户端数据,CMD:{msg.CMD} {msg.Info}");

        if (msg.CMD == CMD.NetPing)
        {
            if (msg.NetPing.IsOver)
            {
                CloseSession();
            }
            else
            {
                // 收到ping请求，则重置检查计数，并回复ping消息到客户端
                checkCounter = 0;
                NetMsg pingMsg = new NetMsg
                {
                    CMD = CMD.NetPing,
                    NetPing = new NetPing
                    {
                        IsOver = false
                    }
                };
                SendMsg(pingMsg);
            }
        }
    }

    private int checkCounter;
    private DateTime checkTime = DateTime.UtcNow.AddSeconds(5);
    protected override void OnUpdate(DateTime now)
    {
        if (now > checkTime)
        {
            checkTime = now.AddSeconds(5);
            checkCounter++;
            if (checkCounter > 3)
            {
                NetMsg pingMsg = new NetMsg
                {
                    CMD = CMD.NetPing,
                    NetPing = new NetPing
                    {
                        IsOver = true
                    }
                };
                OnReceiveMsg(pingMsg);
            }
        }
    }
}
