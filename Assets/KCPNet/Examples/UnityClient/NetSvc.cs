using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KCPNet;
using KCPExampleProtocol;

public class NetSvc : MonoBehaviour
{
    KCPNet<UnitySession, NetMsg> client;

    private void Start()
    {
        string ip = "127.0.0.1";
        client = new KCPNet<UnitySession, NetMsg>();
        client.StartAsClient(ip, 17666);
        client.ConnectServer(200, 500);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            client.CloseClient();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            client.ClientSession.SendMsg(new NetMsg
            {
                Info = "msg from unity"
            });
        }
    }

    private void OnApplicationQuit()
    {
        client.CloseClient();
    }
}
