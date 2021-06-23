using KCPExampleProtocol;
using KCPNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitySession : KCPSession<NetMsg>
{
    protected override void OnConnected()
    {
        Debug.Log($"Thread:{Thread.CurrentThread.ManagedThreadId} Connected.");
    }

    protected override void OnDisConnected()
    {
        Debug.Log($"Thread:{Thread.CurrentThread.ManagedThreadId} DisConnected.");
    }

    protected override void OnReceiveMsg(NetMsg msg)
    {
        Debug.Log($"Thread:{Thread.CurrentThread.ManagedThreadId} Sid:{SessionId} ReceiveClient:{msg.Info}");
        if (msg.CMD == CMD.NetPing)
        {
            if (msg.NetPing.IsOver)
            {
                CloseSession(); 
            }
            else
            {
                checkCounter = 0;
                int delay = (int)DateTime.UtcNow.Subtract(sendTime).TotalMilliseconds;
                Debug.Log($"Thread:{Thread.CurrentThread.ManagedThreadId} NetDelay:{delay}");
            }
        }
    }

    private DateTime sendTime;
    private int checkCounter;
    private DateTime checkTime = DateTime.UtcNow;
    protected override void OnUpdate(DateTime now)
    {
        if (now > checkTime)
        {
            sendTime = now;
            checkTime = now.AddSeconds(5);
            checkCounter++;
            NetMsg pingMsg = new NetMsg
            {
                CMD = CMD.NetPing,
            };
            if (checkCounter > 3)
            {
                pingMsg.NetPing = new NetPing { IsOver = true };
                OnReceiveMsg(pingMsg);
            }
            else
            {
                pingMsg.NetPing = new NetPing { IsOver = false };
                SendMsg(pingMsg);
            }
        }
    }
}
