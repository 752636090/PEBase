using KCPExampleProtocol;
using KCPNet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerStart : MonoBehaviour
{
    public InputField inputText;
    public Button btnServerSend;
    public Text logText;

    private KCPNet<ServerSession, NetMsg> server;

    private void Start()
    {
        btnServerSend.onClick.AddListener(OnServerSend);

        string ip = "127.0.0.1";
        server = new KCPNet<ServerSession, NetMsg>();
        server.StartAsServer(ip, 17666);
        
    }

    private void OnServerSend()
    {
        string input = inputText.text;
        inputText.text = string.Empty;
        if (input == string.Empty)
        {
            return;
        }
        else if (input == "quit")
        {
            server.CloseServer();
            return;
        }
        server.BroadcastMsg(new NetMsg
        {
            Info = input
        });
        LogServerLogic($"·þÎñ¶Ë·¢ËÍ£º{input}");
    }

    private void LogServerLogic(string s)
    {
        logText.text = $"{logText.text}\n{KCPTool.HandleLog(s)}";
    }
}
