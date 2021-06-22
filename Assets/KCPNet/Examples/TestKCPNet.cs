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
        KCPTool.LogFunc = (string s) =>
        {
            Debug.Log(HandleMsg(s));
        };
        KCPTool.ColorLogFunc = (ConsoleColor color, string s) =>
        {
            Debug.Log(HandleMsg(s));
        };
        KCPTool.WarningFunc = (string s) =>
        {
            Debug.LogError(HandleMsg(s));
        };
        KCPTool.ErrorFunc = (string s) =>
        {
            Debug.LogError(HandleMsg(s));
        };

        ClientStart client = gameObject.AddComponent<ClientStart>();
        ServerStart server = gameObject.AddComponent<ServerStart>();
        GameObject testUI = Instantiate(Resources.Load("KCPNetTestUI"), GameObject.Find("Canvas/Root").transform) as GameObject;
        client.text = testUI.transform.Find("ScrollText/Panel/Text").GetComponent<Text>();
        client.btnClientSend = testUI.transform.Find("btnClientSend").GetComponent<Button>();
    }

    private string HandleMsg(string msg)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        return $"Thread:{threadId} {msg}";
    }
}
