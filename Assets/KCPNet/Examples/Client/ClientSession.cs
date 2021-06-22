using KCPExampleProtocol;
using KCPNet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSession : KCPSession<NetMsg>
{
    protected override void OnConnected()
    {

    }

    protected override void OnDisConnected()
    {

    }

    protected override void OnReceiveMsg(NetMsg msg)
    {
        Debug.Log($"Sid:{SessionId} ReceiveServer:{msg.Info}");
    }

    protected override void OnUpdate(DateTime now)
    {

    }
}
