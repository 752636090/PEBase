using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KCPNet;
using KCPExampleProtocol;
using System;
using System.Threading.Tasks;

public class ClientStart : MonoBehaviour
{
    public InputField inputText;
    public Button btnClientSend;

    private KCPNet<ClientSession, NetMsg> client;
    private Task<bool> checkTask = null;

    private void Start()
    {
        if (btnClientSend != null)
        {
            btnClientSend.onClick.AddListener(OnClientSend); 
        }

        string ip = "127.0.0.1";
        client = new KCPNet<ClientSession, NetMsg>();
        client.StartAsClient(ip, 17666);
        checkTask = client.ConnectServer(200, 5000);
        Task.Run(ConnectCheck);
    }

    private void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        client.CloseClient();
    }

    public void OnClientSend()
    {
        string input = inputText.text;
        inputText.text = string.Empty;
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
        Debug.Log($"客户端发送：{input}");
    }

    private static int counter = 0;
    private async void ConnectCheck()
    {
        while (true)
        {
            await Task.Delay(3000);
            if (checkTask != null && checkTask.IsCompleted)
            {
                if (checkTask.Result)
                {
                    KCPTool.ColorLog(ConsoleColor.DarkGreen, "连接服务器成功");
                    checkTask = null;
                    await Task.Run(SendPingMsg);
                }
                else
                {
                    ++counter;
                    if (counter > 4)
                    {
                        KCPTool.Error($"客户端连接服务器失败{counter}次");
                        checkTask = null;
                        break;
                    }
                    else
                    {
                        KCPTool.Warning($"客户端连接服务器失败{counter}次，重试中");
                        checkTask = client.ConnectServer(200, 5000);
                    }
                }
            }
        }
    }

    private async void SendPingMsg()
    {
        while (true)
        {
            await Task.Delay(5000);
            if (client != null && client.ClientSession != null)
            {
                client.ClientSession.SendMsg(new NetMsg
                {
                    CMD = CMD.NetPing,
                    NetPing = new NetPing
                    {
                        IsOver = false
                    }
                });
                KCPTool.ColorLog(ConsoleColor.DarkGreen, "客户端发送心跳");
            }
            else
            {
                KCPTool.ColorLog(ConsoleColor.DarkGreen, "客户端取消心跳");
                break;
            }
        }
    }

    public void Close()
    {
        client.CloseClient();
    }
}
