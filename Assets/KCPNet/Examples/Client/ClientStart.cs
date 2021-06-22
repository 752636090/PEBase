using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KCPNet;
using KCPExampleProtocol;
using System;

public class ClientStart : MonoBehaviour
{
    public Text text;
    public Button btnClientSend;

    private static KCPNet<ClientSession, NetMsg> client;

    private void Start()
    {
        btnClientSend.onClick.AddListener(OnClientSend);

        string ip = "127.0.0.1";
        client = new KCPNet<ClientSession, NetMsg>();
        client.StartAsClient(ip, 17666);
        client.ConnectServer();
    }

    private void Update()
    {
        
    }

    public void OnClientSend()
    {
        string input = text.text;
        text.text = string.Empty;
        if (input == string.Empty)
        {
            return;
        }
        else if (input == "quit")
        {
            client.CloseClient();
            return;
        }
        client.ClientSession.SendMsg(new NetMsg
        {
            Info = input
        });
        Debug.Log($"¿Í»§¶Ë·¢ËÍ£º{input}");
    }
}
