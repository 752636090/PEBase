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
            Debug.LogError(HandleLogMsg(s));
        };
        KCPTool.ErrorFunc = (string s) =>
        {
            Debug.LogError(HandleLogMsg(s));
        };

        ClientStart client = gameObject.AddComponent<ClientStart>();
        ServerStart server = gameObject.AddComponent<ServerStart>();
        GameObject testUI = Instantiate(Resources.Load("KCPNetTestUI"), GameObject.Find("Canvas/Root").transform) as GameObject;
        client.text = testUI.transform.Find("ScrollText/Panel/Text").GetComponent<Text>();
        client.btnClientSend = testUI.transform.Find("btnClientSend").GetComponent<Button>();
    }
}
