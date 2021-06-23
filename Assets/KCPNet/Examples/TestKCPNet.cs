using KCPNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TestKCPNet : MonoBehaviour
{
    private void Start()
    {
        ExampleStart2();
    }

    private void Update()
    {
        //ExampleUpdate1();
    }

    #region Example1
    private string HandleLogMsg(string msg)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        return $"Thread:{threadId} {msg}";
    }

    private void ExampleStart1()
    {
        KCPTool.LogFunc = (string s) =>
        {
            Debug.Log(HandleLogMsg(s));
        };
        KCPTool.ColorLogFunc = (ConsoleColor color, string s) =>
        {
            Debug.Log(HandleLogMsg(s));
        };
        KCPTool.WarningFunc = (string s) =>
        {
            Debug.LogWarning(HandleLogMsg(s));
        };
        KCPTool.ErrorFunc = (string s) =>
        {
            Debug.LogError(HandleLogMsg(s));
        };

        GameObject testUI = Instantiate(Resources.Load("KCPNetTestUI"), GameObject.Find("Canvas/Root").transform) as GameObject;

        ServerStart server = gameObject.AddComponent<ServerStart>();
        server.inputText = testUI.transform.Find("inputServer").GetComponent<InputField>();
        server.btnServerSend = testUI.transform.Find("btnServerSend").GetComponent<Button>();
        server.logText = testUI.transform.Find("ScrollText/Panel/Text").GetComponent<Text>();

        ClientStart client = gameObject.AddComponent<ClientStart>();
        client.inputText = testUI.transform.Find("inputClient").GetComponent<InputField>();
        client.btnClientSend = testUI.transform.Find("btnClientSend").GetComponent<Button>();
    }

    private readonly Queue<ClientStart> tmpClientQueue = new Queue<ClientStart>();
    private void ExampleUpdate1()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                tmpClientQueue.Enqueue(gameObject.AddComponent<ClientStart>());
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                ClientStart client = tmpClientQueue.Dequeue();
                client.Close();
            }
        }
    }
    #endregion

    #region Example2
    private void ExampleStart2()
    {
        #region 主要为了打印服务端日志
        KCPTool.LogFunc = (string s) =>
        {
            Debug.Log(HandleLogMsg(s));
        };
        KCPTool.ColorLogFunc = (ConsoleColor color, string s) =>
        {
            Debug.Log(HandleLogMsg(s));
        };
        KCPTool.WarningFunc = (string s) =>
        {
            Debug.LogWarning(HandleLogMsg(s));
        };
        KCPTool.ErrorFunc = (string s) =>
        {
            Debug.LogError(HandleLogMsg(s));
        }; 
        #endregion
        ServerStart server = gameObject.AddComponent<ServerStart>();

        gameObject.AddComponent<NetSvc>();
    }
    #endregion
}
