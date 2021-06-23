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
        ExampleStart1();
    }

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
        server.inputText = testUI.transform.Find("inputServer/Text").GetComponent<Text>();
        server.btnServerSend = testUI.transform.Find("btnServerSend").GetComponent<Button>();

        ClientStart client = gameObject.AddComponent<ClientStart>();
        client.inputText = testUI.transform.Find("inputClient/Text").GetComponent<Text>();
        client.btnClientSend = testUI.transform.Find("btnClientSend").GetComponent<Button>();
    }
}
